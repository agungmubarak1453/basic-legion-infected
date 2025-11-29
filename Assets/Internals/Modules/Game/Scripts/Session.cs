using System;

using UnityEngine;

namespace BasicLegionInfected.Game
{
    public class Session : MonoBehaviour
    {
        private LevelManager _levelManager;

        public void Initialize(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }

        public void Start()
        {
            _levelManager.LoadLevel();

			throw new NotImplementedException();
		}

        public void Close()
        {
            throw new NotImplementedException();
        }
    }
}
