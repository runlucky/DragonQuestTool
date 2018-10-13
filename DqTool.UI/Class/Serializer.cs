using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DqTool.UI.Class
{
    public static class Serializer<T>
    {
        private static readonly XmlSerializer _serializer = new XmlSerializer(typeof(T));

        public static void Serialize(string path, T data)
        {
            using (var sw = new StreamWriter(path, false, new UTF8Encoding(false)))
            {
                _serializer.Serialize(sw, data);
            }
        }

        public static T Deserialize(string path)
        {
            T data;
            using (var sr = new StreamReader(path, new UTF8Encoding(false)))
            {
                data = (T)_serializer.Deserialize(sr);
            }
            return data;
        }
    }
}
