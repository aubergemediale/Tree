using System;

namespace Tree.ObjectModel
{
    public sealed class Property
    {
        public Guid Id { get; set; }

        public string DataTypeId { get; set; }

        public bool IsRevisionSafe { get; set; }
    }
}