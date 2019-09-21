using System;
using UnityEngine;

public class Bob : MonoBehaviour
{
    private const float BobAmount = 0.05f;
    private Rigidbody _rb = null;
    private Vector3 _pos = Vector3.zero;

    private void Start()
    {
        _pos = this.transform.position;
    }

    private void Update()
    {
        if (this.TryGetComponent<Rigidbody>(out _rb))
        {
            _rb.MovePosition(new Vector3(_pos.x, _pos.y + Mathf.Sin(Time.time * BobAmount), _pos.z));
        }

        else
        {
            this.transform.position = new Vector3(_pos.x, _pos.y + Mathf.Sin(Time.time * BobAmount), _pos.z);
        }
    }
}