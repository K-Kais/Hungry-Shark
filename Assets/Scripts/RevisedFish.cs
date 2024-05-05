using UnityEngine;
using UnityEngine.Pool;

public class RevisedFish : MonoBehaviour
{
    [SerializeField]public IObjectPool<RevisedFish> ObjectPool;
}
