using System.Collections.Generic;

namespace SaveSystem.DTO
{
    [System.Serializable]
    public class PlayerDataDTO
    {
        public int score;
        public int level;
        public int xp;
        public List<StagesDTO> stages;
    }
}