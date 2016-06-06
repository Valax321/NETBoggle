using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace NETBoggle.Client
{
    /// <summary>
    /// Loading and storage of player settings (name etc)
    /// </summary>
    public static class PlayerSettings
    {
        public struct PlayerInfo
        {
            public string PlayerName;

            //Proxy info
            public bool UseProxy;
            public string Proxy_Host;
            public string Proxy_User;

            public PlayerInfo(string name, bool proxy, string p_host, string p_user)
            {
                PlayerName = name;
                UseProxy = proxy;
                Proxy_Host = p_host;
                Proxy_User = p_user;
            }
        }

        const string filename = "settings.json"; //Where we load our settings from.
        public static PlayerInfo Settings = new PlayerInfo();

        // Initialise the settings in case we can't load.
        static PlayerSettings()
        {
            Settings.PlayerName = "Player";
        }

        /// <summary>
        /// Load player settings from disk.
        /// </summary>
        public static void LoadPlayerSettings()
        {
            string json_deserialised;

            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    json_deserialised = sr.ReadToEnd();
                    sr.Close();
                }
            }

            catch (Exception e)
            {
                Debug.Log(e.ToString());
                return;
            }

            Settings = JsonConvert.DeserializeObject<PlayerInfo>(json_deserialised);
            Debug.Log(string.Format("Player settings loaded from {0}", filename));
        }

        /// <summary>
        /// Save player settings to disk.
        /// </summary>
        public static void SavePlayerSettings()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    sw.Write(JsonConvert.SerializeObject(Settings));
                    Debug.Log(string.Format("Player settings saved to {0}", filename));
                    sw.Close();
                }
            }

            catch (Exception e)
            {
                Debug.Log(e.ToString());
                return;
            }
        }
    }
}
