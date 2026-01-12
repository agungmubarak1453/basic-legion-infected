using System;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;

using BasicLegionInfected.Utility;
using BasicLegionInfected.Environment;

namespace BasicLegionInfected.Game
{
    public class Session
    {
        private LevelManager _levelManager;
        private PlayerManager _playerManager;

		private EffectData _infectedEffectData;

        public int RoomCount = 16;
		public int PersonInRoomCount = 3;
        public int StartingInfectedCount = 5;

		public int CurrentPersontCount { get; private set; } = 0;
		public int CurrentInfectedCount { get; private set; } = 0;

        public UnityEvent OnClose { get; private set; } = new();

        public Session(
            LevelManager levelManager, PlayerManager playerManager, EffectData infectedEffectData
        ) {
            _levelManager = levelManager;
            _playerManager = playerManager;
            _infectedEffectData = infectedEffectData;
        }

        private async Task OnStart()
        {
            _playerManager.Clear();

            _levelManager.RoomCount = RoomCount;
            _levelManager.PersonInRoomCount = PersonInRoomCount;

            await _levelManager.LoadLevel();

            Person[] persons = _levelManager.GetComponentsInChildren<Person>();
			GameDoor[] doors = _levelManager.GetComponentsInChildren<GameDoor>();

			int neeededInfectedCount = StartingInfectedCount;
            Randomizer.Shuffle(persons); // To select random person

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

            foreach (GameDoor door in doors)
            {
                door.Open();
            }
		}

        public void OnUpdate() { }

        public void OnFixedUpdate() { }

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

        public async void Start()
        {
            await OnStart();
        }

        public void Close()
        {
            OnClose.Invoke();
		}
    }
}
