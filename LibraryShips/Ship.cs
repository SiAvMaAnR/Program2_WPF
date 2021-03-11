using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LibraryShips
{
    //Типа кораблик
    [Serializable]
    [XmlInclude(typeof(Steamer))]
    [XmlInclude(typeof(Corvette))]
    [XmlInclude(typeof(Sailboat))]
    public abstract class Ship
    {
        private string name;
        private int weight;
        private int maxSpeed;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public int MaxSpeed
        {
            get { return maxSpeed; }
            set { maxSpeed = value; }
        }

        public abstract string PrintToString();//Вывод информации о корабле

        public abstract bool IsSearchContains(string text);
    }
}
