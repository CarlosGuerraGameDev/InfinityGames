using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UnityEngine;
using Mechanics.SaveSystem;
using Mechanics.SaveSystem.Serializables;
using SaveSystem.DTO;
using UnityEngine.Events;
using Utility;

namespace Stages
{
    public class StagesManager : MonoBehaviour
    {
        private SaveManager<StagesDTO> _saveManager;
        private StagesDTO currentSaveData;

        [SerializeField] private StagesSO currentStage;
        [SerializeField] private LevelUtility levelUtility;
        [SerializeField] private StagesSO unlockStage;
        
        [SerializeField] private StageConfiguration stageConfiguration;
        public UnityEvent onStageStart;

        public StageConfiguration StageConfiguration
        {
            get => stageConfiguration;
            set => stageConfiguration = value;
        }

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
            _saveManager = new SaveManager<StagesDTO>(jsonSerializer, saveConfig, loadConfig, defaultData);
            currentSaveData = _saveManager.Load();
        }

        private void Start()
        {
            onStageStart.Invoke();
        }

        [ContextMenu("AddNewLevel")]
        public void LoadStages()
        {
            for (var i = 0; i < stageConfiguration.StagesSos.Count(); i++)
            {
                if (!currentSaveData.stages.Select(stage => stage.id).Contains(stageConfiguration.StagesSos[i].id))
                {
                    
                    int currentStage = stageConfiguration.StagesSos[i].id;
                    
                    _saveManager.EditDataProperty(dto =>
                    {
                        Stage stage = new Stage(false,currentStage);
                        dto.stages.Add(stage);
                        return dto;
                    });
                    Debug.Log("New stage added to the config!");
                }
                else
                {
                    Debug.Log("This stage already exist in the config!");
                }
            }
            currentSaveData = _saveManager.Load();
        }
        
        public void GetNextStage()
        {
            int currentIndex = stageConfiguration.StagesSos.IndexOf(currentStage);

            if (currentIndex == -1)
            {
                Debug.LogError("Current stage not found in the list.");
                levelUtility.LoadScene("Menu");
                return;
            }

            if (currentIndex == stageConfiguration.StagesSos.Count - 1)
            {
                Debug.Log("Current stage is the last one in the list.");
                levelUtility.LoadScene("Menu");
                return;
            }

            StagesSO nextStage = stageConfiguration.StagesSos[currentIndex + 1];
            Debug.Log("The Name of the next stage is : " + nextStage.name);
            levelUtility.LoadScene(nextStage.levelName);
        }
        
        public void UnlockStage()
        {

            if (unlockStage is null)
            {
                return;
            }

            _saveManager.EditDataProperty(dto =>
            {
                foreach (var stage in dto.stages)
                {
                    if (stage.id == unlockStage.id)
                    {
                        if (stage.isUnlocked) return dto;
                        stage.isUnlocked = true;
                        Debug.Log("Stage found!!");
                        return dto;
                    }
                }
                
                Debug.Log("Stage not found!");
                return dto;
            });    
        }
        
    }
}