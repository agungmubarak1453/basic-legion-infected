using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace BasicLegionInfected.Game
{
    public abstract class AEffect : MonoBehaviour
    {
		public float Duration;

		private float _timer;

		public virtual void Initialize(EffectData effectData)
		{
			Duration = effectData.Duration;
			if (effectData.IsDurationStacked)
			{
				_timer += Duration;
			}
			else
			{
				_timer = Duration;
			}
		}

		public abstract void Activate();
        public abstract void ApplyEffect();

		public virtual void Tick(float deltaSecond)
		{
			_timer -= deltaSecond;
			
			if (_timer < 0)
			{
				End();
				return;
			}
		}

		public abstract void ApplyTick();

		public virtual void End()
		{
			GameObject.Destroy(gameObject);
		}
	}
}
