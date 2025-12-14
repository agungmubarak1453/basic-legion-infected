using UnityEngine;
using UnityEngine.Events;
using UnityInput = UnityEngine.Input;

using BasicLegionInfected.Core;
using UnityEngine.EventSystems;

namespace BasicLegionInfected.Input
{
	public class InputManager : ASingleton<InputManager>
	{
		public enum HoldState
		{
			Start,
			InHolding,
			End
		}

		[SerializeField] private GameObject _inputBlocker;

		[SerializeField] public UnityEvent<Vector3> OnHover { get; private set; } = new();
		[SerializeField] public UnityEvent<Vector3> OnClick { get; private set; } = new();
		[SerializeField] public UnityEvent<Vector3, HoldState> OnHold { get; private set; } = new();
		[SerializeField] public UnityEvent<Vector3> OnHoldClick { get; private set; } = new();
		[SerializeField] public UnityEvent<Vector3> OnSwipe { get; private set; } = new();

		public float MinHoldDurationSecond = 2f;
		private float _holdDurationSecond;

		public float MinimumSwipeDistance = 2f;
		private Vector3 _swipeStartPosition;

		public bool IsInputBlocked = false;
		public bool IsOverUI { get; private set; }

        private void OnEnable()
        {
			UnblockUIInput();
        }

        private void Update()
		{
			Vector3 mousePosition = UnityInput.mousePosition;

			OnHover.Invoke(mousePosition);

			IsOverUI = EventSystem.current.IsPointerOverGameObject();

			if (UnityInput.GetMouseButtonDown(0))
			{
				_holdDurationSecond = 0f;
				_swipeStartPosition = mousePosition;

				OnHold.Invoke(mousePosition, HoldState.Start);
			}

			if (UnityInput.GetMouseButton(0))
			{
				_holdDurationSecond += Time.deltaTime;

				OnHold.Invoke(mousePosition, HoldState.InHolding);
			}

			if (UnityInput.GetMouseButtonUp(0))
			{
				if (_holdDurationSecond >= MinHoldDurationSecond)
				{
					OnHoldClick.Invoke(mousePosition);
					OnHold.Invoke(mousePosition, HoldState.End);

					Vector3 swipeDirection = mousePosition - _swipeStartPosition;
					if (swipeDirection.magnitude >= MinimumSwipeDistance)
					{
						OnSwipe.Invoke(swipeDirection);
					}
				}
				else
				{
					if (IsInputBlocked || IsOverUI) return;

					OnClick.Invoke(mousePosition);
				}
			}
		}

        public void BlockUIInput()
		{
			_inputBlocker.SetActive(true);
		}

		public void UnblockUIInput()
		{
			_inputBlocker.SetActive(false);
		}
	}
}
