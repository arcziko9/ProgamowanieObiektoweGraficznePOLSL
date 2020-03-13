using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piłkarze
{
    class Player
    {
        public string firstName {get; private set; }
        public string lastName { get; private set; }
        public int age { get; private set; }
        public double weight { get; private set; }


        public Player(string firstName, string lastName, int age, double weight)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
            this.weight = weight;
        }

        public Player(Player p)
        {
            this.firstName = p.firstName;
            this.lastName = p.lastName;
            this.age = p.age;
            this.weight = p.weight;
        }


        public override string ToString()
        {
            return (firstName + ", " + lastName + ", " + age + "lat, " + weight + "kg");
        }
    }
}
