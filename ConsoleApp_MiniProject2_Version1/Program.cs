using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp_MiniProject2_Version1
{
    class Program
    {
        static void Main(string[] args)
        {
            // 3 years(1095 days) of asset Lifetime
            AssetsContext db = new AssetsContext();
            Category Category1 = new Category("Laptops");
            Category Category2 = new Category("Mobiles");
            //db.Categories.AddRange(Category1, Category2);
            List<LaptopComputer> laptopcomputers = new List<LaptopComputer>();
            List<MobilePhone> mobilesphones = new List<MobilePhone>();

            while (true)
            {
                Console.Title = "Assets Management Program";
                Console.Write("MAIN MENU: CRUD Operations \n ");
                Console.WriteLine("Choose option by entering numbers from keyboard like: 1, 2,3...:");
                Console.WriteLine("1. ADD DATA");
                Console.WriteLine("2. LIST ASSETS");
                Console.WriteLine("3. UPDATE ENTITIES");
                Console.WriteLine("4. DELETE ENTITIES");
                Console.WriteLine("5. SHOW STATISTICS REPORT");
                Console.WriteLine("6. Exit \n>>");
                int ActionMethod = Convert.ToInt32(Console.ReadLine());
                if (ActionMethod == 1)
                {
                    AddData(laptopcomputers, mobilesphones);  //Create
                    Console.WriteLine();
                }
                else if (ActionMethod == 2)
                {
                    ShowData(laptopcomputers, mobilesphones);  //Read
                }
                else if (ActionMethod == 3)
                {
                    UpdateTableData(laptopcomputers, mobilesphones); //Update table
                }
                else if (ActionMethod == 4)
                {
                    DeleteRecord(laptopcomputers, mobilesphones);     //Delete/Remove an entity from the database          
                }
                else if (ActionMethod == 5)
                {
                    AssetsReport(laptopcomputers, mobilesphones);  // Show statistics of the total assets of the company
                }
                else if (ActionMethod == 6)
                {
                    break;
                }


            }
        }
        public static void AddData(List<LaptopComputer> laptopscomputers, List<MobilePhone> mobilePhones)
        {
            
            AssetsContext db = new AssetsContext();
            Category Category1 = new Category("Laptops");
            Category Category2 = new Category("Mobiles");
            db.Categories.AddRange(Category1, Category2);
            Console.WriteLine("Choose an option: What is the asset you want to register? ");
            Console.WriteLine("1.Laptop Computers");
            Console.WriteLine("2. Mobile Phones ");
            int Choice = Convert.ToInt32(Console.ReadLine());
            try
            {
                if (Choice == 1)
                {
                    Console.Write("Input Model of the Laptop:");
                    string Model_Name = Console.ReadLine();
                    Console.Write("Input Office Location:");
                    string Branch_Location = Console.ReadLine();
                    Console.Write("Input the Purchase date of the Laptop Computer(FORMAT 12/28/2019):");
                    DateTime Purchase_Date = Convert.ToDateTime(Console.ReadLine());
                    Console.Write("Input the price of the laptop:");
                    double Asset_Price = Convert.ToDouble(Console.ReadLine());
                    LaptopComputer laptop = new LaptopComputer(Model_Name, Branch_Location, Purchase_Date, Asset_Price);
                    db.LaptopComputers.Add(laptop);
                    db.SaveChanges();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("A new Laptop device has been successfully registered!");
                    Console.ResetColor();
                }
                else if (Choice == 2)
                {
                    Console.Write("Input Model of the mobile:");
                    string Model_Name = Console.ReadLine();
                    Console.Write("Input Office Location:");
                    string Branch_Location = Console.ReadLine();
                    Console.Write("Input Purchase of the mobile(FORMAT 12/28/2019):");
                    DateTime Purchase_Date = Convert.ToDateTime(Console.ReadLine());
                    Console.Write("Input the price of the mobile:");
                    double Asset_Price = Convert.ToDouble(Console.ReadLine());
                    MobilePhone mobiles = new MobilePhone(Model_Name, Branch_Location, Purchase_Date, Asset_Price);
                    db.MobilePhones.Add(mobiles);

                    db.SaveChanges();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("A new Mobile device has been successfully registered!");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public static void ShowData(List<LaptopComputer> laptopscomputers, List<MobilePhone> mobilePhones)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("All assets available in all branches of the company are listed below:  ");
            Console.WriteLine("**Assets that expired or will expired in 90 days are marked RED");
            Console.WriteLine("**Assets that will expire in 180 days are marked YELLOW");
            Console.ResetColor();
            AssetsContext db = new AssetsContext();
            var laptops = db.LaptopComputers.ToList();
            int AssetLifeTime = 1095;
            //var laptops = db.LaptopComputers;
            Console.WriteLine("ID " + " Model".PadRight(20) + "Office".PadRight(20) + "Purchase Date".PadRight(20) + "Price");
            foreach (LaptopComputer L in laptops)
            {
                var CurrentDate = DateTime.Now;
                var date = CurrentDate.Date;
                TimeSpan days = CurrentDate - L.PurchaseDate;
                double NumberofDays = AssetLifeTime - days.Days;
                if (NumberofDays < 90)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(L.ID + "  " + L.ModelName.PadRight(20) + L.BranchLocations.PadRight(20) + L.PurchaseDate.ToString("d").PadRight(20) + L.Price);
                    double Rate_USD = 0.12; // 1SEK = 0.12USD  based on todays currency exchange rage                         
                    double Price_US = laptops.Sum(L => L.Price * (Rate_USD));
                    double Rate_yen = 13.01; // 1SEK = 13,01JPY based on todays currency exchange rage
                    double Price_JP = laptops.Sum(L => L.Price * (Rate_yen));
                    Console.WriteLine("                                                                       (" + "Price in USD is  $" + Price_US + "  and in JPY" + Price_JP + ")");
                    Console.ResetColor();
                }
                else if (NumberofDays < 180 && NumberofDays > 90)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(L.ID + "  " + L.ModelName.PadRight(20) + L.BranchLocations.PadRight(20) + L.PurchaseDate.ToString("d").PadRight(20) + L.Price);
                    double Rate_USD = 0.12; // 1SEK = 0.12USD  based on todays currency exchange rage                         
                    double Price_US = laptops.Sum(L => L.Price * (Rate_USD));
                    double Rate_yen = 13.01; // 1SEK = 13,01JPY based on todays currency exchange rage
                    double Price_JP = laptops.Sum(L => L.Price * (Rate_yen));
                    Console.WriteLine("                                                                       (" + "Price in USD is  $" + Price_US + "  and in JPY" + Price_JP + ")");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(L.ID + "  " + L.ModelName.PadRight(20) + L.BranchLocations.PadRight(20) + L.PurchaseDate.ToString("d").PadRight(20) + L.Price);
                    double Rate_USD = 0.12; // 1SEK = 0.12USD  based on todays currency exchange rage                         
                    double Price_US = laptops.Sum(L => L.Price * (Rate_USD));
                    double Rate_yen = 13.01; // 1SEK = 13,01JPY based on todays currency exchange rage
                    double Price_JP = laptops.Sum(L => L.Price * (Rate_yen));
                    Console.WriteLine("                                                                       (" + "Price in USD is  $" + Price_US + "  and in JPY" + Price_JP + ")");
                }
            }
                var mobiles = db.MobilePhones.ToList();
                foreach (MobilePhone M in mobiles)
                {
                    var CurrentDate1 = DateTime.Now;
                    var date1 = CurrentDate1.Date;
                    TimeSpan days1 = CurrentDate1 - M.PurchaseDate;
                    double NumberofDays1 = AssetLifeTime - days1.Days;
                    if (NumberofDays1 < 90)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(M.ID + "  " + M.ModelName.PadRight(20) + M.BranchLocations.PadRight(20) + M.PurchaseDate.ToString("d").PadRight(20) + M.Price);
                        double Rate_USD = 0.12; // 1SEK = 0.12USD  based on todays currency exchange rage                         
                        double Price_US = mobiles.Sum(M => M.Price * (Rate_USD));
                        double Rate_yen = 13.01; // 1SEK = 13,01JPY based on todays currency exchange rage
                        double Price_JP = mobiles.Sum(M => M.Price * (Rate_yen));
                        Console.WriteLine("                                                                       (" + "Price in USD is  $" + Price_US + "  and in JPY" + Price_JP + ")");
                        Console.ResetColor();
                    }
                    else if (NumberofDays1 < 180 && NumberofDays1 > 90)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(M.ID + "  " + M.ModelName.PadRight(20) + M.BranchLocations.PadRight(20) + M.PurchaseDate.ToString("d").PadRight(20) + M.Price);
                        double Rate_USD = 0.12; // 1SEK = 0.12USD  based on todays currency exchange rage                         
                        double Price_US = mobiles.Sum(M => M.Price * (Rate_USD));
                        double Rate_yen = 13.01; // 1SEK = 13,01JPY based on todays currency exchange rage
                        double Price_JP = mobiles.Sum(M => M.Price * (Rate_yen));
                        Console.WriteLine("                                                                       (" + "Price in USD is  $" + Price_US + "  and in JPY" + Price_JP + ")");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(M.ID + "  " + M.ModelName.PadRight(20) + M.BranchLocations.PadRight(20) + M.PurchaseDate.ToString("d").PadRight(20) + M.Price);
                        double Rate_USD = 0.12; // 1SEK = 0.12USD  based on todays currency exchange rage                         
                        double Price_US = mobiles.Sum(M => M.Price * (Rate_USD));
                        double Rate_yen = 13.01; // 1SEK = 13,01JPY based on todays currency exchange rage
                        double Price_JP = mobiles.Sum(M => M.Price * (Rate_yen));
                        Console.WriteLine("                                                                       (" + "Price in USD is  $" + Price_US + "  and in JPY" + Price_JP + ")");
                    }
                }              
                Console.WriteLine("----------------------------------");
                Console.WriteLine("Great! What would you like to do next?");
                Console.WriteLine("----------------------------------");
        }     
        public static void UpdateTableData(List<LaptopComputer> laptopscomputers, List<MobilePhone> mobilePhones)
        {
            AssetsContext db = new AssetsContext();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Updating Asset List - Choose an option");
            Console.ResetColor();
            Console.WriteLine("Please choose options:");
            Console.WriteLine("1. Update Laptop Computers");
            Console.WriteLine("2. Update Mobile Phones \n>>>");
            int Choice = Convert.ToInt32(Console.ReadLine());
            if (Choice == 1)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Updating Laptop Computers");
                Console.ResetColor();
                Console.Write("Input the ID of the Laptop you want to update: ");
                int ID_to_update = Convert.ToInt32(Console.ReadLine());
                var Update_Querry = db.LaptopComputers.Find(ID_to_update);
                if (Update_Querry != null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("WARNING: If you do not provide any value, it will be Null, so make sure to update all fields");
                    Console.ResetColor();
                    Console.Write("Insert the new model to update:");
                    Update_Querry.ModelName = Console.ReadLine();
                    Console.Write("Insert the office to update:");
                    Update_Querry.BranchLocations = Console.ReadLine();
                    Console.Write("Insert the new purchase date to update(FORMAT 12/28/2019):");
                    Update_Querry.PurchaseDate = Convert.ToDateTime(Console.ReadLine());
                    Console.Write("Insert the new price to update:");
                    Update_Querry.Price = Convert.ToDouble(Console.ReadLine());
                    db.SaveChanges();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("LaptopComputers table has been successfully updated");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The ID of the Laptop you want to update was not found");
                    Console.ResetColor();
                }
            }
            else if (Choice == 2)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Updating Mobile Phones");
                Console.ResetColor();
                Console.Write("Input the ID of the Mobile you want to update:");
                int ID_to_update1 = Convert.ToInt32(Console.ReadLine());
                var Update_Querry1 = db.MobilePhones.Find(ID_to_update1);
                if (Update_Querry1 != null)
                {
                    //Relplacing values in the entity with new values
                    Console.Write("Insert the model to update:");
                    Update_Querry1.ModelName = Console.ReadLine();
                    Console.Write("Insert the office to update:");
                    Update_Querry1.BranchLocations = Console.ReadLine();
                    Console.Write("Insert the new purchase date to update(FORMAT 12/28/2019) :");
                    Update_Querry1.PurchaseDate = Convert.ToDateTime(Console.ReadLine());
                    Console.Write("Ïnsert the new price to update:");
                    Update_Querry1.Price = Convert.ToDouble(Console.ReadLine());
                    db.SaveChanges();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("MobilePhones table has been successfully updated");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The ID of the Mobile Phone you want to update was not found");
                    Console.ResetColor();
                }
            }
        }
        public static void DeleteRecord(List<LaptopComputer> laptopscomputers, List<MobilePhone> mobilePhones)
        {
            AssetsContext db = new AssetsContext();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Deleting an entity from the database ");
            Console.ResetColor();
            Console.WriteLine(" Choose an option \n 1.Delete from Laptop Computers \n 2. Delete from Mobile Phones \n >>>");
            int Choice = Convert.ToInt32(Console.ReadLine());
            if (Choice == 1)
            {
                Console.Write("Input the ID of the Laptop you want to delete: ");
                int ID_To_Delete = Convert.ToInt32(Console.ReadLine());
                var Update_Querry = db.LaptopComputers.Find(ID_To_Delete);
                if (Update_Querry != null)
                {
                    Console.WriteLine("Deleting ..." + Update_Querry.ID + " " + Update_Querry.ModelName + " " + Update_Querry.BranchLocations + " " + Update_Querry.PurchaseDate + " " + Update_Querry.PurchaseDate);
                    db.LaptopComputers.Remove(Update_Querry);
                    db.SaveChanges();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("An entity in the LaptopComputers table has been successfully deleted");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The ID of the laptop you want to delete was not found ");
                    Console.ResetColor();
                }
            }
            else if (Choice == 2)
            {
                Console.Write("Input the ID of the Mobile you want to delete:");
                int ID_To_Delete1 = Convert.ToInt32(Console.ReadLine());
                var Update_Querry1 = db.MobilePhones.Find(ID_To_Delete1);
                if (Update_Querry1 != null)
                {
                    Console.WriteLine("Deleting ..." + Update_Querry1.ID + " " + Update_Querry1.ModelName + " " + Update_Querry1.BranchLocations + " " + Update_Querry1.PurchaseDate + " " + Update_Querry1.PurchaseDate);
                    db.MobilePhones.Remove(Update_Querry1);
                    db.SaveChanges();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("An entity in the MobilePhones table has been successfully deleted");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The ID of the mobile you want to delete was not found");
                    Console.ResetColor();
                }
            }


        }
        public static void AssetsReport(List<LaptopComputer> laptopscomputers, List<MobilePhone> mobilePhones)
        {
            AssetsContext db = new AssetsContext();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Total number of laptop computers available in the company is: " + db.LaptopComputers.Count());
            Console.WriteLine("Total number of mobile phones available in  the company is: " + db.MobilePhones.Count());
            Console.ResetColor();
        }
    }
       
 }


