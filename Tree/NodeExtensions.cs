using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using Tree.ObjectModel;

namespace Tree
{
    public static class NodeExtensions
    {
        public static Node Name(this Node node, string name)
        {
            node.Name = name;
            return node;
        }

        public static Node Type(this Node node, string nodeTypeId)
        {
            node.NodeTypeId = nodeTypeId;
            return node;
        }


        public static Node AddChild(this INode node)
        {
            var child = new Node { ParentNode = node };
            node.Children.Add(child);
            return child;
        }

        public static List<T> ToList<T>(this T node) where T : INode
        {
            var all = new List<T> { node };
            all = AddChildrenAndSelf(all, node.Children);
            return all;
        }

        static List<T> AddChildrenAndSelf<T>(List<T> all, IEnumerable<INode> childeren) where T : INode
        {
            foreach (var n in childeren)
            {
                all.Add((T)n);
                AddChildrenAndSelf(all, n.Children);
            }
            return all;
        }

        public static TreeViewItem ToTreeViewItem(this INode node)
        {
            var container = new TreeViewItem { Header = node };
            var items = container.Items;

            AddNextTreeViewItem(items, node.Children);

            return container;
        }

        static void AddNextTreeViewItem(IList items, IEnumerable<INode> childeren)
        {
            foreach (var node in childeren)
            {
                var nextItem = new TreeViewItem { Header = node };
                items.Add(nextItem);
                AddNextTreeViewItem(nextItem.Items, node.Children);
            }
        }

        public static NodeVm ToNodeVm(this Node parent)
        {
            // self
            var self = new NodeVm(parent);

            // top
            self.ParentNode = parent.ParentNode;

            // children
            VisitNextNodeVm(self, parent.Children);

            return self;
        }

        static void VisitNextNodeVm(NodeVm parentNode, IEnumerable<INode> childeren)
        {
            foreach (var child in childeren)
            {
                var self = new NodeVm(child) { ParentNode = parentNode };
                parentNode.Children.Add(self);

                VisitNextNodeVm(self, child.Children);
            }
        }

        public static Node BackToNode(this NodeVm parent)
        {
            // self
            var self = new Node(){Id = parent.Id,Name = parent.Name,NodeTypeId = parent.NodeTypeId};

            // top
            self.ParentNode = parent.ParentNode;

            // children
            VisitNextNodeVm(self, parent.Children);

            return self;
        }

        static void VisitNextNodeVm(Node parentNode, IEnumerable<INode> childeren)
        {
            foreach (var child in childeren)
            {
                var self = new Node() { ParentNode = parentNode, Id = parentNode.Id, Name = parentNode.Name, NodeTypeId = parentNode.NodeTypeId };
                parentNode.Children.Add(self);

                VisitNextNodeVm(self, child.Children);
            }
        }


        public static string Print(this INode node)
        {
            var list = node.ToList();
            var sb = new StringBuilder();
            var index = 1;
            foreach (var n in list)
            {
                sb.AppendLine(string.Format("{0} {1}", index, n.Name));
                index++;
            }
            return sb.ToString();
        }



    }
}
