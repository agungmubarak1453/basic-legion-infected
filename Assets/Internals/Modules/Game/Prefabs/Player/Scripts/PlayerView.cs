using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using BasicLegionInfected.Input;

namespace BasicLegionInfected.Game
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private PlayerManager _playerManager;

		[SerializeField] private Camera _playerCamera;
		private Vector3 _targetCameraPosition;
		public float ZoomSpeed = 1f;
		public float MinCameraSize = 2f;
		public float MaxCameraSize = 20f;
		private float _targetCameraSize;

		[SerializeField] private GameObject mouseVisualizer;

		private void Start()
		{
			InputManager.Instance.OnSwipe.AddListener(MoveCamera);
			InputManager.Instance.OnHover.AddListener(ShowCurePositioning);
			InputManager.Instance.OnClick.AddListener(Cure);

			_targetCameraPosition = _playerCamera.transform.position;
			_targetCameraSize = _playerCamera.orthographicSize;
		}

		private void Update()
		{
			_playerCamera.transform.position = Vector3.Lerp(
				_playerCamera.transform.position,
				_targetCameraPosition,
				Time.deltaTime * 10f
			);

			_playerCamera.orthographicSize = Mathf.Lerp(
				_playerCamera.orthographicSize,
				_targetCameraSize,
				Time.deltaTime * 10f
			);
		}

		private void OnDisable()
		{
			InputManager.Instance.OnSwipe.RemoveListener(MoveCamera);
			InputManager.Instance.OnHover.RemoveListener(ShowCurePositioning);
			InputManager.Instance.OnClick.RemoveListener(Cure);
		}

		public void ZoomIn()
		{
			AdjustCameraZoom(ZoomSpeed);
		}

		public void ZoomOut()
		{
			AdjustCameraZoom(-ZoomSpeed);
		}

		private void AdjustCameraZoom(float zoomDelta)
		{
			float newCameraSize = _targetCameraSize - zoomDelta;

			if (newCameraSize < MinCameraSize || newCameraSize > MaxCameraSize)
			{
				return;
			}

			_targetCameraSize = newCameraSize;
		}

		private void MoveCamera(Vector3 swipeDirection)
		{
			Vector3 zeroScreenPositionInWorldPosition = _playerCamera.ScreenToWorldPoint(Vector3.zero);
			Vector3 swipeWorldDirection = _playerCamera.ScreenToWorldPoint(swipeDirection) - zeroScreenPositionInWorldPosition;
			swipeWorldDirection.z = 0f;

			Debug.Log($"Swiped with direction {swipeDirection} - {swipeWorldDirection}");

			_targetCameraPosition = _playerCamera.transform.position - swipeWorldDirection;
		}

		private void Cure(Vector3 mouseScreenPosition)
		{
			Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
			mouseWorldPosition.z = 0f;

			_playerManager.Cure(mouseWorldPosition);
		}

		private void ShowCurePositioning(Vector3 mouseScreenPosition)
		{
			// Check mouse outside of screen
			if (
				mouseScreenPosition.x < 0f || mouseScreenPosition.y < 0f ||
				mouseScreenPosition.x > Screen.width || mouseScreenPosition.y > Screen.height
			)
			{
				mouseVisualizer.SetActive(false);
				return;
			}

			Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
			mouseWorldPosition.z = 0f;

			mouseVisualizer.SetActive(true);
			mouseVisualizer.transform.localScale = new Vector3(_playerManager.CureRadius, _playerManager.CureRadius, mouseVisualizer.transform.localScale.z);
			mouseVisualizer.transform.position = mouseWorldPosition;
		}
	}
}
