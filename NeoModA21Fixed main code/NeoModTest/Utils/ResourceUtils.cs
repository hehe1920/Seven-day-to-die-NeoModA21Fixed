namespace NeoModTest.Utils
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public static class ResourceUtils
    {
        public static byte[] GetEmbeddedResource(string resourceFileName, Assembly containingAssembly = null)
        {
            if (containingAssembly == null)
            {
                containingAssembly = Assembly.GetCallingAssembly();
            }
            string name = containingAssembly.GetManifestResourceNames().Single<string>(str => str.EndsWith(resourceFileName));
            using (Stream stream = containingAssembly.GetManifestResourceStream(name))
            {
                byte[] buffer = stream.ReadAllBytes();
                if (buffer.Length == 0)
                {
                    Debug.LogWarning(string.Format("The resource %1 was not found", resourceFileName));
                }
                return buffer;
            }
        }

        public static Texture2D LoadTexture(byte[] texData)
        {
            Texture2D textured = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            System.Type[] types = new System.Type[] { typeof(byte[]) };
            MethodInfo method = typeof(Texture2D).GetMethod("LoadImage", types);
            try
            {
                if (method.Name.Contains("LoadImage"))
                {
                    object[] parameters = new object[] { texData };
                    method.Invoke(textured, parameters);
                }
            }
            catch
            {
                System.Type type = System.Type.GetType("UnityEngine.ImageConversion, UnityEngine.ImageConversionModule");
                if (type == null)
                {
                    throw new ArgumentNullException("converter");
                }
                System.Type[] typeArray2 = new System.Type[] { typeof(Texture2D), typeof(byte[]) };
                MethodInfo info2 = type.GetMethod("LoadImage", typeArray2);
                if (info2 == null)
                {
                    throw new ArgumentNullException("converterMethod");
                }
                object[] objArray2 = new object[] { textured, texData };
                info2.Invoke(null, objArray2);
            }
            return textured;
        }

        public static byte[] ReadAllBytes(this Stream input)
        {
            byte[] buffer = new byte[0x4000];
            using (MemoryStream stream = new MemoryStream())
            {
                int num;
                while ((num = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, num);
                }
                return stream.ToArray();
            }
        }
    }
}

