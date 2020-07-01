using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shopping
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddItemPage : ContentPage
    {
        public AddItemPage()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            //Create an item based on the contents of the controls
            var item = new GroceryListItem
            {
                Name = ItemName.Text,
                Quantity = int.Parse(ItemQuantity.Text)
            };

            //Tell the world about this great achievement
            var @event = new ItemAdded {GroceryListItem = item};
            MessageBus.Publish(@event);

            //our work is done here, shuts down the current window
            this.Navigation.PopModalAsync(true);
        }
    }
}