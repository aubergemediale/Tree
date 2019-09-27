using System;

namespace Tree.ObjectModel
{
    public sealed class NodePropertyValue
    {
        public Guid NodeId { get; set; }

        public string NodePropertyId { get; set; }

        public object Value { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public DateTime Modified { get; set; }

        public string ModifiedBy { get; set; }
    }
}