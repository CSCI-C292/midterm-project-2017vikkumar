using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] RuntimeData _runTimeData;
    [SerializeField] float _mouseSensitivity = 10f;
    [SerializeField] float _moveSpeed = 3f;
    float _currentTilt = 0f;
    [SerializeField] Camera _cam;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        if (_runTimeData.CurrentGameplayState == GameplayState.FreeWalk)
            Movement();

    }
    void Aim()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        _currentTilt -= mouseY * _mouseSensitivity;
        _currentTilt = Mathf.Clamp(_currentTilt, -90, 90);
        transform.Rotate(Vector3.up, mouseX * _mouseSensitivity);
        _cam.transform.localEulerAngles = new Vector3(_currentTilt, 0, 0);
    }
    void Movement()
    {
        Vector3 sidewaysMovement = transform.right * Input.GetAxis("Horizontal");
        Vector3 forwardMovement = transform.forward * Input.GetAxis("Vertical");

        Vector3 movementVector = sidewaysMovement + forwardMovement;

        GetComponent<CharacterController>().Move(movementVector * _moveSpeed * Time.deltaTime);
    }
}
