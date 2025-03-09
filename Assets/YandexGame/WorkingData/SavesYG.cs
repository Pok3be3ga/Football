
using System.Collections.Generic;

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

        // Ваши сохранения
        public int Money = 0;
        public int[] CharacterPartsFirst = new int[10];
        public int[] CharacterPartsSecond = new int[10];

        public List<bool> Hair = new List<bool>();
        public List<bool> Face = new List<bool>();
        public List<bool> Headgear = new List<bool>();
        public List<bool> Top = new List<bool>();
        public List<bool> Bottom = new List<bool>();
        public List<bool> Bag = new List<bool>();
        public List<bool> Shoes = new List<bool>();
        public List<bool> Glove = new List<bool>();
        public List<bool> Eyewear = new List<bool>();

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {

        }
    }
}
