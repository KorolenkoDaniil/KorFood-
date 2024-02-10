using App1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;


namespace App1
{


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageOfContent : ContentPage
    {
        User UserKorFOOD = new User();
        int d = 100;
        public PageOfContent()
        {
            InitializeComponent();
        }
        public int ButtonFont, TextFont, BigTextFont, razmer, size;
        public PageOfContent(User UsualUser, int ButtonFont, int TextFont, int BigTextFont,int razmer)
        {

            InitializeComponent();
            UserKorFOOD = UsualUser;
            NameUser.Text = UserKorFOOD.PersonName;
            this.BigTextFont = BigTextFont;
            this.ButtonFont = ButtonFont;
            this.TextFont = TextFont;
            this.ButtonFont = ButtonFont;
            this.razmer = razmer;
            if (razmer == 1)
            {
                size = 200;
            }
            else if (razmer == 2)
            {
                
            }


            Console.WriteLine("ЦВЕТ " + UserKorFOOD.BackColor);
        }


        protected override void OnAppearing()
        {
            ShowItems();
        }

        private async void ShowItems()
        {
            List<Ad> adList = App.AdBase.GetItems();
            itemCollection.ItemsSource = adList;
            
            
        }



        private async void ToSearchPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageSearch());
        }

        private async void ToSettingsPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageSettings(UserKorFOOD));
        }
        private void PoiskEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = PoiskEntry.Text.ToLower();

            itemCollection.ItemsSource = App.AdBase.GetItems().Where(item => item.Title.ToLower().Contains(searchText) || item.description.ToLower().Contains(searchText));
        }


        public async void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {

            var selectedItem = e.CurrentSelection.FirstOrDefault() as Ad;

            if (selectedItem != null)
            {
                await Navigation.PushAsync(new Pageitem(selectedItem, UserKorFOOD));
            }

            itemCollection.SelectedItem = null;
        }
    }
}