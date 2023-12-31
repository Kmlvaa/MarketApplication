﻿using MarketApplication.Data.Common;
using MarketApplication.Data.Models;

namespace MarketApplication.Data.Models
{
    public class Sale : BaseModel
    {
        private static int _id = 0;
        public Sale()
        {
            Id = _id;
            _id++;
            var date = DateTime.Now;
            Date = new DateTime(date.Year,date.Month,date.Day,date.Hour,date.Minute,date.Second);
        }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public List<SaleItem> Items { get; set; } = new();
        
    }
}
