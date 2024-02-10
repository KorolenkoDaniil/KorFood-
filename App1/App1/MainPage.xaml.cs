using App1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SQLite.SQLite3;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public User UserKor = new User();
        UserRep rep = new UserRep();
        AdRep adrep = new AdRep();

        public int TextFont, ButtonFont, BigTextFont, razmer;

        private const string RegistrationURL = "https://7295-151-249-153-161.ngrok-free.app/Home/Registration";

        public MainPage()
        {
         
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var widthInCentimeters = mainDisplayInfo.Width / mainDisplayInfo.Density;
            var heightInCentimeters = mainDisplayInfo.Height / mainDisplayInfo.Density;

            double a = Math.Sqrt(Math.Pow(widthInCentimeters, 2) + Math.Pow(heightInCentimeters, 2));

            if (a < 600) { TextFont = 12; ButtonFont = 14; BigTextFont = 16; razmer = 1;  }
            else if (a > 601 && a < 1000) { TextFont = 14; ButtonFont = 16; BigTextFont = 24;  razmer = 2; }
            else if (a > 1001 && a < 1600) { TextFont = 17; ButtonFont = 20; BigTextFont = 40; razmer = 3; }
            else
            {
                TextFont = 20;  ButtonFont = 23; BigTextFont = 35; razmer = 4;
            }



            Console.WriteLine(a + "ÿyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy");



            try
            {
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.None || current == NetworkAccess.Unknown)
                {
                    DisplayAlert("ошибка", "Нет подключения к интернету ", "ок");
                    return;
                }
                pr();
                InitializeComponent();
                LabelKorFood.FontSize = BigTextFont;
                log.FontSize = TextFont;
                pass.FontSize = TextFont;
                RegLabel.FontSize = TextFont;
                LabelForget.FontSize = TextFont;
                ButtonToContent.FontSize = ButtonFont;
                ErrorLabel.FontSize = TextFont;
            }
            catch (Exception ex)
            {
                DisplayAlert("ошибка", ex.Message, "ok");
            }
        }
        public MainPage(User item, int ButtonFont, int TextFont, int BigTextFont)
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var widthInCentimeters = mainDisplayInfo.Width / mainDisplayInfo.Density;
            var heightInCentimeters = mainDisplayInfo.Height / mainDisplayInfo.Density;

            double a = Math.Sqrt(Math.Pow(widthInCentimeters, 2) + Math.Pow(heightInCentimeters, 2));

            if (a < 600) { TextFont = 12; ButtonFont = 14; BigTextFont = 16; razmer = 1; }
            else if (a > 601 && a < 1000) { TextFont = 14; ButtonFont = 16; BigTextFont = 24; razmer = 2; }
            else if (a > 1001 && a < 1600) { TextFont = 17; ButtonFont = 20; BigTextFont = 40; razmer = 3; }
            else
            {
                TextFont = 20; ButtonFont = 23; BigTextFont = 35; razmer = 4;
            }


            var current = Connectivity.NetworkAccess;
           
            if (current == NetworkAccess.None || current == NetworkAccess.Unknown)
            {
                DisplayAlert("ошибка", "Нет подключения к интернету ", "ок");
                return;
            }
            pr();
            InitializeComponent();
            log.FontSize = TextFont;
            pass.FontSize = TextFont;
            RegLabel.FontSize = TextFont;
            LabelForget.FontSize = TextFont;
            ButtonToContent.FontSize = ButtonFont;
            ErrorLabel.FontSize = TextFont;
            UserKor = item;
            log.Text = UserKor.login.Trim();
            pass.Text = UserKor.password.Trim();
            
        }

        public async void pr()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.None || current == NetworkAccess.Unknown)
            {
                await DisplayAlert("Ошибка", "Нет подключения к интернету", "Ок");
                return;
            }

            // Проверка подключения к серверу
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(RegistrationURL);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Не удалось подключиться к серверу: {ex.Message}", "Ок");
                return;
            }
        }


        private async void ToPageOfContent(object sender, EventArgs e)
        {
            ////------------------------------------------------------
            ////проверка 

            //ReservationRep r = new ReservationRep();

            //Reservation g = new Reservation()
            //{
            //    id = 1,
            //    numberOfSeats = 1,
            //    number = "fdfgdf",
            //    name = "Test",
            //    cafeName = "Test",
            //    data = DateTime.Now
            //};

            //await r.SaveItem(g);



            ////------------------------------------------------------
















            
            try
            {
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.None || current == NetworkAccess.Unknown)
                {
                    await DisplayAlert("ошибка", "Нет подключения к интернету ", "ок");
                    return;
                }


                UserKor.login = Convert.ToString(log.Text.Trim());
                UserKor.password = Convert.ToString(pass.Text.Trim());


                

                

                if (UserKor.login == "DANILA" && UserKor.password == "12345")
                {
                    UserKor.AdminAdminov = true;
                    await Navigation.PushAsync(new AdminPage(UserKor));
                    return;
                }
                else if (await rep.exists(UserKor.login, UserKor.password.ToString()) == false)
                {
                    ErrorLabel.Text = "Ошибка, пользователь с таким логином\nне существует ";
                    return;
                }
                else
                {
                    UserKor = await rep.FoundUser(log.Text.Trim(), UserKor);
                }


                if (UserKor.AdminBool == true)
                {
                    await adrep.SaveItemsToTableOfAdmin(UserKor.WorkPlace);
                    await Navigation.PushAsync(new AdminPage(UserKor));
                    ErrorLabel.Text = "";
                    return;
                }
                else if (UserKor.AdminBool == false)
                {
                    await adrep.SaveItemsToTable();
                    await Navigation.PushAsync(new PageOfContent(UserKor, ButtonFont, TextFont, BigTextFont, razmer));
                    ErrorLabel.Text = "";
                    return;
                }
            }
            catch (Exception ex)
            {
                //if (string.IsNullOrEmpty(log.Text))
                //{
                //    log.Placeholder = "* это обязательное поле";
                //    log.PlaceholderColor = Color.Red;
                //}
                //else
                //{
                //    log.Placeholder = "придумайте логин";
                //    log.PlaceholderColor = Color.Gray;
                //}
                //if (string.IsNullOrEmpty(pass.Text))
                //{
                //    pass.Placeholder = "* это обязательное поле";
                //    pass.PlaceholderColor = Color.Red;
                //}
                //else
                //{
                //    pass.Placeholder = "придумайте логин";
                //    pass.PlaceholderColor = Color.Gray;
                //}
                //return;
            }

        }

        private async void ToRegistrationPage(object sender, EventArgs e)
        {

            try
            {
                await Navigation.PushAsync(new RegistrationPage(ButtonFont, TextFont, BigTextFont));
            }
            catch (Exception ex)
            {
                await DisplayAlert("ошибка", ex.Message, "ok");
            }
        }

        private void TextChanged(object sender, EventArgs e)
        {
            ErrorLabel.Text = "";
        }
    }

}
