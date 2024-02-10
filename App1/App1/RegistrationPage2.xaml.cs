using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;
using Xamarin.Essentials;
using App1;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage2 : ContentPage
    {

        User RegUser2 = new User();
        public RegistrationPage2()
        {
            InitializeComponent();
        }


        int ButtonFont, TextFont, BigTextFont;
       
        public RegistrationPage2(User item, int ButtonFont, int TextFont, int BigTextFont)
        {
            InitializeComponent();
            RegUser2 = item;
            Age1.Text = "18";
            this.ButtonFont = ButtonFont;
            this.TextFont = TextFont;
            this.BigTextFont = BigTextFont;
            RKorFood.FontSize = BigTextFont;
            ButtonNext.FontSize = ButtonFont;
            ErrorLabel.FontSize = TextFont;
            Sex.FontSize = TextFont;
            Age.FontSize = TextFont;
            Age1.FontSize = TextFont;
            AnswerField.FontSize = TextFont;
            QuestionPicker.FontSize = TextFont;
    }
        private async void ToMainPage(object sender, EventArgs e)
        {
            UserRep repositor = new UserRep();
            RegUser2.BackColor = "#a376af";
            await repositor.SaveItem(RegUser2);

            if (picker.SelectedItem == null && picker.SelectedIndex == -1)
            {
                ErrorLabel.Text = "* обязательно выберите вопрос,";
                return;
            }
            else
            {
                ErrorLabel.Text = "";
            }
            if (string.IsNullOrEmpty(AnswerField.Text))
            {
                AnswerField.Placeholder = "* это обязательное поле";
                AnswerField.PlaceholderColor = Color.Red;
                return;
            }
            else
            {
                AnswerField.Placeholder = "напишите ответ на ваш вопрос";
                AnswerField.PlaceholderColor = Color.Gray;
            }
          
            await Navigation.PushAsync(new MainPage(RegUser2, ButtonFont, TextFont, BigTextFont));
        }

        void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegUser2.Sex = (string)picker.SelectedItem;
            //Age1.Text = (string)picker.SelectedItem;
            
        }




        void slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Age1.Text = Convert.ToString(Math.Round(e.NewValue, 0));
            RegUser2.Age = (int)Math.Round(e.NewValue, 0);
        }

    }
}