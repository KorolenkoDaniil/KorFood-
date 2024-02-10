using App1;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace App1
{
    public class UserRep
    {
        private static readonly HttpClient client = new HttpClient();
        string db_path;
        SQLiteConnection database;
        public UserRep()
        {
        }
        public UserRep(string databasePath)
        {
            db_path = databasePath;
            database = new SQLiteConnection(databasePath);
            database.CreateTable<User>();
        }
        public async Task SaveItem(User user)
        {
            try
            {
                string RegistrationURL = "https://3c55-185-203-155-42.ngrok-free.app/Home/Registration";

                User item = new User();
                item = user;
                Dictionary<string, string> usersParams = new Dictionary<string, string>();

                usersParams.Add("id", user.Id.ToString());
                usersParams.Add("login", user.login);
                usersParams.Add("password", user.password.ToString());
                usersParams.Add("personName", user.PersonName.ToString());
                usersParams.Add("age", user.Age.ToString());
                usersParams.Add("adminbool", user.AdminBool.ToString());
                usersParams.Add("Sex", user.Sex);
                usersParams.Add("WorkPlace", user.WorkPlace);
                usersParams.Add("backcolor", user.BackColor);
                
                //usersParams.Add("Sex", user.Sex);


                FormUrlEncodedContent form = new FormUrlEncodedContent(usersParams);

                HttpResponseMessage response = await client.PostAsync(RegistrationURL, form);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq");
            }
        }



        public async Task SaveColor(User user)
        {
            try
            {
                string RegistrationURL = "https://3c55-185-203-155-42.ngrok-free.app/Home/savecolor";

                User item = new User();
                item = user;
                Dictionary<string, string> usersParams = new Dictionary<string, string>();

                usersParams.Add("backcolor", user.BackColor);

                FormUrlEncodedContent form = new FormUrlEncodedContent(usersParams);

                HttpResponseMessage response = await client.PostAsync(RegistrationURL, form);

                Console.WriteLine(item.BackColor + "\n\n\n");



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq");
            }
        }



        public async Task<bool> FoundLogin(string UserLogin)
        {

            try
            {
                string RegistrationURL = "https://3c55-185-203-155-42.ngrok-free.app/Home/SearchLogin1";

                Dictionary<string, string> dict = new Dictionary<string, string>
            {
                {"login",  UserLogin},
            };

                FormUrlEncodedContent form = new FormUrlEncodedContent(dict);

                HttpResponseMessage response = await client.PostAsync(RegistrationURL, form);

                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result + "GGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGG");
                if (result == "true") return true;
                else return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq");
            }
            return false;

            //using (SQLiteConnection connection = new SQLiteConnection(db_path))
            //{
            //    var user = connection.Table<User>().FirstOrDefault(u => u.login == UserLogin);
            //    return user != null;
            //}
        }
        public async Task<bool> exists(string UserLogin, string UserPassword)
        {
            try
            {
                string RegistrationURL = "https://3c55-185-203-155-42.ngrok-free.app/Home/exists";

                Dictionary<string, string> dict = new Dictionary<string, string>
            {
                {"login",  UserLogin},
                {"password",  UserPassword},
            };

                FormUrlEncodedContent form = new FormUrlEncodedContent(dict);

                HttpResponseMessage response = await client.PostAsync(RegistrationURL, form);

                string result = await response.Content.ReadAsStringAsync();
                if (result == "true") return true;
                else return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq");
            }
            return false;
        }

        public async Task<User> FoundUser(string UserLogin, User A)
        {
            try
            {
                string RegistrationURL = "https://3c55-185-203-155-42.ngrok-free.app/Home/userparams";

                Dictionary<string, string> dict = new Dictionary<string, string>
                {
                    {"login",  UserLogin},
                };

                FormUrlEncodedContent form = new FormUrlEncodedContent(dict);

                HttpResponseMessage response = await client.PostAsync(RegistrationURL, form);

                string result = await response.Content.ReadAsStringAsync();

                Dictionary<string, string> userParams = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

                A.Id = int.Parse(userParams["id"]);
                A.login = userParams["login"];
                A.password = userParams["password"];
                A.AdminBool = Convert.ToBoolean(userParams["adminbool"]);
                A.AdminAdminov = Convert.ToBoolean(userParams["adminAdminovbool"]);
                A.UserPhoto = userParams["userPhoto"];
                A.WorkPlace = userParams["workPlace"];
                A.PersonName = userParams["personName"];
                A.Age = int.Parse(userParams["age"]);
                A.Sex = userParams["sex"];
                A.question = userParams["question"];
                A.answer = userParams["answer"];
                A.BackColor = userParams["BackСolor"];


                Console.WriteLine(A.BackColor + "\n\n\n");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq");
            }

            return A;
        }


    }
}
