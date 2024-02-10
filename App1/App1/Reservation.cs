using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace App1
{
    public class Reservation
    {
        public int id {  get; set; }
        public string name { get; set; }
        public string number {  get; set; }
        public int numberOfSeats { get; set; }
        public string cafeName { get; set; }
        public string comment { get; set; }
        public DateTime data { get; set; }


        public override string ToString()
        {
            return $"{name} {numberOfSeats} {cafeName} {comment} {data}";
        }
    }
}

