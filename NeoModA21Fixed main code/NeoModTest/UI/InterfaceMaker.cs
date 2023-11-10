namespace NeoModTest.UI
{
    using NeoModTest.Utils;
    using System;
    using UnityEngine;

    public static class InterfaceMaker
    {
        private static Texture2D _boxBackground;
        private static GUISkin _customSkin = null;
        private static Texture2D _winBackground;

        private static GUISkin CreateSkin()
        {
            GUISkin target = UnityEngine.Object.Instantiate<GUISkin>(GUI.skin);
            UnityEngine.Object.DontDestroyOnLoad(target);
            _boxBackground = ResourceUtils.LoadTexture(ResourceUtils.GetEmbeddedResource("NeoModTest.UI.guisharp-box.png", null));
            UnityEngine.Object.DontDestroyOnLoad(_boxBackground);
            target.box.onNormal.background = null;
            target.box.normal.background = _boxBackground;
            target.box.normal.textColor = Color.white;
            _winBackground = ResourceUtils.LoadTexture(ResourceUtils.GetEmbeddedResource("NeoModTest.UI.guisharp-window.png", null));
            UnityEngine.Object.DontDestroyOnLoad(_winBackground);
            target.window.onNormal.background = null;
            target.window.normal.background = _winBackground;
            target.window.padding = new RectOffset(6, 6, 0x16, 6);
            target.window.border = new RectOffset(10, 10, 20, 10);
            target.window.normal.textColor = Color.white;
            target.button.padding = new RectOffset(4, 4, 3, 3);
            target.button.normal.textColor = Color.white;
            target.textField.normal.textColor = Color.white;
            target.label.normal.textColor = Color.white;
            return target;
        }

        public static void EatInputInRect(Rect eatRect)
        {
            if (eatRect.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
            {
                Input.ResetInputAxes();
            }
        }

        public static GUISkin CustomSkin
        {
            get
            {
                if (_customSkin == null)
                {
                    try
                    {
                        _customSkin = CreateSkin();
                    }
                    catch (Exception exception)
                    {
                        Debug.LogWarning("Could not load custom GUISkin - " + exception.Message);
                        _customSkin = GUI.skin;
                    }
                }
                return _customSkin;
            }
        }
    }
}

