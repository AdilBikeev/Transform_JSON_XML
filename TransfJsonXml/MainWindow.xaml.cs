using System;
using System.Collections.Generic;
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
using System.Xml;
using Json;
using Newtonsoft.Json;

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
                //TagXml tagXml = new TagXml(xmlNode.OuterXml);//парсим данные xml
                
                rtbXML.Document.Blocks.Clear();
                rtbXML.AppendText(xmlStr);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "ERROR");
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
                MessageBox.Show(exc.Message, "ERROR");
            }
        }
    }
}
