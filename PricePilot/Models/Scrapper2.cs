using HtmlAgilityPack;
using PricePilot.Models;

namespace PricePilot.Models.Models;

public static class Scraper2
{
    public static async Task<List<Product>> Init(string nombre)
    {
        var productName = "minecraft";
        var query = string.Join("+", productName.Split(" ").Select(term => Uri.EscapeDataString(term)));
        var url = "https://www.gamersgate.com";
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

        var productNodes = htmlDocument.DocumentNode.SelectNodes("//div[contains(@class,'product--item')]");
        if (productNodes != null)
        {
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
        else
        {
            return products;
        }
       
    }
}
