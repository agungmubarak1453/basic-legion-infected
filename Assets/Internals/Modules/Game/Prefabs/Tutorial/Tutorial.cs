using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using BasicLegionInfected.Input;

namespace BasicLegionInfected.Game
{
    public class Tutorial : MonoBehaviour
    {
        [field: SerializeField] public string Code { get; private set; }

        public bool StartOnEnable = true;
        public bool IsBlockGameInput = false;

        private TutorialItem[] _tutorialItems;

        private int _currentTutorialItemIndex;

        public bool IsCompleted
        {
            get
            {
                return false;

                int isCompletedData = PlayerPrefs.GetInt($"tutorial_{Code}", 0);

                if (isCompletedData == 1) return true;
                return false;
            }

            set
            {
                if (value) PlayerPrefs.SetInt($"tutorial_{Code}", 1);
            }
        }

        private void OnEnable()
        {
            _tutorialItems = GetComponentsInChildren<TutorialItem>();

            foreach (TutorialItem tutorialItem in _tutorialItems)
            {
                tutorialItem.gameObject.SetActive(false);
                tutorialItem.Set(this);
            }

            if (StartOnEnable) StartTutorial();
        }

        public void StartTutorial()
        {
            if (IsCompleted) return; 

            _currentTutorialItemIndex = -1;

            if (IsBlockGameInput) InputManager.Instance.IsInputBlocked = true;

            StartNextTutorialItem();
        }

        public void StartNextTutorialItem()
        {
            if (_currentTutorialItemIndex > -1 && _currentTutorialItemIndex < _tutorialItems.Length)
            {
                _tutorialItems[_currentTutorialItemIndex].gameObject.SetActive(false);
            }

            _currentTutorialItemIndex++;

            if (_currentTutorialItemIndex >= _tutorialItems.Length)
            {
                IsCompleted = true;

                return;
            }

            _tutorialItems[_currentTutorialItemIndex].StartTutorial();
        }
    }
}
