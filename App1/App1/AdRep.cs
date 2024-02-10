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
    public class AdRep
    {
        string h = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AdKorFood");
    
        SQLiteConnection adBase;
        private static readonly HttpClient client = new HttpClient();
        Ad add = new Ad();


        public AdRep()
        {

        }

        public AdRep(string pathtodb)
        {
            try
            {
                adBase = new SQLiteConnection(pathtodb);
                adBase.CreateTable<Ad>();
                h = pathtodb;
             

                //Ad ad = new Ad()
                //{
                //    Id = 4,
                //    Title = "dfg",
                //    image = "dfgdfg",
                //    description = "Description",
                //    Place = "dfgdg"
                //};

                //adBase.Insert(ad);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "ОШИБКА В СОХДЗАНИИИ БАЗЫ");  
            }
        }





        //        public async Task SaveItemsToTable()
        //        {

        //            string UserLogin = "Danila";
        //            string pass = "123";

        //            string RegistrationURL = "https://3c55-185-203-155-42.ngrok-free.app/Home/last100";

        //            Dictionary<string, string> dict = new Dictionary<string, string>
        //{
        //    {"login",  UserLogin},
        //    {"pass", pass}
        //};

        //            FormUrlEncodedContent form = new FormUrlEncodedContent(dict);

        //            HttpResponseMessage response = await client.PostAsync(RegistrationURL, form);

        //            string result = await response.Content.ReadAsStringAsync();

        //            Dictionary<string, string> AdParams = new Dictionary<string, string>();

        //            AdParams = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

        //            Ad ad = new Ad();

        //            Regex Rid = new Regex(@"id");
        //            Regex RTitle = new Regex(@"Title");
        //            Regex Rimage = new Regex(@"image");
        //            Regex Rdesc = new Regex(@"description");
        //            Regex RPlace = new Regex(@"Place");


        //            foreach (var item in AdParams)
        //            {
        //                if (Rid.IsMatch(item.Key))
        //                {
        //                    ad.Id = int.Parse(item.Value);
        //                }
        //                if (RTitle.IsMatch(item.Key))
        //                {
        //                    ad.Title = item.Value;
        //                }
        //                if (Rdesc.IsMatch(item.Key))
        //                {
        //                    ad.description = item.Value;
        //                }
        //                if (Rimage.IsMatch(item.Key))
        //                {
        //                    ad.image = item.Value;
        //                }
        //                if (RPlace.IsMatch(item.Key))
        //                {
        //                    try
        //                    {
        //                        ad.Place = item.Value;
        //                        Console.WriteLine("Элемент сохранен");
        //                        Console.WriteLine(ad.Id);
        //                        Console.WriteLine(ad.Title);
        //                        Console.WriteLine(ad.image);
        //                        Console.WriteLine(ad.description);
        //                        Console.WriteLine(ad.Place);
        //                        adBase.Insert(ad);
        //                        ad = new Ad();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Console.WriteLine(ex.Message + "OOOOOOOOOOOOOOOOO");
        //                    }
        //                }

        //            }
        //        }

        public void DeleteItem(int id) {
            adBase.Delete(id);
        }

        public async Task SaveItemsToTable()
        {
            string UserLogin = "Danila";
            string pass = "123";

            string RegistrationURL = "https://3c55-185-203-155-42.ngrok-free.app/Home/last100";

            Dictionary<string, string> dict = new Dictionary<string, string>
    {
        {"login",  UserLogin},
        {"pass", pass}
    };

            FormUrlEncodedContent form = new FormUrlEncodedContent(dict);

            HttpResponseMessage response = await client.PostAsync(RegistrationURL, form);

            string result = await response.Content.ReadAsStringAsync();

            Dictionary<int, Ad> AdParams = JsonConvert.DeserializeObject<Dictionary<int, Ad>>(result);

            var connection = new SQLiteConnection(h);


            connection.CreateTable<Ad>();

            connection.DeleteAll<Ad>();


            foreach (var item in AdParams)
            {


                Console.WriteLine($"{item.Value.Title}");

                Ad add = new Ad(); // Создаем новый экземпляр объекта Ad

                add.Id = item.Value.Id;
                add.Title = item.Value.Title;
                add.description = item.Value.description;
                add.image = item.Value.image;
                add.Place = item.Value.Place;

                connection.Insert(add);
            }
        }


        public async Task SaveItemsToTableOfAdmin(string workPLace)
        {

            string RegistrationURL = "https://3c55-185-203-155-42.ngrok-free.app/Home/last100forAdmin";

            Dictionary<string, string> dict = new Dictionary<string, string>
            {
            {"workPlace",  workPLace }

            };

            FormUrlEncodedContent form = new FormUrlEncodedContent(dict);

            HttpResponseMessage response = await client.PostAsync(RegistrationURL, form);

            string result = await response.Content.ReadAsStringAsync();

            Dictionary<int, Ad> AdParams = JsonConvert.DeserializeObject<Dictionary<int, Ad>>(result);

            var connection = new SQLiteConnection(h);


            connection.CreateTable<Ad>();

            connection.DeleteAll<Ad>();


            foreach (var item in AdParams)
            {


                Console.WriteLine($"{item.Value.Title}");

                Ad add = new Ad(); // Создаем новый экземпляр объекта Ad

                add.Id = item.Value.Id;
                add.Title = item.Value.Title;
                add.description = item.Value.description;
                add.image = item.Value.image;
                add.Place = item.Value.Place;

                connection.Insert(add);

                Console.WriteLine(item.Value.description);
                Console.WriteLine(item.Value.image);
                Console.WriteLine(item.Value.Place);
            }
        }




        public int SaveItemToLocalBase(Ad item)
        {
            Console.WriteLine("элемент сохранен!!!!!!!!");
            if (item.Id != 0)
            {
                adBase.Update(item);
                return item.Id;
            }
            else
            {
                Console.WriteLine(item.Id);
                Console.WriteLine(item.Title);
                Console.WriteLine(item.image);
                Console.WriteLine(item.description);
                Console.WriteLine(item.Place);
                return adBase.Insert(item);
            }
        }








        public List<Ad> GetItems()
        {
            return adBase.Table<Ad>().ToList();
        }


        public Ad GetItem(int id)
        {
            return adBase.Get<Ad>(id);

        }


        public async Task SaveItem(Ad item1)
        {
            string RegistrationURL = "https://3c55-185-203-155-42.ngrok-free.app/Home/AdRegistration";

            Ad item = new Ad();
            item = item1;
            Dictionary<string, string> AdParams = new Dictionary<string, string>();

            AdParams.Add("Title", item.Title);
            AdParams.Add("description", item.description);
            AdParams.Add("image", item.image);
            AdParams.Add("place", item.Place);
            AdParams.Add("cafename", item.cafename);



            FormUrlEncodedContent form = new FormUrlEncodedContent(AdParams);

            HttpResponseMessage response = await client.PostAsync(RegistrationURL, form);
        }

        public async Task<Ad> FoundUser(string Title, string desc, Ad A)
        {
            try
            {
                string RegistrationURL = "https://3c55-185-203-155-42.ngrok-free.app/Home/Adparams";

                Dictionary<string, string> dict = new Dictionary<string, string>
                {
                    {"Title",  Title},
                    {"desc",  desc},
                };

                FormUrlEncodedContent form = new FormUrlEncodedContent(dict);

                HttpResponseMessage response = await client.PostAsync(RegistrationURL, form);

                string result = await response.Content.ReadAsStringAsync();

                Dictionary<string, string> AdParams = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);



                A.Id = int.Parse(AdParams["id"]);
                A.Title = AdParams["Title"];
                A.image = AdParams["image"];
                A.description = AdParams["description"];
                A.Place = AdParams["Place"];


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq");
            }
            return A;
        }

    }
}
