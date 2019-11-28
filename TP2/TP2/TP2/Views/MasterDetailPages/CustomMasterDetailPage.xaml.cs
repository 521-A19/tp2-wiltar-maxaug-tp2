using Xamarin.Forms;
using System;
using Prism.Navigation;

namespace TP2.Views.MasterDetailPages
{
    public partial class CustomMasterDetailPage : MasterDetailPage
    {
        public CustomMasterDetailPage()
        {
            InitializeComponent();
        }

        /*
        public bool IsPresentedAfterNavigation //IMasterDetailPageOptions test purpose
        {
            get { return true; }
        } */
    }
}
/*
namespace TP2.Views.MasterDetailPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomMasterDetailPage : MasterDetailPage
    {
        public CustomMasterDetailPage()
        {
            InitializeComponent();
        }
    }
}*/
