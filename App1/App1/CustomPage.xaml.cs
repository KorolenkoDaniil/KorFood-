using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using static System.Math;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomPage : ContentPage
    {
        User user = new User();
        UserRep userRep = new UserRep();

        public CustomPage(User user)
        {
            Console.WriteLine($"{user.PersonName}  {user.BackColor}   {user.Age} ");

            try
            {
                InitializeComponent();
                this.user = user;
                Backcolor = user.BackColor;

                Console.WriteLine("ЦВЕТ " + Backcolor);
                g.BackgroundColor = Color.FromHex(user.BackColor);

                L1.Text = Backcolor.Substring(1, 2);
                L2.Text = Backcolor.Substring(3, 2);
                L3.Text = Backcolor.Substring(5, 2);

                g.BackgroundColor = Color.FromHex(user.BackColor);



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString() + "1111111111111111111111111111111111111111111111111");
            }
        }

        public  string Backcolor = "#a6a0d9";

        void V1(object sender, ValueChangedEventArgs e)
        {
            try
            {
                L1.Text = Round(slider1.Value, 0).ToString();
                Backcolor = $"#{int.Parse(L1.Text).ToString("X")}{int.Parse(L2.Text).ToString("X")}{int.Parse(L3.Text).ToString("X")}";
                g.BackgroundColor = Color.FromHex(Backcolor);
                user.BackColor = Backcolor;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString() + "22222222222222222222222222222222222222");
            }
        }

        void V2(object sender, ValueChangedEventArgs e)
        {
            try
            {
                L2.Text = Round(slider2.Value, 0).ToString();
                Backcolor = $"#{int.Parse(L1.Text).ToString("X")}{int.Parse(L2.Text).ToString("X")}{int.Parse(L3.Text).ToString("X")}";
                g.BackgroundColor = Color.FromHex(Backcolor);
                user.BackColor = Backcolor;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString() + "3333333333333333333333333333333333");
            }
        }

        void V3(object sender, ValueChangedEventArgs e)
        {
            try
            {
                L3.Text = Round(slider3.Value, 0).ToString();
                Backcolor = $"#{int.Parse(L1.Text).ToString("X")}{int.Parse(L2.Text).ToString("X")}{int.Parse(L3.Text).ToString("X")}";
                g.BackgroundColor = Color.FromHex(Backcolor);
                user.BackColor = Backcolor;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString() + "44444444444444444444444444444444444444");
            }
        }

        private async void SaveColor(object sender, EventArgs e)
        {
            await userRep.SaveColor(user);
            Console.WriteLine("элееееееееееееееемент сохранился");
            
        }
    }
}