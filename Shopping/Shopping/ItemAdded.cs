namespace Shopping
{
    /// <summary>
    /// Event class used to notify over the message bus
    /// that an item has been added
    /// </summary>
    public class ItemAdded
    {
        public GroceryListItem GroceryListItem;
    }
}