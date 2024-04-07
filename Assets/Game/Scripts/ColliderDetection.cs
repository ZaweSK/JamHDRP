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
    
    private Tween? _tween;
    
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
        
        if (zoneConfig.Id == "zone1") {
            Guide.Instance.ShowGuide("Ouu, something is happening...", 1f, 0f);
        }
        
        
        _tween = DOTween.To(() => 0f, animatedValue => {
                PostProcessing.Instance.LensDistortion(animatedValue); 
                PostProcessing.Instance.ChromaticAberration(animatedValue);
            }, 0.9f, 1f)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                PostProcessing.Instance.ResetToDefault();
            })
            .OnKill(() => {
                _tween = null;
            });
        
    }

}
