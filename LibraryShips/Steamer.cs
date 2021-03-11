using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShips
{
    //Типа Пароходик
    [Serializable]
    public class Steamer : Ship
    {
        public readonly string type = "Пароход";

        private int massOfCoal;//Масса угля
        private int rangeOfTravel;//Дальность хода

        public int MassOfCoal
        {
            get { return massOfCoal; }
            set { massOfCoal = value; }
        }

        public int RangeOfTravel
        {
            get { return rangeOfTravel; }
            set { rangeOfTravel = value; }
        }

        public Steamer(string name, int maxSpeed, int weight, int massOfCoal, int rangeOfTravel)
        {
            this.Name = name;
            this.MaxSpeed = maxSpeed;
            this.Weight = weight;
            this.MassOfCoal = massOfCoal;
            this.RangeOfTravel = rangeOfTravel;
        }

        public Steamer() { }

        public override string PrintToString()
        {
            return $"Тип судна: {type} / Наименование: {Name} / Масса(т): {Weight} / Макс. скорость: {MaxSpeed} / Масса угля: {massOfCoal} / Дальность хода: {rangeOfTravel}";
        }

        public override bool IsSearchContains(string text)
        {
            text = text.ToUpper();
            if (type.ToUpper().Contains(text)
                || Name.ToUpper().Contains(text)
                || Weight.ToString().Contains(text)
                || MaxSpeed.ToString().Contains(text)
                || MassOfCoal.ToString().Contains(text)
                || RangeOfTravel.ToString().Contains(text))
            {
                return true;
            }
            else return false;
        }
    }
}
