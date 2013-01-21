using System;
using System.Collections.Generic;

namespace LibServiceInfo
{
    public class ServiceFlow
    {

        public Dictionary<string, Block> Blocks;

        public string FirstBlockGUID { get; set; }

        public string Name { get; set; }

        public D3Node RootNode { get; set; }

        public ServiceFlow()
        {
            Blocks = new Dictionary<string, Block>();
        }
        public ServiceFlow(String name)
        {
            Init(name);
        }

        public ServiceFlow(Node rootNode, String name)
        {
            Init(name);
            FirstBlockGUID = rootNode.Children[0].InstanceGUID;
            Dictionary<string, Block> blocks = new Dictionary<string, Block>();
            CreateBlocks(blocks, rootNode);
            Blocks = blocks;
            RootNode = rootNode.d3Node;
        }

        private void Init(String name)
        {
            Blocks = new Dictionary<string, Block>();
            Name = name;
        }

        //Flat Tree
        private void CreateBlocks(Dictionary<string, Block> blocks, Node node)
        {
            Block block = new Block(node);
            if (node.Children.Count > 0)
            {
                //foreach (Node child in node.Children)
                //{
                //    switch (node.GetType().Name)
                //    {
                //    case "ServiceNode":
                //    case "ConditionNode":
                //        block.AddChild(child.Name,new Block(child));
                //        break;
                //    case "ConditionValueNode":
                //    case "SIPResponseNode":
                //        block.AddChild(child.InstanceGUID, new Block(child));
                //        break;
                //    default:
                //        Console.WriteLine("Unkown node type" + child.GetType().Name);
                //        break;
                //    }
                //}
                if (node.Name != "Start")
                {
                    blocks.Add(block.InstanceGUID, block);
                }
                 //Double loop to maintain tree order
                foreach (Node child in node.Children)
                {
                    CreateBlocks(blocks, child);
                }
            }
        }

        private void CreateBlocks(Node node)
        {
            Blocks = new Dictionary<string, Block>();
            CreateBlocks(Blocks,node);
        }
    }
}