using System.Collections.Generic;

namespace ConsoleApp_MiniProject2_Version1
{
    class Category
    {
        public Category(string catName)
        {
            CatName = catName;
        }

        public int ID { get; set; }
        public string CatName { get; set; }
        List<LaptopComputer> LaptopComputers { get; set; }
        List<MobilePhone> MobilePhones { get; set; }
    }
}
