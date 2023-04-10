using HtmlAgilityPack;

namespace PricePilot.Models
{
    public class Scrapper3
    {
        public static async Task<List<Product>> Init()
        {
            var productName = "grand theft auto";
            var query = string.Join("+", productName.Split(" ").Select(term => Uri.EscapeDataString(term)));
            var url = "https://store.steampowered.com/search/?term=";
            var link = $"{url}/es/games/?query={query}";

            var httpClient = new HttpClient();
            var htmlDocument = new HtmlDocument();

            var response = await httpClient.GetAsync(link);
            var content = await response.Content.ReadAsStringAsync();
            htmlDocument.LoadHtml(content);

            List<Product> products = new List<Product>();
            var nameProduct = "";
            var priceProduct = "";
            var image = "";
            var linkProduct = "";

            var productNodes = htmlDocument.DocumentNode.SelectNodes("//div[contains(@id,'search_result_container')]/a");
            foreach (var productNode in productNodes)
            {
                var nameProductNode = productNodes;
                if (nameProductNode != null)
                {
                    nameProduct = productNode.Attributes["data-name"].Value;

                }

                if (nameProductNode != null)
                {
                    priceProduct = productNode.Attributes["data-price"].Value;
                }

                var imageNode = productNode.SelectSingleNode(".//img");
                if (imageNode != null && imageNode.Attributes["src"] != null)
                {
                    image = imageNode.Attributes["src"].Value;
                }

                var linkProductNode = productNode.SelectSingleNode(".//div[contains(@class,'catalog-item--title')]/a");
                if (linkProductNode != null && linkProductNode.Attributes["href"] != null)
                {
                    linkProduct = $"{url}{linkProductNode.Attributes["href"].Value}";
                }

                products.Add(new Product(nameProduct, priceProduct, image, linkProduct, "gamestore"));
            }

            return products;
        }
        }
}
