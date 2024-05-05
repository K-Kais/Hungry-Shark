using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Transform _mouthPoint;
    //[SerializeField] private ObjectPool _objectPool;
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
        yield return new WaitForSeconds(2f);
        //ObjectPool.Instance.Pool.Get();
    }
}

