namespace NeoModTest.ItemModsXML
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="passive_effect")]
    public class PassiveEffect
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Name>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Operation>k__BackingField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private List<NeoModTest.ItemModsXML.Requirement> <Requirement>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Tags>k__BackingField;

        [XmlAttribute(AttributeName="name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName="operation")]
        public string Operation { get; set; }

        [XmlElement(ElementName="requirement")]
        public List<NeoModTest.ItemModsXML.Requirement> Requirement { get; set; }

        [XmlAttribute(AttributeName="tags")]
        public string Tags { get; set; }
    }
}

