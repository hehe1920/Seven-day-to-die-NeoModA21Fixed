namespace NeoModTest
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="item")]
    public class Item
    {
        [XmlAttribute(AttributeName="name")]
        public string Name;
        [XmlElement(ElementName="property")]
        public List<NeoModTest.Property> Property;
    }
}

