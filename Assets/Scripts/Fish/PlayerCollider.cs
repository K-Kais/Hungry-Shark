using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    private void OnTriggerEnter(Collider other)
    {
        _playerController.PlayerAnimation.PlayAnimation(AnimationType.Bite);
        other.gameObject.SetActive(false);
    }
}
    
