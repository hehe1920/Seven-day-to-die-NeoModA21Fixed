namespace NeoModTest
{
    using System;
    using UnityEngine;

    public class ESP : MonoBehaviour
    {
        private Color blackCol;
        public static bool crosshair = false;
        private Color crosshairCol;
        private readonly float crosshairScale = 14f;
        private Color entityBoxCol;
        public static bool fovCircle = false;
        private readonly float lineThickness = 1.75f;
        public static Camera mainCam;
        public static bool playerBox = true;
        public static bool playerCornerBox = false;
        public static bool playerHealth = true;
        public static bool playerName = true;
        public static bool zombieBox = true;
        public static bool zombieCornerBox = false;
        public static bool zombieHealth = true;
        public static bool zombieLineESP = false;
        public static bool zombieName = true;

        private void OnGUI()
        {
            if (UnityEngine.Event.current.type == UnityEngine.EventType.Repaint)
            {
                if (mainCam == 0)
                {
                    mainCam = Camera.main;
                }
                if (fovCircle)
                {
                    ESPUtils.DrawCircle(Color.black, new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2)), 149f);
                    ESPUtils.DrawCircle(Color.black, new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2)), 151f);
                    ESPUtils.DrawCircle((Color) new Color32(30, 0x90, 0xff, 0xff), new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2)), 150f);
                }
                if (crosshair)
                {
                    Vector2 start = new Vector2((Screen.width / 2) - this.crosshairScale, (float) (Screen.height / 2));
                    Vector2 end = new Vector2((Screen.width / 2) + this.crosshairScale, (float) (Screen.height / 2));
                    Vector2 vector3 = new Vector2((float) (Screen.width / 2), (Screen.height / 2) - this.crosshairScale);
                    Vector2 vector4 = new Vector2((float) (Screen.width / 2), (Screen.height / 2) + this.crosshairScale);
                    ESPUtils.DrawLine(start, end, this.crosshairCol, this.lineThickness);
                    ESPUtils.DrawLine(vector3, vector4, this.crosshairCol, this.lineThickness);
                }
                if ((Objects.zombieList.Count > 0) && ((zombieName || zombieBox) || zombieHealth))
                {
                    foreach (EntityZombie zombie in Objects.zombieList)
                    {
                        if ((zombie != null) && zombie.IsAlive())
                        {
                            Vector3 position = mainCam.WorldToScreenPoint(zombie.transform.position);
                            if (ESPUtils.IsOnScreen(position))
                            {
                                Vector3 vector6 = mainCam.WorldToScreenPoint(zombie.emodel.GetHeadTransform().position);
                                float y = Mathf.Abs((float) (vector6.y - position.y));
                                float x = position.x - (y * 0.3f);
                                float num3 = Screen.height - vector6.y;
                                if (zombieBox)
                                {
                                    ESPUtils.OutlineBox(new Vector2(x - 1f, num3 - 1f), new Vector2((y / 2f) + 2f, y + 2f), this.blackCol);
                                    ESPUtils.OutlineBox(new Vector2(x, num3), new Vector2(y / 2f, y), this.entityBoxCol);
                                    ESPUtils.OutlineBox(new Vector2(x + 1f, num3 + 1f), new Vector2((y / 2f) - 2f, y - 2f), this.blackCol);
                                }
                                else if (zombieCornerBox)
                                {
                                    ESPUtils.CornerBox(new Vector2(vector6.x, num3), y / 2f, y, 2f, this.entityBoxCol, true);
                                }
                                if (zombieLineESP)
                                {
                                    ESPUtils.DrawLine(mainCam.WorldToScreenPoint(Objects.localPlayer.emodel.GetBellyPosition()), vector6, Color.green, 5f);
                                }
                                if (zombieName)
                                {
                                    ESPUtils.DrawString(new Vector2(position.x, (Screen.height - position.y) + 8f), zombie.EntityName.Replace("zombie", "Zombie_"), Color.red, true, 12, FontStyle.Normal, 1);
                                }
                                if (zombieHealth)
                                {
                                    float health = zombie.Health;
                                    int maxHealth = zombie.GetMaxHealth();
                                    float num6 = health / ((float) maxHealth);
                                    float height = y * num6;
                                    Color healthColour = ESPUtils.GetHealthColour(health, (float) maxHealth);
                                    ESPUtils.RectFilled(x - 5f, num3, 4f, y, this.blackCol);
                                    ESPUtils.RectFilled(x - 4f, ((num3 + y) - height) - 1f, 2f, height, healthColour);
                                }
                            }
                        }
                    }
                }
                if ((Objects.PlayerList.Count > 1) && ((playerName || playerBox) || playerHealth))
                {
                    foreach (EntityPlayer player in Objects.PlayerList)
                    {
                        if (((player != null) && (player != Objects.localPlayer)) && player.IsAlive())
                        {
                            Vector3 vector8 = mainCam.WorldToScreenPoint(player.transform.position);
                            if (ESPUtils.IsOnScreen(vector8))
                            {
                                Vector3 vector9 = mainCam.WorldToScreenPoint(player.emodel.GetHeadTransform().position);
                                float num8 = Mathf.Abs((float) (vector9.y - vector8.y));
                                float num9 = vector8.x - (num8 * 0.3f);
                                float num10 = Screen.height - vector9.y;
                                if (playerBox)
                                {
                                    ESPUtils.OutlineBox(new Vector2(num9 - 1f, num10 - 1f), new Vector2((num8 / 2f) + 2f, num8 + 2f), this.blackCol);
                                    ESPUtils.OutlineBox(new Vector2(num9, num10), new Vector2(num8 / 2f, num8), this.entityBoxCol);
                                    ESPUtils.OutlineBox(new Vector2(num9 + 1f, num10 + 1f), new Vector2((num8 / 2f) - 2f, num8 - 2f), this.blackCol);
                                }
                                else if (playerCornerBox)
                                {
                                    ESPUtils.CornerBox(new Vector2(vector9.x, num10), num8 / 2f, num8, 2f, this.entityBoxCol, true);
                                }
                                if (playerName)
                                {
                                    ESPUtils.DrawString(new Vector2(vector8.x, (Screen.height - vector8.y) + 8f), player.EntityName, Color.red, true, 12, FontStyle.Normal, 1);
                                }
                                if (playerHealth)
                                {
                                    float num11 = player.Health;
                                    int num12 = player.GetMaxHealth();
                                    float num13 = num11 / ((float) num12);
                                    float num14 = num8 * num13;
                                    Color color = ESPUtils.GetHealthColour(num11, (float) num12);
                                    ESPUtils.RectFilled(num9 - 5f, num10, 4f, num8, this.blackCol);
                                    ESPUtils.RectFilled(num9 - 4f, ((num10 + num8) - num14) - 1f, 2f, num14, color);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Start()
        {
            mainCam = Camera.main;
            this.blackCol = new Color(0f, 0f, 0f, 120f);
            this.entityBoxCol = new Color(0.42f, 0.36f, 0.9f, 1f);
            this.crosshairCol = (Color) new Color32(30, 0x90, 0xff, 0xff);
        }

        private void Update()
        {
            if (zombieCornerBox)
            {
                zombieBox = false;
            }
            else if (zombieBox && zombieCornerBox)
            {
                zombieCornerBox = false;
            }
            if (playerCornerBox)
            {
                playerBox = false;
            }
            else if (playerBox && playerCornerBox)
            {
                playerCornerBox = false;
            }
            if (Objects.localPlayer != null)
            {
                Objects.localPlayer.weaponCrossHairAlpha = crosshair ? 0f : 255f;
            }
        }
    }
}

