namespace NeoModTest.ItemModsXML
{
    using System;
    using System.IO;
    using System.Xml.Serialization;

    public class ModsXml2Object
    {
        public static ItemModifiers Deserialize(string filepath)
        {
            string s = File.ReadAllText(filepath);
            XmlSerializer serializer = new XmlSerializer(typeof(ItemModifiers));
            using (StringReader reader = new StringReader(s))
            {
                return (ItemModifiers) serializer.Deserialize(reader);
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

