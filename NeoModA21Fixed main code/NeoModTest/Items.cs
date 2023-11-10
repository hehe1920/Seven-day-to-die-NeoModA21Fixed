namespace NeoModTest
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="items")]
    public class Items
    {
        [XmlElement(ElementName="item")]
        public List<NeoModTest.Item> Item;
    }
}

