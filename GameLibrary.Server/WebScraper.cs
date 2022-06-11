using GameLibrary.Server.Models;
using HtmlAgilityPack;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace GameLibrary.Server
{
    public static class WebScraper
    {
        public static HtmlDocument GetDocument(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            return doc;
        }

        public static List<Game> GetGameData(string url)
        {
            //List<Game> games = new();
            //List<string> links = new();
            //HtmlDocument doc = GetDocument(url);
            //var baseUri = new Uri(url);

            //HtmlNodeCollection linkNodes = doc.DocumentNode.SelectNodes("//table[contains(@class, \"sectiontable\")]//tr//td[contains(@class, \"col1\")]//a");

            //foreach (var link in linkNodes)
            //{
            //    string href = link.Attributes["href"].Value;
            //    links.Add(new Uri(baseUri, href).AbsoluteUri);
            //}

            //string json = JsonSerializer.Serialize<List<string>>(links);
            //File.WriteAllText(@"C:\links.json", json);

            List<Game> games = new List<Game>();
            List<string> links = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(@"C:\links.json"));

            foreach (var l in links)
            {
                HtmlDocument document = GetDocument(l);

                Game game = new();

                var nodes = document.DocumentNode.SelectNodes("//table[@id = \"table4\"]//tr//td[2]");

                List<string> content = new();
                foreach (var n in nodes)
                {
                    content.Add(n.InnerText);
                }
                game.OfficialTitle = Regex.Replace(content[0] ?? "", @"\t|\n|\r|&nbsp;|-&nbsp;", "").Trim();
                game.CommonTitle = Regex.Replace(content[1] ?? "", @"\t|\n|\r|&nbsp;|-&nbsp;", "").Trim();
                game.SerialNumber = Regex.Replace(content[2] ?? "", @"\t|\n|\r|&nbsp;|-&nbsp;", "").Trim();
                game.Region = Regex.Replace(content[3] ?? "", @"\t|\n|\r|&nbsp;|-&nbsp;", "").Trim();
                game.GenreStyle = Regex.Replace(content[4] ?? "", @"\t|\n|\r|&nbsp;|-&nbsp;", "").Trim();
                game.Developer = Regex.Replace(content[5] ?? "", @"\t|\n|\r|&nbsp;|-&nbsp;", "").Trim();
                game.Publisher = Regex.Replace(content[6] ?? "", @"\t|\n|\r|&nbsp;|-&nbsp;", "").Trim();
                game.DateReleased = Regex.Replace(content[7] ?? "", @"\t|\n|\r|&nbsp;|-&nbsp;", "").Trim();

                game.Barcode = Regex.Replace(document.DocumentNode.SelectSingleNode("//table[@id= \"table7\"]//li").InnerText ?? "", @"\t|\n|\r|&nbsp;|-&nbsp;", "").Trim();
                try
                {
                    game.Language = Regex.Replace(document.DocumentNode.SelectSingleNode("//table[@id= \"table13\"]//tr[1]//td[1]").InnerText ?? "", @"\t|\n|\r|&nbsp;|-&nbsp;", "").Trim();
                }
                catch (Exception ex)
                {
                    game.Language = "";
                }
                game.GameDescription = Regex.Replace(document.DocumentNode.SelectSingleNode("//table[@id= \"table16\"]//tr//td").InnerText ?? "", @"\t|\n|\r|&nbsp;|-&nbsp;", "").Trim();
                game.NumberOfPlayers = Regex.Replace(document.DocumentNode.SelectSingleNode("//table[@id= \"table19\"]//tr[1]//td[2]").InnerText ?? "", @"\t|\n|\r|&nbsp;|-&nbsp;", "").Trim();
                game.NumberOfMemoryCardBlocks = Regex.Replace(document.DocumentNode.SelectSingleNode("//table[@id= \"table19\"]//tr[2]//td[2]").InnerText ?? "", @"\t|\n|\r|&nbsp;|-&nbsp;", "").Trim();
                game.Vibration = Regex.Replace(document.DocumentNode.SelectSingleNode("//table[@id= \"table19\"]//tr[7]//td[2]").InnerText ?? "", @"\t|\n|\r|&nbsp;|-&nbsp;", "").Trim();
                game.MultiTapFunction = Regex.Replace(document.DocumentNode.SelectSingleNode("//table[@id= \"table19\"]//tr[8]//td[2]").InnerText ?? "", @"\t|\n|\r|&nbsp;|-&nbsp;", "").Trim();
                game.LinkCableFunction = Regex.Replace(document.DocumentNode.SelectSingleNode("//table[@id= \"table19\"]//tr[9]//td[2]").InnerText ?? "", @"\t|\n|\r|&nbsp;|-&nbsp;", "").Trim();

                games = new();
                games = JsonSerializer.Deserialize<List<Game>>(File.ReadAllText(@"C:\games.json"));
                games.Add(game);
                string gamesJson = JsonSerializer.Serialize<List<Game>>(games);
                File.WriteAllText(@"C:\games.json", gamesJson);

            }

            return games;
        }
    }
}
