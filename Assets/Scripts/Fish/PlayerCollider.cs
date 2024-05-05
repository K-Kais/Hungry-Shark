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
        StartCoroutine(DisableGameObjectAfterDelay(other.gameObject, animationDuration));
    }
    private IEnumerator DisableGameObjectAfterDelay(GameObject gameObject, float delay)
    {
        gameObject.transform.DOMove(_mouthPoint.position, delay).SetEase(Ease.InBack, 0.1f);
        yield return new WaitForSeconds(delay);

        ObjectPool.Instance.Pool.Release(gameObject.GetComponent<RevisedFish>());
        //yield return new WaitForSeconds(60f);
        //ObjectPool.Instance.Pool.Get();
    }
}

