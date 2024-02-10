using App1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pageitem : ContentPage
    {
        Ad ADD = new Ad();
        User UserItemPage = new User();
        public Pageitem()
        {
            InitializeComponent();

        }
        public Pageitem(Ad Addd, User ser)
        {
            InitializeComponent();
            if (ser.AdminBool == true) { BButton.IsVisible = false; }
            else { BButton.IsVisible = true; }
            ADD = Addd;
            ItemPhoto.Source = ImageSource.FromUri(new Uri(ADD.image));
            for (int i = 0; i < ADD.Title.Length; i++)
            {
                ADD.description.Replace("  ", " ");
                ADD.description.Replace("\n", " ");
                ADD.Title.Replace("  ", " ");
                ADD.Title.Replace("\n", " ");
            }
            DescLabel.Text = ADD.description;
            TitleLabel.Text = ADD.Title;
            UserItemPage = ser;
            NameUser.Text = UserItemPage.PersonName;
            if (UserItemPage.AdminBool == false)
            {
                DeletButton.IsVisible = false;
            }
            else
            {
                DeletButton.IsVisible = true;
            }
        }


        private async void ToSearchPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageSearch());
        }

        private async void ToSettingsPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageSettings(UserItemPage));
        }

        private async void delete(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("", "Вы уверены, что хотитет удалить?", "да", "нет");
            if (answer) { App.AdBase.DeleteItem(ADD.Id); await Navigation.PopAsync(); }
            return;
        }

        private async void ToOrderPage(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new OrderPage(UserItemPage, ADD));
        }

    }
}