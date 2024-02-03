using System;
using System.IO;
using UnityEngine;

namespace Mechanics.SaveSystem
{
    public class SaveManager<T>
    {
        private ISerializer<T> serializer;
        private SaveConfig saveConfig;
        private LoadConfig loadConfig;
        private T defaultData;
        
        public SaveManager(ISerializer<T> serializer, SaveConfig saveConfig, LoadConfig loadConfig , T defaultData)
        {
            this.serializer = serializer;
            this.saveConfig = saveConfig;
            this.loadConfig = loadConfig;
            this.defaultData = defaultData;
            Initialize();
        }

        public SaveManager(ISerializer<T> serializer ,LoadConfig loadConfig)
        {
            this.serializer = serializer;
            this.loadConfig = loadConfig;
            Initialize();
        }

        private void Initialize()
        {
            if (!File.Exists(saveConfig.SaveFilePath))
            {
                CreateDefaultSaveFile();
                Debug.Log("O ficheiro " + saveConfig.SaveFilePath + " não existe e acabou de ser criado!");
            }
            else
            {
                Debug.Log("O ficheiro " + saveConfig.SaveFilePath + " já existe ^^");
            }
        }

        private void CreateDefaultSaveFile()
        {
            var serializedDefaultData = serializer.Serialize(defaultData);
            File.WriteAllText(saveConfig.SaveFilePath, serializedDefaultData);
        }

        public void EditDataProperty(Func<T, T> editAction)
        {
            T loadedData = Load();
            T editedData = editAction(loadedData);
            Save(editedData);
        }
        
        public void Save(T data)
        {
            var serializedData = serializer.Serialize(data);
            File.WriteAllText(saveConfig.SaveFilePath, serializedData);
        }

        public T Load()
        {
            var serializedData = File.ReadAllText(loadConfig.LoadFilePath);
            return serializer.Deserialize(serializedData);
        }
    }

}