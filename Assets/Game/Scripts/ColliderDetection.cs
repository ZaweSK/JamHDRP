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
    
    private Tween? _hueShiftTween;
    
    private readonly List<string> _alreadyTriggered = new List<string>();
    private void OnDestroy() {
        
        _tween?.Kill();
        _tween = null;
    }

    private int _time = 60;

    private void RestartGame() {
        BootController.Instance.RestartGame();
    }

    IEnumerator OneSecondTimer() {
        UI.Instance.ShowCountDown(_time);
        yield return new WaitForSeconds(1f);
        _time--;
        
        if (_time <= 0) {
            UI.Instance.ShowCountDown(_time);
           RestartGame();
        }
        else {
            StartCoroutine(OneSecondTimer());
        }
        
    }
    private void OnTriggerEnter(Collider other) {
        
        if (other.CompareTag("phoneZone")) {
            Debug.Log($"XXX phoneZone");
            
            UI.Instance.ShowGuide("The phone is on stage. I must get to it before I shit my pants", 1f, 0f);
            // UI.Instance.ShowGuide("Hurry ! The poop is near", 1f, 10f);
            // UI.Instance.ShowGuide("Sweet lord in heaven", 1f, 30);
            StartCoroutine(OneSecondTimer());
            return;
        }
        
        var zoneConfig = _zoneConfigs.FirstOrDefault(zoneConfig => other.CompareTag(zoneConfig.Id));
        
        if (_alreadyTriggered.Contains(zoneConfig.Id)) {
            return;
        }
        

        if (zoneConfig == null) {
            Debug.LogError($"ZoneConfig not found for {other.name}");
            return;
        }
        
        _alreadyTriggered.Add(zoneConfig.Id);
        Game.Instance.ApplyZoneConfig(zoneConfig);
        _drugHitAudioSource.Play();
        
        if (zoneConfig.Id == "zone1") {
            UI.Instance.ShowGuide("Ouu, something is happening...", 1f, 0f);
        }
        
        if (zoneConfig.Id == "zone2") {
            UI.Instance.ShowGuide("This is crazy, I must get to the stage", 1f, 0f);
            PostProcessing.Instance.ChromaticAberration(0.6f);
        }
        
        if (zoneConfig.Id == "zone3") {
            UI.Instance.ShowGuide("My face is melting !", 1f, 0f);
            PostProcessing.Instance.ChromaticAberration(0.9f);
        }
        
        
        if (zoneConfig.Id == "zone4") {
            UI.Instance.ShowGuide("My phone must be somewhere near the stage", 1f, 0f);
            PostProcessing.Instance.ChromaticAberration(1f);
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
                            PostProcessing.Instance.LensDistortion(animatedValue);
                        }, 0.6f, 2f)
                        .SetLoops(-1, LoopType.Yoyo)
                        .SetEase(Ease.Linear)
                        .OnComplete(() => {
                        });
                    
                }
                
                if (zoneConfig.Id == "zone4") {
                    _lensDistortionTween?.Kill();
                    _lensDistortionTween = null;
                    
                    _lensDistortionTween = DOTween.To(() => -0.75f, animatedValue => {
                            PostProcessing.Instance.LensDistortion(animatedValue);
                        }, 0.76f, 5f)
                        .SetLoops(-1, LoopType.Yoyo)
                        .SetEase(Ease.Linear)
                        .OnComplete(() => {
                        });
                    
                    
                    _hueShiftTween = DOTween.To(() => 0f, animatedValue => {
                            PostProcessing.Instance.HueAdjustments(animatedValue);
                        }, -200f, 8f)
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
