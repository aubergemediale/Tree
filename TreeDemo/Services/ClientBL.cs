using Microsoft.Practices.Prism.Commands;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Tree;
using Tree.ObjectModel;

namespace TreeDemo.Services
{
    public class ClientBL
    {
        public static void AddEffectiveBehavior(NodeVm nodeVm, User user, Dictionary<string, ICommand> implementedCommands)
        {
            foreach (var node in nodeVm.ToList())
            {
                List<string> allpossibleCommands = TreeServiceClient.GetPossibleCommandsByNodeTypeId(node.NodeTypeId);

                foreach (var possibleCommand in allpossibleCommands)
                {
                    if (IsCommandAllowedForUserAndNode(possibleCommand, user, node))
                    {
                        if (IsCommandImplemented(possibleCommand, implementedCommands))
                        {
                            AddCommandImplementationToNode(implementedCommands, possibleCommand, node);
                        }
                        else
                        {
                            AddNotImplementedCommandToNode(implementedCommands, possibleCommand, node);
                        }
                    }
                }
            }
        }

        static void AddNotImplementedCommandToNode(Dictionary<string, ICommand> implementedCommands, string possibleCommand, NodeVm node)
        {
            if (node.NodeBehavior == null)
                node.NodeBehavior = new NodeBehavior(node);

            node.NodeBehavior.AddCommand(possibleCommand, NotImplementedCommand);
        }

        static void AddCommandImplementationToNode(IReadOnlyDictionary<string, ICommand> implementedCommands, string possibleCommand, NodeVm node)
        {
            
            if(node.NodeBehavior == null)
                node.NodeBehavior = new NodeBehavior(node);

            node.NodeBehavior.AddCommand(possibleCommand, implementedCommands[possibleCommand]);
            
        }

        static bool IsCommandImplemented(string possibleCommand, Dictionary<string, ICommand> implementedCommands)
        {
            return implementedCommands.ContainsKey(possibleCommand);
        }

        static bool IsCommandAllowedForUserAndNode(string possibleCommand, User user, INode node)
        {
            return true;
        }

        static readonly ICommand NotImplementedCommand = new DelegateCommand<string>(
            (s) => MessageBox.Show(string.Format("The Possible Command {0} is not Implemented here.",s)),
            (s) =>true
        );
    }
}
