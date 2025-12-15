using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using BasicLegionInfected.Core;

namespace BasicLegionInfected.Audio
{
    public class AudioManager : ASingleton<AudioManager>
    {
        [SerializeField] private AudioClipCatalogue _catalogue;

        [SerializeField] private AudioSource _backgroundAudioSource;
        [SerializeField] private AudioSource _effectAudioSource;

        public float BackgroundVolume;
        public float EffectVolume;

        private void OnEnable()
        {
            BackgroundVolume = PlayerPrefs.GetFloat("audio-manager_background-volume", 0.3f);
            EffectVolume = PlayerPrefs.GetFloat("audio-manager_effect-volume", 0.5f);
        }

        public void PlayBackgroundAudio(string audioCode)
        {
            _backgroundAudioSource.Stop();

            _backgroundAudioSource.volume = BackgroundVolume;

            AudioClip[] audioClips = _catalogue.GetItem(audioCode);
            _backgroundAudioSource.clip = audioClips[Random.Range(0, audioClips.Length)]; // Random choose

            _backgroundAudioSource.loop = true;

            _backgroundAudioSource.Play();
        }

        public void StopBackgroundAudio()
        {
            _backgroundAudioSource.Stop();
        }

        public void PlayEffectAudio(string audioCode)
        {
            PlayOneShotAudio(audioCode, EffectVolume);
        }

        private void PlayOneShotAudio(string audioCode, float volumeScale)
        {
            AudioClip[] audioClips = _catalogue.GetItem(audioCode);
            AudioClip audioClip = audioClips[Random.Range(0, audioClips.Length)];

            _effectAudioSource.PlayOneShot(audioClip, volumeScale);
        }

        public void SaveVolumeConfiguration()
        {
            PlayerPrefs.SetFloat("audio-manager_background-volume", BackgroundVolume);
            PlayerPrefs.SetFloat("audio-manager_effect-volume", EffectVolume);
        }
    }
}
