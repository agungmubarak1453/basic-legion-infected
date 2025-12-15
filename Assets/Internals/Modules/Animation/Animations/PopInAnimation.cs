using UnityEngine;

namespace BasicLegionInfected.Animation.Animations
{
    public class PopInAnimation : AAnimation
    {
        [SerializeField] private Transform _transform;

        private Vector3 _initialScale;

        private void Awake()
        {
            _initialScale = _transform.localScale;
        }

        protected override void OnEnable()
        {
            _transform.localScale = _initialScale;

            base.OnEnable();
        }

        protected override void OnPlaying()
        {
            _transform.localScale = Vector3.Lerp(_transform.localScale, Vector3.zero, 0.1f * SpeedMultiplier);
        }

        public override void StartAnimation()
        {
            _transform.localScale = _initialScale;

            base.StartAnimation();
        }

        protected override bool CheckAnimationFinishCondition()
        {
            return transform.localScale == Vector3.zero;
        }
    }
}
