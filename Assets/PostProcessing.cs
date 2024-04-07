
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class PostProcessing : MonoBehaviour {
    
    // ======================== SINGLETON ========================
    private void Awake() {
        if (_instance == null) {
            _instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    private static PostProcessing _instance;
    public static PostProcessing Instance => _instance;
    // ======================== ======== ========================

    private void Start() {
        if (_volume.profile.TryGet<ChromaticAberration>(out var chromaticAberration)) {
            _chromaticAberration = chromaticAberration;
        }
        
        if (_volume.profile.TryGet<LensDistortion>(out var lensDistortion)) {
            _lensDistortion = lensDistortion;
        }
        
        if (_volume.profile.TryGet<ColorAdjustments>(out var colorAdjustments)) {
            _colorAdjustments = colorAdjustments;
        }
    }

    [SerializeField]
    private Volume _volume;

    private ChromaticAberration _chromaticAberration;
    private LensDistortion _lensDistortion;
    private ColorAdjustments _colorAdjustments;
    
    public void ChromaticAberration(float value) {
        _chromaticAberration.intensity.value = value;
    }
    
    public void LensDistortion(float value) {
        _lensDistortion.intensity.value = value;
    }
    
    public void HueAdjustments(float value) {
        _colorAdjustments.hueShift.value = value;
    }
    
    public void ResetToDefault() {
        PostProcessing.Instance.ChromaticAberration(0.3f);
        PostProcessing.Instance.LensDistortion(0f);
    }
}