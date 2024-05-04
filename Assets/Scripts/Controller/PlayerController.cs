using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerAnimation _playerAnimation;
    [SerializeField] PlayerCollider _playerCollider;
    [SerializeField] PlayerMovement _playerMovement;
    public PlayerAnimation PlayerAnimation => _playerAnimation;
    public PlayerCollider PlayerCollider => _playerCollider;
    public PlayerMovement PlayerMovement => _playerMovement;

    //private void Awake()
    //{
    //    LoadPlayerAnimation();
    //}
    //private void LoadPlayerAnimation()
    //{
    //    if (_playerAnimation != null) return;
    //    _playerAnimation = FindObjectOfType<PlayerAnimation>();
    //    Debug.Log(transform.name + ": LoadPlayerAnimation", gameObject);
    //}
}
