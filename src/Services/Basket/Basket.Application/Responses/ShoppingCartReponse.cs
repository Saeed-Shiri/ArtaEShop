

using System.ComponentModel;

namespace Basket.Application.Responses
{
    public class ShoppingCartReponse
    {
        public string UserName { get; set; }
        public List<ShoppingCartItemResonse> Items { get; set; } = new List<ShoppingCartItemResonse>();
        public ShoppingCartReponse()
        {
            
        }
        public ShoppingCartReponse(string userName)
        {
            UserName = userName;
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (ShoppingCartItemResonse item in Items) { totalPrice += item.Price * item.Quantity; }
                return totalPrice;
            }
        }
    }
}
