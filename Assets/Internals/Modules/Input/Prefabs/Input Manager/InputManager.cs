using UnityEngine;
using UnityEngine.Events;
using UnityInput = UnityEngine.Input;

using BasicLegionInfected.Core;

namespace BasicLegionInfected.Input
{
	public class InputManager : ASingleton<InputManager>
	{
		[SerializeField] public UnityEvent<Vector3> OnHover { get; private set; } = new();
		[SerializeField] public UnityEvent<Vector3> OnClick { get; private set; } = new();
		[SerializeField] public UnityEvent<Vector3> OnHoldClick { get; private set; } = new();
		[SerializeField] public UnityEvent<Vector3> OnSwipe { get; private set; } = new();

		public float MinHoldDurationSecond = 2f;
		private float _holdDurationSecond;

		public float MinimumSwipeDistance = 2f;
		private Vector3 _swipeStartPosition;

		private void Update()
		{
			Vector3 mousePosition = UnityInput.mousePosition;
			OnHover.Invoke(mousePosition);

			if (UnityInput.GetMouseButtonDown(0))
			{
				_holdDurationSecond = 0f;
				_swipeStartPosition = mousePosition;
			}

			if (UnityInput.GetMouseButton(0))
			{
				_holdDurationSecond += Time.deltaTime;
			}

			if (UnityInput.GetMouseButtonUp(0))
			{
				if (_holdDurationSecond >= MinHoldDurationSecond)
				{
					OnHoldClick.Invoke(mousePosition);
					Vector3 swipeDirection = mousePosition - _swipeStartPosition;
					if (swipeDirection.magnitude >= MinimumSwipeDistance)
					{
						OnSwipe.Invoke(swipeDirection);
					}
				}
				else
				{
					OnClick.Invoke(mousePosition);
				}
			}
		}
	}
}
