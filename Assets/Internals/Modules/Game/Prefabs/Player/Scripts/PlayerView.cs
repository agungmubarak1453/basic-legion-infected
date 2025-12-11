using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

using BasicLegionInfected.Input;

namespace BasicLegionInfected.Game
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private PlayerManager _playerManager;

		[SerializeField] private Camera _playerCamera;

		public float Sensitivity = 0.5f;

		private Vector3 _latestMousePosition;
		private Vector3 _targetCameraPosition;

		public float ZoomSpeed = 1f;
		public float MinCameraSize = 2f;
		public float MaxCameraSize = 20f;
		private float _targetCameraSize;

		[SerializeField] Slider _energySlider;

		[SerializeField] private GameObject mouseCureVisualizer;

		private void Start()
		{
			InputManager.Instance.OnHold.AddListener(MoveCamera);
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

			_energySlider.value = _playerManager.EnergyManager.Energy / 100;
		}

		private void OnDisable()
		{
			InputManager.Instance.OnHold.RemoveListener(MoveCamera);
			InputManager.Instance.OnHover.RemoveListener(ShowCurePositioning);
			InputManager.Instance.OnClick.RemoveListener(Cure);
		}

		private void OnInputClick(Vector3 mous)
		{

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

		private void MoveCamera(Vector3 mousePosition, InputManager.HoldState holdState)
		{
			switch (holdState)
			{
				case InputManager.HoldState.Start:
					_latestMousePosition = mousePosition;
					break;
				case InputManager.HoldState.InHolding:
					Vector3 deltaMousePosition = mousePosition - _latestMousePosition;

					Vector3 zeroScreenPositionInWorldPosition = _playerCamera.ScreenToWorldPoint(Vector3.zero);
					Vector3 swipeWorldDirection = _playerCamera.ScreenToWorldPoint(deltaMousePosition) - zeroScreenPositionInWorldPosition;
					swipeWorldDirection.z = 0f;

					//Debug.Log($"Swiped with direction {deltaMousePosition} - {swipeWorldDirection}");

					_targetCameraPosition = _playerCamera.transform.position - swipeWorldDirection * Sensitivity;
					break;
				case InputManager.HoldState.End:
					break;
			}
		}

		private void Cure(Vector3 mouseScreenPosition)
		{
			Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
			mouseWorldPosition.z = 0f;

			_playerManager.Cure(mouseWorldPosition);
		}

		private void ShowCurePositioning(Vector3 mouseScreenPosition)
		{
			// Check mouse outside of screen and energy availability
			if (
				_playerManager.EnergyManager.Energy < _playerManager.CureEnergy ||
				mouseScreenPosition.x < 0f || mouseScreenPosition.y < 0f ||
				mouseScreenPosition.x > Screen.width || mouseScreenPosition.y > Screen.height
			)
			{
				mouseCureVisualizer.SetActive(false);
				return;
			}

			Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
			mouseWorldPosition.z = 0f;

			mouseCureVisualizer.SetActive(true);
			mouseCureVisualizer.transform.localScale = new Vector3(_playerManager.CureRadius * 2, _playerManager.CureRadius * 2, mouseCureVisualizer.transform.localScale.z);
			mouseCureVisualizer.transform.position = mouseWorldPosition;
		}
	}
}
