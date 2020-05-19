using MiniTotalCommander.Model.Commands;
using MiniTotalCommander.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiniTotalCommander.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        #region Properties

        internal static readonly Model.FileOperationService fileOperationService = new Model.FileOperationService();
        internal static readonly Model.FileTreeService fileTreeService = new Model.FileTreeService();

        private TCPanelViewModel leftPanel;
        private TCPanelViewModel rightPanel;

        public TCPanelViewModel LeftPanel
        {
            get { return leftPanel; }
        }

        private TCPanelViewModel activePanel;

        internal TCPanelViewModel ActivePanel
        {
            get => activePanel; set
            {
                activePanel = value;
                OnPropertyChanged(nameof(ActivePanel));
                if (leftPanel == activePanel)
                {
                    NotActivePanel = rightPanel;
                }
                if (rightPanel == activePanel)
                {
                    NotActivePanel = leftPanel;
                }
            }
        }

        private TCPanelViewModel notActivePanel;

        internal TCPanelViewModel NotActivePanel
        {
            get => notActivePanel; set
            {
                notActivePanel = value;
                OnPropertyChanged(nameof(NotActivePanel));
            }
        }

        public TCPanelViewModel RightPanel
        {
            get { return rightPanel; }
        }

        private ICommand copy;

        public ICommand Copy
        {
            get
            {
                if (copy == null)
                {
                    copy = new RelayCommand(
                        execute =>
                        {
                            fileOperationService.ExecuteOperationByName("Copy",
                            ActivePanel.SelectedItem.Path, NotActivePanel.Path);
                            LeftPanel.RefreshPanel();
                            RightPanel.RefreshPanel();
                        },
                        canExecute =>
                        {
                            if (ActivePanel.SelectedItem is null) return false;
                            return fileOperationService.canExecuteOperationByName("Copy",
                                ActivePanel.SelectedItem.Path, NotActivePanel.Path);
                        });
                }
                return copy;
            }
            set => copy = value;
        }

        #endregion Properties

        #region Constructor

        public MainViewModel()
        {
            fileOperationService.AddCommand(new Copy());
            leftPanel = new TCPanelViewModel(fileTreeService, this);
            rightPanel = new TCPanelViewModel(fileTreeService, this);
            ActivePanel = leftPanel;
            NotActivePanel = rightPanel;
        }

        #endregion Constructor
    }
}
