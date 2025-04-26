using System;
using System.Collections.Generic;

namespace Targil_4
{
    class ClothingItem
    {
        public enum Size
        {
            S,
            M,
            L,
            XL,
            XXL,
            OS
        }

        public enum UsingStatus
        {
            NotInUse,
            LightUse,
            FrequentlyUsed,
        }

        private static uint nextItemId = 1;

        private string _color;
        private double _price;

        public uint ItemId { get; private set; }
        public string ItemName { get; set; }
        public string[] Season { get; set; }
        public Size ItemSize { get; set; }
        public double Price
        {
            get { return _price; }
            set
            {
                if (value > 0)
                {
                    _price = value;
                    Console.WriteLine("Awesome! Price has been set.");
                }
                else
                {
                    Console.WriteLine("Price must be a positive number. Please enter again:");
                    string input = Console.ReadLine();
                    if (double.TryParse(input, out double newPrice))
                    {
                        Price = newPrice;
                    }
                    else
                    {
                        Console.WriteLine("Invalid entry. Please enter a valid number for the price.");
                        Price = 0;
                    }
                }
            }
        }
        public bool Fav { get; set; }
        public UsingStatus ItemStatus { get; set; }
        public string ItemType { get; set; }
        public string Brand { get; set; }
        public string UserID { get; set; }

        public string Color
        {
            get { return _color; }
            set
            {
                if (value.Length == 7 && value.StartsWith("#"))
                {
                    _color = value;
                    Console.WriteLine("Color saved successfully!");
                }
                else
                {
                    Console.WriteLine("Oops! Color must be in RGB format like #123ABC. Try again:");
                    string newColor = Console.ReadLine();
                    Color = newColor;
                }
            }
        }


        public ClothingItem(string name, string color, string[] season, string size, double price, bool fav, sbyte statusnum, string type, string brand, string userID)
        {
            ItemId = nextItemId++;
            ItemName = name;
            Color = color;
            Season = season;
            ItemSize = (Size)Enum.Parse(typeof(Size), size);
            Price = price;
            Fav = fav;
            ItemStatus = (UsingStatus)statusnum;
            ItemType = type;
            Brand = brand;
            UserID = userID;
        }


        public ClothingItem()
        {
            ItemId = nextItemId++;
        }


        public void Print()
        {
            Console.WriteLine("----- Clothing Item Summary -----");
            Console.WriteLine($"Owner ID: {UserID}");
            Console.WriteLine($"Item ID: {ItemId}");
            Console.WriteLine($"Name: {ItemName}");
            Console.WriteLine($"Color: {Color}");
            Console.WriteLine($"Suitable Seasons: {string.Join(", ", Season ?? new string[] { "N/A" })}");
            Console.WriteLine($"Size: {ItemSize}");
            Console.WriteLine($"Price: {Price}");
            Console.WriteLine($"Favorite: {(Fav ? "Yes" : "No")}");
            Console.WriteLine($"Usage: {ItemStatus}");
            Console.WriteLine($"Type: {ItemType}");
            Console.WriteLine($"Brand: {Brand}");
            Console.WriteLine("----------------------------------");
        }
    }
    class User
    {
        public string userid { get; }
        public string email { get; }
        public string password { get; }
        public string firstname { get; }
        public string lastname { get; }
        public string nickname { get; }
        public string phoneNumber { get; }
        public DateTime birthdate { get; }

