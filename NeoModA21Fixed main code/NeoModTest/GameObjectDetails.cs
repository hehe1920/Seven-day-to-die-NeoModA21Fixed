namespace NeoModTest
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class GameObjectDetails
    {
        public List<GameObjectDetails> children;
        public List<string> components;
        public bool enabled;
        public int layer;
        public string localPosition;
        public string name;
        public string parent;
        public string position;

        public GameObjectDetails()
        {
            this.name = "";
            this.parent = "";
            this.enabled = false;
            this.layer = -1;
            this.position = "";
            this.localPosition = "";
            this.components = new List<string>();
            this.children = new List<GameObjectDetails>();
        }

        public GameObjectDetails(GameObject rootObject)
        {
            this.name = "";
            this.parent = "";
            this.enabled = false;
            this.layer = -1;
            this.position = "";
            this.localPosition = "";
            this.components = new List<string>();
            this.children = new List<GameObjectDetails>();
            this.name = rootObject.name;
            if (rootObject.transform.parent != null)
            {
                this.parent = rootObject.transform.parent.gameObject.name;
            }
            else
            {
                this.parent = "null";
            }
            this.enabled = rootObject.activeSelf;
            this.layer = rootObject.layer;
            this.position = rootObject.transform.position.ToString();
            this.localPosition = rootObject.transform.localPosition.ToString();
            Component[] components = rootObject.GetComponents<Component>();
            foreach (Component component in components)
            {
                if (component != null)
                {
                    this.components.Add(component.GetType().FullName);
                }
            }
            int childCount = rootObject.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                this.children.Add(new GameObjectDetails(rootObject.transform.GetChild(i).gameObject));
            }
        }

        private static string CreateXMLGameObject(GameObjectDetails obj)
        {
            string str = "";
            str = (((str + "<parent name=\"" + obj.parent + "\" />\r\n") + "<enabled value=\"" + obj.enabled.ToString() + "\" />\r\n") + "<layer value=\"" + obj.layer.ToString() + "\" />\r\n") + "<components>\r\n";
            foreach (string str2 in obj.components)
            {
                str = str + "<component name=\"" + str2 + "\" />\r\n";
            }
            str = str + "</components>\r\n";
            if (obj.children.Count <= 0)
            {
                return str;
            }
            str = str + "<children>\r\n";
            foreach (GameObjectDetails details in obj.children)
            {
                str = str + "<child name=\"" + details.name + "\">\r\n";
                str = str + CreateXMLGameObject(details);
                str = str + "</child>\r\n";
            }
            return (str + "</children>\r\n");
        }

        public static string XMLSerialize(List<GameObjectDetails> objectTree)
        {
            string str = "<?xml version=\"1.0\"?>\r\n";
            str = (str + "<GameObjects>\r\n") + "<Count value=\"" + objectTree.Count.ToString() + "\" />\r\n";
            foreach (GameObjectDetails details in objectTree)
            {
                str = str + "<GameObject name=\"" + details.name + "\">\r\n";
                str = str + CreateXMLGameObject(details);
                str = str + "</GameObject>\r\n";
            }
            return (str + "</GameObjects>");
        }
    }
}

