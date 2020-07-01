using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Shopping.Tests
{
    public class GroceryListViewModelTests
    {
        private IBluetoothServices _bluetoothServices;
        private IGroceriesRepository _repository;
        private readonly GroceryListViewModel _viewModel;

        public GroceryListViewModelTests()
        {
            _repository = new MockRepository();
            _bluetoothServices = new MockBluetoothServices();
            _viewModel = new GroceryListViewModel(_repository, _bluetoothServices);
        }

        [Fact]
        public void Cannot_add_empty_item()
        {
            _viewModel.CurrentGroceryListItem.Name = "";
            _viewModel.CurrentGroceryListItem.Quantity = 4;
            Assert.False(_viewModel.AddItemCommand.CanExecute(null));
        }

        [Fact]
        public void Cannot_add_item_with_zero_quantity()
        {
            _viewModel.CurrentGroceryListItem.Name = "Fish";
            _viewModel.CurrentGroceryListItem.Quantity = 0;
            Assert.False(_viewModel.AddItemCommand.CanExecute(null));
        }

        [Fact]
        public async Task Adding_an_item_saves_to_repository()
        {
            var itemsBefore = await _repository.GetAll();

            _viewModel.CurrentGroceryListItem.Name = "Fish";
            _viewModel.CurrentGroceryListItem.Quantity = 1;

            _viewModel.AddItemCommand.Execute(null);

            var itemsAfter = await _repository.GetAll();
            Assert.True(itemsBefore.Count == itemsAfter.Count - 1);
            var added = itemsAfter.Last();
            Assert.Equal("Fish", added.Name);
            Assert.Equal(1, added.Quantity);
        }

        [Fact]
        public void Adding_an_item_adds_an_item()
        {
            var itemsBefore = _viewModel.Items.Count;

            _viewModel.CurrentGroceryListItem.Name = "Fish";
            _viewModel.CurrentGroceryListItem.Quantity = 1;

            GroceryListItem added = null;
            _viewModel.Items.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                    added = (GroceryListItem) args.NewItems[0];
            };

            _viewModel.AddItemCommand.Execute(null);

            Assert.Equal(itemsBefore + 1, _viewModel.Items.Count);
            Assert.NotNull(added);
        }

        [Fact]
        public void Initial_model_is_empty()
        {
            Assert.Empty(_viewModel.Items);
        }

        [Fact]
        public async Task Initializing_loads_from_repository()
        {
            await _repository.Save(new GroceryListItem
            {
                Name = "Fish",
                Quantity = 2
            });

            await _viewModel.Initialize();
            Assert.Single(_viewModel.Items);
        }
    }
}
