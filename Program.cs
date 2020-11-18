using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;

namespace WebsiteScrapingBot
{
    class Program
    {

        static async Task Main(string[] args)
        {
            var scraper = new JFWebScraper("http://www.google.com/");
            var conn = await scraper.ConnectToWebsiteAsync();
            if(conn.success)
            {
                var favicon = scraper.GetFavicon();
                Console.WriteLine(conn.response);
                Console.WriteLine(favicon);
            }
            else
            {
                Console.WriteLine(conn.response);
            }
        }

    }
}
