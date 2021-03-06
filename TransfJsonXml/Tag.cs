﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransfJsonXml
{
    [Serializable]
    class Tag
    {
        string name;//имя тега
        public string Name
        {
            get { return name; }
            set {   name = getTagName(value);   }
        }

        public List<Tag> tags = null;//список тегов

        public Tag() { }
        
        public Tag(string xml)
        {
            Name = xml;
            
            if(name.Length + 5 != xml.IndexOf(name + "/>"))//если внутри тега есть ещё теги
            {
                tags = new List<Tag>();

                string contentXML = xml,
                    tagName = getTagName(contentXML);
                int indexStartContent = 0,//индекс начала содержимого тега
                    indexEndContent = 0;//индекс конца содержимого тега

                while (contentXML.IndexOf(tagName + '/') != -1)//пока внутри данного тега не закончились подтеги
                {
                    indexStartContent = tagName.Length + 4;
                    indexEndContent = contentXML.IndexOf(tagName, indexStartContent)-1;

                    contentXML = contentXML.Remove(0, indexStartContent);//удаляем помимо открытого тега - спецсимволы(< > \n \r)
                    contentXML = contentXML.Remove(indexEndContent - (tagName.Length + 4), tagName.Length + 5);//удаляем закрывающий тег

                    if(contentXML.IndexOf('/') != -1)
                    {
                        Tag tag = new Tag(contentXML);
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

        public  string getJsonTag(Tag tag, string json)//возвращает JSON представление Tag в спомощью рекурсии
        {
            json += "\"" + tag.Name + "\"";
            if (tag.tags != null)//если внутри тега есть ещё подтег
            {
                json += " : ";
                if (tag.tags[0].tags != null) json += '{';
                for (int i = 0; i < tag.tags.Count; i++)//перечисляем внутренние теги
                {
                    json = getJsonTag(tag.tags[i], json);
                    if (i + 1 != tag.tags.Count) json += ',';
                }
            }
            json += '}';
            return json;
        }

        public  string toJSON(Tag tag)//преобразует из XML в JSON
        {
            string json = string.Empty;
            json += "{ \"" + tag.Name + "\"";
            if(tag.tags != null)
            {
                json += " : ";
                if (tag.tags[0].tags != null) json += '{';
                for (int i = 0; i < tag.tags.Count; i++)//перечисляем внутренние теги
                {
                    json = getJsonTag(tag.tags[i], json);
                    if (i + 1 != tag.tags.Count) json += ',';
                }
            }

            return json;
        }
    }
}
