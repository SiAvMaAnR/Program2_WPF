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
    public class MainViewModel:BaseViewModel
    {
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


        private string _ErrorInfo;
        public string ErrorInfo
        {
            get { return _ErrorInfo; }
            set
            {
                _ErrorInfo = value;
                OnPropertyChanged(nameof(ErrorInfo));
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
    }
}
