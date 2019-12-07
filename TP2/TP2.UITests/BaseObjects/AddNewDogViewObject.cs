using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP2.Externalization;
using Xamarin.UITest;

namespace TP2.UITests.BaseObjects
{
    public class AddNewDogViewObject : BasePageObject
    {
        const string ID_EMAIL_ENTRY = "NameId";
        const string ID_PASSWORD_ENTRY = "PasswordId";
        const string ID_SECOND_PASSWORD_ENTRY = "SecondPasswordId";
        public AddNewDogViewObject(IApp app) : base(app)
        {
        }

        public void NavigateToAddNewDogPage()
        {
            RegisteUser();
            ConnextionToDogList();
            App.WaitForElement(UiText.DOGS_LIST_PAGE_MAIN_LABEL);
            App.TapCoordinates(100, 100);
            App.Tap("Mon chien en adoption");
            App.Tap("D'accord");
            App.Tap("Ajouter un chien");
            App.WaitForElement("Confirmer l'ajout");
        }

        public void RegisteUser()
        {
            App.Tap(UiText.SIGN_UP);
            App.EnterText(ID_EMAIL_ENTRY, "123@456");
            App.EnterText(ID_PASSWORD_ENTRY, "123456789aA");
            App.EnterText(ID_SECOND_PASSWORD_ENTRY,"123456789aA");
            App.Tap("Register");
        }

        public void ConnextionToDogList()
        {
            App.Tap(ID_EMAIL_ENTRY);
            App.EnterText("123@456");
            App.Tap(ID_PASSWORD_ENTRY);
            App.EnterText(ID_PASSWORD_ENTRY, "123456789aA");
            App.Tap(UiText.CONNECTION);
            App.Tap(UiText.CONNECTION);
        }

        public void AddNewDog()
        {
            App.Tap("Confirmer l'ajout");
            App.WaitForElement(UiText.DOGS_LIST_PAGE_MAIN_LABEL);
            App.TapCoordinates(0, 0);
            App.ScrollDownTo("affenpinscher");
        }
    
    }
}
