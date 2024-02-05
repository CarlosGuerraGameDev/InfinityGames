namespace SaveSystem.DTO
{
    [System.Serializable]
    public class Stage
    {
        public Stage(bool isUnlocked, int id)
        {
            this.isUnlocked = isUnlocked;
            this.id = id;
        }
        
        public bool isUnlocked;
        public int id;
    }
}