using System;
using System.Collections.Generic;
using Mechanics.SaveSystem;
using Mechanics.SaveSystem.DTO;
using Mechanics.SaveSystem.Serializables;
using SaveSystem;
using SaveSystem.DTO;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerManager : MonoBehaviour
    {
        public SaveManager<PlayerDataDTO> saveManager;
        private PlayerDataDTO currentSaveData;

        public PlayerDataDTO CurrentSaveData
        {
            get => currentSaveData;
            set => currentSaveData = value;
        }


        private void Awake()
        {
            SaveConfig saveConfig = new SaveConfig("PlayerData.json");
            LoadConfig loadConfig = new LoadConfig("PlayerData.json");
            
            var defaultData = new PlayerDataDTO
            {
                score = 0,
                level = 1,
                xp = 0,
                stages = new List<StagesDTO>()
            };
            
            ISerializer<PlayerDataDTO> jsonSerializer = new JsonSerializer<PlayerDataDTO>();
            saveManager = new SaveManager<PlayerDataDTO>(jsonSerializer, saveConfig, loadConfig, defaultData);
            currentSaveData = saveManager.Load();
        }


        public void AddScore(int amount)
        {
            saveManager.EditDataProperty(gameData =>
            {
                gameData.score += amount;
                return gameData;
            });
        }
        
    }
}