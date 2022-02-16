namespace joyjet_interview_test.Models
{
    public class CartModel
    {
        public int Id { get; set; }
        public IEnumerable<CartItemModel> Items { get; set; }

        public CartModel(int id, IEnumerable<CartItemModel> items)
        {
            Id = id;
            Items = items;
        }
    }
}
