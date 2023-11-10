namespace NeoModTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class Objects : MonoBehaviour
    {
        public static GameManager gameManager = null;
        public static List<EntityItem> itemList;
        private float lastCacheItems;
        private float lastCachePlayer;
        private float lastCacheZombies;
        public static EntityPlayerLocal localPlayer;
        public static QuestJournal questJournal = null;
        public static WaypointCollection waypointCollection = null;
        public static List<EntityZombie> zombieList;

        private void Start()
        {
            zombieList = new List<EntityZombie>();
            itemList = new List<EntityItem>();
            this.lastCachePlayer = Time.time + 5f;
            this.lastCacheZombies = Time.time + 3f;
            this.lastCacheItems = Time.time + 4f;
        }

        private void Update()
        {
            if (gameManager == null)
            {
                gameManager = GameManager.Instance;
            }
            if ((questJournal == null) && (localPlayer != null))
            {
                questJournal = localPlayer.QuestJournal;
            }
            if ((waypointCollection == null) && (localPlayer != null))
            {
                waypointCollection = localPlayer.Waypoints;
            }
            if (Time.time >= this.lastCachePlayer)
            {
                localPlayer = UnityEngine.Object.FindObjectOfType<EntityPlayerLocal>();
                this.lastCachePlayer = Time.time + 5f;
            }
            else if (Time.time >= this.lastCacheZombies)
            {
                zombieList = UnityEngine.Object.FindObjectsOfType<EntityZombie>().ToList<EntityZombie>();
                this.lastCacheZombies = Time.time + 3f;
            }
            else if (Time.time >= this.lastCacheItems)
            {
                itemList = UnityEngine.Object.FindObjectsOfType<EntityItem>().ToList<EntityItem>();
                this.lastCacheItems = Time.time + 4f;
            }
        }

        public static List<EntityPlayer> PlayerList
        {
            get
            {
                if ((GameManager.Instance != null) && (GameManager.Instance.World > null))
                {
                    return GameManager.Instance.World.GetPlayers();
                }
                return new List<EntityPlayer>();
            }
        }
    }
}

