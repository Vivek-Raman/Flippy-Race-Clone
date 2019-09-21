using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoatMovement))]
public class BoatStateManager : MonoBehaviour
{
    // TODO: Convert to private fields
    [Header("Dot Product Parameters")]
    [Range(-1f, 1f)] public float successValue = 0.9f;

    [Header("Debug")]
    public Text dotProductText = null;
    
    private BoatMovement _boat = null;

    private void Start()
    {
        if (!this.TryGetComponent(out _boat))
        {
            throw new Exception();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        Collider otherCollider = other.collider;
        if (!otherCollider.CompareTag("WaterSlope"))
        {
            return;
        }
        
        _boat.CurrentBoatState = BoatState.Airborne;
    }

    private void OnCollisionEnter(Collision other)
    {
        Collider otherCollider = other.collider;
        if (otherCollider.CompareTag("WaterSlope"))
        {
            float dot = Vector3.Dot(other.transform.up.normalized, this.transform.up.normalized);
            dotProductText.text = dot.ToString();

            // +score, change state if x angle is acceptable
            if (dot > successValue)
            {
                Debug.Log("Landed at " + dot);
                _boat.CurrentBoatState = BoatState.OnWater;

                GameManager.Instance.AddPoint(10);
            }

            // else, kill the player
            else
            {
                Debug.Log("Killed at " + dot);
                GameManager.Instance.OnPlayerDeath(player: this);
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // coin
        if (other.CompareTag("Coin"))
        {
            GameManager.Instance.AddPoint(10);
            Destroy(other.gameObject);
        }
        
        // finish
        if (other.CompareTag("Finish"))
        {
            GameManager.Instance.NextLevel();
        }
        
        // kill zone?
    }
}