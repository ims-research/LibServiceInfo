#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace LibServiceInfo
{
    public class SIPResponseNode : Node
    {
        public SIPResponseNode(D3Node child) : base(child)
        {
            Values = new List<string>();
            Values = child.value.Split().ToList();
        }

        public SIPResponseNode()
        {
            Values = new List<string>();
        }

        public List<string> Values { get; set; }
    }
}