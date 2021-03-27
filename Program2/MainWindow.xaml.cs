using LibraryShips;
using Program2_WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Program2_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private MainViewModel Model;

        public static string Path { get; } = @"D:\source\repos\Program2_WPF\Program2\XML\data.xml";

        public MainWindow()
        {
            InitializeComponent();
            Model = new MainViewModel();
            DataContext = Model;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Model.Ships = await LoadData(Path);
            UpdateTable();
        }

        /// <summary>
        /// Асинхронно загружает файл
        /// </summary>
        public async Task<List<Ship>> LoadData(string path)
        {
            return await Model.XmlLoadAsync<List<Ship>>(path) ?? new List<Ship>();
        }

        /// <summary>
        /// Асинхронно сохраняет файл
        /// </summary>
        public async void SaveData()
        {
            await Model.XmlSaveAsync(Path, Model.Ships);
        }

        /// <summary>
        /// Выполняется обновление таблицы
        /// </summary>
        private void UpdateTable()
        {
            try
            {
                ClearTableView();
                Model.ErrorInfo = "";
                foreach (var item in Model.Ships)
                {
                    if (item is Steamer)
                    {
                        ItemSteamer.Items.Add(item);
                    }
                    if (item is Sailboat)
                    {
                        ItemSailboat.Items.Add(item);
                    }
                    if (item is Corvette)
                    {
                        ItemCorvette.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Model.ErrorInfo = ex.Message;
            }
        }

        /// <summary>
        /// Выполняется обновление таблицы по клику
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpdateData_Click(object sender, RoutedEventArgs e)
        {
            UpdateTable();
        }

        /// <summary>
        /// Выполняется очистка списка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDeleteData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Model.Ships.Clear();
                SaveData();
                UpdateTable();
            }
            catch (Exception ex)
            {
                TextBlockError.Text = ex.Message;
            }
        }

        /// <summary>
        /// Выполняется добавление объектов в список
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (radioButton1.IsChecked == true)
                {
                    Model.Ships.Add(new Steamer()
                    {
                        Name = Model.Name,
                        Weight = int.Parse(Model.Weight),
                        MaxSpeed = int.Parse(Model.MaxSpeed),
                        MassOfCoal = int.Parse(Model.FirstField),
                        RangeOfTravel = int.Parse(Model.SecondField)
                    });
                }
                else if (radioButton2.IsChecked == true)
                {
                    Model.Ships.Add(new Sailboat()
                    {
                        Name = Model.Name,
                        Weight = int.Parse(Model.Weight),
                        MaxSpeed = int.Parse(Model.MaxSpeed),
                        SailMaterial = Model.FirstField,
                        SailArea = int.Parse(Model.SecondField)
                    });
                }
                else if (radioButton3.IsChecked == true)
                {
                    Model.Ships.Add(new Corvette()
                    {
                        Name = Model.Name,
                        Weight = int.Parse(Model.Weight),
                        MaxSpeed = int.Parse(Model.MaxSpeed),
                        Armament = Model.FirstField,
                        Equipment = Model.SecondField
                    });
                }
                else throw new Exception("Не выбран тип судна");
                SaveData();
                UpdateTable();
                textBlockLog.Foreground = Brushes.Black;
                Model.LogInfo = "\nВыберите тип судна";
                Model.ClearTextBoxs();
            }
            catch (Exception ex)
            {
                textBlockLog.Foreground = Brushes.Red;
                Model.LogInfo = ex.Message;
            }
        }

        /// <summary>
        /// Выполняется поиск по полям объектов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClearTableSearch();
                foreach (var item in Model.Ships)
                {
                    if (item.IsSearchContains(textBoxSearch.Text))
                    {
                        if (item is Steamer)
                        {
                            SearchItemSteamer.Items.Add(item);
                        }
                        if (item is Sailboat)
                        {
                            SearchItemSailboat.Items.Add(item);
                        }
                        if (item is Corvette)
                        {
                            SearchItemCorvette.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Model.ErrorInfo = ex.Message;
            }
        }

        /// <summary>
        /// Очистка DataGrid просмотра
        /// </summary>
        private void ClearTableView()
        {
            ItemSteamer.Items.Clear();
            ItemSailboat.Items.Clear();
            ItemCorvette.Items.Clear();
        }

        /// <summary>
        /// Очистка DataGrid поиска
        /// </summary>
        private void ClearTableSearch()
        {
            SearchItemSteamer.Items.Clear();
            SearchItemSailboat.Items.Clear();
            SearchItemCorvette.Items.Clear();
        }

        #region RadioButtonsChecked
        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            Model.TextBlockFirst = "Масса угля";
            Model.TextBlockSecond = "Дальность хода";
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            Model.TextBlockFirst = "Материал паруса";
            Model.TextBlockSecond = "Площадь паруса";
        }

        private void radioButton3_Checked(object sender, RoutedEventArgs e)
        {
            Model.TextBlockFirst = "Вооружение";
            Model.TextBlockSecond = "Оборудование";
        }
        #endregion
    }
}
