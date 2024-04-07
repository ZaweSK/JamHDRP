using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scenes.Scripts;
using UnityEngine;
using DG.Tweening;
#nullable enable


public class ColliderDetection : MonoBehaviour {

    [SerializeField] 
    private List<ZoneConfig> _zoneConfigs;
    
    [SerializeField]
    private AudioSource _drugHitAudioSource;
    
    private Tween? _tween;

    private Tween? _lensDistortionTween;
    
    private readonly List<string> _alreadyTriggered = new List<string>();
    private void OnDestroy() {
        
        _tween?.Kill();
        _tween = null;
    }

    private void OnTriggerEnter(Collider other) {
        var zoneConfig = _zoneConfigs.FirstOrDefault(zoneConfig => other.CompareTag(zoneConfig.Id));
        if (zoneConfig == null) {
            Debug.LogError($"ZoneConfig not found for {other.name}");
            return;
        }
        
        if (_alreadyTriggered.Contains(zoneConfig.Id)) {
            return;
        }
        
        _alreadyTriggered.Add(zoneConfig.Id);
        Game.Instance.ApplyZoneConfig(zoneConfig);
        _drugHitAudioSource.Play();
        
        if (zoneConfig.Id == "zone1") {
            Guide.Instance.ShowGuide("Ouu, something is happening...", 1f, 0f);
        }
        
        if (zoneConfig.Id == "zone2") {
            Guide.Instance.ShowGuide("This is crazy, I must get to the stage", 1f, 0f);
        }
        
        if (zoneConfig.Id == "zone3") {
            Guide.Instance.ShowGuide("My face is melting !", 1f, 0f);
        }
        
        
        
        
        
        
        _tween = DOTween.To(() => 0f, animatedValue => {
                PostProcessing.Instance.LensDistortion(animatedValue); 
                PostProcessing.Instance.ChromaticAberration(animatedValue);
            }, 0.9f, 1f)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                PostProcessing.Instance.ResetToDefault();
                
                if (zoneConfig.Id == "zone3") {
                    
                    _lensDistortionTween = DOTween.To(() => -0.6f, animatedValue => {
                            Debug.Log($"XXX ANIMATE");
                            PostProcessing.Instance.LensDistortion(animatedValue);
                        }, 0.6f, 3f)
                        .SetLoops(-1, LoopType.Yoyo)
                        .SetEase(Ease.Linear)
                        .OnComplete(() => {
                        });
                    
                }
            })
            .OnKill(() => {
                _tween = null;
            });
    }

}
