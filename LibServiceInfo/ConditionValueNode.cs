#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace LibServiceInfo
{
    public class ConditionValueNode : Node
    {
        public ConditionValueNode(D3Node child) : base(child)
        {
            Values = new List<string>();
            Values = child.name.Split().ToList();
        }

        public ConditionValueNode()
        {
            Values = new List<string>();
        }

        public List<string> Values { get; set; }
    }
}