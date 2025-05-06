using System.Collections.Generic;

namespace DailySalesTracker.Models
{
    public class SalesViewModel
    {
        // Holds the new sales data record being added
        public SalesData NewRecord { get; set; } = new SalesData();

        // Holds the list of all sales records retrieved from the database
        public IEnumerable<SalesData> SalesRecords { get; set; } = new List<SalesData>();
    }
}