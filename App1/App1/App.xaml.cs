using System;
using Xamarin.Forms;
using Newtonsoft.Json;
using SQLite;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;


namespace App1
{
    public partial class App : Application
    {
        public const string DataBaseAd = "Ad.db";
        public static AdRep adBase;
        public static AdRep AdBase
        {
            get
            {
                if (adBase == null)
                {
                    adBase = new AdRep(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AdKorFood"));
                }
                return adBase;
            }
        }


        public const string DataBaseCafe = "Cafe.db";
        public static AdRep cafeBase;
        public static AdRep CafeBase
        {
            get
            {
                if (cafeBase == null)
                {
                    cafeBase = new AdRep(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CafeKorFood"));
                }
                return cafeBase;
            }
        }

        public App()
        {

            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
