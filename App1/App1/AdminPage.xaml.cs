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
    public partial class AdminPage : ContentPage
    {
        User Admin = new User();
        public AdminPage()
        {
            InitializeComponent();
        }

        public AdminPage(User admin)
        {
            InitializeComponent();
            Admin = admin;
            NameUser.Text = Admin.PersonName;
            if (Admin.AdminAdminov == true) NewAdmin.IsVisible = true;
            else NewAdmin.IsVisible = false;
        }

        private async void ToPageofAdding(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageOfAdding(Admin, ""));
        }

        protected override void OnAppearing()
        {
            ShowItems();
        }

        private void ShowItems()
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
            await Navigation.PushAsync(new PageSettings(Admin));
        }
        public async void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {

            var selectedItem = e.CurrentSelection.FirstOrDefault() as Ad;

            if (selectedItem != null)
            {
                await Navigation.PushAsync(new Pageitem(selectedItem, Admin));
            }

            itemCollection.SelectedItem = null;
        }
        private async void AddAdmin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageOfAdding(Admin, "admin"));
        }


        private void PoiskEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = PoiskEntry.Text.ToLower(); // Получаем введенный текст и приводим его к нижнему регистру

            // Применяем фильтр к источнику данных CollectionView
            //itemCollection.ItemsSource = App.AdBase.GetItems().Where(item => item.Title.ToLower().Contains(searchText));
        }

        private async void ToOrdersPage(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new CafeOrders(Admin));
        }
    }
}