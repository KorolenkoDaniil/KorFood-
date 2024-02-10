using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPage : ContentPage
    {
        User user = new User(); 
        Ad ad = new Ad();
        Reservation reservation = new Reservation();
        ReservationRep reservrep = new ReservationRep();

        public OrderPage(User user, Ad ad)
        {
            InitializeComponent();
            PoiskEntry.IsVisible = false;
            Fram.IsVisible = false;
            this.user = user;
            this.ad = ad;


            reservation.name = user.PersonName;
            reservation.cafeName = ad.cafename;
            reservation.cafeName = ad.Place;
            reservation.numberOfSeats = int.Parse(numberOfGuests.Text);

            datapicker.MinimumDate = DateTime.Now.AddDays(0);

            LabekCafeName.Text = ad.cafename;
            LabekCafeName.Text = ad.Place;

            DateTime dateTime = DateTime.Now;
            LabelTime.Text = $"{dateTime.Hour}:{dateTime.Minute:D2}";

            reservation.data = dateTime;


            int i = 1;
         
            Grid grid = new Grid(); 
            grid.Margin = new Thickness(20);
            do
            {
                Button button = new Button
                {
                    Text = $"{dateTime.Hour}:{dateTime.Minute:D2}",
                    WidthRequest = 60, 
                    HeightRequest = 40 
                };


                button.Clicked += (sender, e) => Button_Clicked(sender, e, button.Text);

                button.FontSize = 10;
                button.CornerRadius = 10;
                button.Padding = 0;
              
                int row = (i - 1) / 5;
                int column = (i - 1) % 5;


              

                
                grid.Children.Add(button, column, row);
                
                dateTime = dateTime.AddMinutes(30);
                i++;
            } while (dateTime.Day == DateTime.Now.Day);

      
            Buttons.Children.Add(grid);
        }

        private void Button_Clicked(object sender, EventArgs e, string time)
        {
            LabelTime.Text = time;
            reservation.data = new DateTime(reservation.data.Year, reservation.data.Month, reservation.data.Day, int.Parse(time.Substring(0, 2)), int.Parse(time.Substring(3, 2)), 00);
            Console.WriteLine(reservation.data.ToString());
        }


        public void Minus(object sender, EventArgs e)
        {
            int n = int.Parse(numberOfGuests.Text);
            n--;
            if (n <= 1)
            {
                n = 1;
            }
            numberOfGuests.Text = n.ToString();
            reservation.numberOfSeats = n;
        }

        public void Plus(object sender, EventArgs e)
        {
            int n = int.Parse(numberOfGuests.Text);
            n++;
            numberOfGuests.Text = n.ToString();
            reservation.numberOfSeats = n;
        }


        private void dataChanged(object sender, EventArgs e)
        {
            reservation.data = new DateTime(datapicker.Date.Year, datapicker.Date.Month, datapicker.Date.Day, reservation.data.Hour, reservation.data.Minute, 00);
            Console.WriteLine(reservation.data.ToString() + "gggggggggggggggggggggggggggggggggggggggggg");

            DateTime dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 00, 00);
            LabelTime.Text = $"{dateTime.Hour}:{dateTime.Minute:D2}";

            reservation.data = dateTime;


            int i = 1;

            Grid grid = new Grid();
            grid.Margin = new Thickness(20);
            do
            {
                Button button = new Button
                {
                    Text = $"{dateTime.Hour}:{dateTime.Minute:D2}",
                    WidthRequest = 60,
                    HeightRequest = 40
                };


                button.Clicked += (sender1, e1) => Button_Clicked(sender1, e1, button.Text);

                button.FontSize = 10;
                button.CornerRadius = 10;
                button.Padding = 0;

                int row = (i - 1) / 5;
                int column = (i - 1) % 5;





                grid.Children.Add(button, column, row);

                dateTime = dateTime.AddMinutes(30);
                i++;
            } while (dateTime.Day == DateTime.Now.Day);


            Buttons.Children.Add(grid);
        }


        private async void ToSearchPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageSearch());
        }

        private async void ToSettingsPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageSettings(user));
        }

       
        private async void SaveB(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(EntryComment.Text)) {
                reservation.comment = EntryComment.Text;
            }
            else
            {
                reservation.comment = "дополнительных пожеланий нету ";
            }
            Console.WriteLine(reservation);

            await reservrep.SaveItem(reservation);
        }
    }
}