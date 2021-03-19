using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using LibraryShips;
using Program2_WPF.ViewModels;

namespace Program2_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<Ship> ships = new List<Ship>();

        private string path = @"D:\source\repos\Program2_WPF\Program2\XML\data.xml";
        private MainWIndowsVIewModel mainModel;
        public MainWindow()
        {
            InitializeComponent();
            mainModel = new MainWIndowsVIewModel();
            this.DataContext = mainModel;
            LoadData();
        }

        /// <summary>
        /// Асинхронно загружает файл
        /// </summary>
        public async void LoadData()
        {
            await XmlDownloadAsync();
        }
        private async Task XmlDownloadAsync()
        {
            try
            {
                await Task.Run(() =>
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(List<Ship>));
                    using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                    {
                        ships = (List<Ship>)formatter.Deserialize(fs);
                    }
                });
                UpdateTable();
            }
            catch
            {
                mainModel.ErrorInfo = "Не верная структура или пустой xml";
            }
        }

        /// <summary>
        /// Асинхронно сохраняет файл
        /// </summary>
        public async void SaveData()
        {
            await XmlSaveAsync();
        }
        private async Task XmlSaveAsync()
        {
            try
            {
                await Task.Run(() =>
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Ship>));
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        serializer.Serialize(fs, ships);
                    }
                });
            }
            catch (Exception ex)
            {
                mainModel.ErrorInfo = ex.Message;
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

        //Обновить таблицу
        private void UpdateTable()
        {
            try
            {
                ClearTableView();
                TextBlockError.Text = "";
                foreach (var item in ships)
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

        //Обновить
        private void buttonUpdateData_Click(object sender, RoutedEventArgs e)
        {
            UpdateTable();
        }

        //Клик 
        private void buttonDeleteData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ships.Clear();
                XmlSaveAsync();
                UpdateTable();
            }
            catch(Exception ex)
            {
                TextBlockError.Text = ex.Message;
            }
        }

        //Клик добавления объекта
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (radioButton1.IsChecked==true)
                {
                    var name = textBox1.Text;
                    var weight = int.Parse(textBox2.Text);
                    var maxSpeed = int.Parse(textBox3.Text);
                    var massOfCoal = int.Parse(textBox4.Text);
                    var rangeOfTravel = int.Parse(textBox5.Text);
                    ships.Add(new Steamer(name, maxSpeed, weight, massOfCoal, rangeOfTravel));
                }
                else if (radioButton2.IsChecked == true)
                {
                    var name = textBox1.Text;
                    var weight = int.Parse(textBox2.Text);
                    var maxSpeed = int.Parse(textBox3.Text);
                    var sailMaterial = textBox4.Text;
                    var sailArea = int.Parse(textBox5.Text);
                    ships.Add(new Sailboat(name, maxSpeed, weight, sailMaterial, sailArea));
                }
                else if (radioButton3.IsChecked == true)
                {
                    var name = textBox1.Text;
                    var weight = int.Parse(textBox2.Text);
                    var maxSpeed = int.Parse(textBox3.Text);
                    var armament = textBox4.Text;
                    var equipment = textBox5.Text;
                    ships.Add(new Corvette(name, maxSpeed, weight, armament, equipment));
                }
                else
                {
                    throw new Exception("Не выбран тип судна");
                }

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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            XmlSaveAsync();
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // UpdateTable();
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClearTableSearch();
                foreach (var item in ships)
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
