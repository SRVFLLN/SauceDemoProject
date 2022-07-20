namespace SauceDemoProject.Models
{
    public class Product
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Discription { get; set; }

        public double Price { get; set; }

        public string ImageLink { get; set; }

        public override bool Equals(object obj)
        {
            Product model = (Product)obj;
            if (Name == model.Name &
                Discription == model.Discription &
                Price == model.Price &
                ImageLink == model.ImageLink) 
                return true;
            return false;
        }
    }
}
