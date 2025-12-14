using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using BasicLegionInfected.Animation.Animations;

namespace BasicLegionInfected.View
{
    public class Menu : MonoBehaviour
    {
        public string Code;

        [SerializeField] private Button _closeButton;
        [SerializeField] private PopUpAnimation _popOutAnimation;
        [SerializeField] private PopInAnimation _popInAnimation;

        private void Awake()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
            _popInAnimation.OnAnimationFinished.AddListener(OnPopInAnimationFinished);

            MenuManager.Instance.AddMenu(this);
        }

        private void OnCloseButtonClicked()
        {
            Hide();
        }

        private void OnPopInAnimationFinished()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            MenuManager.Instance.RemoveMenu(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);

            _popOutAnimation.StartAnimation();
        }

        public void Hide()
        {
            _popInAnimation.StartAnimation();
        }
    }
}