        public User(string userId, string email, string password,
                    string firstName, string lastName, string nickName,
                    string phoneNumber, DateTime birthDate)
        {
            this.userid = userId;
            this.email = email;
            this.password = password;
            this.firstname = firstName;
            this.lastname = lastName;
            this.nickname = nickName;
            this.phoneNumber = phoneNumber;
            this.birthdate = birthDate;
        }
        public void Print()
        {
            Console.WriteLine($"User Profile: {firstname} {lastname} (ID: {userid})");
            Console.WriteLine($"Nickname: {nickname} | Phone: {phoneNumber} | Birth Date: {birthdate.ToShortDateString()}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            User[] users = new User[]
            {
            new User("123456789", "user1@example.com", "Pass123", "John", "Doe", "Johnny", "123-456-7890", new DateTime(1990, 5, 12)),
            new User("987654321", "user2@example.com", "Secure456", "Jane", "Smith", "Janie", "987-654-3210", new DateTime(1985, 8, 25)),
            new User("951753852", "user3@example.com", "Strong789", "Alex", "Brown", "Al", "555-666-7777", new DateTime(1998, 12, 3))
            };

            List<ClothingItem> clothingItems = new List<ClothingItem>();
            User loggedInUser = null;


            while (loggedInUser == null)
            {
                Console.WriteLine("Login Required - Please enter your email:");
                string enteredEmail = Console.ReadLine();
                Console.WriteLine("Enter your password:");
                string enteredPassword = Console.ReadLine();

                foreach (var user in users)
                {
                    if (user.email == enteredEmail && user.password == enteredPassword)
                    {
                        loggedInUser = user;
                        break;
                    }
                }

                if (loggedInUser == null)
                {
                    Console.WriteLine("Access Denied. Email or password is incorrect.");
                }
                else
                {
                    Console.WriteLine($"Successfully logged in. Welcome back, {loggedInUser.firstname}!");
                }
            }

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n========= Digital Wardrobe Menu =========");
                Console.WriteLine("1. Add a new clothing item");
                Console.WriteLine("2. Show my clothing collection");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option (1, 2, 3): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ClothingItem item = new ClothingItem();

                        Console.WriteLine("Enter the item's name:");
                        item.ItemName = Console.ReadLine();

                        Console.WriteLine("Provide the color (e.g., #AABBCC):");
                        item.Color = Console.ReadLine();

                        Console.WriteLine("Enter seasons separated by commas (e.g., Summer,Winter):");
                        string seasonsInput = Console.ReadLine();
                        item.Season = seasonsInput.Split(',');

                        Console.WriteLine("Choose size (S, M, L, XL, XXL, OS):");
                        string sizeInput = Console.ReadLine();
                        item.ItemSize = (ClothingItem.Size)Enum.Parse(typeof(ClothingItem.Size), sizeInput);

                        Console.Write("Input price: ");
                        double priceInput;
                        while (!double.TryParse(Console.ReadLine(), out priceInput))
                        {
                            Console.Write("Try again with a valid number: ");
                        }
                        item.Price = priceInput;

                        Console.Write("Is it a favorite? (true/false): ");
                        bool favInput;
                        while (!bool.TryParse(Console.ReadLine(), out favInput))
                        {
                            Console.Write("Enter 'true' or 'false': ");
                        }
                        item.Fav = favInput;

                        Console.Write("Set usage level (1, 2, or 3): ");
                        sbyte statusInput;
                        while (!sbyte.TryParse(Console.ReadLine(), out statusInput) || statusInput < 1 || statusInput > 3)
                        {
                            Console.Write("Valid values are 1 to 3 only. Try again: ");
                        }
                        item.ItemStatus = (ClothingItem.UsingStatus)(statusInput - 1);

                        Console.WriteLine("Item type:");
                        item.ItemType = Console.ReadLine();

                        Console.WriteLine("Brand:");
                        item.Brand = Console.ReadLine();

                        item.UserID = loggedInUser.userid;

                        clothingItems.Add(item);

                        Console.WriteLine("Item successfully added to your wardrobe!");
                        break;

                    case "2":
                        int count = 0;
                        foreach (var clothingItem in clothingItems)
                        {
                            if (clothingItem.UserID == loggedInUser.userid)
                            {
                                clothingItem.Print();
                                count++;
                            }
                        }

                        if (count == 0)
                            Console.WriteLine("Your wardrobe is currently empty.");
                        else
                            Console.WriteLine($"Total items in your wardrobe: {count}");
                        break;

                    case "3":
                        Console.WriteLine("You have been logged out. See you next time!");
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please select 1, 2, or 3.");
                        break;
                }
            }
        }
    }
}
