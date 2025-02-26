using Layer_lab._3D_Casual_Character;
using UnityEngine;
using YG;

public class LoadPlayerParts : MonoBehaviour
{
    [SerializeField] private CharacterControl CharacterControl;
    [SerializeField] private bool _secondPlayer;
    public int[] SaveGloves = new int[10];
    private void Start()
    {
        LoadBuysFromJSON();
    }
    public void LoadBuysFromJSON()
    {
        YandexGame.LoadProgress();
        if(_secondPlayer == true) SaveGloves = YandexGame.savesData.CharacterPartsSecond;
        else SaveGloves = YandexGame.savesData.CharacterPartsFirst;
        //CharacterControl.Instance.CharacterBase.SetItem(PartsType.Body, SaveGloves[0]);
        CharacterControl.CharacterBase.SetItem(PartsType.Hair, SaveGloves[1]);
        CharacterControl.CharacterBase.SetItem(PartsType.Face, SaveGloves[2]);
        CharacterControl.CharacterBase.SetItem(PartsType.Headgear, SaveGloves[3]);
        CharacterControl.CharacterBase.SetItem(PartsType.Top, SaveGloves[4]);
        CharacterControl.CharacterBase.SetItem(PartsType.Bottom, SaveGloves[5]);
        CharacterControl.CharacterBase.SetItem(PartsType.Eyewear, SaveGloves[6]);
        CharacterControl.CharacterBase.SetItem(PartsType.Bag, SaveGloves[7]);
        CharacterControl.CharacterBase.SetItem(PartsType.Shoes, SaveGloves[8]);
        CharacterControl.CharacterBase.SetItem(PartsType.Glove, SaveGloves[9]);
    }
}
