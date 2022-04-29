using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
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

namespace SystemInfoWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ComboBoxSelectComponent.SelectedIndex = 0;
        }

        private void GetHardWareInfo(string key)
        {
            ListViewInfo.Items.Clear();
            
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM " + key);
            try
            {
                foreach(ManagementObject obj in searcher.Get())
                {

                    
                    if (obj.Properties.Count == 0)
                    {
                        MessageBox.Show("Не получилося");
                        return;
                    }
                    foreach(PropertyData data in obj.Properties)
                    {
                        
                        if(data.Value != null && !string.IsNullOrEmpty(data.Value.ToString()))
                        {
                            ListViewInfo.Items.Add(new SysInf() { Name = data.Name, Description = data.Value.ToString() });
                            
                        }
                    }
                }

            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }

        }

        struct SysInf
        {
            public string Name { get; set; }
            public string Description { get; set; }

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string key;
            switch (((sender as ComboBox).SelectedItem as TextBlock).Text)
            {
                case "Процессор":
                    key = "Win32_Processor";
                    break;
                case "Материнская плата":
                    key = "Win32_BaseBoard";
                    break;
                case "Видеокарта":
                    key = "Win32_VideoController";
                    break;
                case "Чипсет":
                    key = "Win32_IDEController";
                    break;
                case "Батарея":
                    key = "Win32_Battery";
                    break;
                case "Биос":
                    key = "Win32_BIOS";
                    break;
                case "Оперативная память":
                    key = "Win32_PhysicalMemory";
                    break;
                case "Кэш":
                    key = "Win32_CacheMemory";
                    break;
                case "USB":
                    key = "Win32_USBController";
                    break;
                case "Диск":
                    key = "Win32_DiskDrive";
                    break;
                case "Логические диски":
                    key = "Win32_LogicalDisk";
                    break;
                case "Клавиатура":
                    key = "Win32_Keyboard";
                    break;
                case "Сеть":
                    key = "Win32_NetworkAdapter";
                    break;
                case "Пользователь":
                    key = "Win32_Account";
                    break;
                case "Операционная система":
                    key = "Win32_OperatingSystem";
                    break;
                default:
                    key = "Win32_Processor";
                    break;

            }
            GetHardWareInfo(key);
        }
    }
}
