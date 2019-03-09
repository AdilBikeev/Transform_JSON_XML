using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TransfJsonXml
{
    public  static class Space
    {
        //устанавливает пробелы в JSON формате
        public static string setSpaceJson(string json)
        {
            string jsnNew = string.Empty;
            int countOpenTab = 0;//кол-во сделанных табуляций тегов

            for (int i = 0; i < json.Length; i++)
            {
                if (json[i] == '{')
                {
                    jsnNew += json[i];
                    jsnNew += '\n';
                    countOpenTab++;
                    for (int j = 0; j < countOpenTab; j++) jsnNew += '\t';
                }
                else if(json[i] == ',')
                {
                    jsnNew += json[i];
                    jsnNew += '\n';
                    for (int j = 0; j < countOpenTab; j++) jsnNew += '\t';
                }
                else if (json[i] == '}')
                {
                    jsnNew += '\n';
                    countOpenTab--;
                    for (int j = 0; j < countOpenTab; j++) jsnNew += '\t';
                    jsnNew += '}';
                }
                else
                {
                    jsnNew += json[i];
                }
            }
            return jsnNew;
        }

        //устанавливает смещение в XML формате
        public static string setOffset(string xmlNew, int count)
        {
            xmlNew += "\n";
            for (int k = 0; k < count; k++) xmlNew += '\t';
            return xmlNew;
        }

        //устанавливает пробелы в XML для внутренней части документа
        public static string setSpaceInnerXml(XmlDocument doc, int countSpace)
        {
            string xmlNew = string.Empty;
            XmlDocument docNew = new XmlDocument();

            countSpace++;
            setOffset(xmlNew, countSpace);
            xmlNew += "<" + doc.FirstChild.Name + ">";

            if (doc.DocumentElement.InnerText.IndexOf('/') != -1)//если внутри узла есть подтеги
            {
                List<XmlNode> xmlElements = (from XmlNode item in doc.DocumentElement
                                             select item).ToList();

                for (int i = 0; i < xmlElements.Count; i++)//перебираем все узлы xml документа
                {
                    countSpace++;
                    setOffset(xmlNew, countSpace);

                    //загружаем внутреннюю часть документа
                    if (xmlElements[i].InnerXml != "") docNew.LoadXml(xmlElements[i].InnerXml);

                    //формируем заголовок узла
                    xmlNew += "<" + xmlElements[i].Name + ">";

                    //формируем подузел
                    if (xmlElements[i].InnerXml != "")
                    {
                        countSpace++;
                        setOffset(xmlNew, countSpace);

                        xmlNew += setSpaceInnerXml(docNew, countSpace);

                        setOffset(xmlNew, countSpace);
                    }

                    //закрываем узел
                    if (xmlElements.Count != 0) setOffset(xmlNew, countSpace);
                    xmlNew += "</" + xmlElements[i].Name + ">";
                    if (i + 1 != xmlElements.Count) xmlNew += "\n\t";
                }
            }
            else if (doc.DocumentElement.InnerText != "") xmlNew += doc.DocumentElement.InnerText;
            else xmlNew += "";

            countSpace++;
            setOffset(xmlNew, countSpace);
            xmlNew += "</" + doc.FirstChild.Name + ">";
            return xmlNew;
        }

        //устанавливает пробелы в XML для внешней части документа
        public static string setSpaceXml(XmlDocument doc)
        {
            string xmlNew = string.Empty;
            int countSpace = 0;//кол-во использованных табуляций

            XmlDocument docNew = new XmlDocument();
            xmlNew += "<" + doc.FirstChild.Name + ">\n\t"; countSpace++;

            List<XmlNode> xmlElements = (from XmlNode item in doc.DocumentElement
                                           select item).ToList();

            for(int i = 0; i < xmlElements.Count; i++)//перебираем все узлы xml документа
            {
                    if(xmlElements[i].InnerXml != "") docNew.LoadXml(xmlElements[i].InnerXml);
                    xmlNew += "<" + xmlElements[i].Name + ">\n\t\t";
                    if(xmlElements[i].InnerXml != "") xmlNew += setSpaceInnerXml(docNew,  countSpace);
                    xmlNew += "\n\t</" + xmlElements[i].Name + ">";
                    if (i + 1 != xmlElements.Count) xmlNew += "\n\t";
            }

            xmlNew += "\n</" + doc.FirstChild.Name + ">";
            return xmlNew;
        }
    }
}
