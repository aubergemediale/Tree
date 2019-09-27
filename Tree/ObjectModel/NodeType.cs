using System.Collections.Generic;

namespace Tree.ObjectModel
{
    public sealed class NodeType
    {
        public string Id { get; set; }

        public string ChildNodeTypeOptions { get; set; }

        public List<Property> PropertySet { get; set; }

        public NodeBehavior NodeBehavior { get; set; }
    }
}