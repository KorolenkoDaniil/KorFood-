using App1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageOfAdding : ContentPage
    {
        User Admin = new User();
        AdRep adrep = new AdRep();



        public PageOfAdding()
        {
            InitializeComponent();
        }

        public PageOfAdding(User admin, string addAdmin)
        {
            InitializeComponent();
            Admin = admin;
            NameUser.Text = Admin.login;
            if (addAdmin == "admin")
            {
                titleField.IsVisible = false;
                descField.IsVisible = false;
                imageField.IsVisible = false;
                ButtonOfAdding.IsVisible = false;
                NameField.IsVisible = true;
                LoginField.IsVisible = true;
                PassField.IsVisible = true;
                WorkPlaceField.IsVisible = true;
                imageField2.IsVisible = true;
                ButtonAdAdmin.IsVisible = true;
            }
            else
            {
                titleField.IsVisible = true;
                descField.IsVisible = true;
                imageField.IsVisible = true;
                ButtonOfAdding.IsVisible = true;
                NameField.IsVisible = false;
                LoginField.IsVisible = false;
                PassField.IsVisible = false;
                WorkPlaceField.IsVisible = false;
                imageField2.IsVisible = false;
                ButtonAdAdmin.IsVisible = false;
            }
        }



        Regex photo = new Regex(@"jpg|jpeg|png|gif|JPG|PNG|JPEG");

        private async void AddItemButton(object sender, EventArgs e)
        {
            string title = titleField.Text.Trim();
            string desc = descField.Text.Trim();
            string image = imageField.Text.Trim();
            Regex photo2 = new Regex(@"photo |image|picture|Photo|Image|Picture");

            //if (title.Length < 2)
            //{
            //    await DisplayAlert("ошибка", "минимум 2 знаков", "ок");
            //    return;
            //}
            //if (desc.Length < 3)
            //{
            //    await DisplayAlert("ошибка", "минимум 3 знаков", "ок");
            //    return;
            //}
            //else if (photo.IsMatch(image) == false && photo2.IsMatch(image) == false)
            //{
            //    await DisplayAlert("ошибка", "формат ссылки не поддерживается", "ок");
            //    imageField2.Text = "";
            //    return;
            //}

            Ad i = new Ad
            {
                Title = title,
                description = desc,
                image = image,
                Place = Admin.WorkPlace,
                cafename = Admin.WorkPlace
            };

            await adrep.SaveItem(i);

            titleField.Text = "";
            descField.Text = "";
            imageField.Text = image;

            await Navigation.PopAsync();

        }



        private async void AddAdminButton(object sender, EventArgs e)
        {
            string Name = NameField.Text.Trim();
            string Login = LoginField.Text.Trim();
            string WorkPlace = WorkPlaceField.Text.Trim();
            string AdminPass = PassField.Text.Trim();
            string image = imageField2.Text.Trim();

            Regex photo = new Regex(@"jpg|jpeg|png|gif|JPG|PNG|JPEG");
            Regex photo2 = new Regex(@"photo|image|picture|Photo|Image|Picture");

            UserRep userRep = new UserRep();

            //if (Name.Length < 2)
            //{
            //    await DisplayAlert("ошибка", "минимум 2 знаков", "ок");
            //    return;
            //}
            //if (Login.Length < 2)
            //{
            //    await DisplayAlert("ошибка", "минимум 3 знаков", "ок");
            //    return;
            //}
            //else if (await userRep.FoundLogin(Login))
            //{
            //    await DisplayAlert("ошибка", "Администратор с таикм логином уже существует", "ок");
            //    return;
            //}
            //else if (AdminPass.Length < 2)
            //{
            //    await DisplayAlert("ошибка", "минимум 3 знаков", "ок");
            //    return;
            //}
            //else if (photo.IsMatch(image) == false && photo2.IsMatch(image) == false)
            //{
            //    await DisplayAlert("ошибка", "формат ссылки не поддерживается", "ок");
            //    imageField2.Text = "";
            //    return;
            //}
            //else if (image == "")
            //{
            //    await DisplayAlert("ошибка", "формат ссылки не поддерживается", "ок");
            //    imageField2.Text = "";
            //    image = " ";
            //    return;
            //}
            //else if (WorkPlace.Length < 2)
            //{
            //    await DisplayAlert("ошибка", "минимум 5 знаков", "ок");
            //    return;
            //}

            User i = new User
            {
                login = Login,
                PersonName = Name,
                UserPhoto = "",
                password = AdminPass,
                AdminBool = true,
                AdminAdminov = false,
                WorkPlace = WorkPlace,
                Age = 20,
            };

            UserRep repositor = new UserRep();


            await repositor.SaveItem(i);
            imageField.Text = "";
            NameField.Text = "";
            PassField.Text = "";
            WorkPlaceField.Text = "";
            LoginField.Text = "";
            await Navigation.PopAsync();

        }

        //private async void AddAdminButton(object sender, EventArgs e)
        //{
        //    UserRep userRep = new UserRep();

        //    User newAdmin = new User
        //    {
        //        login = LoginField.Text.Trim(),
        //        PersonName = NameField.Text.Trim(),
        //        UserPhoto = "",
        //        password = PassField.Text.Trim(),
        //        AdminBool = true,
        //        AdminAdminov = false,
        //        WorkPlace = WorkPlaceField.Text.Trim(),
        //        Age = 20,
        //    };

        //    await userRep.SaveItem(newAdmin);

        //    imageField.Text = "";
        //    NameField.Text = "";
        //    PassField.Text = "";
        //    WorkPlaceField.Text = "";
        //    LoginField.Text = "";

        //    await Navigation.PopAsync();
        //}


        private async void ToSearchPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageSearch());
        }

        private async void ToSettingsPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageSettings(Admin));
        }
    }
}