using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace TP2.Services
{
    public class TopMenuBar : ContentPage
    {
        public TopMenuBar()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "THIS IS A TEST" }
                }
            };
        }
    }
}