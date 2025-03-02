
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public enum LevelEnum
        {
            Easy,
            Medium,
            Hard
        }

        // Ваши сохранения
        public int Money = 0;
        public LevelEnum Level = LevelEnum.Easy;
        public int[] CharacterPartsFirst = new int[10];
        public int[] CharacterPartsSecond = new int[10];

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {

        }
    }
}
