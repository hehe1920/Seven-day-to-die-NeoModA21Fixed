namespace NeoModTest.ItemModsXML
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="item_modifiers")]
    public class ItemModifiers
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<NeoModTest.ItemModsXML.ItemModifier> <ItemModifier>k__BackingField;

        [XmlElement(ElementName="item_modifier")]
        public List<NeoModTest.ItemModsXML.ItemModifier> ItemModifier { get; set; }
    }
}

