using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BasicLegionInfected.Game
{
    public class Curer : MonoBehaviour
    {
		[SerializeField] private EffectData _infectedEffectData;

		public float CureRadius = 3f;

		public float Duration = 3f;
		public float Timer { get; private set; }

		private void Awake()
		{
			Timer = Duration;
		}

		private void Update()
		{
			Timer -= Time.deltaTime;

			if (Timer < 0 )
			{
				End();
			}
		}

		private void FixedUpdate()
		{
			Cure();
		}

		public void Cure()
		{
			RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, CureRadius, Vector2.zero);

			foreach (RaycastHit2D hit in hits)
			{
				EffectManager effectManager = hit.collider?.GetComponentInChildren<EffectManager>();
				if (effectManager != null && effectManager.Effects.GetValueOrDefault(_infectedEffectData) != null)
				{
					Debug.Log($"Cured infected at {hit.collider.name}");
					effectManager.RemoveEffect(_infectedEffectData);
				}
			}
		}

		public void End()
		{
			GameObject.Destroy(gameObject);
		}
	}
}
