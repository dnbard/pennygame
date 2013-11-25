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
            var type = games.First().Value.Type;

            if (!ExecuteNonQuery("DELETE FROM gamesale WHERE type=@Type",
                new Dictionary<string, string>() {
                    {"@Type",type}})) 
                return false;
            
            foreach (var game in games)
            {
                //TODO: Modify to use MySQL Transactions
                var self = game.Value;
                ExecuteNonQuery("INSERT INTO gamesale (type, name) VALUES(@Type, @Name)",
                new Dictionary<string, string>() {
                    {"@Type", self.Type},
                    {"@Name", self.Name}});    
            }
            return true;
        }
    }
}
