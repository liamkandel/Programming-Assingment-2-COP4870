using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShopAppLib.Maui.ViewModels
{
    public class InventoryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<ItemViewModel> Items
        {
            get
            {
                return InventoryServiceProxy.Current?.Items?.Select(c => new ItemViewModel(c)).ToList()
                    ?? new List<ItemViewModel>();
            }
        }



        public ItemViewModel SelectedItem { get; set; }
        public InventoryViewModel()
        {
        }

        public void RefreshItems()
        {
            NotifyPropertyChanged("Items");
        }

        public void UpdateItem()
        {
            if (SelectedItem?.Item == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//AddItem?itemId={SelectedItem.Item.Id}");
            //$"//ProjectDetail?clientId={Model.ClientId}"
            InventoryServiceProxy.Current.AddOrUpdate(SelectedItem.Item);
        }

        public void DeleteItem()
        {
            if (SelectedItem?.Item == null)
            {
                return;
            }

            InventoryServiceProxy.Current.Delete(SelectedItem.Item.Id);
            RefreshItems();
        }
    }
}
