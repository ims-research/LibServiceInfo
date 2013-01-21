using System;
using System.Collections.Generic;

namespace LibServiceInfo
{
    public class Block
    {
        public string Name { get; set; }
        public string GlobalGUID { get; set; }
        public string InstanceGUID { get; set; }
        public string DestURI { get; set; }
        public string ConditionType { get; set; }

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
                    DestURI = ServiceManager.ServiceList[node.GlobalGUID].ServiceConfig["Server_URI"];
                    break;
                case "ConditionNode":
                    BlockType = BlockTypes.Condition;
                    ConditionType = ServiceManager.ConditionList[node.GlobalGUID].Type;
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
            if (node.Name != "Start")
            {
                FillBlocks(node, this);
            }
        }

        //Pruned section of tree starting at node
        private void FillBlocks(Node node, Block block)
        {
            if (node == null) return;
            if (node.Children.Count > 0)
            {
                foreach (Node child in node.Children)
                {
                    switch (node.GetType().Name)
                    {
                        case "ServiceNode":
                        case "ConditionNode":
                           block.AddChild(child.Name, new Block(child));
                            break;
                        case "ConditionValueNode":
                        case "SIPResponseNode":
                            block.AddChild(child.InstanceGUID, new Block(child));
                            break;
                        default:
                            Console.WriteLine("Unkown node type" + child.GetType().Name);
                            break;
                    }
                    //FillBlocks(child, block);
                }
            }
        }
    }
}