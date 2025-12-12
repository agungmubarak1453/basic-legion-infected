using UnityEngine;
using UnityEngine.Events;

namespace BasicLegionInfected.Animation
{
    public abstract class AAnimation : MonoBehaviour, IAnimation
    {
        public bool IsAnimationOnStart = true;
        public float SpeedMultiplier = 1f;

        protected bool _isPlaying;

        [field: SerializeField] public UnityEvent OnAnimationFinished { get; private set; } = new();

        protected virtual void OnEnable()
        {
            if (IsAnimationOnStart) StartAnimation();
        }

        protected virtual void Update()
        {
            if (_isPlaying)
            {
                OnPlaying();

                if (CheckAnimationFinishCondition())
                {
                    _isPlaying = false;

                    OnAnimationFinished.Invoke();
                }
            }
        }

        protected abstract void OnPlaying();

        public virtual void StartAnimation()
        {
            _isPlaying = true;
        }

        protected abstract bool CheckAnimationFinishCondition();
    }
}
