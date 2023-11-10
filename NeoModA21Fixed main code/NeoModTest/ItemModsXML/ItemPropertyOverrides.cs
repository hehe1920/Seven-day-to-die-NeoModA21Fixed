namespace NeoModTest.ItemModsXML
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="item_property_overrides")]
    public class ItemPropertyOverrides
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Name>k__BackingField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private List<NeoModTest.ItemModsXML.Property> <Property>k__BackingField;

        [XmlAttribute(AttributeName="name")]
        public string Name { get; set; }

        [XmlElement(ElementName="property")]
        public List<NeoModTest.ItemModsXML.Property> Property { get; set; }
    }
}

