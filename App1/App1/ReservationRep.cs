using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace App1
{
    public class ReservationRep
    {
        string h = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ReservationKorFood");
        SQLiteConnection ReservationBase;
        private static readonly HttpClient client = new HttpClient();
        Reservation add = new Reservation();


        public ReservationRep()
        {

        }

        public ReservationRep(string pathtodb)
        {
            try
            {
                ReservationBase = new SQLiteConnection(pathtodb);
                ReservationBase.CreateTable<Ad>();
                h = pathtodb;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "ОШИБКА В СОХДЗАНИИИ БАЗЫ");
            }
        }

        public void DeleteItem(int id)
        {
            ReservationBase.Delete(id);
        }

        public async Task SaveItemsToTable()
        {
            string UserLogin = "Danila";
            string pass = "123";

            string RegistrationURL = "https://43d8-185-203-155-41.ngrok-free.app/Home/last100";

            Dictionary<string, string> dict = new Dictionary<string, string>
    {
        {"login",  UserLogin},
        {"pass", pass}
    };

            FormUrlEncodedContent form = new FormUrlEncodedContent(dict);

            HttpResponseMessage response = await client.PostAsync(RegistrationURL, form);

            string result = await response.Content.ReadAsStringAsync();

            Dictionary<int, Reservation> ReservationParams = JsonConvert.DeserializeObject<Dictionary<int, Reservation>>(result);

            var connection = new SQLiteConnection(h);
            connection.CreateTable<Reservation>();

            connection.DeleteAll<Reservation>();

            foreach (var item in ReservationParams)
            {
                Reservation Reservation = new Reservation(); // Создаем новый экземпляр объекта Ad

                Reservation.id = item.Value.id;
                Reservation.name = item.Value.name;
                Reservation.numberOfSeats = item.Value.numberOfSeats;  
                Reservation.data = item.Value.data;
                connection.Insert(Reservation);
            }
        }

        //public List<Ad> GetItems()
        //{
        //    return adBase.Table<Ad>().ToList();
        //}

        public Reservation GetItem(int id)
        {
            return ReservationBase.Get<Reservation>(id);

        }


        public async Task SaveItem(Reservation item1)
        {
            string RegistrationURL = "https://3c55-185-203-155-42.ngrok-free.app/Home/SaveReservation";

            Reservation item = new Reservation();
            item = item1;
            Dictionary<string, string > ReservationParams = new Dictionary<string, string>();

            ReservationParams.Add("id", item.id.ToString());
            ReservationParams.Add("name", item.name);
            ReservationParams.Add("number", item.number);
            ReservationParams.Add("numberOfSeats", item.numberOfSeats.ToString());
            ReservationParams.Add("cafeName", item.cafeName);
            ReservationParams.Add("data", item.data.ToString());
            ReservationParams.Add("comment", item.comment);
           
            FormUrlEncodedContent form = new FormUrlEncodedContent(ReservationParams);

            HttpResponseMessage response = await client.PostAsync(RegistrationURL, form);
        }

        //public async Task<Ad> FoundUser(string Title, string desc, Ad A)
        //{
        //    try
        //    {
        //        string RegistrationURL = "https://43d8-185-203-155-41.ngrok-free.app/Home/Adparams";

        //        Dictionary<string, string> dict = new Dictionary<string, string>
        //        {
        //            {"Title",  Title},
        //            {"desc",  desc},
        //        };

        //        FormUrlEncodedContent form = new FormUrlEncodedContent(dict);

        //        HttpResponseMessage response = await client.PostAsync(RegistrationURL, form);

        //        string result = await response.Content.ReadAsStringAsync();

        //        Dictionary<string, string> AdParams = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);



        //        A.Id = int.Parse(AdParams["id"]);
        //        A.Title = AdParams["Title"];
        //        A.image = AdParams["image"];
        //        A.description = AdParams["description"];
        //        A.Place = AdParams["Place"];


        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message + "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq");
        //    }
        //    return A;
        //}

    }
}
