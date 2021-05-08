using DG.Tweening;
using DG.Tweening.Core;
using TMPro;
using UnityEngine;

namespace PreparationScreen
{
    public class PopupText : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI text;

        private void Start()
        {
            var animator = new DOTweenTMPAnimator(text);
            var sequence = DOTween.Sequence().Append(text.DOFade(0, 0.1f));
            for (var i = 0; i < animator.textInfo.characterCount; ++i)
            {
                sequence
                    .Append(animator.DOOffsetChar(i, animator.GetCharOffset(i) + new Vector3(0, 30, 0), 0.4f).SetEase(Ease.OutFlash, 2))
                    .Join(animator.DOFadeChar(i, 1, 0.4f))
                    .Join(animator.DOScaleChar(i, 1f, 0.4f))
                    .Join(animator.DOFadeChar(i, 1f, 0.4f))
                    .SetDelay(0.1f * i);
            }

            sequence
                .Append(text.DOFade(0, 0f))
                .SetLoops(-1);
        }
    }
}
