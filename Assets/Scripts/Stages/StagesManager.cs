using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using Mechanics.SaveSystem;
using Mechanics.SaveSystem.Serializables;
using SaveSystem;
using SaveSystem.DTO;
using UnityEngine.Events;

namespace Stages
{
    public class StagesManager : MonoBehaviour
    {
        
        public SaveManager<StagesDTO> saveManager;
        private StagesDTO currentSaveData;
        
        [SerializeField] private StagesSO unlockStage;

        public StagesDTO CurrentSaveData
        {
            get => currentSaveData;
            set => currentSaveData = value;
        }
        
        private void Awake()
        {
            SaveConfig saveConfig = new SaveConfig("StagesData.json");
            LoadConfig loadConfig = new LoadConfig("StagesData.json");
            
            var defaultData = new StagesDTO(stages: new List<Stage>());
            
            defaultData.stages.Add(new Stage(true,1));
            
            ISerializer<StagesDTO> jsonSerializer = new JsonSerializer<StagesDTO>();
            saveManager = new SaveManager<StagesDTO>(jsonSerializer, saveConfig, loadConfig, defaultData);
            currentSaveData = saveManager.Load();
        }


        public void GoToNextStage()
        {
            
        }
        
        public void LoadStages()
        {
            saveManager.EditDataProperty(dto =>
            {
                dto.stages.Add(new Stage(false, 2));
                return dto;
            });
        }
        
        public void UnlockStage()
        {
            saveManager.EditDataProperty(dto =>
            {
                foreach (var stage in dto.stages)
                {
                    if (stage.id == unlockStage.id)
                    {
                        stage.isUnlocked = true;
                        Debug.Log("Nivel encontrado!");
                        return dto;
                    }
                }
                
                Debug.Log("Nivel não encontrado!");
                return dto;
            });    
        }
        
    }
}