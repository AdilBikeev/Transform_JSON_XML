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
            
        }

        private void ToJSON_Click(object sender, RoutedEventArgs e)//преобразует из XML - JSON объект
        {
            
            string xmlStr = new TextRange(rtbXML.Document.ContentStart, rtbXML.Document.ContentEnd).Text; // запоминаем данные в формате XML и представляем их в виде строки
            string jsonStr = string.Empty;
            Tag tags = new Tag(xmlStr);

            rtbJSON.Document.Blocks.Clear();//очищаем содержимое 
            rtbJSON.AppendText(tags.toJSON(tags));
        }
    }
}
