using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Tree;
using Tree.ObjectModel;

namespace TreeDemo.Services
{
    public class TreeServiceClient
    {
        static List<Node> _dbNodes;
        static List<NodeType> _dbNodeTypes;

        static TreeServiceClient()
        {
            Seed();
        }

        static void Seed()
        {
            var node = new Node();
            node.Name("root").Type("ROOT")
                .AddChild().Name("COMP 1").Type("COMP")
                    .AddChild().Name("PROD 1 von COMP 1").Type("PROD")
                        .AddChild().Name("REPORT 1 von PROD 1 von COMP 1").Type("REPORT")
                        .ParentNode
                        .AddChild().Name("REPORT 2 von PROD 1 von COMP 1").Type("REPORT")
                        .ParentNode
                    .ParentNode
                .ParentNode
                .AddChild().Name("COMP 2").Type("COMP")
                .ParentNode
                .AddChild().Name("COMP 3").Type("COMP")
                    .AddChild().Name("PROD 1 von COMP 3").Type("PROD");

            _dbNodes = new List<Node>();
            _dbNodes.Add(node);

            _dbNodeTypes = new List<NodeType>();
            _dbNodeTypes.Add(new NodeType { Id = "ROOT", ChildNodeTypeOptions = "COMP" });
            _dbNodeTypes.Add(new NodeType{Id = "COMP",ChildNodeTypeOptions = "PROD"});
            _dbNodeTypes.Add(new NodeType { Id = "PROD", ChildNodeTypeOptions = "REPORT;CONSULTANT;CATEGORY" });
            _dbNodeTypes.Add(new NodeType { Id = "REPORT", ChildNodeTypeOptions = "ISSUE;BUG" });
            _dbNodeTypes.Add(new NodeType { Id = "CONSULTANT", ChildNodeTypeOptions = null });
            _dbNodeTypes.Add(new NodeType { Id = "CATEGORY", ChildNodeTypeOptions = null });
            _dbNodeTypes.Add(new NodeType { Id = "ISSUE", ChildNodeTypeOptions = null });
            _dbNodeTypes.Add(new NodeType { Id = "BUG", ChildNodeTypeOptions = null });

        }



        public static Node GetRootNode()
        {
            var rootNode = (from Node n in _dbNodes
                            where n.Name == "root"
                            select n).Single();
            return rootNode;
        }

        public static void PutNode(Node node)
        {
            var parentNode = (from Node n in _dbNodes.First().ToList()
                              where n.Id == node.ParentNode.Id
                              select n).Single();
            parentNode.Children.Add(node);
        }

        public static IEnumerable<NodeType> GetChildNodeOptions(INode node)
        {
            var nodeType = (from NodeType nt in _dbNodeTypes
                where nt.Id == node.NodeTypeId
                select nt).SingleOrDefault();
            if (nodeType == null || string.IsNullOrWhiteSpace(nodeType.ChildNodeTypeOptions))
                return new List<NodeType>();

            var options = nodeType.ChildNodeTypeOptions.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries).Select(s=>s.Trim());

            var foundTypes = new List<NodeType>();
            foreach (var o in options)
            {
                var nodeTypeOption = (from NodeType nt in _dbNodeTypes
                                where nt.Id == o
                                select nt).SingleOrDefault();
                if(nodeTypeOption != null)
                    foundTypes.Add(nodeTypeOption);
            }
            return foundTypes;
        }

        public static NodeType GetChildNodeTypeById(string nodeTypeId)
        {
            var nodeTypeOption = (from NodeType nt in _dbNodeTypes
                                  where nt.Id == nodeTypeId
                                  select nt).Single();
            return nodeTypeOption;
        }

        public static IEnumerable<string> GetChildNodeOptions(string nodeTypeId)
        {
            var nodeType = (from NodeType nt in _dbNodeTypes
                            where nt.Id == nodeTypeId
                            select nt).SingleOrDefault();
            if (nodeType == null || string.IsNullOrWhiteSpace(nodeType.ChildNodeTypeOptions))
                return Enumerable.Empty<string>();

            var options = nodeType.ChildNodeTypeOptions.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim());

            
            var foundTypes = new List<string>();
            foreach (var o in options)
            {
                var nodeTypeOption = (from NodeType nt in _dbNodeTypes
                                      where nt.Id == o
                                      select nt.Id).SingleOrDefault();
                if (nodeTypeOption != null)
                    foundTypes.Add(nodeTypeOption);
            }
            return foundTypes;
        }

        public static IEnumerable<ICommand> GetEffectiveCommands(Node node, User user)
        {
            return Enumerable.Empty<ICommand>();
        }

        public class NodeTypeId
        {
            readonly string _nodeTypeIdString;
            private NodeTypeId(string nodeTypeIdString)
            {
                ValidateNodeTypeIdString(nodeTypeIdString);
                _nodeTypeIdString = nodeTypeIdString;
            }

            public string Value
            {
                get { return _nodeTypeIdString; }
            }

            void ValidateNodeTypeIdString(string nodeTypeIdString)
            {
                throw new NotImplementedException();
            }

            public static NodeTypeId Empty()
            {
                return new NodeTypeId("Empty");
            }
        }


        public static List<string> GetPossibleCommandsByNodeTypeId(string nodeTypeId)
        {
            return new List<string>(){"PutChild"};
        }
    }
}
