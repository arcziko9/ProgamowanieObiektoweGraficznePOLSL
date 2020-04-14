using System;

namespace PiłkarzeMVVM.Model
{
    public class Player
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }

        public Player()
        {
        }

        public Player(string firstName, string lastName, int age, double weight)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.Weight = weight;
        }

        public void Copy(Player player)
        {
            FirstName = player.FirstName;
            LastName = player.LastName;
            Age = player.Age;
            Weight = player.Weight;
        }

        public override string ToString()
        {
            return (FirstName + ", " + LastName + ", " + Age + "lat, " + Weight + "kg");
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Player player = obj as Player;
            return (this.Age == player.Age && this.FirstName == player.FirstName && this.LastName == player.LastName
                && this.Weight == player.Weight);
        }
    }
}

