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
        [SerializeField] private List<ButtonParts> _buttonParts = new();

        private void Start()
        {
            SpawnPartsButton(PartsType.Hair,_characterControl.CharacterBase.PartsHair.ToArray(), $"{PartsType.Hair}", false);
            SpawnPartsButton(PartsType.Face,_characterControl.CharacterBase.PartsFace.ToArray(), $"{PartsType.Face}", false);
            SpawnPartsButton(PartsType.Headgear,_characterControl.CharacterBase.PartsHeadGear.ToArray(), $"{PartsType.Headgear}", true);
            SpawnPartsButton(PartsType.Top,_characterControl.CharacterBase.PartsTop.ToArray(), $"{PartsType.Top}", false);
            SpawnPartsButton(PartsType.Glove,_characterControl.CharacterBase.PartsGlove.ToArray(), $"{PartsType.Glove}", true);
            SpawnPartsButton(PartsType.Bottom,_characterControl.CharacterBase.PartsBottom.ToArray(), $"{PartsType.Bottom}", false);
            SpawnPartsButton(PartsType.Shoes,_characterControl.CharacterBase.PartsShoes.ToArray(), $"{PartsType.Shoes}", false);
            SpawnPartsButton(PartsType.Bag,_characterControl.CharacterBase.PartsBag.ToArray(), $"{PartsType.Bag}", true);
            SpawnPartsButton(PartsType.Eyewear,_characterControl.CharacterBase.PartsEyewear.ToArray(), $"{PartsType.Eyewear}", true);
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

        
        public void SpawnPartsButton(PartsType partsType, GameObject[] parts, string name, bool isEmpty)
        {
            ButtonParts buttonParts = Instantiate(button, content, false);
            buttonParts.SetButton(partsType, parts, GetSprite(name.ToLower()), isEmpty);
            _buttonParts.Add(buttonParts);
        }

        public void SetAllRandom()
        {
            for (int i = 0; i < _buttonParts.Count; i++)
            {
                _buttonParts[i].SetRandom();   
            }
        }
    }
}
