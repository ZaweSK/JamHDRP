using UnityEngine;

public class FirstPersonLook : MonoBehaviour {
    [SerializeField] Transform character;
    public float sensitivity = 2;
    public float smoothing = 1.5f;

    Vector2 velocity;
    Vector2 frameVelocity;


    void Reset() {
        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start() {
        // Lock the mouse cursor to the game screen.
        Cursor.lockState = CursorLockMode.Locked;
    }

    private bool _phase;
    private float _currentDuration = 0f;
    private float _duration = 1f;

    private Vector2 _directionVector;
    private float _directionTimer = 0f;
    private float _directionDuration = 1f;

    private float ComputeFinalValue(int value, float adjustedValue) {
        return value switch {
            0 => 0,
            1 => adjustedValue,
            2 => -adjustedValue,
            _ => adjustedValue
        };
    }

    private bool _applyFirst;

    void Update() {


        if (Game.Instance.TocenieHlavy) {

            if (_phase) {
                _currentDuration += Time.deltaTime;
            }
            else {
                _currentDuration -= Time.deltaTime;
            }

            var normalizedDuration = _currentDuration / Game.Instance.DlzkaJednohoTocenia;

            float adjustedValue = Mathf.Lerp(-Game.Instance.AkoMocTaToci, Game.Instance.AkoMocTaToci, normalizedDuration);


            if (_phase && normalizedDuration >= 1f) {
                _phase = false;
            }

            if (!_phase && normalizedDuration <= 0f) {
                _phase = true;
            }

            _directionTimer += Time.deltaTime;
            if (_directionTimer >= Game.Instance.DlzkaJednohoTocenia) {
                _directionTimer = 0f;


                var randomIntX = Random.Range(1, 3);
                var randomIntY = Random.Range(1, 3);

                _directionVector = new Vector2(
                    ComputeFinalValue(randomIntX, adjustedValue),
                    ComputeFinalValue(randomIntY, adjustedValue));
            }


        }
        else {
            _directionVector = Vector2.zero;
        }



        // Get smooth velocity.
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        // Debug.Log($"XXX mouse delta {mouseDelta}");
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        // Debug.Log($"XXX  rawframeelocity {rawFrameVelocity}");
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing) + _directionVector;


        // frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing) + new Vector2(0.01f, 0.001f);


        // Debug.Log($"XXX frame velocity {frameVelocity}");



        velocity += frameVelocity;

        // Debug.Log($"XXX velocity {velocity}");
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);

        if (!_applyFirst) {

            velocity = new Vector2(180f, -7f);
            _applyFirst = true;
        }

        // Rotate camera up-down and controller left-right from velocity.
        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);

    }
}