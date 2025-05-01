using System;
using System.ComponentModel.DataAnnotations;

namespace DailySalesTracker.Models
{
    public class SalesData
    {
        public SalesData(int id)
        {
            Id = id;
        }
        public SalesData() { }

        [Key] // Primary key annotation
        public int Id { get; set; } // Auto-incremented primary key

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Number of items sold is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Items Sold must be greater than 0.")]
        public int ItemsSold { get; set; }

        [Required(ErrorMessage = "Item price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Item Price must be greater than 0.")]
        public decimal ItemPrice { get; set; }

        public decimal TotalSales => ItemsSold * ItemPrice; // Calculated property
    }
}