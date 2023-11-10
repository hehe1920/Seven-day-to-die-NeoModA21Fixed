namespace NeoModTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using UnityEngine;
    using UnityEngine.UI;

    public static class LocalExtensions
    {
        public static readonly List<AccessModifier> AccessModifiers;
        public static Dictionary<MethodInfo, ParameterInfo[]> ParametersOfMethods;

        static LocalExtensions()
        {
            List<AccessModifier> list1 = new List<AccessModifier> {
                AccessModifier.Private,
                AccessModifier.Protected,
                AccessModifier.ProtectedInternal,
                AccessModifier.Internal,
                AccessModifier.Public
            };
            AccessModifiers = list1;
            ParametersOfMethods = new Dictionary<MethodInfo, ParameterInfo[]>();
        }

        public static AccessModifier Accessmodifier(this MethodInfo methodInfo)
        {
            if (methodInfo.IsPrivate)
            {
                return AccessModifier.Private;
            }
            if (methodInfo.IsFamily)
            {
                return AccessModifier.Protected;
            }
            if (methodInfo.IsFamilyOrAssembly)
            {
                return AccessModifier.ProtectedInternal;
            }
            if (methodInfo.IsAssembly)
            {
                return AccessModifier.Internal;
            }
            if (!methodInfo.IsPublic)
            {
                throw new ArgumentException("Did not find access modifier", "methodInfo");
            }
            return AccessModifier.Public;
        }

        public static AccessModifier Accessmodifier(this PropertyInfo propertyInfo)
        {
            if (propertyInfo.SetMethod == null)
            {
                return propertyInfo.GetMethod.Accessmodifier();
            }
            if (propertyInfo.GetMethod == null)
            {
                return propertyInfo.SetMethod.Accessmodifier();
            }
            int num = Math.Max(AccessModifiers.IndexOf(propertyInfo.GetMethod.Accessmodifier()), AccessModifiers.IndexOf(propertyInfo.SetMethod.Accessmodifier()));
            return AccessModifiers[num];
        }

        public static string Dump(this System.Type type, int indentsize = 4, bool writetofile = true)
        {
            string contents = "Static Type Dump:\r\n";
            string str2 = " ";
            if (indentsize > 0)
            {
                for (int i = 1; i <= indentsize; i++)
                {
                    str2 = str2 + " ";
                }
                if (type == null)
                {
                    return "Type is null";
                }
            }
            try
            {
                contents = contents + "Properties:\r\n";
                Dictionary<string, string> typeProperties = GetTypeProperties(type);
                foreach (KeyValuePair<string, string> pair in typeProperties)
                {
                    if (pair.Key > null)
                    {
                        string[] textArray1 = new string[] { contents, str2, pair.Key, ": ", pair.Value, "\r\n" };
                        contents = string.Concat(textArray1);
                    }
                }
            }
            catch
            {
            }
            try
            {
                contents = contents + "Fields:\r\n";
                Dictionary<string, string> typeFields = GetTypeFields(type);
                foreach (KeyValuePair<string, string> pair2 in typeFields)
                {
                    if (pair2.Key > null)
                    {
                        string[] textArray2 = new string[] { contents, str2, pair2.Key, ": ", pair2.Value, "\r\n" };
                        contents = string.Concat(textArray2);
                    }
                }
            }
            catch
            {
            }
            try
            {
                contents = contents + "Methods:\r\n";
                List<string> typeMethods = GetTypeMethods(type);
                foreach (string str4 in typeMethods)
                {
                    if (str4 > null)
                    {
                        contents = contents + str2 + str4 + "\r\n";
                    }
                }
            }
            catch
            {
            }
            if (writetofile)
            {
                try
                {
                    File.WriteAllText(TrainerMenu.baseDirectory + @"\TypeDump-" + type.FullName + ".txt", contents, Encoding.ASCII);
                    Debug.Log("Dump File Written To: " + TrainerMenu.baseDirectory + @"\TypeDump-" + type.FullName + ".txt");
                }
                catch (Exception exception)
                {
                    Debug.LogError("Dump Type Error: " + exception.Message);
                }
            }
            return contents;
        }

        public static string Dump(this object obj, int indentsize = 4, bool writetofile = true, string fileindex = "")
        {
            string contents = "Object Dump:\r\n";
            string str2 = " ";
            if (indentsize > 0)
            {
                for (int i = 1; i <= indentsize; i++)
                {
                    str2 = str2 + " ";
                }
                if (obj.Equals(typeof(Nullable)))
                {
                    return "Object is null";
                }
            }
            try
            {
                contents = contents + "Properties:\r\n";
                Dictionary<string, string> objectProperties = GetObjectProperties(obj);
                foreach (KeyValuePair<string, string> pair in objectProperties)
                {
                    if (pair.Key > null)
                    {
                        string[] textArray1 = new string[] { contents, str2, pair.Key, ": ", pair.Value, "\r\n" };
                        contents = string.Concat(textArray1);
                    }
                }
            }
            catch
            {
            }
            try
            {
                contents = contents + "Fields:\r\n";
                Dictionary<string, string> objectFields = GetObjectFields(obj);
                foreach (KeyValuePair<string, string> pair2 in objectFields)
                {
                    if (pair2.Key > null)
                    {
                        string[] textArray2 = new string[] { contents, str2, pair2.Key, ": ", pair2.Value, "\r\n" };
                        contents = string.Concat(textArray2);
                    }
                }
            }
            catch
            {
            }
            try
            {
                contents = contents + "Methods:\r\n";
                List<string> objectMethods = GetObjectMethods(obj);
                foreach (string str4 in objectMethods)
                {
                    if (str4 > null)
                    {
                        contents = contents + str2 + str4 + "\r\n";
                    }
                }
            }
            catch
            {
            }
            if (writetofile)
            {
                try
                {
                    File.WriteAllText(TrainerMenu.baseDirectory + @"\ObjectDump-" + obj.GetType().FullName + fileindex + ".txt", contents, Encoding.ASCII);
                    Debug.Log("Dump File Written To: " + TrainerMenu.baseDirectory + @"\ObjectDump-" + obj.GetType().FullName + fileindex + ".txt");
                }
                catch (Exception exception)
                {
                    Debug.LogError("Dump Object Error: " + exception.Message);
                }
            }
            return contents;
        }

        public static void DumpAllText(this GameObject obj)
        {
            string contents = "";
            Text[] componentsInChildren = obj.GetComponentsInChildren<Text>();
            contents = contents + "Texts CNT: " + componentsInChildren.Length.ToString() + "\r\n";
            foreach (Text text in componentsInChildren)
            {
                string[] textArray1 = new string[] { contents, "GameObj: ", text.transform.parent.gameObject.name, "> ", text.text.Replace("\n", " "), "\r\n" };
                contents = string.Concat(textArray1);
            }
            File.WriteAllText(TrainerMenu.baseDirectory + @"\" + obj.name + "-" + obj.GetHashCode().ToString() + "-TextsDump.txt", contents);
            Debug.Log("Dumped All Texts For " + obj.name + " To: " + TrainerMenu.baseDirectory + @"\" + obj.name + "-" + obj.GetHashCode().ToString() + "-TextsDump.txt");
        }

        public static void DumpGameObjectXML(this GameObject obj)
        {
            if (obj != null)
            {
                List<GameObjectDetails> objectTree = new List<GameObjectDetails> {
                    new GameObjectDetails(obj)
                };
                if (!Directory.Exists(TrainerMenu.baseDirectory + @"\OBJECT_DUMPS\xml\"))
                {
                    Directory.CreateDirectory(TrainerMenu.baseDirectory + @"\OBJECT_DUMPS\xml\");
                }
                File.WriteAllText(TrainerMenu.baseDirectory + @"\OBJECT_DUMPS\xml\" + obj.name + ".xml", GameObjectDetails.XMLSerialize(objectTree));
            }
        }

        public static ParameterInfo[] GetCachedParemeters(this MethodInfo mo)
        {
            ParameterInfo[] parameters;
            if (!ParametersOfMethods.TryGetValue(mo, out parameters))
            {
                parameters = mo.GetParameters();
                ParametersOfMethods[mo] = parameters;
            }
            return parameters;
        }

        private static Dictionary<string, string> GetObjectFields(object obj)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (!obj.Equals(typeof(Nullable)))
            {
                System.Type type = obj.GetType();
                foreach (FieldInfo info in type.GetFields(TrainerMenu.fieldBindingFlags))
                {
                    try
                    {
                        object obj2 = info.GetValue(obj);
                        string str = (obj2 == null) ? " " : obj2.ToString();
                        dictionary.Add(info.Name, str);
                    }
                    catch (Exception exception)
                    {
                        Debug.LogError("ERROR DUMPIMG FIELD: (" + info.Name + ") - " + exception.Message);
                    }
                }
            }
            return dictionary;
        }

        private static List<string> GetObjectMethods(object obj)
        {
            List<string> list = new List<string>();
            if (!obj.Equals(typeof(Nullable)))
            {
                System.Type type = obj.GetType();
                foreach (MethodInfo info in type.GetMethods(TrainerMenu.defaultBindingFlags))
                {
                    try
                    {
                        System.Type returnType = info.ReturnType;
                        string[] textArray1 = new string[] { info.Accessmodifier().ToString(), " ", returnType.Name.ToString(), " ", info.Name, " (" };
                        string item = string.Concat(textArray1);
                        IOrderedEnumerable<ParameterInfo> enumerable = from obj2 in info.GetParameters()
                            orderby obj2.Position
                            select obj2;
                        foreach (ParameterInfo info2 in enumerable)
                        {
                            string[] textArray2 = new string[] { item, info2.ParameterType.Name, " ", info2.Name, ", " };
                            item = string.Concat(textArray2);
                        }
                        if (item.EndsWith(", "))
                        {
                            item = item.Remove(item.Length - 2);
                        }
                        item = item + ")";
                        list.Add(item);
                    }
                    catch (Exception exception)
                    {
                        Debug.LogError("ERROR DUMPIMG METHOD: (" + info.Name + ") - " + exception.Message);
                    }
                }
            }
            return list;
        }

        private static Dictionary<string, string> GetObjectProperties(object obj)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (!obj.Equals(typeof(Nullable)))
            {
                System.Type type = obj.GetType();
                foreach (PropertyInfo info in type.GetProperties(TrainerMenu.propertyBindingFlags))
                {
                    try
                    {
                        object obj2 = info.GetValue(obj, new object[0]);
                        string str = "";
                        if (!obj2.Equals(typeof(Nullable)))
                        {
                            str = obj2.ToString();
                        }
                        dictionary.Add(info.Accessmodifier().ToString() + " " + info.Name, str);
                    }
                    catch (Exception exception)
                    {
                        Debug.LogError("ERROR DUMPIMG PROPERTY: (" + info.Name + ") - " + exception.Message);
                    }
                }
            }
            return dictionary;
        }

        private static Dictionary<string, string> GetTypeFields(System.Type type)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (!type.Equals(typeof(Nullable)))
            {
                foreach (FieldInfo info in type.GetFields(TrainerMenu.bindflags))
                {
                    try
                    {
                        object obj2 = info.GetValue(type);
                        string str = (obj2 == null) ? " " : obj2.ToString();
                        dictionary.Add(info.Name, str);
                    }
                    catch (Exception exception)
                    {
                        Debug.LogError("ERROR DUMPIMG FIELD: (" + info.Name + ") - " + exception.Message);
                    }
                }
            }
            return dictionary;
        }

        private static List<string> GetTypeMethods(System.Type type)
        {
            List<string> list = new List<string>();
            if (!type.Equals(typeof(Nullable)))
            {
                foreach (MethodInfo info in type.GetMethods(TrainerMenu.bindflags))
                {
                    try
                    {
                        System.Type returnType = info.ReturnType;
                        string[] textArray1 = new string[] { info.Accessmodifier().ToString(), " ", returnType.Name.ToString(), " ", info.Name, " (" };
                        string item = string.Concat(textArray1);
                        IOrderedEnumerable<ParameterInfo> enumerable = from obj2 in info.GetParameters()
                            orderby obj2.Position
                            select obj2;
                        foreach (ParameterInfo info2 in enumerable)
                        {
                            string[] textArray2 = new string[] { item, info2.ParameterType.Name, " ", info2.Name, ", " };
                            item = string.Concat(textArray2);
                        }
                        if (item.EndsWith(", "))
                        {
                            item = item.Remove(item.Length - 2);
                        }
                        item = item + ")";
                        list.Add(item);
                    }
                    catch (Exception exception)
                    {
                        Debug.LogError("ERROR DUMPIMG METHOD: (" + info.Name + ") - " + exception.Message);
                    }
                }
            }
            return list;
        }

        private static Dictionary<string, string> GetTypeProperties(System.Type type)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (!type.Equals(typeof(Nullable)))
            {
                foreach (PropertyInfo info in type.GetProperties(TrainerMenu.propertyBindingFlags))
                {
                    try
                    {
                        object obj2 = info.GetValue(type, new object[0]);
                        string str = (obj2 == null) ? " " : obj2.ToString();
                        dictionary.Add(info.Accessmodifier().ToString() + " " + info.Name, str);
                    }
                    catch (Exception exception)
                    {
                        Debug.LogError("ERROR DUMPIMG PROPERTY: (" + info.Name + ") - " + exception.Message);
                    }
                }
            }
            return dictionary;
        }

        public static bool HasComponent<T>(this GameObject flag) where T: Component => 
            (flag?.GetComponent<T>() != null);

        public static void Move<T>(this List<T> list, int oldIndex, int newIndex)
        {
            T item = list[oldIndex];
            list.RemoveAt(oldIndex);
            if (newIndex > oldIndex)
            {
                newIndex--;
            }
            list.Insert(newIndex, item);
        }

        public static void Move<T>(this List<T> list, T item, int newIndex)
        {
            if (item > null)
            {
                int index = list.IndexOf(item);
                if (index > -1)
                {
                    list.RemoveAt(index);
                    if (newIndex > index)
                    {
                        newIndex--;
                    }
                    list.Insert(newIndex, item);
                }
            }
        }

        public static string ToFixedString(this string value, int length, char appendChar = ' ')
        {
            int num = value.Length;
            int count = (length == num) ? 0 : (length - num);
            return ((count == 0) ? value : ((count > 0) ? (value + new string(' ', count)) : new string(new string(value.ToCharArray().Reverse<char>().ToArray<char>()).Substring(count * -1, value.Length - (count * -1)).ToCharArray().Reverse<char>().ToArray<char>())));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LocalExtensions.<>c <>9 = new LocalExtensions.<>c();
            public static Func<ParameterInfo, int> <>9__11_0;
            public static Func<ParameterInfo, int> <>9__7_0;

            internal int <GetObjectMethods>b__7_0(ParameterInfo obj2) => 
                obj2.Position;

            internal int <GetTypeMethods>b__11_0(ParameterInfo obj2) => 
                obj2.Position;
        }

        public enum AccessModifier
        {
            Private,
            Protected,
            ProtectedInternal,
            Internal,
            Public
        }
    }
}

