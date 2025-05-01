using Microsoft.AspNetCore.Mvc;
using DailySalesTracker.Models; // For SalesDbContext and SalesData
using System.Linq;

namespace DailySalesTracker.Controllers
{
    public class SalesController : Controller
    {
        private readonly SalesDbContext _context;

        // Constructor to inject the database context
        public SalesController(SalesDbContext context)
        {
            _context = context;
        }

        // Display the main form with all records
        public IActionResult Index()
        {
            // Retrieve all records from the database
            var salesRecords = _context.SalesRecords.ToList();
            Console.WriteLine($"Retrieved {salesRecords.Count} records from database.");

            var viewModel = new SalesViewModel
            {
                SalesRecords = salesRecords,
                NewRecord = new SalesData()
            };

            return View(viewModel); // Pass ViewModel to the view
        }

        // Add a new record to the database
        [HttpPost]
        public IActionResult AddRecord(SalesViewModel viewModel)
        {
            Console.WriteLine($"Incoming Data - Date: {viewModel.NewRecord.Date}, Items Sold: {viewModel.NewRecord.ItemsSold}, Item Price: {viewModel.NewRecord.ItemPrice}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model validation failed.");
                // If validation fails, reload the current sales list
                viewModel.SalesRecords = _context.SalesRecords.ToList();
                return View("Index", viewModel);
            }

            // Add a hardcoded test record for debugging purposes
            var testRecord = new SalesData
            {
                Date = DateTime.Now,
                ItemsSold = 10,
                ItemPrice = 5.99m
            };

            _context.SalesRecords.Add(testRecord); // Add hardcoded test record
            Console.WriteLine("Hardcoded test record added to context.");

            // Add the new record from the form

            _context.SalesRecords.Add(viewModel.NewRecord);
            try
            {
                _context.SaveChanges(); // Save changes to the database
                Console.WriteLine("Record successfully saved to database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while saving data: {ex.Message}");
            }

            return RedirectToAction("Index");
        }

        // Display sales history for the last 15 days
        public IActionResult History()
        {
            // Query records from the last 15 days
            var recentSales = _context.SalesRecords
                .Where(record => record.Date >= DateTime.Now.AddDays(-15))
                .OrderByDescending(record => record.Date)
                .ToList();

            Console.WriteLine($"Retrieved {recentSales.Count} recent records from database.");

            return View(recentSales); // Pass the sales records to the view
        }
    }
}