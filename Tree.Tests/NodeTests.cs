using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tree.ObjectModel;

namespace Tree.Tests
{
    [TestClass]
    public class NodeTests
    {
        [TestMethod]
        public void NodeInit()
        {
            var node = new Node();

            Assert.IsTrue(node.Id != Guid.Empty);
            Assert.IsTrue(node.Id != default(Guid));

            Assert.IsNotNull(node.Children);
        }
    }
}
