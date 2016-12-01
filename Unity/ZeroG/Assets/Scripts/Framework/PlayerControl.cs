using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    class PlayerControl:Singleton<PlayerControl>
    {
        private List<PlayerClass> _players = new List<PlayerClass>();

        private List<PlayerData> _data = new List<PlayerData>();

        public List<Transform> SpawnPoints;

        public GameObject USAPrefab;

        public GameObject JapanPrefab;

        public GameObject RussianPrefab;

        public GameObject ChinaPrefab;

        public void Awake()
        {
            LoadPlayers(ControlCenter.Instance._playerData);
        }

        public List<PlayerData> DebugData()
        {
            PlayerData data1 = new PlayerData();
            data1.PlayerCountry = PlayerData.Country.China;
            data1.PlayerNum = 0;

            PlayerData data2 = new PlayerData();
            data2.PlayerCountry = PlayerData.Country.China;
            data2.PlayerNum = 1;

            PlayerData data3 = new PlayerData();
            data3.PlayerCountry = PlayerData.Country.USA;
            data3.PlayerNum = 2;

            PlayerData data4 = new PlayerData();
            data4.PlayerCountry = PlayerData.Country.USA;
            data4.PlayerNum = 3;

            List<PlayerData> data = new List<PlayerData>() { data1, data2, data3, data4 };

            return data;
        }

        public void LoadPlayers(List<PlayerData> data) 
        {
            _data = data;
            for (int i = 0; i < data.Count; i++)
            {
                LoadPlayer(data[i]);
            }
        }

        private void LoadPlayer(PlayerData data)
        {
            if (data != null)
            {
                GameObject prefab = RetrievePrefab(data);
                Transform spawnPoint = SpawnPoints[data.PlayerNum];
                if (prefab != null && spawnPoint != null)
                {
                    GameObject obj = (GameObject)Instantiate(prefab,spawnPoint.position,spawnPoint.localRotation);
                    PlayerClass player = obj.GetComponent<PlayerClass>();

                    player.LoadPlayerData(data);

                    obj.transform.parent = gameObject.transform;

                    _players.Add(player);
                }
            }
        }

        public List<PlayerData> GetAllPlayerData() 
        {
            List<PlayerData> data = new List<PlayerData>();

            foreach (PlayerClass player in _players)
            {
                data.Add(player.Data);
            }

            return data;
        }

        public PlayerData GetPlayerData(int index)
        {
            if (_players.Count < index)
            {
                return _data[index];
            }

            return null;
        }

        public List<PlayerClass> GetAllPlayers()
        {
            return _players;
        }

        public PlayerClass GetPlayer(int index)
        {
            if (_players.Count < index)
            {
                return _players[index];
            }

            return null;
        }

        private GameObject RetrievePrefab( PlayerData data)
        {
            switch (data.PlayerCountry)
            { 
                case PlayerData.Country.China:
                    if (ChinaPrefab != null)
                        return ChinaPrefab;
                break;
                case PlayerData.Country.Russia:
                if (RussianPrefab != null)
                    return RussianPrefab;
                break;
                case PlayerData.Country.Japan:
                if (JapanPrefab != null)
                    return JapanPrefab;
                break;
                case PlayerData.Country.USA:
                if (USAPrefab != null)
                    return USAPrefab;
                break;
            }

            return null;
        }

        public void PausePlayers()
        {
            foreach (PlayerClass player in _players)
            {
                //player.Pause();
            }
        }

        public void StartPlayers()
        {
            foreach (PlayerClass player in _players)
            {
                //player.Start();
            }
        }

        public override void Destroyed()
        {
            throw new NotImplementedException();
        }
    }
