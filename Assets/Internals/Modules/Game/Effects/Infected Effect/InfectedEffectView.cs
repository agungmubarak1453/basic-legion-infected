using UnityEngine;

namespace BasicLegionInfected.Game
{
    public class InfectedEffectView : MonoBehaviour
    {
		[SerializeField] public Color _infectedColor;


		private void Start()
		{
			SpriteRenderer spriteRenderer = transform.parent.GetComponentInChildren<SpriteRenderer>();

			spriteRenderer.color = _infectedColor;
		}
	}
}
