using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class Movement : MonoBehaviour  {
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();


    private bool _phase = false;
    
    private int _currentKeysVariant = 0;
    
    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
        
        
    }
    
        private float _currentDuration = 0f;
        private bool _readyToSwitch;
        private float _switchMappingCurrentDuration;
    
    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        
        // ====================================================================== 

        var inputHorizontal = Input.GetAxis("Horizontal"); // Dolava / doprava
        var inputVertical = Input.GetAxis("Vertical");

        if (Game.Instance.ZmenTlacitkaKedStojis) {
            if (inputVertical == 0 && inputHorizontal == 0) {
                _readyToSwitch = true;
            }
            else {
                if (_readyToSwitch) {
                    var variant = Random.Range(5, 9);
                    _currentKeysVariant = variant;
                    _readyToSwitch = false;
                }
            }
        }

        if (Game.Instance.ZmenTlacitkaPoCase > 0) {
            _switchMappingCurrentDuration += Time.deltaTime;
            if (_switchMappingCurrentDuration >= Game.Instance.ZmenTlacitkaPoCase) {
                var variant = Random.Range(5, 9);
                _currentKeysVariant = variant;
                
                _switchMappingCurrentDuration = 0f;
            }
        }
       
        
        if (Game.Instance.ZanasanieDoStrany) {
            if (inputVertical == 0 && inputHorizontal == 0) {
                _currentDuration = 0f;
            }
        
            if (inputVertical != 0 || inputHorizontal != 0) {

                if (_phase) {
                    _currentDuration += Time.deltaTime;
                }
                else {
                    _currentDuration -= Time.deltaTime;
                    
                }
               
                var normalizedDuration = _currentDuration / Game.Instance.DlzkaJednehoZanosuDoStrany;
                
                float adjustedValue = Mathf.Lerp(-Game.Instance.AkoMocTaZanasa, Game.Instance.AkoMocTaZanasa, normalizedDuration);
                inputHorizontal = Input.GetAxis("Horizontal") +adjustedValue;
                inputVertical = Input.GetAxis("Vertical") + adjustedValue;
                
                
                if (_phase && normalizedDuration >= 1f) {
                    _phase = false;
                }
                
                if (!_phase && normalizedDuration <= 0f) {
                    _phase = true;
                }
            }
        }

        
        var verticalVelocity = targetMovingSpeed * inputVertical;
        var horizontalVelocity = targetMovingSpeed * inputHorizontal;
        
        var targetVelocity =new Vector2( horizontalVelocity, verticalVelocity);
        
        if (Game.Instance.ZmenTlacitka) {
            
            if (Game.Instance.ZmenTlacitkaKedStojis || Game.Instance.ZmenTlacitkaPoCase > 0) {
                targetVelocity = MapKeys(targetVelocity, _currentKeysVariant);
            }
            
            
            targetVelocity = MapKeys(targetVelocity, Game.Instance.VariantZmenyTlacitok);
        }

        var rotation = transform.rotation;
        var result = rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
        rigidbody.velocity = result;
    }

    private Vector2 MapKeys(Vector2 baseVelocity, int variant) {
        if (variant == 1) {
            return new Vector2(-baseVelocity.x, -baseVelocity.y);
        }
        
        if (variant == 2) {
            return new Vector2(baseVelocity.y, -baseVelocity.x);
            // return new Vector2(-baseVelocity.y, baseVelocity.x);
        }
        
        if (variant == 3) {
            var xValue = 0f;
            var yValue = 0f;
            if (baseVelocity.x > 0) {
                xValue = -baseVelocity.x;
            } else if (baseVelocity.x < 0) {
                yValue = baseVelocity.x;
            }
            if (baseVelocity.y > 0) {
                xValue = baseVelocity.y;
            } else if (baseVelocity.y < 0) {
                yValue = -baseVelocity.y;
            }
        
            var final = new Vector2(xValue, yValue);
            return final;
        }

        if (variant == 4) {
            var xValue = 0f;
            var yValue = 0f;
            if (baseVelocity.x > 0) {
                xValue = -baseVelocity.x;
            } else if (baseVelocity.x < 0) {
                xValue = -baseVelocity.x;
            }
            if (baseVelocity.y > 0) {
                xValue = baseVelocity.y;
            } else if (baseVelocity.y < 0) {
                xValue = -baseVelocity.y;
            }
            
            var final = new Vector2(xValue, yValue);
            return final;
        }

        if (variant == 5) {
            return baseVelocity;
        }
        
        if (variant == 6) {
            return new Vector2(-baseVelocity.y, baseVelocity.x);
        }

        if (variant == 7) {
            return new Vector2(-baseVelocity.x, -baseVelocity.y);
        }
        
        if (variant == 8) {
            return new Vector2(baseVelocity.y, -baseVelocity.x);
        }
        
        
        

        return baseVelocity;
    }
}