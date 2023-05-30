#if ENABLE_INPUT_SYSTEM && ENABLE_INPUT_SYSTEM_PACKAGE
#define USE_INPUT_SYSTEM
    using UnityEngine.InputSystem;
    using UnityEngine.InputSystem.Controls;
#endif

using UnityEngine;

public class ViewerController : MonoBehaviour
{
    class CameraState
    {
        public float yaw;
        public float pitch;
        public float roll;
        public float x;
        public float y;
        public float z;
        public bool Groundbound;
        public Vector3 BoundsMin;
        public Vector3 BoundsMax;

        public void SetFromTransform(Transform t)
        {
            pitch = t.eulerAngles.x;
            yaw = t.eulerAngles.y;
            roll = t.eulerAngles.z;
            x = t.position.x;
            y = t.position.y;
            z = t.position.z;
        }

        public void SetBounds(Vector3 _min, Vector3 _max)
        {
            BoundsMin = _min;
            BoundsMax = _max;
        }

        public void Translate(Vector3 translation)
        {
            Vector3 rotatedTranslation;
            if (!Groundbound)
            {
                rotatedTranslation = Quaternion.Euler(pitch, yaw, roll) * translation;
            }
            else
            {
                rotatedTranslation = Quaternion.Euler(0, yaw, 0) * translation;
            }
            x += rotatedTranslation.x;
            if(x > BoundsMax.x)
            {
                x = BoundsMax.x;
            }
            else
            {
                if(x < BoundsMin.x)
                {
                    x = BoundsMin.x;
                }
            }
            y += rotatedTranslation.y;
            if (y > BoundsMax.y)
            {
                y = BoundsMax.y;
            }
            else
            {
                if (y < BoundsMin.y)
                {
                    y = BoundsMin.y;
                }
            }
            z += rotatedTranslation.z;
            if (z > BoundsMax.z)
            {
                z = BoundsMax.z;
            }
            else
            {
                if (z < BoundsMin.z)
                {
                    z = BoundsMin.z;
                }
            }
        }

        public void LerpTowards(CameraState target, float positionLerpPct, float rotationLerpPct)
        {
            yaw = Mathf.Lerp(yaw, target.yaw, rotationLerpPct);
            pitch = Mathf.Lerp(pitch, target.pitch, rotationLerpPct);
            roll = Mathf.Lerp(roll, target.roll, rotationLerpPct);

            x = Mathf.Lerp(x, target.x, positionLerpPct);
            y = Mathf.Lerp(y, target.y, positionLerpPct);
            z = Mathf.Lerp(z, target.z, positionLerpPct);            
        }

        public void UpdateTransform(Transform t)
        {
            t.eulerAngles = new Vector3(pitch, yaw, roll);
            t.position = new Vector3(x, y, z);
        }
    }

    CameraState m_TargetCameraState = new CameraState();
    CameraState m_InterpolatingCameraState = new CameraState();

    [Header("Movement Settings")]
    [Tooltip("Exponential boost factor on translation, controllable by mouse wheel.")]
    public float boost = 1;

    [Tooltip("Time it takes to interpolate camera position 99% of the way to the target."), Range(0.001f, 1f)]
    public float positionLerpTime = 0.2f;

    [Header("Rotation Settings")]
    [Tooltip("X = Change in mouse position.\nY = Multiplicative factor for camera rotation.")]
    public AnimationCurve mouseSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));

    [Tooltip("Time it takes to interpolate camera rotation 99% of the way to the target."), Range(0.001f, 1f)]
    public float rotationLerpTime = 0.01f;

    [Tooltip("Whether or not to invert our Y axis for mouse input to rotation.")]
    public bool invertY = false;

    [SerializeField] GameObject WalkingUI;
    [SerializeField] GameObject FlyingUI;
    [SerializeField] Vector3 BoundsMin;
    [SerializeField] Vector3 BoundsMax;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        m_TargetCameraState.Groundbound = true;
        m_TargetCameraState.y = 1.7f;
        WalkingUI.SetActive(true);
    }

    void OnEnable()
    {
        m_TargetCameraState.SetFromTransform(transform);
        m_TargetCameraState.SetBounds(BoundsMin, BoundsMax);
        m_InterpolatingCameraState.SetFromTransform(transform);
        m_InterpolatingCameraState.SetBounds(BoundsMin, BoundsMax);
    }

    Vector3 GetInputTranslationDirection()
    {
        Vector3 direction = new Vector3();
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            direction += Vector3.right;
        }
        if (Input.GetKey(KeyCode.Q) && !m_TargetCameraState.Groundbound)
        {
            direction += Vector3.down;
        }
        if (Input.GetKey(KeyCode.E) && !m_TargetCameraState.Groundbound)
        {
            direction += Vector3.up;
        }
        return direction;
    }

    void Update()
    {
        Vector3 translation = Vector3.zero;

#if ENABLE_LEGACY_INPUT_MANAGER

        // Exit Sample  
        /*
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        */
        // Hide and lock cursor when right mouse button pressed




        /*
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Unlock and show cursor when right mouse button released
        if (Input.GetMouseButtonUp(1))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        */



        // Rotation
        var mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * (invertY ? 1 : -1));

        var mouseSensitivityFactor = mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);

        m_TargetCameraState.yaw += mouseMovement.x * mouseSensitivityFactor;
        m_TargetCameraState.pitch += mouseMovement.y * mouseSensitivityFactor;

        // Translation
        translation = GetInputTranslationDirection() * Time.deltaTime;

        // Speed up movement when shift key held
        if (Input.GetKey(KeyCode.LeftShift))
        {
            translation *= 2.0f;
        }



        // Modify movement by a boost factor (defined in Inspector and modified in play mode through the mouse scroll wheel)
        //boost += Input.mouseScrollDelta.y * 0.2f;
        translation *= Mathf.Pow(2.0f, boost);
        translation *= boost;

#elif USE_INPUT_SYSTEM
            // TODO: make the new input system work
#endif

        m_TargetCameraState.Translate(translation);

        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_TargetCameraState.Groundbound = !m_TargetCameraState.Groundbound;
            if (m_TargetCameraState.Groundbound)
            {
                m_TargetCameraState.y = 1.7f; 
            }
            WalkingUI.SetActive(m_TargetCameraState.Groundbound);
            FlyingUI.SetActive(!m_TargetCameraState.Groundbound);
        }

        // Framerate-independent interpolation
        // Calculate the lerp amount, such that we get 99% of the way to our target in the specified time
        var positionLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / positionLerpTime) * Time.deltaTime);
        var rotationLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / rotationLerpTime) * Time.deltaTime);
        m_InterpolatingCameraState.LerpTowards(m_TargetCameraState, positionLerpPct, rotationLerpPct);

        m_InterpolatingCameraState.UpdateTransform(transform);
    }
}