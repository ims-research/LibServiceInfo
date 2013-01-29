#region

using System;
using System.Collections.Generic;

#endregion

namespace LibServiceInfo
{
    public class ServiceFlow
    {
        public Dictionary<string, Block> Blocks;

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

        public string FirstBlockGUID { get; set; }

        public string Name { get; set; }

        public D3Node RootNode { get; set; }

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
                if (node.Name != "Start")
                {
                    blocks.Add(block.InstanceGUID, block);
                }
                foreach (Node child in node.Children)
                {
                    CreateBlocks(blocks, child);
                }
            }
        }

        private void CreateBlocks(Node node)
        {
            Blocks = new Dictionary<string, Block>();
            CreateBlocks(Blocks, node);
        }
    }
}