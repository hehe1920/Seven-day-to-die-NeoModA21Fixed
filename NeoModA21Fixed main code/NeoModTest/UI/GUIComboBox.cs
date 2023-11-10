namespace NeoModTest.UI
{
    using NeoModTest;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public static class GUIComboBox
    {
        private const string ExpandDownButtonText = " ▼ ";
        private static PopupWindow popupWindow;

        public static int Box(int itemIndex, string[] items, string callerId)
        {
            int num3;
            GUIStyle style = new GUIStyle {
                normal = { textColor = Color.cyan },
                alignment = TextAnchor.LowerCenter
            };
            switch (items.Length)
            {
                case 0:
                    return -1;

                case 1:
                    GUILayout.Label(items[0], style, Array.Empty<GUILayoutOption>());
                    return 0;
            }
            if (((popupWindow != null) && (callerId == popupWindow.OwnerId)) && popupWindow.CloseAndGetSelection(out num3))
            {
                itemIndex = num3;
                UnityEngine.Object.Destroy(popupWindow);
                popupWindow = null;
            }
            Vector2 popupDimensions = GetPopupDimensions(items);
            GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.Width(popupDimensions.x), GUILayout.Height(22f) };
            GUILayout.Box(items[itemIndex], style, options);
            Vector2 position = GUIUtility.GUIToScreenPoint(GUILayoutUtility.GetLastRect().position);
            GUILayoutOption[] optionArray2 = new GUILayoutOption[] { GUILayout.Width(24f) };
            if (GUILayout.Button(" ▼ ", optionArray2) && EnsurePopupWindow())
            {
                popupWindow.Show(callerId, items, itemIndex, position, popupDimensions);
            }
            return itemIndex;
        }

        private static bool EnsurePopupWindow()
        {
            if (popupWindow != null)
            {
                return true;
            }
            TrainerMenu menu = UnityEngine.Object.FindObjectOfType<TrainerMenu>();
            if (menu == null)
            {
                return false;
            }
            if (menu.GetComponent<PopupWindow>() == null)
            {
                popupWindow = menu.gameObject.AddComponent<PopupWindow>();
            }
            return (popupWindow != null);
        }

        private static Vector2 GetPopupDimensions(string[] items)
        {
            float x = 250f;
            float num2 = 0f;
            for (int i = 0; i < items.Length; i++)
            {
                Vector2 vector = GUI.skin.button.CalcSize(new GUIContent(items[i]));
                if (vector.x > x)
                {
                    x = vector.x;
                }
                num2 += vector.y;
            }
            return new Vector2(x + 36f, num2 + 36f);
        }

        private class PopupWindow : MonoBehaviour
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
            private string <OwnerId>k__BackingField;
            private readonly GUIStyle hoverStyle = CreateHoverStyle();
            private const float MaxPopupHeight = 400f;
            private Vector2? mouseClickPoint;
            private string[] popupItems;
            private Rect popupRect;
            private Vector2 popupScrollPosition = Vector2.zero;
            private readonly int popupWindowId = GUIUtility.GetControlID(FocusType.Passive);
            private bool readyToClose;
            private int selectedIndex;
            private static readonly GUIStyle WindowStyle = CreateWindowStyle();

            private void Close()
            {
                this.OwnerId = null;
                this.popupItems = null;
                this.selectedIndex = -1;
                this.mouseClickPoint = null;
            }

            public bool CloseAndGetSelection(out int currentIndex)
            {
                if (this.readyToClose)
                {
                    currentIndex = this.selectedIndex;
                    this.Close();
                    return true;
                }
                currentIndex = -1;
                return false;
            }

            private static GUIStyle CreateHoverStyle()
            {
                GUIStyle style = new GUIStyle(GUI.skin.label) {
                    hover = { textColor = Color.yellow }
                };
                Texture2D textured = new Texture2D(1, 1);
                textured.SetPixel(0, 0, new Color());
                textured.Apply();
                style.hover.background = textured;
                style.font = GUI.skin.font;
                style.alignment = TextAnchor.MiddleCenter;
                return style;
            }

            private static GUIStyle CreateWindowStyle()
            {
                Texture2D textured = new Texture2D(0x10, 0x10, TextureFormat.RGBA32, false) {
                    wrapMode = TextureWrapMode.Clamp
                };
                for (int i = 0; i < textured.width; i++)
                {
                    for (int j = 0; j < textured.height; j++)
                    {
                        if ((((i == 0) || (i == (textured.width - 1))) || (j == 0)) || (j == (textured.height - 1)))
                        {
                            textured.SetPixel(i, j, new Color(0f, 0f, 0f, 1f));
                        }
                        else
                        {
                            textured.SetPixel(i, j, new Color(0.05f, 0.05f, 0.05f, 0.95f));
                        }
                    }
                }
                textured.Apply();
                GUIStyle style = new GUIStyle(GUI.skin.window) {
                    normal = { background = textured },
                    onNormal = { background = textured }
                };
                style.border.top = style.border.bottom;
                style.padding.top = style.padding.bottom;
                return style;
            }

            public void OnGUI()
            {
                if (this.OwnerId > null)
                {
                    GUI.ModalWindow(this.popupWindowId, this.popupRect, new UnityEngine.GUI.WindowFunction(this.WindowFunction), string.Empty, WindowStyle);
                }
            }

            public void Show(string ownerId, string[] items, int currentIndex, Vector2 position, Vector2 popupSize)
            {
                this.OwnerId = ownerId;
                this.popupItems = items;
                this.selectedIndex = currentIndex;
                this.popupRect = new Rect(position, new Vector2(popupSize.x, Mathf.Min(400f, popupSize.y)));
                this.popupScrollPosition = new Vector2();
                this.mouseClickPoint = null;
                this.readyToClose = false;
            }

            public void Update()
            {
                if (this.OwnerId > null)
                {
                    if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) || Input.GetMouseButtonDown(2))
                    {
                        Vector3 mousePosition = Input.mousePosition;
                        mousePosition.y = Screen.height - mousePosition.y;
                        this.mouseClickPoint = new Vector2?(mousePosition);
                    }
                    else
                    {
                        this.mouseClickPoint = null;
                    }
                }
            }

            private void WindowFunction(int windowId)
            {
                if (this.OwnerId > null)
                {
                    this.popupScrollPosition = GUILayout.BeginScrollView(this.popupScrollPosition, false, false, Array.Empty<GUILayoutOption>());
                    int selectedIndex = this.selectedIndex;
                    this.selectedIndex = GUILayout.SelectionGrid(this.selectedIndex, this.popupItems, 1, this.hoverStyle, Array.Empty<GUILayoutOption>());
                    GUILayout.EndScrollView();
                    if ((selectedIndex != this.selectedIndex) || (this.mouseClickPoint.HasValue && !this.popupRect.Contains(this.mouseClickPoint.Value)))
                    {
                        this.readyToClose = true;
                    }
                }
            }

            public string OwnerId { get; private set; }
        }
    }
}

