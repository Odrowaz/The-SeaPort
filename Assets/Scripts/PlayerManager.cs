using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Blend = Animator.StringToHash("Blend");
    [SerializeField] float speed = 3.0F;
    [SerializeField] float rotateSpeed = 60.0F;
    [SerializeField] Vector3 forward = Vector3.forward;

    float _xRotate = 0.0f;

    private const float _maxVerticalAngle = 60;

    CharacterController _mCharacterController;
    private Camera _camera;
    
    [SerializeField] Animator playerAnimator;

    private void Start()
    {
        _camera = Camera.main;
        _mCharacterController = GetComponent<CharacterController>();
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Rotate around y - axis
        if (Input.GetAxis("Mouse X")!=0)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime, 0);
        }

        if (Input.GetAxis("Mouse Y") != 0)
        {
            _xRotate -= Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
            _xRotate = Mathf.Clamp(_xRotate, -_maxVerticalAngle, _maxVerticalAngle);
            
            //Debug.Log(_xRotate);
            
            _camera.transform.localEulerAngles = new Vector3(_xRotate, 0, 0);
        }
        
        // Move forward / backward (no time.deltatime -> docs says is m/s)
        float turbo = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;
        float curSpeed = speed * turbo;
        Vector3 realForward = transform.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical");
        
        realForward += transform.TransformDirection(Vector3.right) * Input.GetAxis("Horizontal");
        
        realForward = Vector3.ClampMagnitude(realForward, 1);

        _mCharacterController.SimpleMove(realForward * curSpeed);

        float vSpeed = curSpeed * Input.GetAxis("Vertical");
        float hSpeed = curSpeed * Input.GetAxis("Horizontal");
        
        //Set animation speed for Blend Tree
        playerAnimator.SetFloat(Blend, Mathf.Abs(curSpeed * vSpeed != 0 ? vSpeed : hSpeed));
        playerAnimator.SetFloat(Speed, vSpeed != 0 ? vSpeed : hSpeed);

    }

}
