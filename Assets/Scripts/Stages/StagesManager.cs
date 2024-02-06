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
        private StagesDTO _currentSaveData;
        
        [SerializeField] private LevelUtility levelUtility;
        [SerializeField] private StagesSO unlockStage;
        [SerializeField] private int curretStage = 0;
        
        [SerializeField] private StageConfiguration stageConfiguration;
        public UnityEvent onStageStart;

        public StageConfiguration StageConfiguration
        {
            get => stageConfiguration;
            set => stageConfiguration = value;
        }

        public StagesDTO CurrentSaveData
        {
            get => _currentSaveData;
            set => _currentSaveData = value;
        }

        private void Awake()
        {
            SaveConfig saveConfig = new SaveConfig("StagesData.json");
            LoadConfig loadConfig = new LoadConfig("StagesData.json");
            
            var defaultData = new StagesDTO(stages: new List<Stage>());
            
            defaultData.stages.Add(new Stage(true,1));
            
            ISerializer<StagesDTO> jsonSerializer = new JsonSerializer<StagesDTO>();
            _saveManager = new SaveManager<StagesDTO>(jsonSerializer, saveConfig, loadConfig, defaultData);
            _currentSaveData = _saveManager.Load();
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
                if (!_currentSaveData.stages.Select(stage => stage.id).Contains(stageConfiguration.StagesSos[i].id))
                {
                    
                    int currentStage = stageConfiguration.StagesSos[i].id;
                    
                    _saveManager.EditDataProperty(dto =>
                    {
                        Stage stage = new Stage(false,currentStage);
                        dto.stages.Add(stage);
                        return dto;
                    });
                    Debug.Log("Novo level adicionado á config!");
                }
                else
                {
                    Debug.Log("Este level já existe no jogo!");
                }
            }
            _currentSaveData = _saveManager.Load();
        }
        
        
        public void GoToNextStage()
        {

            //Check if this currentLevel is the last Level
            if (_saveManager.Load().stages[curretStage].id == _saveManager.Load().stages.Last().id)
            {
                //Send the player to he main menu
                levelUtility.LoadScene("Menu");
                return;
            }

            curretStage++;

        }
        
        //Unlocks the levelStage
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