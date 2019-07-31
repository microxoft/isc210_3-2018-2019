using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities
{
    public class Mission
    {
        public string id { get; set; }
        public string description { get; set; }
        public string prerequisites { get; set; }
        public List<MissionTask> Tasks { get; set; }
    }
}
