using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 


public class Invoice
{
    public string? CustomerName { get; set; }
    
    public List<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>(); // Non-nullable
    
    public int InvoiceId { get; set; }
    
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
    
    

    public Invoice()
    {

        CustomerName = string.Empty; // or any default value
        InvoiceItems = new List<InvoiceItem>();
    }

    public void AddItem(string itemName, decimal itemPrice, int quantity)
    {
        var item = new InvoiceItem
        {
            ItemName = itemName,
            ItemPrice = itemPrice,
            Quantity = quantity
        };

        InvoiceItems.Add(item);
    }

    public decimal CalculateTotalAmount()
    {
        TotalAmount = InvoiceItems.Sum(item => item.ItemPrice * item.Quantity);
        return TotalAmount;
    }

    public string GenerateReceipt()
    {
        var receipt = new StringBuilder();

        receipt.AppendLine("Invoice Receipt");
        receipt.AppendLine($"Invoice ID: {InvoiceId}");
        receipt.AppendLine($"Customer Name: {CustomerName}");
        receipt.AppendLine($"Invoice Date: {InvoiceDate}");
        receipt.AppendLine("Items:");

        foreach (var item in InvoiceItems)
        {
             if (item != null)
             {
            receipt.AppendLine($"  - {item.ItemName} x{item.Quantity}: ${item.ItemPrice * item.Quantity}");
        }
            
        }

        receipt.AppendLine($"Total Amount: ${TotalAmount}");

        return receipt.ToString();
        
    }
}

public class InvoiceItem
{   
    public string? ItemName { get; set; } // Note the '?'
    public decimal ItemPrice { get; set; }
    public int Quantity { get; set; }
    public InvoiceItem()
    {
        ItemName = string.Empty; // or any default value
    }   
}
