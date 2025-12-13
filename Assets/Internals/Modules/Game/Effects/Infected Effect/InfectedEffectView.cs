using UnityEngine;

using BasicLegionInfected.Animation;
using BasicLegionInfected.Game.Infected;

namespace BasicLegionInfected.Game
{
    public class InfectedEffectView : MonoBehaviour
    {
        [SerializeField] private InfectedEffect _infectedEffect;

        [SerializeField] public Color _infectedColor;

		private SpriteRenderer _spriteRenderer;
		private Color _oldColor;

		private void Awake()
		{
			_infectedEffect.OnEnd.AddListener(Revert);

			_spriteRenderer = transform.parent.GetComponentInChildren<SpriteRenderer>();

			_oldColor = _spriteRenderer.color;
			_spriteRenderer.color = _infectedColor;
        }

		public void Revert()
		{
			_spriteRenderer.color = _oldColor;

			GameObject particle = ParticleManager.Instance.SpawnParticle("cured", _infectedEffect.transform.position);
		}
	}
}
