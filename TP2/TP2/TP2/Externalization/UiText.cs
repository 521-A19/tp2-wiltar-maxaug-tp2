﻿using System.Runtime.Serialization;

namespace TP2.Externalization
{
    public static class UiText
    {
        //MainPage
        public const string WELCOME_ON_DOGFINDER = "Bienvenue sur DogFinder !";
        public const string SERVICES = "Services d'adoption de chiens";
        public const string NEWEST_STARS = "Nos nouveaux amis";
        public const string SIGN_IN = "Se connecter";
        public const string SIGN_UP = "S'inscrire";
        public const string EMAIL = "Courriel";
        public const string PASSWORD = "Mot de passe";
        public const string CONNECTION = "Connexion";
        public const string GO_TO_DOG_LIST = "Voir la liste de chiens";

        //DogShop
        public const string WARNING = "Attention";
        public const string CONFIRM = "D'accord";
        public const string SUCCESS = "Succès";
        public const string DOG_INFO_MODIFIED = "Les changements ont été effectués";
        public const string USER_NOT_CONNECTED = "Vous devez être connecté pour placer en adoption votre chien";
        public const string NO_CURRENT_DOG = "Aucun chien en adoption";
        public const string BUTTON_REGISTER_DOG = "Cliquez sur placer un chien en adoption";

        public static string USER_ALREADY_CONNECTED = "User is already connected";
        public static string ERROR = "Erreur";
        public static string USER_NOT_FOUND = "Utilisateur ou mot de passe erroné";
        public static string OK = "Ok";
        public const string UserNameRequired = "A password is required.";
        public const string LowercaseRequired = "At least on lowercase is required.";
        public const string UppercaseRequired = "At least one Uppercase is required.";
        public const string NumericCharacterRequired = "At least one numeric chareacter is required.";
        public const string MoreThanTenCharactersRequired = "More than ten characters is required.";
        public const string ValidEmailRequired = "A valid Email is required.";
        public const string NotValidLogInMessage = "Bad Username or password";
        public const string NotValidLogInTitle = "Wrong Authentification !";
        public const string ErrorExceptionThrowTitle = "Error !!";
        public const string ErrorExceptionThrowMessage = "Un problème est survenue lors de l'exécution.";
        public const string ExceptionUserIsAlreadyLogIn = "You are already log in !";
        public const string SecondPasswordIsTheSameOfTheFirst = "Please write the right password!";
        public const string NameAndPriceShouldNotBeEmptyException = "Name should not be empty and Price should not be equal to 0. TY!";
        public const string Okay = "Okay";
    }
}