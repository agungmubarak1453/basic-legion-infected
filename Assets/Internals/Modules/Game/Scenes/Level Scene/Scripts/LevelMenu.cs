using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using BasicLegionInfected.View;
using Codice.Client.BaseCommands.CheckIn.Progress;
using BasicLegionInfected.Input;

namespace BasicLegionInfected.Game
{
    public class LevelMenu : MonoBehaviour
    {
        [SerializeField] private Menu _menu;
        [SerializeField] private SceneManager _sceneManager;

        [SerializeField] private Button _repeatButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _backToMainButton;

        private void Awake()
        {
            _repeatButton.onClick.AddListener(OnRepeatButtonClicked);
            _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            _backToMainButton.onClick.AddListener(OnBackToMainButtonClicked);
        }

        private void OnRepeatButtonClicked()
        {
            RepeatGame();
            _menu.Hide();
        }

        private void OnSettingsButtonClicked()
        {
            _menu.OnHide.AddListener(OnMenuHideInSettingButtonContext);

            _menu.Hide();
        }

        private void OnMenuHideInSettingButtonContext()
        {
            Menu settingsMenu = MenuManager.Instance.GetMenu("settings");
            settingsMenu.OnHide.AddListener(OnSettingsMenuHide);

            settingsMenu.Show();
        }

        private void OnBackToMainButtonClicked()
        {
            GameManager.Instance.ExitGame();
            _menu.Hide();
            _sceneManager.LoadScene("Main Menu Scene");
        }

        private void OnSettingsMenuHide()
        {
            Menu settingsMenu = MenuManager.Instance.GetMenu("settings");
            settingsMenu.OnHide.RemoveListener(OnSettingsMenuHide);

            _menu.OnHide.RemoveListener(OnMenuHideInSettingButtonContext);

            _menu.Show();
        }

        public void RepeatGame()
        {
            GameManager.Instance.RepeatGame();
        }
    }
}
