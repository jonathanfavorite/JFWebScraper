﻿using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using System.Threading.Tasks;

namespace WebsiteScrapingBot
{

    public class JFWebScraperResponse
    {
        public bool success = false;
        public string response = "Default Response";
    }
    public class JFWebScraper
    {
        private HtmlWeb web;
        public string website;
        private HtmlDocument request;
        public JFWebScraper()
        {
            web = new HtmlWeb();
            website = null;
        }
        public JFWebScraper(string Website)
        {
            this.website = Website;
            web = new HtmlWeb();
        }
        public async Task<JFWebScraperResponse> ConnectToWebsiteAsync()
        {
            var ret = new JFWebScraperResponse { response = "Connecting to WebsiteASYNC", success = false };
            try
            {
                request = await web.LoadFromWebAsync(website);
                ret.success = true;
                ret.response = String.Format("Successfully connected to {0}", website);
            }
            catch(Exception ex)
            {
                ret.response = String.Format("Error: {0}", ex.Message);
            }
            return ret;
        }
        public string GetFavicon()
        {
            return ParseFavicon(request);
        }
        private string ParseFavicon(HtmlDocument websiteData)
        {
            var favicon = "default";

            var el = websiteData.DocumentNode.SelectSingleNode("/html/head/link[@rel='icon' and @href]");
            var el1 = websiteData.DocumentNode.SelectSingleNode("/html/head/link[@rel='shortcut icon' and @href]");
            var el2 = websiteData.DocumentNode.SelectSingleNode("/html/head/link[@rel='icon shortcut' and @href]");

            var found = false;
            if (el != null)
            {
                favicon = el.Attributes["href"].Value;
                found = true;
            }
            else if (el1 != null)
            {
                favicon = el1.Attributes["href"].Value;
                found = true;
            }
            else if (el2 != null)
            {
                favicon = el2.Attributes["href"].Value;
                found = true;
            }

            if (favicon.Contains("https://") || favicon.Contains("http://")) { }
            else
            {
                if (found)
                {
                    favicon = website + favicon;
                }
            }

            return favicon;
        }
    }
}
