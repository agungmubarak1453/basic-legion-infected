using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace BasicLegionInfected.Game
{
    public class TutorialItem : MonoBehaviour
    {
        private Tutorial _tutorial;

        [SerializeField] private Button _nextButton;

        private void Awake()
        {
            _nextButton.onClick.AddListener(OnNextButtonClicked);
        }

        private void OnNextButtonClicked()
        {
            _tutorial.StartNextTutorialItem();
        }

        public void Set(Tutorial tutorial)
        {
            _tutorial = tutorial;
        }

        public void StartTutorial()
        {
            gameObject.SetActive(true);
        }
    }
}
