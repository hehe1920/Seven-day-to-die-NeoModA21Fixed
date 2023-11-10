namespace NeoModTest.ItemModsXML
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="effect_group")]
    public class EffectGroup
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private NeoModTest.ItemModsXML.DisplayValue <DisplayValue>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Name>k__BackingField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private List<NeoModTest.ItemModsXML.PassiveEffect> <PassiveEffect>k__BackingField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private List<NeoModTest.ItemModsXML.Requirement> <Requirement>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <Tiered>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<NeoModTest.ItemModsXML.TriggeredEffect> <TriggeredEffect>k__BackingField;

        [XmlElement(ElementName="display_value")]
        public NeoModTest.ItemModsXML.DisplayValue DisplayValue { get; set; }

        [XmlAttribute(AttributeName="name")]
        public string Name { get; set; }

        [XmlElement(ElementName="passive_effect")]
        public List<NeoModTest.ItemModsXML.PassiveEffect> PassiveEffect { get; set; }

        [XmlElement(ElementName="requirement")]
        public List<NeoModTest.ItemModsXML.Requirement> Requirement { get; set; }

        [XmlAttribute(AttributeName="tiered")]
        public bool Tiered { get; set; }

        [XmlElement(ElementName="triggered_effect")]
        public List<NeoModTest.ItemModsXML.TriggeredEffect> TriggeredEffect { get; set; }
    }
}

