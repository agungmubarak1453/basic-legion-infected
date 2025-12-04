using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BasicLegionInfected.Game
{
    [CreateAssetMenu(fileName = "Effect Data", menuName = "Basic Legion Infected/Game/Effect Data")]
    public class EffectData : ScriptableObject
    {
        public GameObject EffectPrefab;
        [Tooltip("Use -1 for permanent effect")] public float Duration;
        public bool IsDurationStacked;
        public bool IsEffectStacked;

        public AEffect Initialize(GameObject effected)
        {
            GameObject newEffect = GameObject.Instantiate(EffectPrefab, effected.transform);
            AEffect effect = newEffect.GetComponent<AEffect>();

            effect.Initialize(this);

            return effect;
        }
	}
}
