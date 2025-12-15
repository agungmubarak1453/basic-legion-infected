using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;

namespace BasicLegionInfected.Animation.Animations
{
    public class TextTypingAnimation : AAnimation
    {
        [SerializeField] private TextMeshProUGUI _text;

        private string _initialText;

        private void Awake()
        {
            _initialText = _text.text;
        }

        public override void StartAnimation()
        {
            base.StartAnimation();

            _text.text = "";

            StartCoroutine(TypeText());
        }

        protected override bool CheckAnimationFinishCondition()
        {
            return _text.text.Equals(_initialText);
        }

        protected override void OnPlaying()
        {

        }

        private IEnumerator TypeText()
        {
            string textBuffer = "";
            foreach (char c in _initialText)
            {
                textBuffer += c;
                _text.text = textBuffer;
                yield return new WaitForSeconds(1f / SpeedMultiplier);
            }
        }
    }
}
