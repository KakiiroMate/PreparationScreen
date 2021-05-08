using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace PreparationScreen
{
    // ref: https://qiita.com/flankids/items/c36ccdc02031fa304365
    [RequireComponent(typeof(Animator))]
    public class AnimationFPS : MonoBehaviour {
    
        [SerializeField]
        AnimationClip clip;
        [SerializeField, Range(1, 30)]
        int fps = 30;

        PlayableGraph graph;
        AnimationClipPlayable clipPlayable;
        float thresholdTime;
        float skippedTime;

        void Awake() {
            graph = PlayableGraph.Create();
            clipPlayable = AnimationClipPlayable.Create(graph, clip);
            
            var animator = GetComponent<Animator>();
            var output = AnimationPlayableOutput.Create(graph, $"{clip.name}.output", animator);
            output.SetSourcePlayable(clipPlayable);

            InitializeThresholdTime();

            graph.Play();
        }

        void InitializeThresholdTime() {
            thresholdTime = 1f / fps;
        }

        void Update() {
            if (skippedTime < Mathf.Epsilon) {
                clipPlayable.Pause();
            }
            if (thresholdTime > skippedTime) {
                skippedTime += Time.deltaTime * (float)clipPlayable.GetSpeed();
                return;
            }

            var currentTime = clipPlayable.GetTime() + skippedTime + Time.deltaTime * (float)clipPlayable.GetSpeed();
            skippedTime = 0f;

            if (currentTime > clip.length) {
                currentTime -= clip.length;
            }
            clipPlayable.SetTime(currentTime);
        }

        void OnDestroy() {
            if (graph.IsValid()) {
                graph.Destroy();
            }
        }

        void OnValidate() {
            InitializeThresholdTime();
        }
    }
}