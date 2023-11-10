namespace NeoModTest.ItemModsXML
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="display_value")]
    public class DisplayValue
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Name>k__BackingField;

        [XmlAttribute(AttributeName="name")]
        public string Name { get; set; }
    }
}

