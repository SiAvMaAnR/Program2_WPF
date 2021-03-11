using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShips
{
    //Типа Парусник
    [Serializable]
    public class Sailboat : Ship
    {
        public readonly string type = "Парусник";

        private string sailMaterial;//Материал паруса
        private int sailArea;//Площадь паруса

        public string SailMaterial
        {
            get { return sailMaterial; }
            set { sailMaterial = value; }
        }

        public int SailArea
        {
            get { return sailArea; }
            set { sailArea = value; }
        }

        public Sailboat(string name, int maxSpeed, int weight, string sailMaterial, int sailArea)
        {
            this.Name = name;
            this.MaxSpeed = maxSpeed;
            this.Weight = weight;
            this.SailMaterial = sailMaterial;
            this.SailArea = sailArea;
        }

        public Sailboat() { }

        public override string PrintToString()
        {
            return $"Тип судна: {type} / Наименование: {Name} / Масса(т): {Weight} / Макс. скорость: {MaxSpeed} / Материал паруса: {sailMaterial} / Площадь паруса: {SailArea}";
        }

        public override bool IsSearchContains(string text)
        {
            text = text.ToUpper();
            if (type.ToUpper().Contains(text)
                || Name.ToUpper().Contains(text)
                || Weight.ToString().Contains(text)
                || MaxSpeed.ToString().Contains(text)
                || SailMaterial.ToUpper().Contains(text)
                || SailArea.ToString().Contains(text))
            {
                return true;
            }
            else return false;
        }
    }
}
