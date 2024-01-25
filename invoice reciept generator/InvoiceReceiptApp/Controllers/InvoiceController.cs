using Microsoft.AspNetCore.Mvc;
using InvoiceReceiptApp.Models;

namespace InvoiceReceiptApp.Models{
    public class InvoiceController : Controller
{
    public IActionResult Index()
    {
        // Implement logic to retrieve and display invoices
        return View();
    }

    public IActionResult Generate()
    {
        // Implement logic to generate invoice
        return View();
    }
}
}
