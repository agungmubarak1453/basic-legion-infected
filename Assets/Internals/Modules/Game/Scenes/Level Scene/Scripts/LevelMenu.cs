using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace BasicLegionInfected.Game
{
    public class LevelMenu : MonoBehaviour
    {
        [SerializeField] private Button _repeatButton;

        private void Awake()
        {
            _repeatButton.onClick.AddListener(OnRepeatButtonClicked);
        }

        private void OnRepeatButtonClicked()
        {
            RepeatGame();
        }

        public void RepeatGame()
        {
            GameManager.Instance.RepeatGame();
        }
    }
}
