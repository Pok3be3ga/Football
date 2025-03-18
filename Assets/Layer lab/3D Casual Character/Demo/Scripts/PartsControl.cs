using UnityEngine;
using System.Collections.Generic;

namespace Layer_lab._3D_Casual_Character
{
    public class PartsControl : MonoBehaviour
    {
        [SerializeField] private CharacterControl _characterControl;
        [SerializeField] private ButtonParts button;
        [SerializeField] private Transform content;
        [SerializeField] private Sprite[] spriteIcons;
        public List<ButtonParts> ButtonParts = new();
        private int[] _saveGloves = new int[9];
        [SerializeField] private GameSettings _gameSettings;

        private void Start()
        {
            _saveGloves = _gameSettings.SaveGlovesFirstPlayer;
            SpawnPartsButton(PartsType.Hair,_characterControl.CharacterBase.PartsHair.ToArray(), $"{PartsType.Hair}", false, _saveGloves[0]);
            SpawnPartsButton(PartsType.Face,_characterControl.CharacterBase.PartsFace.ToArray(), $"{PartsType.Face}", false, _saveGloves[1]);
            SpawnPartsButton(PartsType.Headgear,_characterControl.CharacterBase.PartsHeadGear.ToArray(), $"{PartsType.Headgear}", true, _saveGloves[2]);
            SpawnPartsButton(PartsType.Top,_characterControl.CharacterBase.PartsTop.ToArray(), $"{PartsType.Top}", false, _saveGloves[3]);
            SpawnPartsButton(PartsType.Glove,_characterControl.CharacterBase.PartsGlove.ToArray(), $"{PartsType.Glove}", true, _saveGloves[4]);
            SpawnPartsButton(PartsType.Bottom,_characterControl.CharacterBase.PartsBottom.ToArray(), $"{PartsType.Bottom}", false, _saveGloves[5]);
            SpawnPartsButton(PartsType.Shoes,_characterControl.CharacterBase.PartsShoes.ToArray(), $"{PartsType.Shoes}", false, _saveGloves[6]);
            SpawnPartsButton(PartsType.Bag,_characterControl.CharacterBase.PartsBag.ToArray(), $"{PartsType.Bag}", true, _saveGloves[7]);
            SpawnPartsButton(PartsType.Eyewear,_characterControl.CharacterBase.PartsEyewear.ToArray(), $"{PartsType.Eyewear}", true, _saveGloves[8]);
            button.gameObject.SetActive(false);
        }

        private Sprite GetSprite(string name)
        {
            foreach (var sprite in spriteIcons)
            {
                if (sprite.name.Contains(name))
                {
                    return sprite;
                }
            }

            return null;
        }

        
        public void SpawnPartsButton(PartsType partsType, GameObject[] parts, string name, bool isEmpty, int index)
        {
            ButtonParts buttonParts = Instantiate(button, content, false);
            buttonParts.SetButton(partsType, parts, GetSprite(name.ToLower()), isEmpty, index);
            ButtonParts.Add(buttonParts);
        }
        public void SetAllRandom()
        {
            for (int i = 0; i < ButtonParts.Count; i++)
            {
                ButtonParts[i].SetRandom();   
            }
        }
    }
}
