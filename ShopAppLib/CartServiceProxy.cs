using ShopAppLib.Models;
using System.Collections.ObjectModel;

namespace ShopAppLib
{
    public class CartServiceProxy
    {

        private CartServiceProxy()
        {
            cart = new List<Item>();
        }

        private static CartServiceProxy? instance;
        private static object instanceLock = new object();
        public static CartServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CartServiceProxy();
                    }
                }

                return instance;
            }
        }

        private List<Item>? cart;
        public ReadOnlyCollection<Item>? Cart
        {
            get
            {
                return cart?.AsReadOnly();
            }
        }

        //======== functionality
        public int LastId
        {
            get
            {
                if (cart?.Any() ?? false)
                {
                    return cart?.Select(c => c.Id)?.Max() ?? 0;
                }
                return 0;
            }
        }
        public Item? AddOrUpdate(Item? item)
        {
            if (cart == null)
            {
                return null;
            }

            var isAdd = true;

            var existingItem = cart.FirstOrDefault(c => c.Id == item.Id);

            if (existingItem != null) { existingItem.Stock++; }

            else
            {
                Item newItem = new Item
                {
                    Name = item.Name,
                    Price = item.Price,
                    Id = item.Id,
                    Stock = 1
                };

                cart.Add(newItem);
            }


            return item;
        }

        public void Delete(int id)
        {
            if (cart == null)
            {
                return;
            }
            var itemToDelete = cart.FirstOrDefault(c => c.Id == id);

            if (itemToDelete != null)
            {
                cart.Remove(itemToDelete);
            }
        }
    }
}
