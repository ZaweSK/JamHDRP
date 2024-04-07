using Scenes.Scripts;
using UnityEngine;

public class TheFirstHit : MonoBehaviour {

    private bool _firstHitDone;
    private float _currentDuration = 0f;
    private float _duration = 4f;

    [SerializeField]
    private ZoneConfig _config;
    
    void Start() {
        Game.Instance.ApplyZoneConfig(_config);
        
        UI.Instance.ShowGuide("This is bad. I need to find my phone", 1f, 1.5f);
        
    }
    
    void Update() {
        if (_firstHitDone) {
            return;
        }
        
        _currentDuration += Time.deltaTime;
        var normalizedDuration = _currentDuration / _duration;
        var value = Mathf.Lerp(1f, 0.3f, normalizedDuration);

        PostProcessing.Instance.ChromaticAberration(value);
        PostProcessing.Instance.LensDistortion(value);
        
        if (normalizedDuration >= 1f) {
            _firstHitDone = true;
            PostProcessing.Instance.ResetToDefault();
        }
    }
}
