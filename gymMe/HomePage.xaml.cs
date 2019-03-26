using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace gymMe
{
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new NewGymPage());

        }
    }
}
