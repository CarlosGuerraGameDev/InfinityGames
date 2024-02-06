using System.Linq;
using Stages;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class UIStage : MonoBehaviour
    {
        [SerializeField] private CanvasScaler scaler;
        [SerializeField] private LevelUtility levelUtility;
        [SerializeField] private int referenceResolutionX = 2930;
        [SerializeField] private StagesManager stagesManager;
        [SerializeField] private GameObject stageElementsTemplate;
        [SerializeField] private GameObject content;
        [SerializeField] private GameObject dummyStage;

        public UnityEvent onUIStageStartEvent;
        
        private void Start()
        {
            if (UIUtility.IsThisDeviceTablet())
            {
                scaler.referenceResolution = new Vector2(referenceResolutionX, scaler.referenceResolution.y);
            }
            
            stagesManager.LoadStages();
            LoadStagesToScrollView();
            onUIStageStartEvent.Invoke();
        }
        
        private void LoadStagesToScrollView()
        {
            foreach (var stage in stagesManager.StageConfiguration.StagesSos)
            {
                
                GameObject instanciateStage = Instantiate(stageElementsTemplate, content.transform, false);
                
                bool isUnlocked = stagesManager.CurrentSaveData.stages.Any(listStage => listStage.id == stage.id && listStage.isUnlocked);
                string stageState = isUnlocked ? "Unlocked" : "Locked";
                string stageNameAndState = stage.name + "\n" + stageState;
                Material backgroundStage = isUnlocked ? stage.backgroundStage : stage.backgroundLockedStage;
                string stageRewards = "Rewards " + "\n<size=70>+" + stage.rewardScore + " points</s> " + "\n<size=70>+" + stage.rewardXp + " xp</s>";
              
                
                instanciateStage.GetComponentsInChildren<TMP_Text>()[0].text = stageNameAndState;
                instanciateStage.GetComponent<Image>().material = backgroundStage;
                instanciateStage.GetComponentsInChildren<TMP_Text>()[1].text = stageRewards;

                if (isUnlocked)
                {
                    instanciateStage.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        levelUtility.LoadScene(stage.levelName);
                    });
                    
                }
            }
            
            Instantiate(dummyStage, content.transform, false);

        }

    }
}