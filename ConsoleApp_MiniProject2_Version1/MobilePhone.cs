using System;

namespace ConsoleApp_MiniProject2_Version1
{
    class MobilePhone
    {
        public MobilePhone(string modelName, string branchLocations, DateTime purchaseDate, double price)
        {
            ModelName = modelName;
            BranchLocations = branchLocations;
            PurchaseDate = purchaseDate;
            Price = price;
        }

        public int ID { get; set; }
        public string ModelName { get; set; }
        public string BranchLocations { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double Price { get; set; }
        //public int CategoryId { get; set; }
        //public Category Category { get; set; }
    }
}
