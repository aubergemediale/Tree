using System;
using System.Collections.Generic;

namespace Tree
{
    public interface INode
    {
        Guid Id { get; set; }
        string NodeTypeId { get; set; }
        string Name { get; set; }
        INode ParentNode { get; set; }
        List<INode> Children { get; set; }
    }
}