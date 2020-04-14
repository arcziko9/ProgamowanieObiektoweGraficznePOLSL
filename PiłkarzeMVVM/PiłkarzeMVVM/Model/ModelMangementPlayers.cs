using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace PiłkarzeMVVM.Model
{
    public class ModelMangementPlayers
    {
        public List<Player> playerList = new List<Player>();

        public void LoadPlayersFromJsonFile()
        {
            string json = File.ReadAllText(@"D:\Politechnika\Semestr4\ProgamowanieObiektoweGraficznePOLSL\PiłkarzeMVVM\PiłkarzeMVVM\json1.json");
            playerList = JsonConvert.DeserializeObject<List<Player>>(json);
        }
        public void AddPlayer(string firstName, string lastName, int age, double weight)
        {
            //playerList.Add(new Player(firstName, lastName, age, weight));
            UpdateFile();
        }
        public void RemovePlayer(Player player)
        {
            playerList.Remove(player);
            UpdateFile();
        }
        public void ModifyPlayer(Player newPlayer, Player oldPlayer)
        {
            playerList.Remove(oldPlayer);
            playerList.Add(newPlayer);
            UpdateFile();
        }
        private void UpdateFile()
        {
            File.WriteAllText(@"json1.json", JsonConvert.SerializeObject(playerList));
        }
    }
}
