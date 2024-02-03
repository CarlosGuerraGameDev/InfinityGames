using System.IO;
using UnityEngine;

namespace Mechanics.SaveSystem
{
    public class LoadConfig
    {
        public string LoadFilePath;

        public LoadConfig(string fileName)
        {
            LoadFilePath = Path.Combine(Application.persistentDataPath, fileName);
        }
    }
}