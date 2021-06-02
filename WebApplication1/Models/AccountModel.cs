using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class AccountModel
    {
        public int ID { get; set; }
        public string Account { get; set; }
        public string Pwd { get; set; }
        public int GroupID { get; set; }
        public string ImageUrl { get; set; }
        public string AccountName { get; set; }
    }
}