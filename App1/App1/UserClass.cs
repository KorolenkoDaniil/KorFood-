using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    [Table("UsersK")]
    public class User
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public bool AdminBool { get; set; }
        public bool AdminAdminov { get; set; }
        public string UserPhoto { get; set; }
        public string WorkPlace { get; set; }
        public string PersonName { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public string BackColor { get; set; }
    }

}
