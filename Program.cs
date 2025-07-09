using System;

class Program
{
    static void Main(string[] args)
    {
        var repository = new ProductRepository();
        bool isRunning = true;

        while (isRunning)
        {
            ShowMenu();

            string choice = Console.ReadLine();
            Console.WriteLine(); // Empty line for clarity

            switch (choice)
            {
                case "1":
                    CreateProduct(repository);
                    break;
                case "2":
                    ListProducts(repository);
                    break;
                case "3":
                    UpdateProduct(repository);
                    break;
                case "4":
                    DeleteProduct(repository);
                    break;
                case "5":
                    isRunning = false;
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please choose 1-5.");
                    break;
            }
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("=== Product Management ===");
        Console.WriteLine("1. Create Product");
        Console.WriteLine("2. View All Products");
        Console.WriteLine("3. Update Product");
        Console.WriteLine("4. Delete Product");
        Console.WriteLine("5. Exit");
        Console.Write("Choose an option: ");
    }

    static void CreateProduct(ProductRepository repo)
    {
        Console.Write("Enter product name: ");
        string name = Console.ReadLine();

        Console.Write("Enter product price: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            var product = new Product
            {
                Name = name,
                Price = price
            };
            repo.Add(product);
            Console.WriteLine("Product created successfully.");
        }
        else
        {
            Console.WriteLine("Invalid price.");
        }
    }

    static void ListProducts(ProductRepository repo)
    {
        var products = repo.GetAll();
        if (products.Count == 0)
        {
            Console.WriteLine("No products found.");
        }
        else
        {
            Console.WriteLine("--- Products ---");
            foreach (var p in products)
            {
                Console.WriteLine($"ID: {p.Id} | Name: {p.Name} | Price: {p.Price:C}");
            }
        }
    }

    static void UpdateProduct(ProductRepository repo)
    {
        Console.Write("Enter product ID to update: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var existing = repo.GetById(id);
            if (existing == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            Console.Write("Enter new name: ");
            string newName = Console.ReadLine();

            Console.Write("Enter new price: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal newPrice))
            {
                var updated = new Product
                {
                    Id = id,
                    Name = newName,
                    Price = newPrice
                };

                if (repo.Update(updated))
                    Console.WriteLine("Product updated successfully.");
                else
                    Console.WriteLine("Update failed.");
            }
            else
            {
                Console.WriteLine("Invalid price.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    static void DeleteProduct(ProductRepository repo)
    {
        Console.Write("Enter product ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            if (repo.Delete(id))
                Console.WriteLine("Product deleted successfully.");
            else
                Console.WriteLine("Product not found.");
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }
}
