using ShopAppLib.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
namespace ShopAppLib.Maui.ViewModels
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand? EditCommand { get; private set; }
        public ICommand? DeleteCommand { get; private set; }

        public Item? Item;

        private string? name;
        public string? Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string? description;
        public string? Description
        {
            get => description;
            set
            {
                if (description != value)
                {
                    description = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private decimal price;
        public decimal Price
        {
            get => price;
            set
            {
                if (price != value)
                {
                    price = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int stock;
        public int Stock
        {
            get => stock;
            set
            {
                if (stock != value)
                {
                    stock = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string FormattedPrice
        {
            get { return price.ToString("C"); }
        }

        public int Id { get; set; }

        private void ExecuteEdit(ItemViewModel? i)
        {
            if (i?.Item == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//AddItem?itemId={i.Item.Id}");
        }

        private void ExecuteDelete(int? id)
        {
            if (id == null)
            {
                return;
            }

            InventoryServiceProxy.Current.Delete(id ?? 0);
        }

        public void Add()
        {
            var newItem = new Item
            {
                Name = this.Name,
                Description = this.Description,
                Price = this.Price,
                Stock = this.Stock,
                Id = this.Id
            };
            InventoryServiceProxy.Current.AddOrUpdate(newItem);
        }

        public void AddToCart()
        {
            var newItem = new Item
            {
                Name = this.Name,
                Description = this.Description,
                Price = this.Price,
                Stock = this.Stock,
                Id = this.Id
            };
            CartServiceProxy.Current.AddOrUpdate(newItem);
        }

        public void SetupCommands()
        {
            EditCommand = new Command(
               (c) => ExecuteEdit(c as ItemViewModel));
            DeleteCommand = new Command(
   (c) => ExecuteDelete((c as ItemViewModel)?.Item?.Id));
        }

        public ItemViewModel()
        {
            Item = new Item();
            SetupCommands();
        }

        public ItemViewModel(int id)
        {
            Item = InventoryServiceProxy.Current?.Items?.FirstOrDefault(i => i.Id == id);

            if (Item == null)
            {
                Item = new Item();
            }

            Id = Item.Id;
            Name = Item.Name;
            Description = Item.Description;
            Price = Item.Price;
            Stock = Item.Stock;
        }

        public ItemViewModel(Item i)
        {
            Item = i;
            Id = Item.Id;
            Name = Item.Name;
            Description = Item.Description;
            Price = Item.Price;
            Stock = Item.Stock;
            SetupCommands();

        }

        public string? Display
        {
            get
            {
                return ToString();
            }
        }
    }
}
