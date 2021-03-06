﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsQuery;
using PennyGameLibrary.Database;
using PennyGameLibrary.Utility;

namespace PennyGameLibrary.Parser
{
    class SteamParser : BaseParser
    {
        Dictionary<string, Game> games = new Dictionary<string, Game>();

        public SteamParser()
        {
            Url = "http://steamdb.info/sales/";
            try
            {
                Document = CQ.CreateFromUrl(Url);
                Parse();
            }
            catch (Exception ex)
            {
                Logger.WriteError(ex.Message);
            }
        }

        protected override void Parse()
        {
            Logger.Write("Parsing " + Url);
            var titles = Document.Select(".appimg"); 
            titles.Each(e =>
                {
                    var element = CQ.Create(e);
                    var name = element.Select("a:nth-child(2)").Text();
                    var steamLink = element.Select("a:nth-child(2)").Attr("href");
                    var link = "http://steamdb.info" + element.Select("a:nth-child(1)").Attr("href");
                    
                    //check discount field. it sometimes aren't parsed.
                    var discount = element.Find(".price-discount").Text();
                    if (string.IsNullOrWhiteSpace(discount))
                        discount = element.Find(".price-discount-minor").Text();
                    if (string.IsNullOrWhiteSpace(discount))
                        discount = element.Find(".price-discount-major").Text();
                    
                    var initialPrice = element.Find(".price-initial").Text();
                    var salePrice = element.Select("td:nth-child(5)").Text();
                    var type = element.Select("td:nth-child(1)").Text();
                    var imagePart = CQ.CreateFragment(element.Attr("data-title")).Select("img").Attr("src");
                    var image = "http:" + imagePart;
                    if (string.IsNullOrWhiteSpace(imagePart))
                    {
                        var steamId = element.Attr("data-appid");
                        if (!string.IsNullOrWhiteSpace(steamId))
                            image = string.Format("http://cdn4.steampowered.com/v/gfx/apps/{0}/header_292x136.jpg",
                                                  steamId);
                    }

                    var untilRaw = element.Select("td:nth-child(2) span");
                    var untilDate = default(DateTime);
                    if (untilRaw.Any())
                        DateTime.TryParse(untilRaw.Attr("title"), out untilDate);

                    if (type == "Game")
                    {
                        var game = new Game
                            {
                                Name = name,
                                InternalLink = link,
                                Link = steamLink,
                                Discount = discount,
                                Price = initialPrice,
                                SalePrice = salePrice, 
                                Type = type, 
                                ImageUrl = image, 
                                Store = "Steam"
                            };

                        if (untilDate != default(DateTime)) game.Date = untilDate;
                        games.Add(game.Name, game);
                    }
                });
            Logger.Write("Steam sale on {0} items; {1} going to be added", titles.Count(), games.Count);
            GamesDal.SaveGamesListWithDrop(games);
        }
    }
}
