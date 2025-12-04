using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace BasicLegionInfected.Game
{
    public abstract class AEffect : MonoBehaviour
    {
		private bool _isPermanent = false;
		private float _timer;

		public virtual void Initialize(EffectData effectData)
		{
			if(effectData.DurationSecond < 0)
			{
				_isPermanent = true;
			}

			if (effectData.IsDurationStacked)
			{
				_timer += effectData.DurationSecond;
			}
			else
			{
				_timer = effectData.DurationSecond;
			}
		}

		public abstract void Activate();
        public abstract void ApplyEffect();

		public virtual void Tick(float deltaSecond)
		{
			if (_isPermanent) return;

			_timer -= deltaSecond;
			
			if (_timer < 0)
			{
				End();
			}
		}

		public abstract void ApplyTick();

		public virtual void End()
		{
			GameObject.Destroy(gameObject);
		}
	}
}
