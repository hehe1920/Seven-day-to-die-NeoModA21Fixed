namespace NeoModTest
{
    using System;
    using System.IO;
    using System.Xml.Serialization;

    public class Xml2Object
    {
        public static Items Deserialize(string filepath)
        {
            string s = File.ReadAllText(filepath);
            XmlSerializer serializer = new XmlSerializer(typeof(Items));
            using (StringReader reader = new StringReader(s))
            {
                return (Items) serializer.Deserialize(reader);
            }
        }

        public static T Deserialize<T>(string filepath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StreamReader reader = new StreamReader(filepath);
            T local = (T) serializer.Deserialize(reader.BaseStream);
            reader.Close();
            return local;
        }
    }
}

