namespace NeoModTest.ItemModsXML
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="item_modifier")]
    public class ItemModifier
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <BlockedTags>k__BackingField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private List<NeoModTest.ItemModsXML.EffectGroup> <EffectGroup>k__BackingField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private string <Icon>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <InstallableTags>k__BackingField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private List<NeoModTest.ItemModsXML.ItemPropertyOverrides> <ItemPropertyOverrides>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ModifierTags>k__BackingField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private string <Name>k__BackingField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private List<NeoModTest.ItemModsXML.Property> <Property>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Type>k__BackingField;

        [XmlAttribute(AttributeName="blocked_tags")]
        public string BlockedTags { get; set; }

        [XmlElement(ElementName="effect_group")]
        public List<NeoModTest.ItemModsXML.EffectGroup> EffectGroup { get; set; }

        [XmlAttribute(AttributeName="icon")]
        public string Icon { get; set; }

        [XmlAttribute(AttributeName="installable_tags")]
        public string InstallableTags { get; set; }

        [XmlElement(ElementName="item_property_overrides")]
        public List<NeoModTest.ItemModsXML.ItemPropertyOverrides> ItemPropertyOverrides { get; set; }

        [XmlAttribute(AttributeName="modifier_tags")]
        public string ModifierTags { get; set; }

        [XmlAttribute(AttributeName="name")]
        public string Name { get; set; }

        [XmlElement(ElementName="property")]
        public List<NeoModTest.ItemModsXML.Property> Property { get; set; }

        [XmlAttribute(AttributeName="type")]
        public string Type { get; set; }
    }
}

