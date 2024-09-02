namespace BiletPortal.Models
{
    public class CardItem
    {
        public long ProductId {  get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }   
        public string ProductPicture { get; set; }
        public decimal Total
        {
            get { return Quantity * Price; }
        }
        public CardItem()
        {
        }
        public CardItem(Products products)
        {
            ProductId = products.ProductId;
            ProductName = products.ProductName;
            Quantity = 1;
            Price = (decimal)products.ProductPrice;
            ProductPicture = products.ProductPicture;
        }




    }
}
