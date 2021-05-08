using DG.Tweening;
using TMPro;
using UnityEngine;

namespace PreparationScreen
{
    public class LoggerText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI textMeshPro;
        [SerializeField] string[] log;
        
        void Start()
        {
            var sequence = DOTween.Sequence();
            foreach (var text in log)
            {
                sequence
                    .Append(textMeshPro.DOText(text, 1.5f))
                    .Append(textMeshPro.DOFade(1f, 0.4f))
                    .AppendCallback(() =>
                    {
                        textMeshPro.text = text;
                    });
            }

            sequence
                .SetLoops(-1);
        }
    }
}
