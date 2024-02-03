using System.IO;
using UnityEngine;

namespace Mechanics.SaveSystem
{
    public class SaveConfig
    {
        public string SaveFilePath;

        public SaveConfig(string fileName)
        {
            SaveFilePath = Path.Combine(Application.persistentDataPath, fileName);
        }
    }
}