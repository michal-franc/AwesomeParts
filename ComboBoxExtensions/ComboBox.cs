﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace ComboBoxExtensions
{
    using CB = System.Windows.Controls.ComboBox;

    public static class ComboBox
    {
        public enum ComboBoxMode
        {
            Default = 0,
            Async,
            AsyncEager,
        }

        #region Mode

        public static DependencyProperty ModeProperty =
            DependencyProperty.RegisterAttached(
                "Mode",
                typeof(ComboBoxMode),
                typeof(ComboBox),
                new PropertyMetadata(ComboBoxMode.Default, ComboBox.ModePropertyChanged));

        public static ComboBoxMode GetMode(DependencyObject target)
        {
            return (ComboBoxMode)target.GetValue(ComboBox.ModeProperty);
        }

        public static void SetMode(DependencyObject target, ComboBoxMode mode)
        {
            target.SetValue(ComboBox.ModeProperty, mode);
        }

        private static void ModePropertyChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Add a shim that manages the ItemsSource, SeletedItem, and SelectedValue bindings. The shim
            // may eagerly select the selected item while the list is loading and will refresh the bindings
            // when the load completes.
            if (!DesignerProperties.IsInDesignTool && ((ComboBoxMode)e.NewValue != ComboBoxMode.Default))
            {
                CB comboBox = sender as CB;
                if (comboBox != null)
                {
                    Shim.Create(comboBox, (ComboBoxMode)e.NewValue);
                }
            }
        }

        #endregion

        public class Shim : INotifyPropertyChanged
        {
            private readonly BindingListener _bindingListener;

            // VM properties
            private List<object> _items = new List<object>();
            private object _selectedItem = null;
            private string _displayMemberPath = null;

            // Specifies whether the shim is working with underlying bindings for SelectedValue
            private bool _selectedValueMode;
            // Specifies whether the SelectedItem is eagerly selected before it is available in the Items
            private bool _useEagerSelection;
            // Suppresses the SelectedItem setter when making updates to the Items collection
            private bool _ignoreCallback;

            public static Shim Create(CB comboBox, ComboBoxMode mode)
            {
                Shim shim = new Shim();
                shim.Initialize(comboBox, (mode == ComboBoxMode.AsyncEager));
                return shim;
            }

            private Shim()
            {
                this._bindingListener = new BindingListener(this);
            }

            private void Initialize(CB comboBox, bool useEagerSelection)
            {
                // Add to visual tree to enable ElementName binding
                comboBox.Tag = this._bindingListener;

                this._useEagerSelection = useEagerSelection;

                BindingExpression be = comboBox.GetBindingExpression(ItemsControl.ItemsSourceProperty);
                if (be != null)
                {
                    BindingOperations.SetBinding(this._bindingListener, BindingListener.ItemsSourceProperty, be.ParentBinding);
                    BindingOperations.SetBinding(comboBox, ItemsControl.ItemsSourceProperty, new Binding("Items") { Source = this });
                }

                be = comboBox.GetBindingExpression(Selector.SelectedItemProperty);
                if (be != null)
                {
                    BindingOperations.SetBinding(this._bindingListener, BindingListener.SelectedItemProperty, be.ParentBinding);
                    BindingOperations.SetBinding(comboBox, Selector.SelectedItemProperty, new Binding("SelectedItem") { Source = this, Mode = BindingMode.TwoWay });
                }

                be = comboBox.GetBindingExpression(Selector.SelectedValueProperty);
                if (be != null)
                {
                    // We'll always bind the ComboBox to SelectedItem, but we'll map it to the existing SelectedValue binding
                    this._selectedValueMode = true;

                    BindingOperations.SetBinding(this._bindingListener, BindingListener.SelectedValueProperty, be.ParentBinding);
                    comboBox.ClearValue(Selector.SelectedValueProperty);

                    // Bind SelectedValuePath
                    be = comboBox.GetBindingExpression(Selector.SelectedValuePathProperty);
                    if (be != null)
                    {
                        BindingOperations.SetBinding(this._bindingListener, BindingListener.SelectedValuePathProperty, be.ParentBinding);
                        comboBox.ClearValue(Selector.SelectedValuePathProperty);
                    }
                    else
                    {
                        this._bindingListener.SelectedValuePath = comboBox.SelectedValuePath;
                        comboBox.SelectedValuePath = null;
                    }

                    // Bind DisplayMemberPath
                    be = comboBox.GetBindingExpression(Selector.DisplayMemberPathProperty);
                    if (be != null)
                    {
                        BindingOperations.SetBinding(this._bindingListener, BindingListener.DisplayMemberPathProperty, be.ParentBinding);
                    }
                    else
                    {
                        this._bindingListener.DisplayMemberPath = comboBox.DisplayMemberPath;
                    }
                    BindingOperations.SetBinding(comboBox, Selector.DisplayMemberPathProperty, new Binding("DisplayMemberPath") { Source = this });

                    // Check selection mode and path properties
                    if (this._useEagerSelection && (this._bindingListener.DisplayMemberPath != this._bindingListener.SelectedValuePath))
                    {
                        throw new InvalidOperationException("Cannot use eager selection when the DisplayMemberPath and the SelectedValuePath differ. Try using basic ComboBoxMode.Async selection instead.");
                    }

                    BindingOperations.SetBinding(comboBox, Selector.SelectedItemProperty, new Binding("SelectedItem") { Source = this, Mode = BindingMode.TwoWay });
                }
            }

            public IEnumerable Items
            {
                get { return this._items; }
            }

            public object SelectedItem
            {
                get { return this._selectedItem; }
                set
                {
                    if (!this._ignoreCallback && (this._selectedItem != value))
                    {
                        this._selectedItem = value;
                        if (this._selectedValueMode)
                        {
                            this._bindingListener.SelectedValue = Shim.GetValue(value, this._bindingListener.SelectedValuePath);
                        }
                        else
                        {
                            this._bindingListener.SetValue(BindingListener.SelectedItemProperty, value);
                        }
                        this.RaisePropertyChanged("SelectedItem");
                    }
                }
            }

            public string DisplayMemberPath
            {
                get { return this._displayMemberPath; }
            }

            private void HandleItemsSourceChanged(DependencyPropertyChangedEventArgs e)
            {
                this.UpdateSubscription(e.OldValue as INotifyCollectionChanged, e.NewValue as INotifyCollectionChanged);
                this.SyncToItemsSource();
            }

            private void HandleSelectedItemChanged(DependencyPropertyChangedEventArgs e)
            {
                this.SyncToSelectedItem();
            }

            private void HandleSelectedValueChanged(DependencyPropertyChangedEventArgs e)
            {
                this.SyncToSelectedValue();
            }

            private void HandleSelectedValuePathChanged(DependencyPropertyChangedEventArgs e)
            {
                this.SyncToSelectedValue();
            }

            private void HandleDisplayMemberPathChanged(DependencyPropertyChangedEventArgs e)
            {
                this._displayMemberPath = this._bindingListener.DisplayMemberPath;
                this.RaisePropertyChanged("DisplayMemberPath");
            }

            private void UpdateSubscription(INotifyCollectionChanged oldIncc, INotifyCollectionChanged newIncc)
            {
                if (oldIncc != null)
                {
                    oldIncc.CollectionChanged -= this.OnCollectionChanged;
                }

                if (newIncc != null)
                {
                    newIncc.CollectionChanged += this.OnCollectionChanged;
                }
            }

            private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                this.SyncToItemsSource();
            }

            private void SyncToItemsSource()
            {
                this._items = (this._bindingListener.ItemsSource == null) ?
                    new List<object>() :
                    this._bindingListener.ItemsSource.Cast<object>().ToList();

                this._ignoreCallback = true;
                this.RaisePropertyChanged("Items");
                this._ignoreCallback = false;

                if (this._selectedValueMode)
                {
                    // Reset selected item and path from the value specified during eager selection
                    this._selectedItem = this.GetSelectedItemFromSelectedValue();
                    this._displayMemberPath = this._bindingListener.DisplayMemberPath;
                    this.RaisePropertyChanged("DisplayMemberPath");
                }
                this.RaisePropertyChanged("SelectedItem");
            }

            private void SyncToSelectedItem()
            {
                if (this._useEagerSelection && !this._items.Contains(this._bindingListener.SelectedItem))
                {
                    // Eagerly add SelectedItem to the list and assume it will be valid once the items are updated
                    this._items = new List<object> { this._bindingListener.SelectedItem };

                    this._ignoreCallback = true;
                    this.RaisePropertyChanged("Items");
                    this._ignoreCallback = false;
                }

                this._selectedItem = this._bindingListener.SelectedItem;
                this.RaisePropertyChanged("SelectedItem");
            }

            private void SyncToSelectedValue()
            {
                object selectedItem = this.GetSelectedItemFromSelectedValue();
                if (this._useEagerSelection && (selectedItem == null))
                {
                    // Eagerly add SelectedValue to the list and assume it will be valid once the items are updated
                    this._items = new List<object> { this._bindingListener.SelectedValue };

                    this._ignoreCallback = true;
                    this.RaisePropertyChanged("Items");
                    this._ignoreCallback = false;

                    // Clear the DisplayMemberPath for eager selection so the binding displays the SelectedValue literally
                    this._selectedItem = this._bindingListener.SelectedValue;
                    this._displayMemberPath = null;
                    this.RaisePropertyChanged("DisplayMemberPath");
                    this.RaisePropertyChanged("SelectedItem");
                }
                else
                {
                    // Reset the DisplayMemberPath in case it was changed during eager selection
                    this._selectedItem = selectedItem;
                    this._displayMemberPath = this._bindingListener.DisplayMemberPath;
                    this.RaisePropertyChanged("DisplayMemberPath");
                    this.RaisePropertyChanged("SelectedItem");
                }
            }

            private object GetSelectedItemFromSelectedValue()
            {
                foreach (object item in this.Items)
                {
                    if (object.Equals(Shim.GetValue(item, this._bindingListener.SelectedValuePath), this._bindingListener.SelectedValue))
                    {
                        return item;
                    }
                }
                return null;
            }

            private static object GetValue(object item, string valuePath)
            {
                string[] paths = (valuePath == null) ? new string[0] : valuePath.Split('.');
                foreach (string path in paths)
                {
                    if (item == null)
                    {
                        break;
                    }
                    PropertyInfo property = item.GetType().GetProperty(path);
                    if (property == null)
                    {
                        item = null;
                        break;
                    }
                    item = property.GetValue(item, null);
                }
                return item;
            }

            #region INotifyPropertyChanged

            public event PropertyChangedEventHandler PropertyChanged;

            private void RaisePropertyChanged(string propertyName)
            {
                this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }

            private void OnPropertyChanged(PropertyChangedEventArgs e)
            {
                PropertyChangedEventHandler handler = this.PropertyChanged;
                if (handler != null)
                {
                    handler(this, e);
                }
            }

            #endregion

            #region BindingListener

            private class BindingListener : DependencyObject
            {
                public static readonly DependencyProperty ItemsSourceProperty =
                    DependencyProperty.Register(
                        "ItemsSource",
                        typeof(IEnumerable),
                        typeof(BindingListener),
                        new PropertyMetadata(null, BindingListener.HandleItemsSourceChanged));

                public static readonly DependencyProperty SelectedItemProperty =
                    DependencyProperty.Register(
                        "SelectedItem",
                        typeof(object),
                        typeof(BindingListener),
                        new PropertyMetadata(null, BindingListener.HandleSelectedItemChanged));

                public static readonly DependencyProperty SelectedValueProperty =
                    DependencyProperty.Register(
                        "SelectedValue",
                        typeof(object),
                        typeof(BindingListener),
                        new PropertyMetadata(null, BindingListener.HandleSelectedValueChanged));

                public static readonly DependencyProperty SelectedValuePathProperty =
                    DependencyProperty.Register(
                        "SelectedValuePath",
                        typeof(string),
                        typeof(BindingListener),
                        new PropertyMetadata(null, BindingListener.HandleSelectedValuePathChanged));

                public static readonly DependencyProperty DisplayMemberPathProperty =
                    DependencyProperty.Register(
                        "DisplayMemberPath",
                        typeof(string),
                        typeof(BindingListener),
                        new PropertyMetadata(null, BindingListener.HandleDisplayMemberPathChanged));

                private static void HandleItemsSourceChanged(object sender, DependencyPropertyChangedEventArgs e)
                {
                    ((BindingListener)sender)._shim.HandleItemsSourceChanged(e);
                }

                private static void HandleSelectedItemChanged(object sender, DependencyPropertyChangedEventArgs e)
                {
                    ((BindingListener)sender)._shim.HandleSelectedItemChanged(e);
                }

                private static void HandleSelectedValueChanged(object sender, DependencyPropertyChangedEventArgs e)
                {
                    ((BindingListener)sender)._shim.HandleSelectedValueChanged(e);
                }

                private static void HandleSelectedValuePathChanged(object sender, DependencyPropertyChangedEventArgs e)
                {
                    ((BindingListener)sender)._shim.HandleSelectedValuePathChanged(e);
                }

                private static void HandleDisplayMemberPathChanged(object sender, DependencyPropertyChangedEventArgs e)
                {
                    ((BindingListener)sender)._shim.HandleDisplayMemberPathChanged(e);
                }

                public IEnumerable ItemsSource
                {
                    get { return (IEnumerable)this.GetValue(BindingListener.ItemsSourceProperty); }
                    set { this.SetValue(BindingListener.ItemsSourceProperty, value); }
                }

                public object SelectedItem
                {
                    get { return (object)this.GetValue(BindingListener.SelectedItemProperty); }
                    set { this.SetValue(BindingListener.SelectedItemProperty, value); }
                }

                public object SelectedValue
                {
                    get { return (object)this.GetValue(BindingListener.SelectedValueProperty); }
                    set { this.SetValue(BindingListener.SelectedValueProperty, value); }
                }

                public string SelectedValuePath
                {
                    get { return (string)this.GetValue(BindingListener.SelectedValuePathProperty); }
                    set { this.SetValue(BindingListener.SelectedValuePathProperty, value); }
                }

                public string DisplayMemberPath
                {
                    get { return (string)this.GetValue(BindingListener.DisplayMemberPathProperty); }
                    set { this.SetValue(BindingListener.DisplayMemberPathProperty, value); }
                }

                private readonly Shim _shim;

                public BindingListener(Shim shim)
                {
                    this._shim = shim;
                }
            }

            #endregion
        }
    }
}
