using MiniTotalCommander.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiniTotalCommander.ViewModel.FileInfo
{
    public class ListItemBase : BaseViewModel
    {
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string path;

        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                OnPropertyChanged(nameof(Path));
            }
        }

        private ICommand command;

        public ICommand Command
        {
            get { return command; }
            set { command = (RelayCommand)value; OnPropertyChanged(nameof(Command)); }
        }
    }
}
