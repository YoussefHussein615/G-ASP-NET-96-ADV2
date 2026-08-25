namespace c__Adv2
{
    internal class ADV_2
    {
        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public double Price { get; set; }
            public int Stock { get; set; }
        }
        class Program
        {
            static List<Product> catalog = new List<Product>()
            {
                new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 1200, Stock = 10 },
                new Product { Id = 2, Name = "Phone", Category = "Electronics", Price = 800, Stock = 25 },
                new Product { Id = 3, Name = "TShirt", Category = "Clothing", Price = 30, Stock = 100 },
                new Product { Id = 4, Name = "Jeans", Category = "Clothing", Price = 60, Stock = 50 },
                new Product { Id = 5, Name = "Chocolate", Category = "Food", Price = 5, Stock = 200 },

            };

            // Task 1 :
            // Uses Func<Product, bool> so the caller can plug in any filter logic
            // (category, price, stock, name, . . ) without ever modifying this method.
            static List<Product> SearchProducts(List<Product> products, Func<Product, bool> condition)
            {
                List<Product> result = new List<Product>();
                foreach (var p in products)
                {
                    if (condition(p))
                        result.Add(p);
                }
                return result;
            }

            //  Task 3.1:
            // Uses Action<Product>  the caller decides the print format via the lambda,
            // this method just knows how to loop and invoke it.
            static void PrintReport(List<Product> products, Action<Product> printAction)
            {
                foreach (var p in products)
                {
                    printAction(p);
                }
            }

            // Task 3.2:
            // Uses Func<Product, string>  the caller decides how each product becomes
            // a string; this method just applies the function and collects the results.
            static List<string> TransformProducts(List<Product> products, Func<Product, string> transform)
            {
                List<string> result = new List<string>();
                foreach (var p in products)
                {
                    result.Add(transform(p));
                }
                return result;
            }

            // Task 3.3:
            // Uses Predicate<Product> (builtin delegate for a method that returns bool),
            // functionally similar to Func<Product,bool> but matches the builtin delegate requirement.
            static List<Product> FilterProducts(List<Product> products, Predicate<Product> match)
            {
                List<Product> result = new List<Product>();
                foreach (var p in products)
                {
                    if (match(p))
                        result.Add(p);
                }
                return result;
            }
            static void Main(string[] args)
            {
                // Task 1: 
                Console.WriteLine(" Electronics ");
                var electronics = SearchProducts(catalog, p => p.Category == "Electronics");
                foreach (var p in electronics)
                    Console.WriteLine($"{p.Name} ${p.Price} (Stock: {p.Stock})");

                Console.WriteLine("Under $50");
                var under50 = SearchProducts(catalog, p => p.Price < 50);
                foreach (var p in under50)
                    Console.WriteLine($"{p.Name} ${p.Price} (Stock: {p.Stock})");

                Console.WriteLine("In Stock");
                var inStock = SearchProducts(catalog, p => p.Stock > 0);
                foreach (var p in inStock)
                    Console.WriteLine($"{p.Name} ${p.Price} (Stock: {p.Stock})");

                Console.WriteLine("Clothing Under $100");
                var clothingUnder100 = SearchProducts(catalog, p => p.Category == "Clothing" && p.Price < 100);
                foreach (var p in clothingUnder100)
                    Console.WriteLine($"{p.Name} ${p.Price} (Stock: {p.Stock})");

                //Task 03.1:
                Console.WriteLine("Short Report");
                PrintReport(catalog, p => Console.WriteLine($"{p.Name} ${p.Price}"));

                Console.WriteLine("Detailed Report");
                PrintReport(catalog, p => Console.WriteLine($"[{p.Category}] , {p.Name} | Price: ${p.Price} | Stock: {p.Stock}"));

                // Task 3.2
                Console.WriteLine("Summary List");
                var summaryList = TransformProducts(catalog, p => $"{p.Name} , (${p.Price})");
                foreach (var s in summaryList)
                    Console.WriteLine(s);

                Console.WriteLine("Price Labels");
                var priceLabels = TransformProducts(catalog, p => p.Price > 100 ? "Expensive!" : "Affordable");
                for (int i = 0; i < catalog.Count; i++)
                    Console.WriteLine($"{catalog[i].Name}: {priceLabels[i]}");

                // Task 3.3:
                Console.WriteLine("Low Stock Alert");
                var lowStock = FilterProducts(catalog, p => p.Stock < 20);
                foreach (var p in lowStock)
                    Console.WriteLine($"[LOW STOCK] {p.Name}: only {p.Stock} left!");
            }
        }
    }
}
