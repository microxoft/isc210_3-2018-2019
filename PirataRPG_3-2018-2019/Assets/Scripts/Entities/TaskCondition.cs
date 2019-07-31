using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities
{
    public enum TaskConditionType
    {
        CloseTo,
        KeyPressed,
        Destroyed,
        Inventoried
    }
    public class TaskCondition
    {
        public TaskConditionType Type { get; set; }
        public string uniqueObjectNameFrom { get; set; }
        public string uniqueObjectNameTo { get; set; }
        public float Quantity { get; set; }
    }
}
