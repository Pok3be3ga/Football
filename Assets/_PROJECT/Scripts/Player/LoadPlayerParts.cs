using Layer_lab._3D_Casual_Character;
using UnityEngine;
public class LoadPlayerParts : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private CharacterControl CharacterControl;
    [SerializeField] private bool _secondPlayer;
    private int[] _saveGloves = new int[9];
    private void Start()
    {
        LoadGloves();
    }
    public void LoadGloves()
    {
        if (_secondPlayer) _saveGloves = _gameSettings.SaveGlovesSecondPlayer;
        else _saveGloves = _gameSettings.SaveGlovesFirstPlayer;
        
        CharacterControl.CharacterBase.SetItem(PartsType.Hair, _saveGloves[0]);
        CharacterControl.CharacterBase.SetItem(PartsType.Face, _saveGloves[1]);
        CharacterControl.CharacterBase.SetItem(PartsType.Headgear, _saveGloves[2]);
        CharacterControl.CharacterBase.SetItem(PartsType.Top, _saveGloves[3]);
        CharacterControl.CharacterBase.SetItem(PartsType.Glove, _saveGloves[4]);
        CharacterControl.CharacterBase.SetItem(PartsType.Bottom, _saveGloves[5]);
        CharacterControl.CharacterBase.SetItem(PartsType.Shoes, _saveGloves[6]);
        CharacterControl.CharacterBase.SetItem(PartsType.Bag, _saveGloves[7]);
        CharacterControl.CharacterBase.SetItem(PartsType.Eyewear, _saveGloves[8]);
        CharacterControl.CharacterBase.SetRoot();
    }
}
