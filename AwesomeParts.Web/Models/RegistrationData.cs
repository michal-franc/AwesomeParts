namespace AwesomeParts.Web
{
    using System.ComponentModel.DataAnnotations;
    using AwesomeParts.Web.Resources;
    using System;

    /// <summary>
    /// Class containing the values and validation rules for user registration.
    /// </summary>
    public sealed partial class RegistrationData
    {
        /// <summary>
        /// Gets and sets the user name.
        /// </summary>
        [Key]
        [Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(ValidationErrorResources))]
        [Display(Order = 0, Name = "UserNameLabel", ResourceType = typeof(RegistrationDataResources))]
        [RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessageResourceName = "ValidationErrorInvalidUserName", ErrorMessageResourceType = typeof(ValidationErrorResources))]
        [StringLength(255, MinimumLength = 4, ErrorMessageResourceName = "ValidationErrorBadUserNameLength", ErrorMessageResourceType = typeof(ValidationErrorResources))]
        public string UserName { get; set; }

        /// <summary>
        /// Gets and sets the email address.
        /// </summary>
        [Key]
        [Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(ValidationErrorResources))]
        [Display(Order = 2, Name = "EmailLabel", ResourceType = typeof(RegistrationDataResources))]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                           ErrorMessageResourceName = "ValidationErrorInvalidEmail", ErrorMessageResourceType = typeof(ValidationErrorResources))]
        public string Email { get; set; }

        /// <summary>
        /// Gets and sets the friendly name of the user.
        /// </summary>
        [Display(Order = 1, Name = "FriendlyNameLabel", Description = "FriendlyNameDescription", ResourceType = typeof(RegistrationDataResources))]
        [StringLength(255, MinimumLength = 0, ErrorMessageResourceName = "ValidationErrorBadFriendlyNameLength", ErrorMessageResourceType = typeof(ValidationErrorResources))]
        public string FriendlyName { get; set; }

        ///// <summary>
        ///// Gets and sets the security question.
        ///// </summary>
        //[Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(ValidationErrorResources))]
        //[Display(Order = 5, Name = "SecurityQuestionLabel", ResourceType = typeof(RegistrationDataResources))]
        //public string Question { get; set; }

        ///// <summary>
        ///// Gets and sets the answer to the security question.
        ///// </summary>
        //[Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(ValidationErrorResources))]
        //[Display(Order = 6, Name = "SecurityAnswerLabel", ResourceType = typeof(RegistrationDataResources))]
        //public string Answer { get; set; }

        [Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(ValidationErrorResources))]
        [Display(Order = 3, Name="Imię")]
        [RegularExpression("^[a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻ]*$", ErrorMessage = "Imię może zawierać tylko litery z przediału a-z i A-Z oraz polskie znaki.")]
        [StringLength(255, MinimumLength = 2, ErrorMessage="Długość imienia powinna liczyć od 2 do 255 znaków.")]
        public string Imie { get; set; }

        [Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(ValidationErrorResources))]
        [Display(Order = 4, Name = "Nazwisko")]
        [RegularExpression("^[a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻ]*$", ErrorMessage = "Nazwisko może zawierać tylko litery z przediału a-z i A-Z oraz polskie znaki.")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Długość nazwiska powinna liczyć od 2 do 255 znaków.")]
        public string Nazwisko { get; set; }

        [Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(ValidationErrorResources))]
        [Display(Order = 12, Name = "Telefon")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Numer telefonu może zawierać tylko liczby 0-9")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Długość numeru telefonu nie powinna przekraczać 12 znaków.")]
        public string Telefon { get; set; }

        [Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(ValidationErrorResources))]
        [Display(Order = 5, Name = "Firma")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Długość nazwy firmy powinna liczyć od 2 do 255 znaków.")]
        public string Firma { get; set; }

        [Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(ValidationErrorResources))]
        [Display(Order = 6, Name = "NIP")]
        [RegularExpression("^(([0-9]{3}[- ][0-9]{2}[- ][0-9]{2}[- ][0-9]{3}))$", ErrorMessage = "NIP powinien być podany w formacie xxx-xx-xx-xxx lub xxx xx xx xxx")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Długość numeru NIP powinna liczyć 13 znaków.")]
        public string NIP { get; set; }

        [Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(ValidationErrorResources))]
        [Display(Order = 7, Name = "Ulica")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Imię może zawierać tylko litery z przediału a-z i A-Z.")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Długość imienia powinna liczyć od 2 do 255 znaków.")]
        public string Ulica { get; set; }

        [Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(ValidationErrorResources))]
        [Display(Order = 8, Name = "Numer lokalu")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Numer ulicy może zawierać tylko cyfry z przediału 0-9.")]
        [StringLength(6, MinimumLength = 2, ErrorMessage = "Długość imienia powinna liczyć od 2 do 6 znaków.")]
        public string Numer { get; set; }

        [Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(ValidationErrorResources))]
        [Display(Order = 10, Name = "Miasto")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Nazwa miasta może zawierać tylko litery z przediału a-z i A-Z.")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Długość nazwy miasta powinna liczyć od 2 do 255 znaków.")]
        public string Miasto { get; set; }

        [Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(ValidationErrorResources))]
        [Display(Order = 9, Name = "Kod pocztowy")]
        [RegularExpression("^[0-9]{2}-[0-9]{3}$", ErrorMessage = "Kod pocztowy powinien być podany w formacie: xx-xxx")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Długość kodu pocztowego powinna liczyć 6 znaków.")]
        public string KodPocztowy { get; set; }

        [Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(ValidationErrorResources))]
        [Display(Order = 11, Name = "Kraj")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage="Nazwa kraju może się składać tylko ze znaków z zakresu a-z i A-Z")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Długość imienia powinna liczyć od 2 do 255 znaków.")]
        public string Kraj { get; set; }

        [Display(AutoGenerateField=false)]
        public Guid UserID { get; set; }
    }
}
