using WarehouseApp.Models;
using System.Linq;
using System;
using System.Drawing;
using System.Runtime.ConstrainedExecution;


namespace WarehouseApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(WarehouseContext db)
        {
            db.Database.EnsureCreated();

            // Проверка инициализации БД
            if (db.Components.Any())
            {
                return;   // База данных инициализирована
            }
            
            Random randObj = new Random(1);


            // for supplier table
            string[] supplierNames = { "Global Supply Co.", "Reliable Goods Inc.", "Prime Products Ltd.", "Superior Supplies Corp.",
                "Quality Merchandise Co.", "Elite Trading Group", "Provisions Unlimited", "Premium Goods Distributors", "Optimal Sourcing Solutions",
                "Supreme Suppliers Inc.", "Top-tier Trading Company", "Trustworthy Imports Ltd.", "Exclusive Exporters Corp.",
                "Ultimate Wholesale Distributors", "First-Class Goods Ltd.", "Premier Product Providers", "Exceptional Sourcing Co.",
                "Reliable Merchandise Distributors", "Quality Imports Ltd.", "Prime Wholesale Solutions", "Superior Trading Partners",
                "Premium Supply Co.", "Elite Merchandise Distributors", "Proven Sourcing Solutions", "Superior Imports Inc.",
                "Trustworthy Suppliers Ltd.", "Exclusive Goods Corp.", "Ultimate Trading Group", "First-Rate Suppliers Inc.",
                "Premier Merchandise Distributors", "Exceptional Imports Ltd.", "Reliable Wholesale Solutions", "Quality Trading Company",
                "Prime Merchandise Distributors", "Superior Goods Ltd.", "Premium Imports Inc.", "Elite Wholesale Solutions", "Proven Trading Group",
                "Superior Suppliers Ltd.", "Trustworthy Goods Corp.", "Exclusive Wholesale Distributors", "Ultimate Imports Inc.", "First-Class Trading Company",
                "Premier Supplies Corp.", "Exceptional Sourcing Co.", "Reliable Merchandise Distributors", "Quality Imports Ltd.", "Prime Wholesale Solutions",
                "Superior Trading Partners", "Premium Supply Co." };

            // for type component table
            string[] typeComponentNames = { "процессор", "материнская плата", "оперативная память (RAM)", "видеокарта", "жесткий диск (HDD или SSD)", "блок питания",
                "сетевая карта", "оптический привод (CD/DVD)", "звуковая карта", "кулеры (для процессора и корпуса)", "Bluetooth-адаптер", "Wi-Fi-адаптер",
                "картридер (для чтения карт памяти)" };

            // for component table
            string[] videoCardNames = { "NVIDIA GeForce RTX 3080", "NVIDIA GeForce RTX 3090", "NVIDIA GeForce RTX 3070", "NVIDIA GeForce RTX 3060 Ti",
                "NVIDIA GeForce RTX 2080 Ti", "NVIDIA GeForce RTX 2080 Super", "NVIDIA GeForce RTX 2070 Super", "NVIDIA GeForce RTX 2060 Super",
                "AMD Radeon RX 6900 XT", "AMD Radeon RX 6800 XT", "AMD Radeon RX 6700 XT", "AMD Radeon RX 6600 XT", "NVIDIA GeForce GTX 1660 Ti",
                "NVIDIA GeForce GTX 1660 Super", "NVIDIA GeForce GTX 1650 Super", "AMD Radeon RX 580", "AMD Radeon RX 5700 XT", "AMD Radeon RX 5600 XT",
                "NVIDIA GeForce GT 1030", "NVIDIA GeForce GTX 1050 Ti" };
            string[] cpuNames = { "Intel Core i9-11900K", "Intel Core i7-11700K", "Intel Core i5-11600K", "AMD Ryzen 9 5950X", "AMD Ryzen 9 5900X",
                "AMD Ryzen 7 5800X", "AMD Ryzen 5 5600X", "Intel Core i9-10900K", "Intel Core i7-10700K", "Intel Core i5-10600K", "AMD Ryzen 9 3950X",
                "AMD Ryzen 9 3900X", "AMD Ryzen 7 3800X", "AMD Ryzen 5 3600X", "Intel Core i9-9900K", "Intel Core i7-9700K", "Intel Core i5-9600K",
                "AMD Ryzen 7 3700X", "AMD Ryzen 5 3600", "Intel Core i3-10100" };
            string[] ramNames = { "Corsair Vengeance LPX 16GB (2 x 8GB) DDR4-3200", " G.Skill Trident Z RGB 16GB (2 x 8GB) DDR4-3600",
                " Kingston HyperX Fury 8GB DDR4-2666", " Crucial Ballistix 32GB (2 x 16GB) DDR4-3200",
                " Corsair Dominator Platinum RGB 32GB (2 x 16GB) DDR4-3600", " Team T-Force Vulcan Z 16GB (2 x 8GB) DDR4-3000",
                " Patriot Viper Steel Series 16GB (2 x 8GB) DDR4-3200", " G.Skill Ripjaws V 32GB (2 x 16GB) DDR4-3200",
                " Corsair Vengeance RGB Pro 16GB (2 x 8GB) DDR4-3200", " Crucial Ballistix MAX 32GB (2 x 16GB) DDR4-4000",
                " Kingston HyperX Predator RGB 16GB (2 x 8GB) DDR4-3600", " Team Group T-Force Delta RGB 8GB DDR4-3200",
                " Corsair Vengeance LPX 32GB (2 x 16GB) DDR4-3600", " G.Skill Trident Z Neo 32GB (2 x 16GB) DDR4-3600",
                " Crucial Ballistix RGB 16GB (2 x 8GB) DDR4-3200", " Corsair Dominator Platinum RGB 64GB (2 x 32GB) DDR4-3600",
                " Patriot Viper RGB 16GB (2 x 8GB) DDR4-3200", " Kingston HyperX Fury RGB 16GB (2 x 8GB) DDR4-3200",
                " Team Group T-Force Xtreem ARGB 32GB (2 x 16GB) DDR4-3600", " Corsair Vengeance RGB Pro SL 32GB (2 x 16GB) DDR4-3200" };
            string[] motherboardNames = { "ASUS ROG Strix X570-E Gaming", "Gigabyte Z590 AORUS Master", "MSI MPG B550 Gaming Carbon WiFi",
                "ASUS Prime X570-Pro", "Gigabyte B450 AORUS Elite", "MSI MAG B560M Mortar WiFi", "ASRock X570 Phantom Gaming 4", "ASUS TUF Gaming X570-Plus (Wi-Fi)",
                "Gigabyte Z590 AORUS Ultra", "MSI MPG Z490 Gaming Carbon WiFi", "ASUS ROG Maximus XIII Hero", "ASRock B550 Phantom Gaming 4", "Gigabyte X570 AORUS Elite",
                "MSI MAG B550 TOMAHAWK", "ASUS Prime Z590-A", "Gigabyte B450M DS3H", "ASRock B550 Steel Legend", "MSI MPG X570 Gaming Plus",
                "ASUS ROG Strix B550-F Gaming (Wi-Fi)", "Gigabyte Z590 AORUS Pro AX" };

            int supplierCount = supplierNames.GetLength(0);
            int typeComponentCount = typeComponentNames.GetLength(0);
            int componentCount = 75;

            string supplierName;
            for (int supplierId = 0; supplierId < supplierCount; supplierId++)
            {
                supplierName = supplierNames[supplierId];
                db.Suppliers.Add(new Supplier
                {
                    Name = supplierName
                });
            }

            db.SaveChanges();

            string typeComponentName;
            for (int typeComponentId = 0; typeComponentId < typeComponentCount; typeComponentId++)
            {
                typeComponentName = typeComponentNames[typeComponentId];
                db.TypeComponents.Add(new TypeComponent
                {
                    Name = typeComponentName
                });
            }

            db.SaveChanges();


            string componentName;
            int supplierIdRand;
            int typeCompId;
            int price;
            int amount;
            DateTime date;

            static DateTime RandomDay(DateTime start)
            {
                Random gen = new Random();
                //DateTime start = new DateTime(1995, 1, 1);
                int range = (DateTime.Today - start).Days;
                return start.AddDays(gen.Next(range));
            }


            for (int componentId = 0; componentId < componentCount; componentId++)
            {
                if (componentId < 20)
                {
                    componentName = videoCardNames[componentId];
                    typeCompId = 4;
                }
                else if (componentId < 40)
                {
                    componentName = cpuNames[componentId - 20];
                    typeCompId = 1;
                }
                else if (componentId < 60)
                {
                    componentName = ramNames[componentId - 40];
                    typeCompId = 3;
                }
                else
                {
                    componentName = motherboardNames[componentId - 60];
                    typeCompId = 2;
                }

                supplierIdRand = randObj.Next(1, supplierCount);
                price = randObj.Next(100, 1000);
                amount = randObj.Next(1, 15);
                date = RandomDay(new DateTime(2022, 1, 1));

                db.Components.Add(new Component
                {
                    Name = componentName,
                    SupplierId = supplierIdRand,
                    TypeComponentId = typeCompId,
                    Price = price,
                    Amount = amount,
                    Date = date
                });
            }
            db.SaveChanges();
        }
    }
}
