namespace Shopping
{
    /// <summary>
    /// Doesn't need to be INotifyPropertyChanged because the properties never change!
    /// </summary>
    public class GroceryListItem
    {
        /// <summary>
        /// The Id is required for SqlLite
        /// </summary>
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }


        /// <summary>
        /// When data binding to an object without a DataTemplate,
        /// the result of ToString is displayed
        /// </summary>
        public override string ToString()
        {
            return Name + " " + Quantity;
        }
    }
}