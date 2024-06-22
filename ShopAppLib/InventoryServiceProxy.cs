// Liam Kandel
using ShopAppLib.Models;
using System.Collections.ObjectModel;

namespace ShopAppLib
{
    public class InventoryServiceProxy
    {

        private InventoryServiceProxy()
        {
            items = new List<Item>()
            {
                new Item{Name="iPhone", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Price=999.99M, Stock=12, Id=1},
                new Item{Name="Computer", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Price=100.926M, Stock=3, Id=2 },
                new Item{Name="Headphones", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Price=33.1M, Stock=6, Id=3 },
                new Item{Name="Water Bottle", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Price=300.5M, Stock=3, Id=4 },
                new Item{Name="Speaker", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Price=300.5M, Stock=6, Id=5 },
                new Item{Name="Notebook", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Price=300.5M, Stock=-4, Id=6 },
            };
        }

        private static InventoryServiceProxy? instance;
        private static object instanceLock = new object();
        public static InventoryServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new InventoryServiceProxy();
                    }
                }

                return instance;
            }
        }

        private List<Item>? items;
        public ReadOnlyCollection<Item>? Items
        {
            get
            {
                return items?.AsReadOnly();
            }
        }

        //======== functionality
        public int LastId
        {
            get
            {
                if (items?.Any() ?? false)
                {
                    return items?.Select(c => c.Id)?.Max() ?? 0;
                }
                return 0;
            }
        }
        public Item? AddOrUpdate(Item? item)
        {
            if (items == null)
            {
                return null;
            }

            var isAdd = false;

            if (item.Id == 0)
            {
                item.Id = LastId + 1;
                isAdd = true;
            }

            if (isAdd)
            {
                items.Add(item);
            }
            else
            {
                var existingItem = items.FirstOrDefault(i => i.Id == item.Id);
                if (existingItem != null)
                {
                    existingItem.Name = item.Name;
                    existingItem.Price = item.Price;
                    existingItem.Stock = item.Stock;
                    existingItem.Description = item.Description;
                }
            }

            return item;
        }

        public void Delete(int id)
        {
            if (items == null)
            {
                return;
            }
            var itemToDelete = items.FirstOrDefault(c => c.Id == id);

            if (itemToDelete != null)
            {
                items.Remove(itemToDelete);
            }
        }
    }
}

