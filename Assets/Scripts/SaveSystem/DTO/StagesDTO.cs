using System.Collections.Generic;

namespace SaveSystem.DTO
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