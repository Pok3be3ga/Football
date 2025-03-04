using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Layer_lab._3D_Casual_Character
{
    public class ButtonParts : MonoBehaviour
    {
        [SerializeField] private CharacterControl _characterControl;
        private GameObject[] _parts;
        public int Index { get; private set; }
        [SerializeField] private TMP_Text textTitle;
        [field: SerializeField] private bool IsEmpty { get; set; }

        [SerializeField] private Image imageIcon;

        private PartsType CurrentPartType;
        public void SetButton(PartsType partsType, GameObject[] parts, Sprite icon, bool isNone)
        {
            CurrentPartType = partsType;
            IsEmpty = isNone;
            imageIcon.sprite = icon;
            imageIcon.SetNativeSize();
            _parts = parts;
            SetParts();
        }
    
        private void SetParts()
        {
            if (IsEmpty)
            {
                _characterControl.CharacterBase.SetItem(CurrentPartType, -1);
                Index = -1;
            }
            else
            {
                _characterControl.CharacterBase.SetItem(CurrentPartType, 0);
            }

            _SetTitle();
        }

  
    

        public void OnClick_Next()
        {
            Index++;
        
            if (IsEmpty)
            {
                if (Index >= _parts.Length) Index = -1;
            }
            else
            {
                if (Index >= _parts.Length) Index = 0;
            }
        
            _SetParts();
            _SetTitle();
        }

        public void OnClick_Previous()
        {
            Index--;

            if (IsEmpty)
            {
                if (Index < -1) Index = _parts.Length - 1;
            }
            else
            {
                if (Index < 0) Index = _parts.Length - 1;   
            }

            _SetParts();
            _SetTitle();
        }


        private void _SetParts()
        {
            _characterControl.CharacterBase.SetItem(CurrentPartType, Index);
        }
    

        private void _SetTitle()
        {
            if (!IsEmpty && Index <= -1 || IsEmpty && Index <= -1)
            {
                textTitle.text = "--";
                textTitle.CrossFadeAlpha(0.3f, 0f, true);
            }
            else
            {
                string result = _parts[Index].name.Replace("Pack1_", "");
                result = result.Replace("_", "");
                textTitle.text = result;
                textTitle.CrossFadeAlpha(1f, 0f, true);
            }
        }

        public void SetRandom()
        {
            int random = 0;
            
            if (IsEmpty)
            {
                random = Random.Range(-_parts.Length, _parts.Length - 1);
                if (random < -1) random = -1;
            }
            else
            {
                random = Random.Range(0, _parts.Length - 1);
            }
            
            Index = random;
            _characterControl.CharacterBase.SetItem(CurrentPartType, random);
            _SetTitle();
        }
    }
}
