using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Tree;
using Tree.ObjectModel;
using TreeDemo.Services;

namespace TreeDemo
{
    class MainWindowViewModel : BindableBase
    {
        readonly Dictionary<string, ICommand> _implementedCommands;

        public MainWindowViewModel()
        {
            _implementedCommands = new Dictionary<string, ICommand>();
            _implementedCommands.Add("PutChild", PutChildCommand);

            TreeViewItems = new ObservableCollection<TreeViewItem>();
            Load();
        }

        void Load()
        {
            
            // load node graph from db
            Node rootNode = TreeServiceClient.GetRootNode();

            // convert node objects to vm objects
            NodeVm rootNodeVm = rootNode.ToNodeVm();
           
            // add effective behaviour
            ClientBL.AddEffectiveBehavior(rootNodeVm, null,_implementedCommands);
           
            // WPF-specific display logic
            Display(rootNodeVm);
        }


        void Display(NodeVm rootNodeVm)
        {
            var tvis = rootNodeVm.ToTreeViewItem();

            TreeViewItems.Clear();
            TreeViewItems.Add(tvis);
        }

       



        public ObservableCollection<TreeViewItem> TreeViewItems { get; set; }

        DelegateCommand<NodeVm> PutChildCommand
        {
            get
            {
                if (_putChildNodeCommand != null)
                    return _putChildNodeCommand;

                _putChildNodeCommand = new DelegateCommand<NodeVm>((node) =>
                {
                    var newChildNode = new NodeVm(new Node());
                    newChildNode.Name = "New Child of rootNodeVm " + node.Name;
                    newChildNode.ParentNode = node;
                    _putChildNodeCommand_Node = newChildNode;

                    ChildNodeTypes.Clear();
                    ChildNodeTypes.AddRange(TreeServiceClient.GetChildNodeOptions(node));
                    ChildNodeOptionsVisibility = Visibility.Visible;
                   
                  
                }, 
                (node) => true);
                return _putChildNodeCommand;
            }
        }
        DelegateCommand<NodeVm> _putChildNodeCommand;
        NodeVm _putChildNodeCommand_Node;


        public DelegateCommand PutChildCommand2
        {
            get
            {
                if (_putChildNodeCommand2 != null)
                    return _putChildNodeCommand2;
                _putChildNodeCommand2 = new DelegateCommand(() =>
                {
                    ChildNodeOptionsVisibility = Visibility.Collapsed;
                    _putChildNodeCommand_Node.NodeTypeId = _selectedChildNodeType.Id;
                    TreeServiceClient.PutNode(_putChildNodeCommand_Node.BackToNode());
                    Load();
                },
                () => _selectedChildNodeType != null);
                return _putChildNodeCommand2;
            }
        }
        DelegateCommand _putChildNodeCommand2;


        public Visibility ChildNodeOptionsVisibility
        {
            get { return _childNodeOptionsVisibility; }
            set
            {
                _childNodeOptionsVisibility = value;
                OnPropertyChanged(()=>ChildNodeOptionsVisibility);
            }
        }
        Visibility _childNodeOptionsVisibility = Visibility.Collapsed;

        public NodeType SelectedChildNodeType
        {
            get { return _selectedChildNodeType; }
            set
            {
                _selectedChildNodeType = value;
                OnPropertyChanged(()=>SelectedChildNodeType);
               _putChildNodeCommand2.RaiseCanExecuteChanged();
            }
        }
        NodeType _selectedChildNodeType;

        public ObservableCollection<NodeType> ChildNodeTypes
        {
            get { return _childNodeTypes; }
            set
            {
                _childNodeTypes = value;
                OnPropertyChanged(()=>ChildNodeTypes);
            }
        }
        ObservableCollection<NodeType> _childNodeTypes = new ObservableCollection<NodeType>();

    }
}
