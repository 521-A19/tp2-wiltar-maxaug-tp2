using Xamarin.Forms;
using System;
using Prism.Navigation;

namespace TP2.Views.MasterDetailPages
{
    public partial class CustomMasterDetailPage : MasterDetailPage, IMasterDetailPageOptions
    {
        public CustomMasterDetailPage()
        {
            InitializeComponent();
        }

        public bool IsPresentedAfterNavigation //Master Detail Page toujours présent
        {
            get { return true; }
        }
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
