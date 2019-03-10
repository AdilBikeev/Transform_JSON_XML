using System;
using System.Windows;
using System.Windows.Documents;
using System.Xml;
using Newtonsoft.Json;
using Microsoft.Win32;
using System.IO;

namespace TransfJsonXml
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ToXML_Click(object sender, RoutedEventArgs e)
        {
            string jsonStr = new TextRange(rtbJSON.Document.ContentStart, rtbJSON.Document.ContentEnd).Text; // запоминаем данные в формате JSON и представляем их в виде строки
            try
            {
                XmlDocument doc = JsonConvert.DeserializeXmlNode(jsonStr);
                string xmlStr = Space.setSpaceXml(doc);
                
                rtbXML.Document.Blocks.Clear();
                rtbXML.AppendText(xmlStr);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ToJSON_Click(object sender, RoutedEventArgs e)//преобразует из XML - JSON объект
        {
            string xmlStr = new TextRange(rtbXML.Document.ContentStart, rtbXML.Document.ContentEnd).Text; // запоминаем данные в формате JSON и представляем их в виде строки
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlStr);

                string jsonStr = JsonConvert.SerializeXmlNode(doc);
                jsonStr = Space.setSpaceJson(jsonStr);


                rtbJSON.Document.Blocks.Clear();
                rtbJSON.AppendText(jsonStr);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MiSaveAsJson_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();

            string filter = "JSON (*.json)| *json";
            try
            {
                saveDlg.Filter = filter;//устанавливаем расширения файлов, в которых могут быть сохранены данные

                if (saveDlg.ShowDialog() == true)//если пользователь указал куда сохранять файл
                {
                    using(StreamWriter writer = new StreamWriter(saveDlg.FileName + ".json"))
                    {
                        string str = new TextRange(rtbJSON.Document.ContentStart, rtbJSON.Document.ContentEnd).Text;
                        writer.Write(str);
                        MessageBox.Show("Файл успешно сохранён по указанному вами пути", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }catch(Exception exc)
            {
                MessageBox.Show(exc.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MiSaveAsXML_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();

            string filter = "XML (*.xml)| *xml";
            try
            {
                saveDlg.Filter = filter;//устанавливаем расширения файлов, в которых могут быть сохранены данные
           
                if (saveDlg.ShowDialog() == true)//если пользователь указал куда сохранять файл
                {
                    using (StreamWriter writer = new StreamWriter(saveDlg.FileName + ".xml"))
                    {
                        string str = new TextRange(rtbXML.Document.ContentStart, rtbXML.Document.ContentEnd).Text;
                        writer.Write(str);
                        MessageBox.Show("Файл успешно сохранён по указанному вами пути", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MiOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            string[] filter = { ".xml", ".json" };
            try
            {
                openDlg.Filter = "XML (*.xml)|*.xml|JSON (*.json)|*.json";//устанавливаем расширения файлов, в которых могут быть сохранены данные

                if (openDlg.ShowDialog() == true)//если пользователь указал какой файл открывать
                {
                    using (StreamReader reader = new StreamReader(openDlg.FileName))
                    {
                        string textFile = reader.ReadToEnd();
                        if (openDlg.FilterIndex == 1)//если открыт XML файл
                        {
                            rtbXML.AppendText(textFile);
                            ToJSON_Click(null, e);
                        }
                        else
                        {
                            rtbJSON.AppendText(textFile);
                            ToXML_Click(null, e);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnClearJSON_Click(object sender, RoutedEventArgs e)
        {
            rtbJSON.Document.Blocks.Clear();
        }

        private void BtnClearXML_Click(object sender, RoutedEventArgs e)
        {
            rtbXML.Document.Blocks.Clear();
        }

        private void MiClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
