using System.Collections.Generic;

using UnityEngine;

namespace BasicLegionInfected.Game
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private EffectData _infectedEffectData;

        public float CureRadius = 3f;

		public void Cure(Vector3 position)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(position, CureRadius, Vector2.zero);

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
    }
}
