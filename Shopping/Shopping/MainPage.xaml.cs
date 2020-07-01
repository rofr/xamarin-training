using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace Shopping
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private GroceriesRepository _groceriesRepository;
        public MainPage()
        {
            InitializeComponent();

            //Example of creating a binding from code
            MyCollectionView.SetBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(Items), source : this));

            _groceriesRepository = new GroceriesRepository();
            var items = _groceriesRepository.GetAll().Result;
            foreach (var item in items) Items.Add(item);

            MessageBus.Subscribe<ItemAdded>(async e =>
            {
                await _groceriesRepository.Save(e.GroceryListItem);
                Items.Add(e.GroceryListItem);
            });
        }

        
        public ObservableCollection<GroceryListItem> Items { get; private set; } = new ObservableCollection<GroceryListItem>();


        private void Button_OnClicked(object sender, EventArgs e)
        {
            //Bring up a new page to enter the item details
            // when the page closes, it will send a message on the message bus
            this.Navigation.PushModalAsync(new AddItemPage());
        }
    }
}
