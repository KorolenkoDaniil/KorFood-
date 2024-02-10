using App1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSettings : ContentPage
    {
        User UserSettings = new User();
        UserRep userrep = new UserRep();    


        public PageSettings(User userSettings)
        {
            InitializeComponent();
            UserSettings = userSettings;
            NameUser.Text = userSettings.PersonName;
            EntryLogin.IsVisible = false;
            EntryName.IsVisible = false;
            EntryNumber.IsVisible = false;
            FieldName.IsVisible = true;
            FieldNumber.IsVisible = true;
            FieldName.IsVisible = true;
            FieldName.Text = userSettings.PersonName;
            FieldLogin.Text = userSettings.login;
            EntryLogin.Text = userSettings.login;
            EntryName.Text = userSettings.PersonName;
            ButtonSave.IsVisible = false;




            Console.WriteLine("ЦВЕТ " + userSettings.BackColor);
        }

        private async void ToSearchPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageSearch());
        }


        private async void ToPageofAdding(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void ToCustomPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CustomPage(UserSettings));


        }


        private void ButtonClicked(object sender, EventArgs e)
        {

        }

        public void clickedName(object sender, EventArgs e)
        {
            FieldName.IsVisible = false;
            EntryName.IsVisible = true;
            ButtonSave.IsVisible = true;
        }
        public void clickedNumber(object sender, EventArgs e)
        {
            FieldNumber.IsVisible = false;
            EntryNumber.IsVisible = true;
            ButtonSave.IsVisible = true;
        }
        public void clickedLogin(object sender, EventArgs e)
        {
            FieldLogin.IsVisible = false;
            EntryLogin.IsVisible = true;
            ButtonSave.IsVisible = true;
        }

        public async void SaveChanges(object sender, EventArgs e)
        {
            Console.WriteLine(UserSettings.Id + "sdddddddddddddddddddddddddddddddddddddddd");
            UserSettings.login = FieldLogin.Text;
            UserSettings.PersonName = FieldName.Text;
            await userrep.SaveItem(UserSettings);
            ButtonSave.IsVisible = false;
        }
    }
}