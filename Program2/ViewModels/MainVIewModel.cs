using LibraryShips;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Program2_WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(name));
            }
        }

        private string weight;
        public string Weight
        {
            get { return weight; }
            set
            {
                weight = value;
                OnPropertyChanged(nameof(weight));
            }
        }

        private string maxSpeed;
        public string MaxSpeed
        {
            get { return maxSpeed; }
            set
            {
                maxSpeed = value;
                OnPropertyChanged(nameof(maxSpeed));
            }
        }

        private string firstField;
        public string FirstField
        {
            get { return firstField; }
            set
            {
                firstField = value;
                OnPropertyChanged(nameof(firstField));
            }
        }

        private string secondField;
        public string SecondField
        {
            get { return secondField; }
            set
            {
                secondField = value;
                OnPropertyChanged(nameof(secondField));
            }
        }

        private List<Ship> ships = new List<Ship>();
        public List<Ship> Ships
        {
            get
            {
                return ships;
            }
            set
            {
                ships = value;
                OnPropertyChanged();
            }
        }

        private string textBlockFirst;
        public string TextBlockFirst
        {
            get { return textBlockFirst; }
            set
            {
                textBlockFirst = value;
                OnPropertyChanged(nameof(textBlockFirst));
            }
        }

        private string textBlockSecond;
        public string TextBlockSecond
        {
            get { return textBlockSecond; }
            set
            {
                textBlockSecond = value;
                OnPropertyChanged(nameof(textBlockSecond));
            }
        }

        private string errorInfo;
        public string ErrorInfo
        {
            get { return errorInfo; }
            set
            {
                errorInfo = value;
                OnPropertyChanged(nameof(ErrorInfo));
            }
        }

        private string logInfo = "\nВыберите тип судна!";
        public string LogInfo
        {
            get { return logInfo; }
            set
            {
                logInfo = value;
                OnPropertyChanged(nameof(logInfo));
            }
        }

        /// <summary>
        /// Десериализация
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">Путь</param>
        /// <returns></returns>
        public async Task<T> XmlLoadAsync<T>(string path)
        {

            return await Task.Run(() =>
             {
                 try
                 {
                     XmlSerializer formatter = new XmlSerializer(typeof(List<Ship>));
                     using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                     {
                         return (T)formatter.Deserialize(fs);
                     }
                 }
                 catch
                 {
                     ErrorInfo = "Не верная структура или пустой xml";
                     return default(T);
                 }
             });
        }

        /// <summary>
        /// Сериализация
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">Путь</param>
        /// <param name="list">Список</param>
        /// <returns></returns>
        public async Task<bool> XmlSaveAsync<T>(string path, T list)
        {
            return await Task.Run(() =>
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        serializer.Serialize(fs, ships);
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    ErrorInfo = ex.Message ?? "";
                    return false;
                }
            });
        }

        /// <summary>
        /// Очистка полей ввода данных(ТекстБоксов)
        /// </summary>
        public void ClearTextBoxs()
        {
            Name = null;
            Weight = null;
            MaxSpeed = null;
            FirstField = null;
            SecondField = null;
        }
    }
}
