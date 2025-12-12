using UnityEngine;
using UnityEngine.Events;

namespace BasicLegionInfected.Animation
{
    public class PopUpAnimation : MonoBehaviour
    {
        [SerializeField] private Transform _transform;

        public bool IsAnimationOnStart = true;
        public float SpeedMultiplier = 1f;

        private bool _isPlaying;
        private Vector3 _initialScale;

        [field: SerializeField] public UnityEvent OnAnimationFinished { get; private set; } = new();

        private void Awake()
        {
            _initialScale = _transform.localScale;
        }

        private void Start()
        {
            if(IsAnimationOnStart) StartAnimation();
        }

        private void Update()
        {
            if (_isPlaying)
            {
                _transform.localScale = Vector3.Lerp(_transform.localScale, _initialScale, 0.1f * SpeedMultiplier);

                if (transform.localScale == _initialScale)
                {
                    _isPlaying = false;

                    OnAnimationFinished.Invoke();
                }
            }
        }

        public void StartAnimation()
        {
            _transform.localScale = Vector3.zero;
            _isPlaying = true;
        }
    }
}
