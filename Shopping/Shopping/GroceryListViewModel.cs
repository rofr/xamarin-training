using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Shopping
{
    public class GroceryListViewModel : BaseViewModel
    {
        private readonly IGroceriesRepository _repository;
        private readonly IBluetoothServices _bluetoothServices;

        public ObservableCollection<GroceryListItem> Items { get; set; }

        private GroceryListItem _currentItem;
        public GroceryListItem CurrentGroceryListItem
        {
            get => _currentItem;
            set => Set(ref _currentItem, value);
        }

        public Command AddItemCommand { get; set; }

        public GroceryListViewModel(IGroceriesRepository repository, IBluetoothServices bts)
        {
            _bluetoothServices = bts;

            CurrentGroceryListItem = new GroceryListItem();
            Items = new ObservableCollection<GroceryListItem>();
            AddItemCommand = new Command(AddItem, CanAddItem);
            _repository = repository;

        }

        public async Task Initialize()
        {
            var items = await _repository.GetAll();
            foreach (var item in items) Items.Add(item);
        }

        private bool CanAddItem()
        {
            return !string.IsNullOrWhiteSpace(CurrentGroceryListItem.Name) 
                   && CurrentGroceryListItem.Quantity > 0;
        }

        /// <summary>
        /// The AddItemCommand points to this method
        /// </summary>
        private async void AddItem()
        {
            Items.Add(CurrentGroceryListItem);
            await _repository.Save(CurrentGroceryListItem);
            CurrentGroceryListItem = new GroceryListItem();
            AddItemCommand.ChangeCanExecute();
        }
    }
}