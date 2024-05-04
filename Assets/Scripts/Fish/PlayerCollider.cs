using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private PlayerAnimation _playerAnimation;
    private void OnTriggerEnter(Collider other)
    {
        _playerAnimation.PlayAnimation(AnimationType.Bite);
        other.gameObject.SetActive(false);
    }
}
    
