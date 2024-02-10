using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Essentials;

namespace App1
{
    [Table("AdKorFood")]
    public class Ad
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public string Place { get; set; }
        public string cafename { get; set; }

    }
}
