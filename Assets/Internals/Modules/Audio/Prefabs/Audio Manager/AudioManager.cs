using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using BasicLegionInfected.Core;

namespace BasicLegionInfected.Audio
{
    public class AudioManager : ASingleton<AudioManager>
    {
        [SerializeField] private AudioClipCatalogue _catalogue;

        [SerializeField] private AudioSource _audioSource;

        public float BackgroundVolume = 0.5f;
        public float EffectVolume = 0.1f;

        public void PlayBackgroundAudio(string audioCode)
        {
            _audioSource.Stop();

            _audioSource.volume = BackgroundVolume;

            AudioClip audioClip = _catalogue.GetItem(audioCode);
            _audioSource.clip = audioClip;

            _audioSource.loop = true;

            _audioSource.Play();
        }

        public void StopBackgroundAudio(string audioCode)
        {
            _audioSource.Stop();
        }

        public void PlayEffectAudio(string audioCode)
        {
            PlayOneShotAudio(audioCode, BackgroundVolume);
        }

        private void PlayOneShotAudio(string audioCode, float volumeScale)
        {
            AudioClip audioClip = _catalogue.GetItem(audioCode);

            _audioSource.PlayOneShot(audioClip, volumeScale);
        }
    }
}
