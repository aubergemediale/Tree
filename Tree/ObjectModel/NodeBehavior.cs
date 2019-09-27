using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Tree.ObjectModel
{
    public sealed class NodeBehavior : INodeBehavior
    {
        readonly Dictionary<string, ICommand> _commands = new Dictionary<string, ICommand>();
        readonly INode _node;

        public NodeBehavior(INode node)
        {
            _node = node;
           
        }

        public bool CanExecute(object commandParameter)
        {
            var commandName = commandParameter as string;
            if (commandName == null)
                throw new ArgumentException("You must specify a command name string as CommandParameter",
                    "commandParameter");

            return _commands.ContainsKey(commandName) && _commands[commandName].CanExecute(_node);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object commandParameter)
        {
            var commandName = commandParameter as string;
            if (commandName == null)
                throw new ArgumentException("You must specify a command name string as CommandParameter",
                    "commandParameter");

            _commands[commandName].Execute(_node);
        }

        public void AddCommand(string commandName, ICommand command)
        {
            if(commandName == null)
                throw new ArgumentNullException("commandName");
            if(command == null)
                throw new ArgumentNullException("command");

            _commands.Add(commandName, command);
        }
    }
}