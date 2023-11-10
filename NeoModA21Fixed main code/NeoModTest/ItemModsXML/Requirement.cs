namespace NeoModTest.ItemModsXML
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="requirement")]
    public class Requirement
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private string <Buff>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Cvar>k__BackingField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private string <Name>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Operation>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ProgressionName>k__BackingField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private string <SeedType>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Stat>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Tags>k__BackingField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private string <Target>k__BackingField;

        [XmlAttribute(AttributeName="buff")]
        public string Buff { get; set; }

        [XmlAttribute(AttributeName="cvar")]
        public string Cvar { get; set; }

        [XmlAttribute(AttributeName="name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName="operation")]
        public string Operation { get; set; }

        [XmlAttribute(AttributeName="progression_name")]
        public string ProgressionName { get; set; }

        [XmlAttribute(AttributeName="seed_type")]
        public string SeedType { get; set; }

        [XmlAttribute(AttributeName="stat")]
        public string Stat { get; set; }

        [XmlAttribute(AttributeName="tags")]
        public string Tags { get; set; }

        [XmlAttribute(AttributeName="target")]
        public string Target { get; set; }
    }
}

