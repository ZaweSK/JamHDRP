using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Guide : MonoBehaviour {

    [SerializeField] 
    private TextMeshProUGUI _text;

    private void HideText() {
        var color = _text.color;
        var adjustedColor = new Color(color.r, color.g, color.b, 0);
        _text.color = adjustedColor;
    }
    
    private void SetAlpha(float alpha) {
        var color = _text.color;
        var adjustedColor = new Color(color.r, color.g, color.b, alpha);
        _text.color = adjustedColor;
    }
    
    public void ShowGuide(string text, float duration, float delay) {
        HideText();
        _text.text = text;
        _textOngoingDuration = 0f;
        _fadeInDuration = 0f;
        _fadeOutDuration = 0f;
        _textTotalDuration = duration;
        
        
        _delayTotalDuration = delay;
        
        _animate = true;
    }

    private void Start() {
        HideText();
    }

    private float _fadeInDuration;
    private float _fadeOutDuration;
    private float _textOngoingDuration = 0f;
    private float _textTotalDuration;
    private float _delayOngoingDuration;
    private float _delayTotalDuration;
    private bool _animate = false;
    
    void Update() {
        if (!_animate) return;
        
        _delayOngoingDuration += Time.deltaTime;
        if (_delayOngoingDuration < _delayTotalDuration) return;
        

        _fadeInDuration += Time.deltaTime;
        var normalizedFadeDuration = _fadeInDuration / 1;
        var alpha = Mathf.Lerp(0, 1, normalizedFadeDuration);
        
        SetAlpha(alpha);
        
        if (normalizedFadeDuration >= 1f) {
            _textOngoingDuration += Time.deltaTime;

            if (_textOngoingDuration >= _textTotalDuration) {
                
                
                _fadeOutDuration += Time.deltaTime;
                var normalizedFadeOutDuration = _fadeOutDuration / 1;
                var alphaOut = Mathf.Lerp(1, 0, normalizedFadeOutDuration);
                SetAlpha(alphaOut);
                
                if (normalizedFadeOutDuration >= 1f) {
                    _animate = false;
                }
            } 
        }
    }
    
    private static Guide _instance;
      
    private void Awake() {
        if (_instance == null) {
            _instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    public static Guide Instance => _instance;
}
