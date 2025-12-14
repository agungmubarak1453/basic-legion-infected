using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using BasicLegionInfected.View;

namespace BasicLegionInfected.Game
{
    public class LevelSceneView : MonoBehaviour
    {
        [SerializeField] private Button _menuButton;

        private void Awake()
        {
            _menuButton.onClick.AddListener(OnMenuButtonClicked);
        }

        private void OnEnable()
        {
            Menu levelMenu = MenuManager.Instance.GetMenu("level");
            levelMenu.gameObject.SetActive(false);
        }

        private void OnMenuButtonClicked()
        {
            ShowMenu();
        }

        public void ShowMenu()
        {
            MenuManager.Instance.ShowMenu("level");
        }
    }
}
