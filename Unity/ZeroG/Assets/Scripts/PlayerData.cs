using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
    public class PlayerData
    {
        public enum Country {Japan, USA, China, Russia};
	    public Country PlayerCountry;
	    public int PlayerNum;
        public int PlayerScore;
        public Color PlayerColor
        {
            get
            {
                switch (PlayerCountry)
                {

                    case Country.China:
                        return Color.yellow;
                    case Country.USA:
                        return Color.blue;
                    case Country.Japan:
                        return Color.red;
                    case Country.Russia:
                        return Color.white;
                    default:
                        return Color.green;
                }
            }
            set { }
        }
    }
