using UnityEngine;

public class TheFirstHit : MonoBehaviour {

    private bool _firstHitDone;
    private float _currentDuration = 0f;
    private float _duration = 4f;
    
    void Update() {
        
        _currentDuration += Time.deltaTime;
        var normalizedDuration = _currentDuration / _duration;
        var value = Mathf.Lerp(1f, 0.3f, normalizedDuration);

        PostProcessing.Instance.ChromaticAberration(value);
        PostProcessing.Instance.LensDistortion(value);
        
        if (normalizedDuration >= 1f) {
            _firstHitDone = true;
            PostProcessing.Instance.ChromaticAberration(0.3f);
            PostProcessing.Instance.LensDistortion(0f);
        }
    }
}
