using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransfJsonXml
{
    class TagXml
    {
        string name;//имя тега
        public string Name
        {
            get { return name; }
            set { name = getTagName(value); }
        }

        public List<TagXml> tags = null;//список тегов

        public TagXml() { }

        public TagXml(string xml)
        {
            Name = xml;

            if (name.Length + 3 != xml.IndexOf('/' + name + '>'))//если внутри тега есть ещё теги
            {
                tags = new List<TagXml>();

                string contentXML = xml,
                    tagName = getTagName(contentXML);
                int indexStartContent = 0,//индекс начала содержимого тега
                    indexEndContent = 0;//индекс конца содержимого тега

                while (contentXML.IndexOf('/' + tagName + '>') != -1)//пока внутри данного тега не закончились подтеги
                {
                    indexStartContent = tagName.Length + 2;
                    indexEndContent = contentXML.IndexOf(tagName, indexStartContent) - 1;

                    contentXML = contentXML.Remove(0, indexStartContent);//удаляем помимо открытого тега - спецсимволы(< > \n \r)
                    contentXML = contentXML.Remove(indexEndContent - (tagName.Length + 3), tagName.Length + 3);//удаляем закрывающий тег

                    if (contentXML.IndexOf('/') != -1)
                    {
                        TagXml tag = new TagXml(contentXML);
                        tags.Add(tag);
                    }
                }
            }
        }
        public string getTagName(string xml)
        {
            string tagName = string.Empty;
            for (ushort i = 1; xml[i] != '>'; i++)
            {
                tagName += xml[i];
            }
            return tagName;
        }
    }
}
