using System.Collections.Generic;
using SaveSystem.DTO;

namespace SaveSystem
{
    [System.Serializable]
    public class StagesDTO
    {
        public List<Stage> stages;

        public StagesDTO()
        {
        }

        public StagesDTO(List<Stage> stages)
        {
            this.stages = stages;
        }
    }
    
}