using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities
{
    public class Level
    {
        public List<List<char>> Map { get; set; }
        public List<GameEntity> Entities { get; set; }
        public List<Mission> Missions { get; set; }
    }
}
