using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Layer_lab._3D_Casual_Character
{
    public class ButtonParts : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private Button _priceButton;
        [SerializeField] private CharacterControl _characterControl;
        [SerializeField] private int _price;
        public int Index { get; private set; }
        [SerializeField] private TMP_Text textTitle;
        [field: SerializeField] private bool IsEmpty { get; set; }
        [SerializeField] private Image imageIcon;

        private PartsType CurrentPartType;

        private GameObject[] _parts;
        private void OnEnable()
        {
            SaveInGameSettings();
        }
        public void SetButton(PartsType partsType, GameObject[] parts, Sprite icon, bool isNone, int index)
        {
            CurrentPartType = partsType;
            IsEmpty = isNone;
            imageIcon.sprite = icon;
            imageIcon.SetNativeSize();
            _parts = parts;
            SetParts(index);
        }
        [ContextMenu("ResetParts")]
        public void SetParts()
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
        private void SetParts(int index)
        {
            _characterControl.CharacterBase.SetItem(CurrentPartType, index);
            Index = index;
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
            SetPriceButton();
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
            SetPriceButton();
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
                textTitle.text = (Index + 1).ToString();
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
        public void SaveInGameSettings()
        {
            if (_gameSettings.Eyewear.Count == 0 && _parts.Length != 0)
            {
                List<bool> bools = new List<bool>();
                {
                    for (int i = 0; i < _parts.Length; i++)
                    {
                        if (_parts[i].TryGetComponent<Buy>(out Buy buy))
                            bools.Add(true);
                        else
                            bools.Add(false);
                    }
                }
                if (CurrentPartType == PartsType.Hair) _gameSettings.Hair = bools;
                if (CurrentPartType == PartsType.Face) _gameSettings.Face = bools;
                if (CurrentPartType == PartsType.Headgear) _gameSettings.Headgear = bools;
                if (CurrentPartType == PartsType.Top) _gameSettings.Top = bools;
                if (CurrentPartType == PartsType.Bottom) _gameSettings.Bottom = bools;
                if (CurrentPartType == PartsType.Bag) _gameSettings.Bag = bools;
                if (CurrentPartType == PartsType.Shoes) _gameSettings.Shoes = bools;
                if (CurrentPartType == PartsType.Glove) _gameSettings.Glove = bools;
                if (CurrentPartType == PartsType.Eyewear) _gameSettings.Eyewear = bools;
            }
        }
        public void SetPriceButton()
        {
            _priceButton.gameObject.SetActive(false);

            if (CurrentPartType == PartsType.Hair && _gameSettings.Hair[Index] == false && Index > 0) _priceButton.gameObject.SetActive(true);
            if (CurrentPartType == PartsType.Face && _gameSettings.Face[Index] == false && Index > 0) _priceButton.gameObject.SetActive(true);
            if (CurrentPartType == PartsType.Headgear && _gameSettings.Headgear[Index] == false && Index > 0) _priceButton.gameObject.SetActive(true);
            if (CurrentPartType == PartsType.Top && _gameSettings.Top[Index] == false && Index > 0) _priceButton.gameObject.SetActive(true);
            if (CurrentPartType == PartsType.Bottom && _gameSettings.Bottom[Index] == false && Index > 0) _priceButton.gameObject.SetActive(true);
            if (CurrentPartType == PartsType.Bag && _gameSettings.Bag[Index] == false && Index > 0) _priceButton.gameObject.SetActive(true);
            if (CurrentPartType == PartsType.Shoes && _gameSettings.Shoes[Index] == false && Index > 0) _priceButton.gameObject.SetActive(true);
            if (CurrentPartType == PartsType.Glove && _gameSettings.Glove[Index] == false && Index > 0) _priceButton.gameObject.SetActive(true);
            if (CurrentPartType == PartsType.Eyewear && _gameSettings.Eyewear[Index] == false && Index > 0) _priceButton.gameObject.SetActive(true);

            if ((_parts.Length / 3) > Index) _price = 100;
            else if ((_parts.Length / 3) < Index && (_parts.Length / 3 * 2) > Index) _price = 200;
            else if ((_parts.Length / 3 * 2) < Index) _price = 300;

            _priceButton.GetComponentInChildren<TextMeshProUGUI>().text = _price.ToString();

            PriceButtonOnClick();
        }
        private void PriceButtonOnClick()
        {
            _priceButton.onClick.RemoveAllListeners();
            _priceButton.onClick.AddListener(() =>
            {
                if (_price <= _gameSettings.Money)
                {
                    _gameSettings.Money -= _price;
                    YandexGame.savesData.Money = _gameSettings.Money;
                    YandexGame.SaveProgress();
                    _parts[Index].gameObject.AddComponent<Buy>();
                    if (CurrentPartType == PartsType.Hair) _gameSettings.Hair[Index] = true;
                    else if (CurrentPartType == PartsType.Face) _gameSettings.Face[Index] = true;
                    else if (CurrentPartType == PartsType.Headgear) _gameSettings.Headgear[Index] = true;
                    else if (CurrentPartType == PartsType.Top) _gameSettings.Top[Index] = true;
                    else if (CurrentPartType == PartsType.Bottom) _gameSettings.Bottom[Index] = true;
                    else if (CurrentPartType == PartsType.Bag) _gameSettings.Bag[Index] = true;
                    else if (CurrentPartType == PartsType.Shoes) _gameSettings.Shoes[Index] = true;
                    else if (CurrentPartType == PartsType.Glove) _gameSettings.Glove[Index] = true;
                    else if (CurrentPartType == PartsType.Eyewear) _gameSettings.Eyewear[Index] = true;

                    _priceButton.gameObject.SetActive(false);

                }
            });
        }
    }

}
