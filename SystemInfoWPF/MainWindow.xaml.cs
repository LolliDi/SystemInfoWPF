using System;
using System.Management;
using System.Windows;
using System.Windows.Controls;

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
                foreach (ManagementObject obj in searcher.Get())
                {


                    if (obj.Properties.Count == 0)
                    {
                        MessageBox.Show("Не получилося");
                        return;
                    }
                    foreach (PropertyData data in obj.Properties)
                    {

                        if (data.Value != null && !string.IsNullOrEmpty(data.Value.ToString()))
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

        //switch (type)
        //    {
        //        case 0x0: outValue = "Unknown"; break;
        //        case 0x1: outValue = "Other"; break;
        //        case 0x2: outValue = "DRAM"; break;
        //        case 0x3: outValue = "Synchronous DRAM"; break;
        //        case 0x4: outValue = "Cache DRAM"; break;
        //        case 0x5: outValue = "EDO"; break;
        //        case 0x6: outValue = "EDRAM"; break;
        //        case 0x7: outValue = "VRAM"; break;
        //        case 0x8: outValue = "SRAM"; break;
        //        case 0x9: outValue = "RAM"; break;
        //        case 0xa: outValue = "ROM"; break;
        //        case 0xb: outValue = "Flash"; break;
        //        case 0xc: outValue = "EEPROM"; break;
        //        case 0xd: outValue = "FEPROM"; break;
        //        case 0xe: outValue = "EPROM"; break;
        //        case 0xf: outValue = "CDRAM"; break;
        //        case 0x10: outValue = "3DRAM"; break;
        //        case 0x11: outValue = "SDRAM"; break;
        //        case 0x12: outValue = "SGRAM"; break;
        //        case 0x13: outValue = "RDRAM"; break;
        //        case 0x14: outValue = "DDR"; break;
        //        case 0x15: outValue = "DDR2"; break;
        //        case 0x16: outValue = "DDR2 FB-DIMM"; break;
        //        case 0x17: outValue = "Undefined 23"; break;
        //        case 0x18: outValue = "DDR3"; break;
        //        case 0x19: outValue = "FBD2"; break;
        //        default: outValue = "Undefined"; break;
        //    }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string key;
            switch (((sender as ComboBox).SelectedItem as TextBlock).Text)
            {
                case "Процессор":
                    key = "Win32_Processor"; //CurrentClockSpeed стандартная тактовая частота
                                             //L2CacheSize - второй уровень кеша в кб
                                             //L3CacheSize - третий уровень кеша в кб
                                             //Name - название
                                             //NumberOfCores - количество ядер
                                             //ThreadCount - количество потоков


                    break;
                case "Материнская плата":
                    key = "Win32_BaseBoard"; //Manufacturer - производитель
                    //Product - модель
                    break;
                case "Видеокарта":
                    key = "Win32_VideoController"; //AdapterCompatibility - производитель
                                                   //Caption - модель
                                                   //VideoProcessor
                                                   //AdapterRAM - количество видеопамяти байт (нужно в мб)
                                                   //MaxRefreshRate - максимальный фпс
                                                   //CurrentVerticalResolution - разрешение вертикальное
                                                   //CurrentVHorizontalResolution - разрешение горизонтальное
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
                    key = "Win32_PhysicalMemory"; //Speed
                      //Capacity размер в байтах
                      //MaxVoltage
                      //MemoryType (код)

                    break;
                case "Кэш":
                    key = "Win32_CacheMemory"; //Purpose (L1, L2, L3 Cache) лвла
                                               //MaxCacheSize - раземер кеша
                    break;
                case "USB":
                    key = "Win32_USBController";
                    break;
                case "Диск":
                    key = "Win32_DiskDrive"; //Caption - название
                    //Size (делить на миллион с округлением в меньшую сторону и получим размер в гб)
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
                    key = "Win32_OperatingSystem"; //Caption - название
                    //OSArchitecture - 64 или 32
                    //Version - версия

                    break;

                case "Вентиляторы":
                    // key = "Win32_PhysicalMemoryArray"; maxCapacity - максимальный объем оперативы
                    // MemoryDevices - количество слотов
                    key = "Win32_DesktopMonitor";
                    break;
                default:
                    key = "Win32_Processor";
                    break;

            }
            GetHardWareInfo(key);
        }
    }
}
