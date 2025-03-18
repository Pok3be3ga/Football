using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixer;
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Slider _slider;
    [SerializeField] private GameSettings _gameSettings;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _toggle.onValueChanged.AddListener(ToggleMusic);
        _slider.onValueChanged.AddListener(ChangeVolume);
        _toggle.isOn = _gameSettings.MusicToggle;
        _slider.value = _gameSettings.AudioValume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleMusic(bool enabled)
    {
        if (enabled) _audioMixer.audioMixer.SetFloat("Music", 0);
        else _audioMixer.audioMixer.SetFloat("Music", -80);
        _gameSettings.MusicToggle = enabled;
    }
    public void ChangeVolume(float volume)
    {
        _audioMixer.audioMixer.SetFloat("Master", Mathf.Lerp(-80,0,volume));
        _gameSettings.AudioValume = volume;
    }
}
