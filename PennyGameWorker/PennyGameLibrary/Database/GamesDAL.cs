using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PennyGameLibrary.Parser;

namespace PennyGameLibrary.Database
{
    class GamesDal: BaseDal
    {
        public static bool SaveGamesListWithDrop(Dictionary<string, Game> games)
        {
            if (games.Count == 0) return false;
            var store = games.First().Value.Store;

            if (!ExecuteNonQuery("DELETE FROM gamesale WHERE store=@store",
                new Dictionary<string, object>() {
                    {"@store", store}})) 
                return false;
            
            foreach (var game in games)
            {
                //TODO: Modify to use MySQL Transactions
                var self = game.Value;
                ExecuteNonQuery("INSERT INTO gamesale (type, name, link, internalLink, discount, price, salePrice, date, imageUrl, store)" +
                                "VALUES(@type, @name, @link, @ilink, @discount, @price, @sprice, @date, @image, @store)",
                new Dictionary<string, object>() {
                    {"@type", self.Type},
                    {"@name", self.Name},
                    {"@link", self.Link}, 
                    {"@ilink", self.InternalLink}, 
                    {"@discount", self.Discount}, 
                    {"@price", self.Price}, 
                    {"@sprice", self.SalePrice}, 
                    {"@date", self.Date}, 
                    {"@image", self.ImageUrl}, 
                    {"@store", self.Store}
                });    
            }
            return true;
        }
    }
}
