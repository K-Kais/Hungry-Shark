using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Pool;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Transform _mouthPoint;
    private void OnTriggerEnter(Collider other)
    {
        float animationDuration;
        _playerController.PlayerAnimation.PlayAnimation(AnimationType.Bite, out animationDuration);
        other.transform.DOMove(_mouthPoint.position, animationDuration / 2f).SetEase(Ease.InBack, 0.1f).OnComplete(() =>
        {
            ObjectPool.Instance.Pool.Release(other.GetComponent<RevisedFish>());
        });
    }
}

