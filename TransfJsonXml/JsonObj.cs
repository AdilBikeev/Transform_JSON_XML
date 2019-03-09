using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransfJsonXml
{
    class JsonObj
    {
        static string setSpace(string json)
        {
            string jsnNew = string.Empty;
            for (int i = 0; i < json.Length; i++)
            {
                jsnNew += json[i];

                if (json[i] == '{')
                {
                    jsnNew += '\n';
                    for (int j = 0; j <= i; j++) jsnNew += '\t';
                }
            }
            return jsnNew;
        }
        /*
        string name;//имя объекта json
        public string Name
        {
            get { return name; }
            set { name = getObjName(value); }
        }

        public List<JsonObj> jsnList = null;//список json объектов

        public JsonObj() { }

        public JsonObj(string json)
        {
            string jsonObj = string.Empty, 
                nameObj = string.Empty,
                contentObj = string.Empty;
            int i = 0;
            while(i < json.Length)
            {
                for(; (i < json.Length) &&  (json[i] != ':'); i++)//запоминаем имя объекта JSON
                {
                    nameObj += json[i];
                }
                
                json.Remove(0, nameObj.Length + 2);
            }

        }
        */
        /*
        public string getObjName(string json)
        {
            
            string objName = string.Empty;
            ushort i = 1;
            for (; i < json.Length && json[i] != ':' && json[i] != '}'; i++)
            {
                objName += json[i];
            }

            if(json[i] == ':')//если объект JSON содержит значения
            {
                json = json.Remove(0, objName.Length + 2);
                JsonObj obj = new JsonObj(json);
                jsnList.Add(obj);
            }

            return objName;
            return string.Empty;
        }*/
    }
}
