namespace MvcMarket.FrontEnd.Models
{
    using BackEnd;

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}