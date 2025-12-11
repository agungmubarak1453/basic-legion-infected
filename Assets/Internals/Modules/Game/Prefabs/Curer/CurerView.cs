using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using BasicLegionInfected.Utility;

namespace BasicLegionInfected.Game
{
    public class CurerView : MonoBehaviour
    {
        [SerializeField] private Curer _curer;

        [SerializeField] private GameObject _radiusVisualizer;

        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private Color _beforeDestructionColor;
        [SerializeField] private float _notificationBeforeDestructionTimeSeconds = 2f;

		private void Start()
		{
			Vector3 size = new(_curer.CureRadius * 2, _curer.CureRadius * 2, transform.localScale.z);

			_radiusVisualizer.transform.localScale = size;
		}

        private void Update()
        {
            if (Rounder.IsNearFloat(_curer.Timer, _notificationBeforeDestructionTimeSeconds, 0.5f))
            {
                _sprite.color = _beforeDestructionColor;
            }
        }
    }
}
