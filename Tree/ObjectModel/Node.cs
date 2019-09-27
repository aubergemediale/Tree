using System;
using System.Collections.Generic;

namespace Tree.ObjectModel
{
    public sealed class Node : INode
    {
        public Node()
        {
            Id = Guid.NewGuid();
            Children = new List<INode>();
        }

        public Guid Id { get; set; }

        public string NodeTypeId { get; set; }

        public string Name { get; set; }

        public INode ParentNode { get; set; }

        public List<INode> Children { get; set; }

    }
}