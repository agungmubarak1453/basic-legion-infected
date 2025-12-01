using UnityEngine;
using UnityEngine.Events;

namespace BasicLegionInfected.Debugging.Core
{
    public class InspectorDebugger : MonoBehaviour
    {
        public UnityEvent OnAction = new();

        public void DoAction()
        {
            OnAction.Invoke();
        }
    }
}
