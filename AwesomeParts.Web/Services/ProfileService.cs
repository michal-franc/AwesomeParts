
namespace AwesomeParts.Web.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using AwesomeParts.Web.Models;
    using System.Web.Security;
    using System.Web.Profile;
    using BazaDanych.Entities;
    using BazaDanych.Repositories;


    // TODO: Create methods containing your application logic.
    [EnableClientAccess()]
    public class ProfileService : DomainService
    {
        private KlientRepository _klientContext = new KlientRepository();

        [Query()]
        public IEnumerable<ProfileData> GetProfile()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Update operation that unblocks update support of <see cref="RegistrationData"/> class to Silverlight client appliacation.
        /// </summary>
        /// <param name="profile"></param>
        [Update()]
        public void UpdateRegistrationData(ProfileData profile)
        {
        }

        [Invoke(HasSideEffects = true)]
        public void UpdateUserData(ProfileData user)
        {
            MembershipUser loggedUser = Membership.GetUser(user.UserID);
            loggedUser.Email = user.Email;

            Membership.UpdateUser(loggedUser);

            ProfileBase profile = ProfileBase.Create(loggedUser.UserName, true);
            profile.SetPropertyValue("FriendlyName", user.FriendlyName);
            profile.Save();

            Klient klient = (from k in _klientContext.GetAll().AsQueryable()
                             where k.User_id == user.UserID
                             select k).First();

            klient.Email = loggedUser.Email;
            klient.Firma = user.Firma;
            klient.Imie = user.Imie;
            klient.KodPocztowy = user.KodPocztowy;
            klient.Kraj = user.Kraj;
            klient.Login = loggedUser.UserName;
            klient.Miasto = user.Miasto;
            klient.Nazwisko = user.Nazwisko;
            klient.NIP = user.NIP;
            klient.Numer = user.Numer;
            klient.User_id = (Guid)loggedUser.ProviderUserKey;
            klient.Telefon = user.Telefon;
            klient.Ulica = user.Ulica;

            _klientContext.Update(klient);
        }

        [Query()]
        public IEnumerable<ProfileData> GetUserByUserName(string userName)
        {
            MembershipUser loggedUser = Membership.GetUser(userName);

            if (Roles.IsUserInRole(loggedUser.UserName, "Klient"))
            {
                ProfileBase profile = ProfileBase.Create(loggedUser.UserName, true);
                string friendlyName = (string)profile.GetPropertyValue("FriendlyName");
                Guid userID = (Guid)loggedUser.ProviderUserKey;

                Klient klient = (from k in _klientContext.GetAll().AsQueryable()
                                 where k.User_id == userID
                                 select k).First();

                return new List<ProfileData>() 
                { 
                    new ProfileData
                    {
                        Email = loggedUser.Email,
                        Firma = klient.Firma,
                        FriendlyName = friendlyName,
                        Imie = klient.Imie,
                        KodPocztowy = klient.KodPocztowy,
                        Kraj = klient.Kraj,
                        Miasto = klient.Miasto,
                        Nazwisko = klient.Nazwisko,
                        NIP = klient.NIP,
                        Numer = klient.Numer,
                        Telefon = klient.Telefon,
                        Ulica = klient.Ulica,
                        UserID = userID,
                    }
                };
            }
            else
                return new List<ProfileData>();
        }

        [Query()]
        public IQueryable<ProfileData> GetUserIdByUserName(string userName)
        {
            MembershipUser loggedUser = Membership.GetUser(userName);
            Guid userID = (Guid)loggedUser.ProviderUserKey;

            return (from k in _klientContext.GetAll().AsQueryable()
                    where k.User_id == userID
                    select new ProfileData
                    {
                        id = k.Id
                    });
        }
    }
}


