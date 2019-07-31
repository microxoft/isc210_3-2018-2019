using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities
{
    public class Game
    {
        public string PlayerName { get; set; }
        public Level CurrentLevel { get; set; }
        public List<InventoryItem> Inventory { get; set; }
        private static Game _instance;

        public static Game Instance()
        {
            if (_instance == null)
            {
                _instance = new Game();
                _instance.CurrentLevel = new Level();
                _instance.CurrentLevel.Entities = new List<GameEntity>();
                _instance.CurrentLevel.Missions = new List<Mission>();
            }
            return _instance;
        }

    }
}
