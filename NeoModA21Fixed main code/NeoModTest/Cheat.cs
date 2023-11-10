namespace NeoModTest
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class Cheat : MonoBehaviour
    {
        private int _Color;
        public static bool aimbot;
        public static bool chams;
        private Material chamsMaterial;
        public static bool infiniteAmmo;
        private float lastChamTime;
        public static bool magicBullet;
        public static bool noWeaponBob;
        public static bool speed;

        private void Aimbot()
        {
            float num = 9999f;
            Vector2 zero = Vector2.zero;
            foreach (EntityZombie zombie in Objects.zombieList)
            {
                if ((zombie != null) && zombie.IsAlive())
                {
                    Vector3 bellyPosition = zombie.emodel.GetBellyPosition();
                    Vector3 position = ESP.mainCam.WorldToScreenPoint(bellyPosition);
                    if ((Vector2.Distance(new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2)), new Vector2(position.x, position.y)) <= 150f) && ESPUtils.IsOnScreen(position))
                    {
                        float num2 = Math.Abs(Vector2.Distance(new Vector2(position.x, Screen.height - position.y), new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2))));
                        if (num2 < num)
                        {
                            num = num2;
                            zero = new Vector2(position.x, Screen.height - position.y);
                        }
                    }
                }
            }
            if (Objects.PlayerList.Count > 1)
            {
                foreach (EntityPlayer player in Objects.PlayerList)
                {
                    if (((player != null) && player.IsAlive()) && (player != Objects.localPlayer))
                    {
                        Vector3 vector4 = player.emodel.GetBellyPosition();
                        Vector3 vector5 = ESP.mainCam.WorldToScreenPoint(vector4);
                        if ((Vector2.Distance(new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2)), new Vector2(vector5.x, vector5.y)) <= 150f) && ESPUtils.IsOnScreen(vector5))
                        {
                            float num3 = Math.Abs(Vector2.Distance(new Vector2(vector5.x, Screen.height - vector5.y), new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2))));
                            if (num3 < num)
                            {
                                num = num3;
                                zero = new Vector2(vector5.x, Screen.height - vector5.y);
                            }
                        }
                    }
                }
            }
            if (zero != Vector2.zero)
            {
                double num4 = zero.x - (((float) Screen.width) / 2f);
                double num5 = zero.y - (((float) Screen.height) / 2f);
                num4 /= 10.0;
                num5 /= 10.0;
                mouse_event(1, (int) num4, (int) num5, 0, 0);
            }
        }

        private void ApplyChams(Entity entity, Color color)
        {
            foreach (Renderer renderer1 in entity.GetComponentsInChildren<Renderer>())
            {
                renderer1.material = this.chamsMaterial;
                renderer1.material.SetColor(this._Color, color);
            }
        }

        private void MagicBullet()
        {
            EntityZombie killedEntity = null;
            EntityPlayer player = null;
            foreach (EntityZombie zombie2 in Objects.zombieList)
            {
                if ((zombie2 != null) && zombie2.IsAlive())
                {
                    Vector3 position = zombie2.emodel.GetHeadTransform().position;
                    Vector3 vector2 = ESP.mainCam.WorldToScreenPoint(position);
                    if (Vector2.Distance(new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2)), new Vector2(vector2.x, vector2.y)) <= 120f)
                    {
                        killedEntity = zombie2;
                    }
                }
            }
            foreach (EntityPlayer player2 in Objects.PlayerList)
            {
                if (((player2 != null) && player2.IsAlive()) && (player2 != Objects.localPlayer))
                {
                    Vector3 vector3 = player2.emodel.GetHeadTransform().position;
                    Vector3 vector4 = ESP.mainCam.WorldToScreenPoint(vector3);
                    if (Vector2.Distance(new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2)), new Vector2(vector4.x, vector4.y)) <= 120f)
                    {
                        player = player2;
                    }
                }
            }
            if (player != null)
            {
                DamageSource source = new DamageSource(EnumDamageSource.External, EnumDamageTypes.Concuss);
                killedEntity.DamageEntity(source, 100, false, 1f);
                DamageResponse response = new DamageResponse {
                    Fatal = true
                };
                player.Kill(response);
            }
            if (killedEntity != null)
            {
                DamageSource source1 = new DamageSource(EnumDamageSource.External, EnumDamageTypes.Concuss) {
                    CreatorEntityId = Objects.localPlayer.entityId
                };
                killedEntity.DamageEntity(source1, 100, false, 1f);
                killedEntity.AwardKill(Objects.localPlayer);
                Objects.localPlayer.AddKillXP(killedEntity, 1f);
            }
        }

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        private void Start()
        {
            this.lastChamTime = Time.time + 10f;
            Material material1 = new Material(Shader.Find("Hidden/Internal-Colored")) {
                hideFlags = HideFlags.HideAndDontSave
            };
            this.chamsMaterial = material1;
            this._Color = Shader.PropertyToID("_Color");
            this.chamsMaterial.SetInt("_SrcBlend", 5);
            this.chamsMaterial.SetInt("_DstBlend", 10);
            this.chamsMaterial.SetInt("_Cull", 0);
            this.chamsMaterial.SetInt("_ZTest", 8);
            this.chamsMaterial.SetInt("_ZWrite", 0);
            this.chamsMaterial.SetColor(this._Color, Color.magenta);
        }

        private void Update()
        {
            if (noWeaponBob && (Objects.localPlayer != null))
            {
                vp_FPWeapon weapon = Objects.localPlayer.vp_FPWeapon;
                if (weapon != null)
                {
                    weapon.BobRate = Vector4.zero;
                    weapon.ShakeAmplitude = Vector3.zero;
                    weapon.RenderingFieldOfView = 120f;
                    weapon.StepForceScale = 0f;
                }
            }
            if (Input.GetKeyDown(KeyCode.PageDown))
            {
                if (Objects.localPlayer == null)
                {
                    return;
                }
                Inventory inventory = Objects.localPlayer.inventory;
                if (inventory != null)
                {
                    ItemActionAttack holdingGun = inventory.GetHoldingGun();
                    if (holdingGun != null)
                    {
                        holdingGun.InfiniteAmmo = !holdingGun.InfiniteAmmo;
                        infiniteAmmo = holdingGun.InfiniteAmmo;
                    }
                }
            }
            if (Input.GetKey(KeyCode.Mouse1) && magicBullet)
            {
                this.MagicBullet();
            }
            if ((Input.GetKey(KeyCode.Mouse1) && (Objects.zombieList.Count > 0)) && aimbot)
            {
                this.Aimbot();
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                speed = !speed;
                Time.timeScale = speed ? 6f : 1f;
            }
            if ((Time.time >= this.lastChamTime) && chams)
            {
                foreach (Entity entity in UnityEngine.Object.FindObjectsOfType<Entity>())
                {
                    if (entity != null)
                    {
                        switch (entity.entityType)
                        {
                            case EntityType.Unknown:
                                this.ApplyChams(entity, Color.white);
                                break;

                            case EntityType.Player:
                                this.ApplyChams(entity, Color.cyan);
                                break;

                            case EntityType.Zombie:
                                this.ApplyChams(entity, Color.red);
                                break;

                            case EntityType.Animal:
                                this.ApplyChams(entity, Color.yellow);
                                break;
                        }
                    }
                }
                this.lastChamTime = Time.time + 10f;
            }
        }
    }
}

