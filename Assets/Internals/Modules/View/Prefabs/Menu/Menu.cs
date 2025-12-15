using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using BasicLegionInfected.Animation.Animations;
using BasicLegionInfected.Input;

using UnityEngine.Events;

namespace BasicLegionInfected.View
{
    public class Menu : MonoBehaviour
    {
        public string Code;

        [SerializeField] private Button _closeButton;
        [SerializeField] private PopUpAnimation _popOutAnimation;
        [SerializeField] private PopInAnimation _popInAnimation;

        public bool IsHideOnStart = true;

        [field: SerializeField] public UnityEvent OnHide { get; private set; } = new();

        private void Awake()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
            _popInAnimation.OnAnimationFinished.AddListener(OnPopInAnimationFinished);

            MenuManager.Instance.AddMenu(this);

            if (IsHideOnStart) gameObject.SetActive(false);
        }

        private void OnCloseButtonClicked()
        {
            Hide();
        }

        private void OnPopInAnimationFinished()
        {
            InputManager.Instance.UnblockUIInput();
            gameObject.SetActive(false);

            OnHide.Invoke();
        }

        private void OnDestroy()
        {
            MenuManager.Instance.RemoveMenu(this);
        }

        public void Show()
        {
            InputManager.Instance.BlockUIInput();

            gameObject.SetActive(true);

            _popOutAnimation.StartAnimation();
        }

        public void Hide()
        {
            _popInAnimation.StartAnimation();
        }
    }
}
