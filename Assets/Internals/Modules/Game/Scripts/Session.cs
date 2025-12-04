using System;

using UnityEngine;

namespace BasicLegionInfected.Game
{
    public class Session : MonoBehaviour
    {
        private LevelManager _levelManager;

		private EffectData _infectedEffectData;
        public int _infectedCount;

		public void Initialize(
            LevelManager levelManager, EffectData infectedEffectData, int infectedCount
        ) {
            _levelManager = levelManager;
            _infectedEffectData = infectedEffectData;
            _infectedCount = infectedCount;
        }

        public void Start()
        {
            _levelManager.LoadLevel();

            Person[] persons = _levelManager.GetComponentsInChildren<Person>();

            int neeededInfectedCount = _infectedCount;
            foreach (Person person in persons)
            {
                if (neeededInfectedCount > 0)
                {
                    EffectManager effectManager = person.GetComponentInChildren<EffectManager>();
                    effectManager.AddEffect(_infectedEffectData);

					neeededInfectedCount--;
                }
                else
                {
                    break;
                }
            }
		}

        public void Close()
        {
            throw new NotImplementedException();
        }
    }
}
