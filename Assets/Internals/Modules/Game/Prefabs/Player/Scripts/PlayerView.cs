using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace BasicLegionInfected.Game
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private PlayerManager _playerManager;

		[SerializeField] private GameObject mouseVisualizer;

		private void Update()
		{
			Vector3 mouseScreenPosition = Input.mousePosition;

			// Check mouse outside of screen
			if (
				mouseScreenPosition.x < 0f || mouseScreenPosition.y < 0f ||
				mouseScreenPosition.x > Screen.width || mouseScreenPosition.y > Screen.height
			) {
				mouseVisualizer.SetActive(false);
				return;
			}

			Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
			mouseWorldPosition.z = 0f;

			mouseVisualizer.SetActive(true);
			mouseVisualizer.transform.localScale = new Vector3(_playerManager.CureRadius, _playerManager.CureRadius, mouseVisualizer.transform.localScale.z);
			mouseVisualizer.transform.position = mouseWorldPosition;

			if (Input.GetMouseButtonDown(0))
			{
				Debug.Log($"Click at {mouseWorldPosition}");

				_playerManager.Cure(mouseWorldPosition);
			}
		}
	}
}
