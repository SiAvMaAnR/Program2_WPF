using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShips
{
    //Типа Корвет
    [Serializable]
    public class Corvette : Ship
    {
        public readonly string type = "Корвет";

        private string armament;//Вооружение
        private string equipment;//Оборудование

        public string Armament
        {
            get { return armament; }
            set { armament = value; }
        }

        public string Equipment
        {
            get { return equipment; }
            set { equipment = value; }
        }

        public Corvette(string name, int maxSpeed, int weight, string armament, string equipment)
        {
            this.Name = name;
            this.MaxSpeed = maxSpeed;
            this.Weight = weight;
            this.Armament = armament;
            this.Equipment = equipment;
        }

        public Corvette() { }

        public override string PrintToString()
        {
            return $"Тип судна: {type} / Наименование: {Name} / Масса(т): {Weight} / Макс. скорость: {MaxSpeed} / Вооружение: {armament} / Оборудование: {equipment}";
        }

        public override bool IsSearchContains(string text)
        {
            text = text.ToUpper();
            if (type.ToUpper().Contains(text)
                || Name.ToUpper().Contains(text)
                || Weight.ToString().Contains(text)
                || MaxSpeed.ToString().Contains(text)
                || Armament.ToUpper().Contains(text)
                || Equipment.ToUpper().Contains(text))
            {
                return true;
            }
            else return false;
        }
    }
}
