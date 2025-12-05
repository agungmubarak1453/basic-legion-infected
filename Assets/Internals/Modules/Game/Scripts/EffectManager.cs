using System.Collections;
using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace BasicLegionInfected.Game
{
    public class EffectManager : MonoBehaviour
    {
        public Dictionary<EffectData, AEffect> Effects { get; private set; } = new();

		private void Update()
		{
			foreach (AEffect effect in Effects.Values)
			{
				effect.Tick(Time.deltaTime);
				effect.ApplyTick();
			}
		}

		private void FixedUpdate()
		{
			foreach (AEffect effect in Effects.Values)
			{
				effect.ApplyFixedTick();
			}
		}

		public void AddEffect(EffectData effectData)
		{
			AEffect effect = Effects.GetValueOrDefault(effectData);

			if (effectData.IsEffectStacked && effect != null)
			{
				effect.Initialize(effectData);
				effect.Activate();
			}
			else
			{
				Effects[effectData] = effectData.Initialize(gameObject);

				effect = Effects[effectData];
				effect.Activate();
				effect.ApplyEffect();
			}
		}

		public void RemoveEffect(EffectData effectData) {
			AEffect effect = Effects.GetValueOrDefault(effectData);

			if (effect == null) {
				return;
			}

			effect.End();
			Effects.Remove(effectData);
		}
	}
}
