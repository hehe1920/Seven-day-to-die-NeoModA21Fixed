﻿namespace NeoModTest
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="property")]
    public class Property
    {
        [XmlAttribute(AttributeName="class")]
        public string Class;
        [XmlAttribute(AttributeName="name")]
        public string Name;
        [XmlAttribute(AttributeName="param1")]
        public string Param1;
        [XmlElement(ElementName="property")]
        public List<Property> property;
        [XmlAttribute(AttributeName="value")]
        public string Value;
    }
}

