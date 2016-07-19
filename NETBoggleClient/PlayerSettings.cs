using System;
using System.IO;
using Newtonsoft.Json;

namespace NETBoggle.Client
{
    /// <summary>
    /// Loading and storage of player settings (name etc)
    /// </summary>
    public static class PlayerSettings
    {
        /// <summary>
        /// Information about a player
        /// </summary>
        public struct PlayerInfo
        {
            /// <summary>
            /// The name we wantt to use
            /// </summary>
            public string PlayerName;

            /// <summary>
            /// The name of our server
            /// </summary>
            public string Host_ServerName;
            /// <summary>
            /// The password for our server
            /// </summary>
            public string Host_ServerPassword;

            /// <summary>
            /// Ctor
            /// </summary>
            /// <param name="name"></param>
            /// <param name="s_name"></param>
            /// <param name="s_pass"></param>
            public PlayerInfo(string name, string s_name, string s_pass)
            {
                PlayerName = name;
                Host_ServerName = s_name;
                Host_ServerPassword = s_pass;
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
