namespace NeoModTest.ItemModsXML
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="triggered_effect")]
    public class TriggeredEffect
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Action>k__BackingField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private string <Buff>k__BackingField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private string <Cvar>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Operation>k__BackingField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private string <Parent_Transform>k__BackingField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private string <ParentTransform>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Part>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Prefab>k__BackingField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private NeoModTest.ItemModsXML.Requirement <Requirement>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Sound>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Target>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <TransformPath>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Trigger>k__BackingField;

        [XmlAttribute(AttributeName="action")]
        public string Action { get; set; }

        [XmlAttribute(AttributeName="buff")]
        public string Buff { get; set; }

        [XmlAttribute(AttributeName="cvar")]
        public string Cvar { get; set; }

        [XmlAttribute(AttributeName="operation")]
        public string Operation { get; set; }

        [XmlAttribute(AttributeName="parent_transform")]
        public string Parent_Transform { get; set; }

        [XmlAttribute(AttributeName="parentTransform")]
        public string ParentTransform { get; set; }

        [XmlAttribute(AttributeName="part")]
        public string Part { get; set; }

        [XmlAttribute(AttributeName="prefab")]
        public string Prefab { get; set; }

        [XmlElement(ElementName="requirement")]
        public NeoModTest.ItemModsXML.Requirement Requirement { get; set; }

        [XmlAttribute(AttributeName="sound")]
        public string Sound { get; set; }

        [XmlAttribute(AttributeName="target")]
        public string Target { get; set; }

        [XmlAttribute(AttributeName="transform_path")]
        public string TransformPath { get; set; }

        [XmlAttribute(AttributeName="trigger")]
        public string Trigger { get; set; }
    }
}

