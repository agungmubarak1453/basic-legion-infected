using System;

using UnityEngine;
using UnityEngine.Events;

namespace BasicLegionInfected.Game
{
    public class Session : MonoBehaviour
    {
        private LevelManager _levelManager;

		private EffectData _infectedEffectData;
        public int StartingInfectedCount;

        public int CurrentPersontCount { get; private set; } = 0;
		public int CurrentInfectedCount { get; private set; } = 0;

        public UnityEvent OnClose { get; private set; } = new();

		public void Initialize(
            LevelManager levelManager, EffectData infectedEffectData, int startingInfectedCount
        ) {
            _levelManager = levelManager;
            _infectedEffectData = infectedEffectData;
            StartingInfectedCount = startingInfectedCount;
        }

        public void Start()
        {
            _levelManager.LoadLevel();

            Person[] persons = _levelManager.GetComponentsInChildren<Person>();

            int neeededInfectedCount = StartingInfectedCount;
            foreach (Person person in persons)
            {
                CurrentPersontCount += 1;

                EffectManager effectManager = person.GetComponentInChildren<EffectManager>();

                effectManager.OnEffectAdded.AddListener(OnPersonEffectAdded);
                effectManager.OnEffectRemoved.AddListener(OnPersonEffectRemoved);

				if (neeededInfectedCount > 0)
                {
                    effectManager.AddEffect(_infectedEffectData);

					neeededInfectedCount--;
                }
            }
		}

        private void OnPersonEffectAdded(EffectData effectData)
        {
            if( effectData == _infectedEffectData)
            {
                CurrentInfectedCount += 1;
			}
		}

        private void OnPersonEffectRemoved(EffectData effectData)
        {
			if (effectData == _infectedEffectData)
			{
				CurrentInfectedCount -= 1;

                if (CurrentInfectedCount <= 0)
                {
                    Close();
				}
			}
		}

        public void Close()
        {
            OnClose.Invoke();
		}
    }
}
