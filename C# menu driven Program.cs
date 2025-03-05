using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    const double VAT_RATE = 0.16;

    static void Main(string[] args)
    {
        string inputFilePath = GetInputFilePath();
        List<GroceryItem> groceryItems = ReadGroceryItemsFromFile(inputFilePath);

        double subtotal = 0;
        List<GroceryItemTotal> itemTotals = new List<GroceryItemTotal>();

        foreach (var item in groceryItems)
        {
            double total = item.Quantity * item.Price;
            subtotal += total;
            itemTotals.Add(new GroceryItemTotal(item, total));
        }

        double tax = subtotal * VAT_RATE;
        double grandTotal = subtotal + tax;

        PrintReceipt(itemTotals, subtotal, tax, grandTotal);

        string outputFilePath = "receipt.txt";
        WriteReceiptToFile(outputFilePath, itemTotals, subtotal, tax, grandTotal);

        Console.WriteLine($"Receipt written to {outputFilePath}");
    }

    static string GetInputFilePath()
    {
        Console.Write("Enter the input file path: ");
        return Console.ReadLine();
    }

    static List<GroceryItem> ReadGroceryItemsFromFile(string filePath)
    {
        List<GroceryItem> items = new List<GroceryItem>();

        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 3)
                {
                    string name = parts[0].Trim();
                    int quantity = int.Parse(parts[1].Trim());
                    double price = double.Parse(parts[2].Trim());
                    items.Add(new GroceryItem(name, quantity, price));
                }
            }
        }

        return items;
    }

    static void PrintReceipt(List<GroceryItemTotal> itemTotals, double subtotal, double tax, double grandTotal)
    {
        Console.WriteLine("Shopping Receipt");
        Console.WriteLine("----------------");
        foreach (var itemTotal in itemTotals)
        {
            Console.WriteLine($"{itemTotal.Item.Name}: {itemTotal.Item.Quantity} x {itemTotal.Item.Price:C} = {itemTotal.Total:C}");
        }
        Console.WriteLine("----------------");
        Console.WriteLine($"Subtotal: {subtotal:C}");
        Console.WriteLine($"Tax (16%): {tax:C}");
        Console.WriteLine($"Grand Total: {grandTotal:C}");
    }

    static void WriteReceiptToFile(string filePath, List<GroceryItemTotal> itemTotals, double subtotal, double tax, double grandTotal)
    {
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            sw.WriteLine("Shopping Receipt");
            sw.WriteLine("----------------");
            foreach (var itemTotal in itemTotals)
            {
                sw.WriteLine($"{itemTotal.Item.Name}: {itemTotal.Item.Quantity} x {itemTotal.Item.Price:C} = {itemTotal.Total:C}");
            }
            sw.WriteLine("----------------");
            sw.WriteLine($"Subtotal: {subtotal:C}");
            sw.WriteLine($"Tax (16%): {tax:C}");
            sw.WriteLine($"Grand Total: {grandTotal:C}");
        }
    }
}

class GroceryItem
{
    public string Name { get; }
    public int Quantity { get; }
    public double Price { get; }

    public GroceryItem(string name, int quantity, double price)
    {
        Name = name;
        Quantity = quantity;
        Price = price;
    }
}

class GroceryItemTotal
{
    public GroceryItem Item { get; }
    public double Total { get; }

    public GroceryItemTotal(GroceryItem item, double total)
    {
        Item = item;
        Total = total;
    }
}
