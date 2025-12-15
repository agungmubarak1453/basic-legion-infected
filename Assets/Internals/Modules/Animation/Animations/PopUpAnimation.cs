using BasicLegionInfected.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace BasicLegionInfected.Animation.Animations
{
    public class PopUpAnimation : AAnimation
    {
        [SerializeField] private Transform _transform;

        private Vector3 _initialScale;

        private void Awake()
        {
            _initialScale = _transform.localScale;
        }

        protected override void OnPlaying()
        {
            _transform.localScale = Vector3.Lerp(_transform.localScale, _initialScale, 0.1f * SpeedMultiplier);
        }

        public override void StartAnimation()
        {
            _transform.localScale = Vector3.zero;

            base.StartAnimation();
        }

        protected override bool CheckAnimationFinishCondition()
        {
            return Rounder.IsNearVector3(transform.localScale, _initialScale); ;
        }
    }
}
