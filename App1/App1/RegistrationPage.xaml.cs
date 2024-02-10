using App1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        private static readonly HttpClient client = new HttpClient();


        int ButtonFont, TextFont, BigTextFont;
        public RegistrationPage(int ButtonFont, int TextFont, int BigTextFont)
        {
            InitializeComponent();
            this.ButtonFont = ButtonFont;
            this.TextFont = TextFont;
            this.BigTextFont = BigTextFont;
            DPKoorFood.FontSize = BigTextFont;
            ToNextRegPage.FontSize = ButtonFont;
            ErrorLabel.FontSize = TextFont;
            log.FontSize = TextFont;
            pass.FontSize = TextFont;
            NameField.FontSize = TextFont;
        }
        private async void ToNextRegPage2(object sender, EventArgs e)
        {
            User user = new User();
          
           

            try
            {
                UserRep rep = new UserRep();
               

                //loginFound == true
                if (await rep.FoundLogin(user.login) == true)
                {
                    ErrorLabel.Text = "Ошибка, пользователь с таким логином\nуже существует ";
                    return;
                }
                else
                {
                    user.login = log.Text.Trim();
                    user.password = pass.Text.Trim();
                    user.AdminBool = false;
                    user.AdminAdminov = false;
                    user.PersonName = NameField.Text.Trim();
                    await Navigation.PushAsync(new RegistrationPage2(user, ButtonFont, TextFont, BigTextFont));
                }
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(log.Text))
                {
                    log.Placeholder = "* это обязательное поле";
                    log.PlaceholderColor = Color.Red;
                    return;
                }
                else
                {
                    log.Placeholder = "придумайте логин";
                    log.PlaceholderColor = Color.Gray;
                }
                if (string.IsNullOrEmpty(pass.Text))
                {
                    pass.Placeholder = "* это обязательное поле";
                    pass.PlaceholderColor = Color.Red;
                    return;
                }
                else
                {
                    pass.Placeholder = "придумайте логин";
                    pass.PlaceholderColor = Color.Gray;
                }
                if (string.IsNullOrEmpty(NameField.Text))
                {
                    NameField.Placeholder = "* это обязательное поле";
                    NameField.PlaceholderColor = Color.Red;
                    return;
                }
                else
                {
                    NameField.Placeholder = "придумайте логин";
                    NameField.PlaceholderColor = Color.Gray;
                }
                
            }
        }
        private void TextChanged(object sender, EventArgs e)
        {
            //    так писать если нужно что-то делать при измененении каждого символа
            ErrorLabel.Text = "";
        }
    }
}
