using System.Collections;
using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace BasicLegionInfected.Game
{
    public class EffectManager : MonoBehaviour
    {
        public Dictionary<EffectData, AEffect> _effects = new();

		private void Update()
		{
			foreach (AEffect effect in _effects.Values)
			{
				effect.Tick(Time.deltaTime);
				effect.ApplyTick();
			}
		}

		private void AddEffect(EffectData effectData)
		{
			AEffect effect = _effects[effectData];

			if (effectData.IsEffectStacked && effect != null)
			{
				effect.Initialize(effectData);
				effect.Activate();
			}
			else
			{
				_effects[effectData] = effectData.Initialize(gameObject);

				effect = _effects[effectData];
				effect.ApplyEffect();
			}
		}

		private void RemoveEffect(EffectData effectData) {
			_effects[effectData]?.End();
			_effects.Remove(effectData);
		}
	}
}
