using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using BasicLegionInfected.Audio;
using BasicLegionInfected.View;

namespace BasicLegionInfected.Game
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private Menu _menu;
        [SerializeField] private Slider _audioEffectVolumeSlider;
        private DragDetector _audioEffectVolumeSliderDragDetector;

        private void Awake()
        {
            _menu.OnHide.AddListener(OnMenuHide);
            _audioEffectVolumeSlider.onValueChanged.AddListener(OnAudioEffectVolumeSliderValueChanged);

            _audioEffectVolumeSliderDragDetector = _audioEffectVolumeSlider.GetComponentInChildren<DragDetector>();
        }

        private void OnEnable()
        {
            _audioEffectVolumeSlider.value = AudioManager.Instance.EffectVolume;
        }

        private void OnMenuHide()
        {
            AudioManager.Instance.SaveVolumeConfiguration();
        }

        private void OnAudioEffectVolumeSliderValueChanged(float value)
        {
            AudioManager.Instance.EffectVolume = value;

            if (_audioEffectVolumeSliderDragDetector.IsDragging) return;

            AudioManager.Instance.PlayEffectAudio("effect_test_sound");
        }
    }
}
