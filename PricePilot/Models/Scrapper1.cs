using HtmlAgilityPack;
using PricePilot.Models;

namespace PricePilot.Models.Models
{
    public static class Scraper
    {
        public static async Task<List<Product>> Init()
        {
            var name = "minecraft";
            var query = string.Join("%20", name.Split(" ").Select(term => Uri.EscapeDataString(term)));
            var url = "https://www.eneba.com";
            var link = $"{url}/store/all?text=/{query}&enbCampaign=Main%20Search&enbContent=search%20dropdown%20-%20categories&enbMedium=link&enbTerm=";

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


            var productNodes = htmlDocument.DocumentNode.SelectNodes("//div[contains(@class,'uy1qit')]");
            foreach (var productNode in productNodes)
            {
               

                var nameProductNode = productNode.SelectSingleNode(".//span[contains(@class,'YLosEL')]");
                if (nameProductNode != null)
                {
                     nameProduct = nameProductNode.InnerText;
                }

                var priceProductNode = productNode.SelectSingleNode(".//span[contains(@class,'L5ErLT')]");
                if (priceProductNode != null)
                {
                     priceProduct = priceProductNode.InnerText;
                }

                var imageNode = productNode.SelectSingleNode(".//img");
                if (imageNode != null && imageNode.Attributes["src"] != null)
                {
                     image = imageNode.Attributes["src"].Value;
                }

                var linkProductNode = productNode.SelectSingleNode(".//a[contains(@class,'oSVLlh')]");
                if (linkProductNode != null && linkProductNode.Attributes["href"] != null)
                {
                    linkProduct = $"{url}{linkProductNode.Attributes["href"].Value}";
                }

                products.Add(new Product(nameProduct, priceProduct, image, linkProduct, "Asdasd"));

            }
            return products;
        }
    }
}

