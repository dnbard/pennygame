using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PennyGameLibrary.Parser;
using PennyGameLibrary.Utility;

namespace PennyGameLibrary.Database
{
    class GamesDal : BaseDal
    {
        public static void SaveGamesListWithDrop(Dictionary<string, Game> games)
        {
            if (games.Count == 0) return;
            var store = games.First().Value.Store;

            if (!ExecuteNonQuery("DELETE FROM gamesale WHERE store=@store",
                new Dictionary<string, object>() {
                    {"@store", store}}))
                return;

            AddListToDatabase(games);
            Logger.Write("{0} gamesale items were added", games.Count);
        }

        public static void SaveGamesList(Dictionary<string, Game> games)
        {
            if (games.Count == 0) return;
            AddListToDatabase(games);
            Logger.Write("{0} gamesale items were added", games.Count);
        }

        public static void SaveGame(Game game)
        {
            if (game == null) return;
            AddGameToDatabase(game);
            Logger.Write("1 gamesale item were added");
        }

        private static void AddListToDatabase(Dictionary<string, Game> games)
        {
            foreach (var game in games)
            {
                //TODO: Modify to use MySQL Transactions
                var self = game.Value;
                AddGameToDatabase(self);
            }
        }

        private static void AddGameToDatabase(Game game)
        {
            ExecuteNonQuery("INSERT INTO gamesale (type, name, link, internalLink, discount, price, salePrice, date, imageUrl, store)" +
                                "VALUES(@type, @name, @link, @ilink, @discount, @price, @sprice, @date, @image, @store)",
                new Dictionary<string, object>() {
                    {"@type", game.Type},
                    {"@name", game.Name},
                    {"@link", game.Link}, 
                    {"@ilink", game.InternalLink}, 
                    {"@discount", game.Discount}, 
                    {"@price", game.Price}, 
                    {"@sprice", game.SalePrice}, 
                    {"@date", game.Date}, 
                    {"@image", game.ImageUrl}, 
                    {"@store", game.Store}
                });
        }        
    }
}
