using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using BasicLegionInfected.Core;
using BasicLegionInfected.View;
using System;

namespace BasicLegionInfected.Game
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private SceneManager _sceneManager;

        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _creditsButton;

        private void Awake()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            _creditsButton.onClick.AddListener(OnCreditsButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            _sceneManager.LoadScene("Level Scene");
        }

        private void OnSettingsButtonClicked()
        {
            MenuManager.Instance.ShowMenu("settings");
        }

        private void OnCreditsButtonClicked()
        {
            MenuManager.Instance.ShowMenu("credits");
        }
    }
}
