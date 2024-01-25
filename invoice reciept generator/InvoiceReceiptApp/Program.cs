using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Authentication;

class Program
{
    static void Main()
    {
        // Ask for customer details only once
        Console.WriteLine("Enter Customer Name:");
        string customerName = Console.ReadLine();

        // Check if customerName is null
        if (customerName != null)
        {
            Console.WriteLine("Enter Customer Address:");
            string customerAddress = GetValidAddress();

            Console.WriteLine("Enter Customer Phone Number (minimum 10 digits):");
            string phoneNumber = GetValidPhoneNumber();

            List<Product> products = GetProducts();

            double totalAmount = CalculateTotalAmount(products);

            string outputPath = "AyushVH.pdf";

            using (var writer = new PdfWriter(outputPath))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var document = new Document(pdf);

                    // Add shop name in red color
                    var shopNameParagraph = new Paragraph("AVH Traders")
                    .SetFontColor(iText.Kernel.Colors.ColorConstants.RED)
                    .SetFontSize(20f)
                  
                    .SetBold();
                    document.Add(shopNameParagraph);

                    // Add customer details
                    document.Add(new Paragraph("Customer Details:"));
                    document.Add(new Paragraph($"Name: {customerName}"));
                    document.Add(new Paragraph($"Address: {customerAddress}"));
                    document.Add(new Paragraph($"Phone Number: {phoneNumber}"));

                    // Add item details in a table
                    document.Add(new Paragraph("\nItem Details:"));
                    CreateTable(document, products);

                    // Add total amount
                    document.Add(new Paragraph($"\nTotal Amount: ₹{totalAmount:F2}"));

                    Console.WriteLine($"PDF bill generated successfully: {outputPath}");
                }
            }
        }
        else
        {
            Console.WriteLine("Customer Name cannot be null. Exiting the program.");
        }
    }

    static void CreateTable(Document document, List<Product> products)
    {
        // Create a table with 4 columns
        var table = new Table(new float[] { 1, 1, 1, 1 })
            .SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);

        // Add table headers
        table.AddHeaderCell("Product Name");
        table.AddHeaderCell("Quantity");
        table.AddHeaderCell("Amount ");
        table.AddHeaderCell("Total ");

        // Add table rows
        foreach (var product in products)
        {
            table.AddCell(product.Name);
            table.AddCell(product.Quantity.ToString());
            table.AddCell(product.Amount.ToString("F2"));
            table.AddCell((product.Quantity * product.Amount).ToString("F2"));
        }

        // Add the table to the document
        document.Add(table);
    }

    static string GetValidAddress()
    {
        string address;
        while (true)
        {
            address = Console.ReadLine();

            // Check if the address contains only alphabetical characters
            if (Regex.IsMatch(address!, "^[1-200,a-zA-Z ]+$"))
            {
                break;
            }

            Console.WriteLine("Invalid input. Please enter a valid address containing only alphabetical characters.");
        }

        return address!;
    }

    static string GetValidPhoneNumber()
    {
        string phoneNumber;
        while (true)
        {
            phoneNumber = Console.ReadLine();

            // Check if the phone number contains only digits and has a minimum length of 10
            if (Regex.IsMatch(phoneNumber!, "^[0-9]{10,}$"))
            {
                break;
            }

            Console.WriteLine("Invalid input. Please enter a valid phone number with a minimum of 10 digits:");
        }

        return phoneNumber!;
    }
    static List<Product> GetProducts()
    {
        List<Product> products = new List<Product>();

        while (true)
        {
            Console.WriteLine("Enter Product Name (or 'done' to finish):");
            string productName = Console.ReadLine();

            if (productName.ToLower() == "done")
            {
                break;
            }

            Console.WriteLine($"Enter Quantity for {productName}:");
            int quantity;
            while (!int.TryParse(Console.ReadLine(), out quantity) || quantity < 1)
            {
                Console.WriteLine("Invalid input. Please enter a valid positive integer for Quantity:");
            }

            Console.WriteLine($"Enter Amount for {productName}:");
            double amount;
            while (!double.TryParse(Console.ReadLine(), out amount) || amount < 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid non-negative number for Amount:");
            }

            products.Add(new Product { Name = productName, Quantity = quantity, Amount = amount });
        }

        return products;
    }

    static double CalculateTotalAmount(List<Product> products)
    {
        double totalAmount = 0.0;

        foreach (var product in products)
        {
            totalAmount += product.Amount * product.Quantity;
        }

        return totalAmount;
    }

    class Product
    {
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
    }
}
