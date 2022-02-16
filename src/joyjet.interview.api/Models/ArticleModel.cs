namespace joyjet_interview_test.Models
{
    public class ArticleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Price { get; set; } // I didn't use decimal because Price is in cents

        public ArticleModel(int id, string name, long price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
