using System;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    public BoatState CurrentBoatState
    {
        get => currentState;
        set => currentState = value;
    }

    // TODO: Convert to private fields
    public BoatState currentState = BoatState.OnWater;
    public float forwardSpeed = 100f;
    public float turnSpeed = 30f;
    public float rotationSpeed = 50f;
    public SingleJoystick joystickInput = null;

    private bool _isTouchPressed = false;
    private Rigidbody _rigidbody = null;

    private void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _isTouchPressed = Input.touchCount > 0;
    }

    private void LateUpdate()
    {
        switch (CurrentBoatState)
        {
            // if on water, boat moves along joystick x
            case BoatState.OnWater:
                OnWater();
                break;

            // if on air, do spin
            case BoatState.Airborne:
                Airborne();
                break;
        }
    }

    private void OnWater()
    {
        Vector3 input = joystickInput.GetInputDirection();
        
        // remove y component
        input = new Vector3(input.x, 0f, 0f);
        float fwd = (_isTouchPressed) ? 1f : 0f;
        
        // adds forward and turn force
        _rigidbody.AddForce(Time.deltaTime * ((turnSpeed * input) + (forwardSpeed * fwd * Vector3.forward)));
    }

    private void Airborne()
    {
        if (!_isTouchPressed) return;
        // spin
        Quaternion deltaRotation = Quaternion.Euler(rotationSpeed * Time.deltaTime, 0f, 0f);
        _rigidbody.MoveRotation(this.transform.rotation * deltaRotation);
    }
}