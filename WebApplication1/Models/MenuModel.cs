using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class MenuModel
    {
        public int AccountID { get; set; }
        public string MeunID { get; set; }
        public int GroupID { get; set; }
        public int ShopID { get; set; }
        public string MealCount { get; set; }
        public string MealName { get; set; }
    }
}