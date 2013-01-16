using System;
using System.Collections.Generic;

namespace LibServiceInfo
{
    public class Block
    {
        public string Name { get; set; }
        public string GlobalGUID { get; set; }
        public string InstanceGUID { get; set; }

        public Dictionary<string, Block> NextBlocks { get; set; }
        public Block ParentBlock { get; set; }
        public BlockTypes BlockType { get; set; }


        public enum BlockTypes
        {
            Service,
            Condition,
            ConditionOption,
            SIPResponse
        }

        public Block()
        {
            NextBlocks = new Dictionary<string, Block>();
        }

        public void AddChild(string key, Block block)
        {
            NextBlocks.Add(key, block);
        }

        public Block(Node node)
        {
            NextBlocks = new Dictionary<string, Block>();
            Name = node.Name;
            GlobalGUID = node.GlobalGUID;
            InstanceGUID = node.InstanceGUID;
            switch (node.GetType().Name)
            {
                case "ServiceNode":
                    BlockType = BlockTypes.Service;
                    break;
                case "ConditionNode":
                    BlockType = BlockTypes.Condition;
                    break;
                case "ConditionValueNode":
                    BlockType = BlockTypes.ConditionOption;
                    break;
                case "SIPResponseNode":
                    BlockType = BlockTypes.SIPResponse;
                    break;
                default:
                    Console.WriteLine("Unkown node type" + node.GetType().Name);
                    break;
            }
        }
    }
}