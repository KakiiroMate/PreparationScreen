using DG.Tweening;
using UnityEngine;

namespace PreparationScreen
{
    public class Rotate : MonoBehaviour
    {

        [SerializeField] GameObject target;
        void Start()
        {
            target.transform.DOLocalRotate(new Vector3(0, 359f, 0), 8f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        }
    }
}
