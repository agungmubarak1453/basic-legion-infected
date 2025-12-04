using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace BasicLegionInfected.Game.Infected
{
	public class InfectedEffect : AEffect
	{
		[SerializeField] private EffectData InfectedEffectData;
		private Collider2D _effectedCollider;

		public float DetectionRadius = 2f;

		public override void Activate()
		{
			_effectedCollider = transform.parent.GetComponent<Collider2D>();
		}

		public override void ApplyEffect()
		{
			// Nothing to do when effect is applied;
		}

		public override void ApplyTick()
		{
			
		}

		public override void ApplyFixedTick()
		{
			RaycastHit2D[] hits = Physics2D.CircleCastAll(
				_effectedCollider.transform.position, DetectionRadius, Vector2.zero
			);

			foreach (RaycastHit2D hit in hits)
			{
				if (hit.collider == _effectedCollider) continue;

				Debug.Log($"InfectedEffect from {transform.parent.name} touch {hit.collider.name}");

				EffectManager effectManager = hit.collider.GetComponentInChildren<EffectManager>();

				if (effectManager != null && effectManager.Effects.GetValueOrDefault(InfectedEffectData) == null)
				{
					effectManager.AddEffect(InfectedEffectData);
				}
			}
		}
	}
}
