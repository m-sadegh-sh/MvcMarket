namespace MvcMarket.FrontEnd.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using BackEnd;
    using System;

    public class Cart
    {
        private readonly List<CartLine> _lines = new List<CartLine>();
        private MvcMarketDataContext _dataContext = new MvcMarketDataContext();
        public IList<CartLine> Lines
        {
            get { return _lines.AsReadOnly(); }
        }

        public ShippingDetails ShippingDetails(Guid userId)
        {
            return _dataContext.ShippingDetails
               .FirstOrDefault(sd => sd.UserId == userId);
        }

        public void AddItem(Product product, int quantity)
        {
            var line = _lines
                .FirstOrDefault(l => l.Product.ProductId == product.ProductId);
            if (line == null)
                _lines.Add(new CartLine { Product = product, Quantity = quantity });
            else
                line.Quantity += quantity;
        }

        public decimal ComputeTotalValue()
        {
            return _lines.Sum(l => l.Product.Price * l.Quantity);
        }

        public void Clear()
        {
            _lines.Clear();
        }

        public void RemoveLine(Product product)
        {
            _lines.RemoveAll(l => l.Product.ProductId == product.ProductId);
        }

        internal Guid Submit(Guid userId)
        {
            var shippingDetails = _dataContext.ShippingDetails.FirstOrDefault(sd => sd.UserId == userId);
            shippingDetails.CreditAmount -= ComputeTotalValue();
            var cart = new BackEnd.Cart
            {
                CartId = Guid.NewGuid(),
                Status = 0,
                UserId = userId,
                ShippedOn = DateTime.Now
            };
            _dataContext.Carts.InsertOnSubmit(cart);
            foreach (var cartLine in _lines)
            {
                var _cartLine = new BackEnd.CartLine
                {
                    CartId = cart.CartId,
                    ProductId = cartLine.Product.ProductId,
                    Quantity = cartLine.Quantity
                };
                _dataContext.CartLines.InsertOnSubmit(_cartLine);
            }
            _dataContext.SubmitChanges();
            return cart.CartId;
        }
    }
}