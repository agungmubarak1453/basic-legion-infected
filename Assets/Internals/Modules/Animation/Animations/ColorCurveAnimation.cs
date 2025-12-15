using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using BasicLegionInfected.Core;

namespace BasicLegionInfected.Animation
{
    public class ColorCurveAnimation : AAnimation
    {
        [SerializeField] private SpriteRenderer _sprite;

        private Color _initialColor;
        public Color TargetColor;
        public float DurationSeconds = 1f;
        public bool IsLooping = true;

        [SerializeField] private AnimationCurve _animationCurve;
        private CurveUser _curveUser;

        private void Awake()
        {
            _initialColor = _sprite.color;
            _curveUser = new(_animationCurve, DurationSeconds, IsLooping);
        }

        protected override void OnPlaying()
        {
            //Debug.Log($"ColorCurveAnimation _curveUser.IsPlaying.: {_curveUser.IsPlaying}");
            //Debug.Log($"ColorCurveAnimation _curveUser.GetCurrentTimeValue(): {_curveUser.GetCurrentTimeValue()}");
            _sprite.color = Color.Lerp(_initialColor, TargetColor, _curveUser.GetCurrentTimeValue());
        }

        public override void StartAnimation()
        {
            base.StartAnimation();

            _curveUser.Start();
        }

        protected override bool CheckAnimationFinishCondition()
        {
            //Debug.Log($"_curveUser.IsPlaying: {_curveUser.IsPlaying}");
            return !_curveUser.IsPlaying;
        }
    }
}
