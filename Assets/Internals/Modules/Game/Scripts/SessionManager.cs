using UnityEngine;

namespace BasicLegionInfected.Game
{
    public class SessionManager : MonoBehaviour
    {
        [SerializeField] EffectData _infectedEffectData;

        public Session CreateGameSession()
        {
            LevelManager levelManager = FindObjectOfType<LevelManager>();

            Session newSession = gameObject.AddComponent<Session>();
            newSession.Initialize(levelManager, _infectedEffectData, 2);

            return newSession;
        }
    }
}
