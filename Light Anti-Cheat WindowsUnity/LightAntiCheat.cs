using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace LightAntiCheatUnityClient
{
    public class LightAntiCheat : MonoBehaviour
    {
        public AntiCheatComponent component;

        private void Start()
        {
            if (Application.isEditor)
            {
                Debug.LogWarning("Please build the game in order to start the Anti-Cheat");
            }
            else
            {
                InitializeAntiCheat();
                component.ReceiveDetected();
            }
        }

        private string getAntiCheatName()
        {
            return System.Environment.CurrentDirectory + "\\Light Anti-Cheat.exe";
        }

        private void InitializeAntiCheat()
        {
            System.Diagnostics.Process.Start(getAntiCheatName());
        }
    }
}
