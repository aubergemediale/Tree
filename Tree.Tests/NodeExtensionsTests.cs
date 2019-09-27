using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Windows.Controls;
using Tree.ObjectModel;

namespace Tree.Tests
{
    [TestClass]
    public class NodeExtensionsTests
    {
        static Node Node { get; set; }

        [ClassInitialize]
        public static void Arrange(TestContext context)
        {
            Node = Seed.GetSampleTree();
        }

        [TestMethod]
        public void ToList()
        {
            var list = Node.ToList();
            Assert.IsTrue(list.Count == 8);
        }

        [TestMethod]
        public void Print()
        {
            var text = Node.Print();
            Assert.IsTrue(text.Contains("root"));
            Assert.IsTrue(text.Contains("8 "));
            Console.WriteLine(text);
        }

        [TestMethod]
        public void ToTreeViewItems()
        {
            var tvi = Node.ToTreeViewItem();
            var tviList = new List<TreeViewItem>();
            tviList.Add(tvi);
            var tv = new TreeView();
            tv.ItemsSource = tviList;
            var count = tv.Items.Count;
            Assert.IsTrue(count == 1,"Should count only the one root node.");

        }
    }
}
