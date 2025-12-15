using UnityEngine;

namespace BasicLegionInfected.Core
{
    public class CurveUser
    {
        public AnimationCurve Curve;
        public float DurationSeconds;
        public bool IsLooping;

        private bool _isPlaying;
        public bool IsPlaying
        {
            get
            {
                if (!_isPlaying) return false;


                if (IsLooping)
                {
                    return true;
                }
                else
                {
                    float timeElapsedSeconds = Time.time - StartTime;
                    bool isFinished = timeElapsedSeconds > DurationSeconds;

                    if (isFinished) _isPlaying = false;

                    return _isPlaying;
                }
            }
            private set
            {
                _isPlaying = value;
            }
        }
        public float StartTime { get; private set; }

        public CurveUser(AnimationCurve curve, float durationSeconds, bool isLooping)
        {
            Curve = curve;
            IsLooping = isLooping;
            DurationSeconds = durationSeconds;
        }

        public void Start()
        {
            StartTime = Time.time;

            IsPlaying = true;
        }

        public float GetCurrentTimeValue()
        {
            if (IsPlaying)
            {
                float timeElapsedSeconds = Time.time - StartTime;
                float latestCycleTimeElapsedSeconds = timeElapsedSeconds % DurationSeconds;
                float normalizedTimeElapsedSeconds = Mathf.Clamp01(latestCycleTimeElapsedSeconds / DurationSeconds);
                float value = Curve.Evaluate(normalizedTimeElapsedSeconds);

                return value;
            }

            return 0f;
        }
    }
}
