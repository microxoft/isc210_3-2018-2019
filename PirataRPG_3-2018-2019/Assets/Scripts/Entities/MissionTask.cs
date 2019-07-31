using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities
{
    public class MissionTask
    {
        public string id { get; set; }
        public string description { get; set; }
        public string prerequisites { get; set; }
        public List<TaskCondition> Conditions { get; set; }
        public List<TaskAction> Actions { get; set; }
    }
}
