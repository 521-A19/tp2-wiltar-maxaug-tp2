using System.Runtime.Serialization;

namespace TP2.Externalization
{
    public static class UiText
    {
        //CustomMasterDetailPage
        public const string BUTTON_TO_SHOPPING_CART_PAGE = "Mon panier";
        public const string BUTTON_TO_DOG_SHOP_PAGE = "Mon chien en adoption";
        public const string BUTTON_TO_DOGS_LIST_PAGE = "Liste de chiens";
        public const string BUTTON_TO_USER_PROFIL_PAGE = "Profile";
        public const string BUTTON_CONNEXION = "Connection";
        public const string BUTTON_DECONNEXION = "Déconnection";

        //Dog seeder
        public const string ANY_DOG_NAME = "Rex";
        public const string ANY_DOG_IMAGE_URL= "https://images.dog.ceo/breeds/clumber/n02101556_823.jpg";
        public const float  ANY_DOG_PRICE = (float)259.99;
        public const string ANY_DOG_RACE = "Husky";
        public const string ANY_DOG_SEX = "Male";
        public const string ANY_DOG_DESCRIPTION = "Jeune chien de 4 mois, super énergique";

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
        public const string DOG_SHOP_PAGE_MAIN_TITLE = "Mon chien en adoption";
        public const string WARNING = "Attention";
        public const string CONFIRM = "D'accord";
        public const string SUCCESS = "Succès";
        public const string DOG_INFO_MODIFIED = "Les changements ont été effectués";
        public const string USER_NOT_CONNECTED = "Vous devez être connecté pour placer en adoption votre chien";
        public const string NO_CURRENT_DOG = "Aucun chien en adoption";
        public const string BUTTON_ADD_NEW_DOG = "Ajouter un chien";
        public const string BUTTON_SAVE_CHANGES = "Sauvegarder changements";

        //DogsList
        public const string DOGS_LIST_PAGE_MAIN_TITLE = "Liste de chiens globale";
        public const string DOGS_LIST_PAGE_MAIN_LABEL = "Voici les petits amis en adoption";

        //ShoppingCart
        public const string SHOPPING_CART_PAGE_MAIN_TITLE = "Mon panier";
        public const string BUTTON_BUY_SHOPPING_CART = "Acheter";
        public const string BUTTON_CANCEL_SHOPPING_CART = "Annuler";
        public const string INVALID_CONFIRMATION_CREDIT_CARD = "La carte de crédit n'est pas valide.";

        //DogDetail
        public const string TITLE_DOG_DETAIL = "Voici le chien ";
        public const string BUTTON_ADD_TO_SHOPPING_CART = "Ajouter au panier";

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

        //Register
        public const string BUTTON_CONFIRM_REGISTRATION = "Confirmer";
        public const string REGISTER_PAGE_MAIN_TITLE = "S'enregistrer";
        public const string LOGIN_IS_ALREADY_REGISTERED = "Ce nom d'utilisateur est déjà utilisé!";
        public const string USER_REGISTER_ALERT = "Désolé";
        public const string OKAY_CHANGE_NAME = "D'accord";

        //UserProfile
        public const string USER_PROFILE_PAGE_MAIN_TITLE = "Profile";
        public const string BUTTON_DELETE_MY_DOG = "Supprimer le chien X";

        //AddNewDog
        public const string DOG_NEED_A_NAME = "Le chien a besoin d'un nom";
        public const string DOG_NEED_A_GOOD_PRICE = "Le chien doit avoir un prx supérieur à 0$";
    }
}