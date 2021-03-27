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

        //Обновить таблицу
        private void UpdateTable()
        {
            try
            {
                ClearTableView();
                TextBlockError.Text = "";
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
                TextBlockError.Text = ex.Message;
            }
        }

        private void ClearTableView()
        {
            ItemSteamer.Items.Clear();
            ItemSailboat.Items.Clear();
            ItemCorvette.Items.Clear();
        }

        private void ClearTableSearch()
        {
            SearchItemSteamer.Items.Clear();
            SearchItemSailboat.Items.Clear();
            SearchItemCorvette.Items.Clear();
        }

        //Обновить
        private void buttonUpdateData_Click(object sender, RoutedEventArgs e)
        {
            UpdateTable();
        }

        //Клик очистки всех данных
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

        //Клик добавления объекта
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (radioButton1.IsChecked == true)
                {
                    var name = textBox1.Text;
                    var weight = int.Parse(textBox2.Text);
                    var maxSpeed = int.Parse(textBox3.Text);
                    var massOfCoal = int.Parse(textBox4.Text);
                    var rangeOfTravel = int.Parse(textBox5.Text);
                    Model.Ships.Add(new Steamer(name, maxSpeed, weight, massOfCoal, rangeOfTravel));
                }
                else if (radioButton2.IsChecked == true)
                {
                    var name = textBox1.Text;
                    var weight = int.Parse(textBox2.Text);
                    var maxSpeed = int.Parse(textBox3.Text);
                    var sailMaterial = textBox4.Text;
                    var sailArea = int.Parse(textBox5.Text);
                    Model.Ships.Add(new Sailboat(name, maxSpeed, weight, sailMaterial, sailArea));
                }
                else if (radioButton3.IsChecked == true)
                {
                    var name = textBox1.Text;
                    var weight = int.Parse(textBox2.Text);
                    var maxSpeed = int.Parse(textBox3.Text);
                    var armament = textBox4.Text;
                    var equipment = textBox5.Text;
                    Model.Ships.Add(new Corvette(name, maxSpeed, weight, armament, equipment));
                }
                else
                {
                    throw new Exception("Не выбран тип судна");
                }
                SaveData();
                UpdateTable();
                textBlockLog.Foreground = Brushes.Black;
                textBlockLog.Text = "Выберите тип судна";
                ClearTextBoxs();
            }
            catch (Exception ex)
            {
                textBlockLog.Foreground = Brushes.Red;
                textBlockLog.Text = ex.Message;
            }
        }

        private void ClearTextBoxs()
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            textBlockFirst.Text = "Масса угля";
            textBlockSecond.Text = "Дальность хода";
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            textBlockFirst.Text = "Материал паруса";
            textBlockSecond.Text = "Площадь паруса";
        }

        private void radioButton3_Checked(object sender, RoutedEventArgs e)
        {
            textBlockFirst.Text = "Вооружение";
            textBlockSecond.Text = "Оборудование";
        }

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
                TextBlockError.Text = ex.Message;
            }
        }

        
    }
}
