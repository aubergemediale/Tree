using System;
using System.Collections.Generic;
using Tree.ObjectModel;

namespace Tree
{
    public sealed class NodeVm : INode
    {
        public NodeVm(INode node)
        {
            Id = node.Id;
            Name = node.Name;
            NodeTypeId = node.NodeTypeId;
            Children = new List<INode>();
        }

        public Guid Id { get; set; }

        public string NodeTypeId { get; set; }

        public string Name { get; set; }

        public INode ParentNode { get; set; }

        public List<INode> Children { get; set; }

        public NodeBehavior NodeBehavior { get; set; }
    }
}
