using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BasicLegionInfected.Game
{
    public class GameManagerAccesser : MonoBehaviour
    {
        public void PlayGame()
        {
            GameManager.Instance.PlayGame();
        }
    }
}
