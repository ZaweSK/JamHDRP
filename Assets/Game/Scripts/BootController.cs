using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BootController : MonoBehaviour {
    
    [SerializeField] 
    private TextMeshProUGUI _text;
    
    [SerializeField]
    private AudioListener _menuAudioListener;
    

    [SerializeField] 
    private AudioSource _menuMusic;
    
    [SerializeField] 
    private GameObject _viewObject;

    void Start() {
        _menuMusic.Play();
        
        _loadingOperation = SceneManager.LoadSceneAsync(GameSceneName, LoadSceneMode.Additive);
        _loadingOperation.allowSceneActivation = false; 
    }
    
    private const string GameSceneName = "Game";
    

    public void RestartGame() {
        _menuMusic.Stop();
        SceneManager.UnloadSceneAsync(GameSceneName);
        _viewObject.SetActive(true);
        _sceneLoaded = false;
        _menuAudioListener.enabled = true;
        
        HideText();
        
        _opacityTimer = 0;
        _timer = 0;
        
        _loadingOperation = SceneManager.LoadSceneAsync(GameSceneName, LoadSceneMode.Additive);
        _loadingOperation.allowSceneActivation = false; 
    }

    private bool _sceneLoaded;

    private float _timer = 0;

    private float _opacityTimer = 0;
    private float _opacityDuration = 1f;

    private void HideText() {
        var color = _text.color;
        var adjustedColor = new Color(color.r, color.g, color.b, 0);
        _text.color = adjustedColor;
    }
    
    private void Update() {
        if (_loadingOperation != null && _loadingOperation.progress >= 1) {
            _viewObject.SetActive(false);
            _loadingOperation = null;
        }
        
        _timer += Time.deltaTime;
        if (_timer < 1) {
            return;
        }
        
        if (_sceneLoaded) {
            return;
        }
        
        _opacityTimer += Time.deltaTime;
        var normalizedOpacity = _opacityTimer / _opacityDuration;
        var color = _text.color;
        var adjustedColor = new Color(color.r, color.g, color.b, normalizedOpacity);
        _text.color = adjustedColor;
        
        
        if (Input.anyKey) {
            // SceneManager.LoadSceneAsync(GameSceneName, LoadSceneMode.Additive);
            
            _sceneLoaded = true;
            _menuMusic.Stop();
            
            if (_loadingOperation != null) {
                _loadingOperation.allowSceneActivation = true; 
                _menuAudioListener.enabled = false;
                
            }
        }
    }

    private AsyncOperation _loadingOperation;

    private static BootController _instance;
      
    private void Awake() {
        HideText();
        if (_instance == null) {
            _instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }
    public static BootController Instance => _instance;

}
