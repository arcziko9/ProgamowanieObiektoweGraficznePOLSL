using Newtonsoft.Json;
using PiłkarzeMVVM.Model;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;

namespace PiłkarzeMVVM.ViewModel
{
    class Management : ViewModelBase
    {
        private string dataPath = @"C:\Users\Arkadiusz\source\repos\PiłkarzeMVVM\PiłkarzeMVVM\json2.json";
        private string firstName = null;
        private string lastName = null;
        private int? age = 16;
        private double? weight = 50;
        private Player selectedPlayer = null;
        private BindingList<Player> players = new BindingList<Player>();
        private int[] ageItems = new int[50];


        #region Properties
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                onPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                onPropertyChanged(nameof(LastName));
            }
        }

        public int? Age
        {
            get => age;
            set
            {
                age = value;
                onPropertyChanged(nameof(Age));
            }
        }

        public double? Weight
        {
            get => weight;
            set
            {
                weight = value;
                onPropertyChanged(nameof(Weight));
            }
        }

        public Player SelectedPlayer
        {
            get => selectedPlayer;
            set
            {
                selectedPlayer = value;
                onPropertyChanged(nameof(SelectedPlayer));
                if (ChangePlayer.CanExecute(null)) ChangePlayer.Execute(null);
            }
        }

        public int[] AgeItems
        {
            get => ageItems;
            set
            {
                ageItems = value;
                onPropertyChanged(nameof(AgeItems));
            }
        }

        public BindingList<Player> Players
        {
            get => players;
            set
            {
                players = value;
                onPropertyChanged(nameof(Players));
            }
        }
        #endregion

        #region Commands
        private bool FieldsNotNull { get { return (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && Age > 0 && Weight > 0); } }

        private ICommand addPlayer;
        private ICommand deletePlayer;
        private ICommand modifyPlayer;
        private ICommand clear;
        private ICommand changePlayer;
        private ICommand loadData;
        private ICommand saveData;
        private ICommand loadAgeItems;

        public ICommand LoadAgeItems
        {
            get
            {
                if (loadAgeItems is null)
                {
                    

                    loadData = new RelayCommand(execute =>
                    {
                        for (int i = 0; i < 50; i++)
                        {
                            ageItems[i] = 16 + i;
                        };
                        onPropertyChanged(nameof(LoadAgeItems));
                    }, canExecute => true);
                }
                return loadData;
            }
        }

        public ICommand LoadData
        {
            get
            {
                if (loadData is null)
                {
                    loadData = new RelayCommand(execute =>
                    {
                        var jsonPlayers = File.ReadAllText(dataPath);
                        Players = JsonConvert.DeserializeObject<BindingList<Player>>(jsonPlayers);
                        onPropertyChanged(nameof(LoadData));
                        Players.ResetBindings();
                    }, canExecute => File.Exists(dataPath) && (new FileInfo(dataPath).Length > 0));
                }
                return loadData;
            }
        }
        public ICommand SaveData
        {
            get
            {
                if (saveData is null)
                {
                    saveData = new RelayCommand(execute =>
                    {
                        var jsonPlayers = JsonConvert.SerializeObject(Players);
                        File.WriteAllText(dataPath, jsonPlayers);
                        onPropertyChanged(nameof(SaveData));
                    }, canExecute => true);
                }
                return saveData;
            }
        }
        public ICommand AddPlayer
        {
            get
            {
                if (addPlayer is null)
                {
                    addPlayer = new RelayCommand(execute =>
                        {
                            var player = new Player(FirstName, LastName, (int)Age, (double)Weight);
                            if (!Players.Contains(player))
                            {
                                Players.Add(player);
                                onPropertyChanged(nameof(Players));
                            }
                        }, canExecute => FieldsNotNull);
                }
                return addPlayer;
            }
        }
        public ICommand Clear
        {
            get
            {
                if (clear is null)
                {
                    clear = new RelayCommand(
                        execute =>
                        {
                            FirstName = null;
                            LastName = null;
                        }, canExecute => true);
                }
                return clear;
            }
        }
        public ICommand ChangePlayer
        {
            get
            {
                if (changePlayer is null)
                {
                    changePlayer = new RelayCommand(
                        execute =>
                        {
                            FirstName = SelectedPlayer.FirstName;
                            LastName = SelectedPlayer.LastName;
                            Age = SelectedPlayer.Age;
                            Weight = SelectedPlayer.Weight;
                        }, canExecute => SelectedPlayer != null);
                }
                return changePlayer;
            }
        }
        public ICommand DeletePlayer
        {
            get
            {
                if (deletePlayer == null)
                {
                    deletePlayer = new RelayCommand(execute =>
                    {
                        var player = new Player(FirstName, LastName, (int)Age, (double)Weight);
                        if (Players.Contains(player))
                        {
                            Players.Remove(player);
                            onPropertyChanged(nameof(Players));
                        }
                    }, canExecute => FieldsNotNull && SelectedPlayer != null
                    );
                }
                return deletePlayer;
            }
        }
        public ICommand ModifyPlayer
        {
            get
            {
                if (modifyPlayer is null)
                {
                    modifyPlayer = new RelayCommand(execute =>
                    {
                        var player = new Player(FirstName, LastName, (int)Age, (double)Weight);
                        if (Players.Contains(SelectedPlayer))
                        {
                            var index = players.IndexOf(selectedPlayer);
                            Players[index].Copy(player);
                            Players.ResetItem(index);
                        }
                    }, canExecute => FieldsNotNull && SelectedPlayer != null);
                }
                return modifyPlayer;
            }
        }
        #endregion
        public Management()
        {
        }
    }
}

