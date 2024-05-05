using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : Singleton<ObjectPool>
{
    private ObjectPool<RevisedFish> _objectPool;
    public ObjectPool<RevisedFish> Pool => _objectPool;
    public void InitObjectPool()
    {
        _objectPool = new ObjectPool<RevisedFish>(CreateObject, OnGetFromPool, OnReleaseToPool, OnDestroyFromPool,
            collectionCheck: false);
    }

    private RevisedFish CreateObject()
    {
        Debug.Log("Create new fish");
        RevisedFish revisedObjectInstance = SpawnManager.Instance.CreateRandomFish().GetComponent<RevisedFish>();
        revisedObjectInstance.ObjectPool = _objectPool;
        return revisedObjectInstance;
    }
    private void OnReleaseToPool(RevisedFish pooledObject) => pooledObject.gameObject.SetActive(false);
    private void OnGetFromPool(RevisedFish pooledObject) => pooledObject.gameObject.SetActive(true);
    private void OnDestroyFromPool(RevisedFish pooledObject) => Destroy(pooledObject.gameObject);
}
