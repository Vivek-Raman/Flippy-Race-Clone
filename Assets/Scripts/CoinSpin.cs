using System;
using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 90f;

    private void Update()
    {
        this.transform.Rotate(0f, Time.deltaTime * spinSpeed, 0f);
    }
}