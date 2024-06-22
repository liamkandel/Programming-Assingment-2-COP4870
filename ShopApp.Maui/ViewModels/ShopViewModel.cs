using ShopAppLib;
using ShopAppLib.Maui.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShopApp.Maui.ViewModels
{
    public class ShopViewModel : INotifyPropertyChanged
    {
        public ShopViewModel()
        {
            InventoryQuery = string.Empty;
        }

        public decimal SubTotal
        {
            get
            {
                return Cart.Sum(c => c.Price * c.Stock);
            }
        }

        public string FormattedSubTotal
        {
            get { return SubTotal.ToString("C"); }
        }

        private string inventoryQuery;
        public string InventoryQuery
        {
            set
            {
                inventoryQuery = value;
                NotifyPropertyChanged();
            }
            get { return inventoryQuery; }
        }
        public List<ItemViewModel> Items
        {
            get
            {
                return InventoryServiceProxy.Current.Items.Where(p => p != null)
                    .Where(p => p?.Name?.ToUpper()?.Contains(InventoryQuery.ToUpper()) ?? false)
                    .Select(p => new ItemViewModel(p)).ToList()
                    ?? new List<ItemViewModel>();
            }
        }
        public ItemViewModel ItemToBuy { get; set; }

        public List<ItemViewModel> Cart
        {
            get
            {
                return CartServiceProxy.Current?.Cart?.Select(c => new ItemViewModel(c)).ToList()
                    ?? new List<ItemViewModel>();
            }
        }

        public void Refresh()
        {
            InventoryQuery = string.Empty;
            NotifyPropertyChanged(nameof(Items));
            NotifyPropertyChanged(nameof(ItemToBuy));
            NotifyPropertyChanged(nameof(Cart));
            NotifyPropertyChanged(nameof(SubTotal));
            NotifyPropertyChanged(nameof(FormattedSubTotal));
        }

        public void Search()
        {
            NotifyPropertyChanged(nameof(Items));
        }

        public void PlaceInCart()
        {

            if (ItemToBuy.Stock > 0)
            {
                CartServiceProxy.Current.AddOrUpdate(ItemToBuy.Item);
                ReduceItemToBuy();
                Refresh();
            }
        }

        public void ReduceItemToBuy()
        {
            var existingItem = InventoryServiceProxy.Current.Items.FirstOrDefault(i => i.Id == ItemToBuy.Id);
            existingItem.Stock--;
            ItemToBuy.Stock--;

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

