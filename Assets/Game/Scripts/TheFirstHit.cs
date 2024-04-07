using UnityEngine;

public class TheFirstHit : MonoBehaviour {
    [SerializeField] 
    private AudioSource _sound;
    
    void Start() {

        // var drunkSettings = Drunk.GetSettings();
        // drunkSettings.drunkenness = 1f;
        //
        // var blurrySettings = Blurry.GetSettings();
        // blurrySettings.strength = 1f;
        //
        // Guide.Instance.ShowGuide("HELLO", 3f, 3f);
    }
    
    private bool _firstHitDone;
    private float _currentDuration = 0f;
    private float _duration = 4f;
    
    void Update() {
        
        _currentDuration += Time.deltaTime;
        var normalizedDuration = _currentDuration / _duration;
        var value = Mathf.Lerp(10f, 0.4f, normalizedDuration);
        
        // var drunkSettings = Drunk.GetSettings();
        // drunkSettings.drunkenness = value;
        //
        // var blurrySettings = Blurry.GetSettings();
        // blurrySettings.strength = value - 0.3f;
        
        if (normalizedDuration >= 1f) {
            _firstHitDone = true;
            
            // drunkSettings.drunkenness = 0.4f;
            // blurrySettings.strength = 0.1f;
        }
        
    }
}
