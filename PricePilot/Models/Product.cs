namespace PricePilot.Models
{
    public class Product
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public string Page { get; set; }

        public Product(string name, string price, string image, string link, string page)
        {
            Name = name;
            Price = price;
            Image = image;
            Link = link;
            Page = page;
        }
    }
}
