using MiniTotalCommander.ViewModel.Base;
using MiniTotalCommander.ViewModel.FileInfo;
using MiniTotalCommander.ViewModel.FileTreeCommands;
using MiniTotalCommander.ViewModel.ListItems;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTotalCommander.ViewModel
{
    using R = Properties.Resources;

    internal class TCPanelViewModel : BaseViewModel
    {
        internal readonly Model.FileTreeService fileTreeService;
        internal readonly MainViewModel main;

        #region Properties

        private string path;
        private string selectedDrive;
        private BindingList<String> drives;
        private BindingList<ListItemBase> contents;
        private ListItemBase selectedItem;
        private String pathLabel;
        private String driveLabel;

        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                OnPropertyChanged(nameof(Path));
            }
        }

        public string SelectedDrive
        {
            get { return selectedDrive; }
            set
            {
                selectedDrive = value;
                OnPropertyChanged(nameof(SelectedDrive));
            }
        }

        public BindingList<String> Drives
        {
            get { return drives; }
            set
            {
                drives = value;
                OnPropertyChanged(nameof(Drives));
            }
        }

        public BindingList<ListItemBase> Contents
        {
            get { return contents; }
            set
            {
                contents = value;
                OnPropertyChanged(nameof(Contents));
            }
        }

        public ListItemBase SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        #region Labels

        public String PathLabel
        {
            get { return pathLabel; }
            set
            {
                pathLabel = value;
            }
        }

        public String DriveLabel
        {
            get { return driveLabel; }
            set
            {
                driveLabel = value;
            }
        }

        #endregion Labels

        #endregion Properties

        #region Constructor

        internal TCPanelViewModel(Model.FileTreeService treeService, MainViewModel mainViewModel)
        {
            main = mainViewModel;
            fileTreeService = treeService;
            DriveLabel = R.DriveLabel;
            PathLabel = R.PathLabel;
            Drives = new BindingList<string>(fileTreeService.GetDrives());
            Path = Drives[0];
            RefreshPanel();
        }

        #endregion Constructor

        #region Methods

        public void switchActivePanel(object sender, System.EventArgs e)
        {
            main.ActivePanel = this;
        }

        public void FileEnter(object sender, System.EventArgs e)
        {
            if (SelectedItem != null)
            {
                SelectedItem.Command.Execute(this);
            }
        }

        public void PathEnterPress(object sender, System.EventArgs e)
        {
            if (!Directory.Exists(Path)) return;
            RefreshPanel();
        }

        public void DriveChanged(object sender, System.EventArgs e)
        {
            Path = SelectedDrive;
            RefreshPanel();
        }

        public void RefreshPanel()
        {
            SelectedDrive = Path.Substring(0, Path.IndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
            Contents = BuildFileTree();
            SelectedItem = Contents[0];
            SelectedItem = null;
        }

        public BindingList<ListItemBase> BuildFileTree()
        {
            var fileTree = new BindingList<ListItemBase>();
            if (SelectedDrive != Path)
            {
                var parentdir = fileTreeService.GetParentDir(this.Path);
                fileTree.Add(new ParentDirItem
                {
                    Name = R.ParentDir,
                    Path = parentdir,
                    Command = OpenDir.Open
                });
            }
            foreach (var dir in fileTreeService.GetDirectories(Path))
            {
                fileTree.Add(new DirItem
                {
                    Name = dir,
                    Path = Path + System.IO.Path.DirectorySeparatorChar + dir,
                    Command = OpenDir.Open
                });
            }
            foreach (var dir in fileTreeService.GetFiles(Path))
            {
                fileTree.Add(new FileItem
                {
                    Name = dir,
                    Path = Path + System.IO.Path.DirectorySeparatorChar + dir,
                    Command = RunFile.Run
                });
            }
            return fileTree;
        }

        #endregion Methods
    }
}
