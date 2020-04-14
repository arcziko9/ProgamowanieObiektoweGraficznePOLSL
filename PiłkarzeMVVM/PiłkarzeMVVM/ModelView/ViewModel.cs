using PiłkarzeMVVM.ModelView.BaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PiłkarzeMVVM.ModelView
{
    class ViewModel : ViewModelBase
    {
        private Player _player;
        private ObservableCollection<Player> _players;
        private ICommand _AddCommand;
        private ICommand _ModifyCommand;
        private ICommand _DeleteCommand;

        public Player Player
        {
            get { return _player; }
            set { _player = value;
                onPropertyChanged("Player");
                }    
        }

        public ObservableCollection<Player> Players
        {
            get { return _players; }
            set { _players = value;
                onPropertyChanged("Player");
            }
        }

        public ICommand AddCommand
        {
            get
            {
                if(_AddCommand == null)
                {
                    _AddCommand = new RelayCommand(param => this.Add(), null);
                }
                return _AddCommand;
            }
        }

        public ViewModel()
        {
            Player = new Player();
            Players = new ObservableCollection<Player>();
            Players.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Players_CollectionChanged);

        }
        void Players_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            onPropertyChanged("Players");
        }

        private void Add()
        {
            Players.Add(Player);
            Player = new Player();
        }
    }
}
