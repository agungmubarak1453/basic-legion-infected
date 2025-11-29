using UnityEngine;

namespace BasicLegionInfected.Game
{
    public class SessionManager : MonoBehaviour
    {
        public Session CreateGameSession()
        {
            LevelManager levelManager = FindObjectOfType<LevelManager>();

            Session newSession = gameObject.AddComponent<Session>();
            newSession.Initialize(levelManager);

            return newSession;
        }
    }
}
