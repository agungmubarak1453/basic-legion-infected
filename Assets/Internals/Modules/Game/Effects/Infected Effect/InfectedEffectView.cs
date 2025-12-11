using UnityEngine;

namespace BasicLegionInfected.Game
{
    public class InfectedEffectView : MonoBehaviour
    {
		[SerializeField] public Color _infectedColor;

		private SpriteRenderer _spriteRenderer;
		private Color _oldColor;

		private void Awake()
		{
			_spriteRenderer = transform.parent.GetComponentInChildren<SpriteRenderer>();

			_oldColor = _spriteRenderer.color;
			_spriteRenderer.color = _infectedColor;
		}

		public void Revert()
		{
			_spriteRenderer.color = _oldColor;
		}
	}
}
