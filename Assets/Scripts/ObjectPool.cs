using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : Singleton<ObjectPool>
{
    private IObjectPool<RevisedFish> _objectPool;
    public IObjectPool<RevisedFish> Pool => _objectPool;
    public void InitObjectPool(int count)
    {
        _objectPool = new ObjectPool<RevisedFish>(CreateObject, OnGetFromPool, OnReleaseToPool, OnDestroyFromPool,
            defaultCapacity: count);
    }

    private RevisedFish CreateObject()
    {
        RevisedFish revisedObjectInstance = new RevisedFish();
        revisedObjectInstance.ObjectPool = _objectPool;
        return revisedObjectInstance;
    }
    private void OnReleaseToPool(RevisedFish pooledObject) => pooledObject.gameObject.SetActive(false);
    private void OnGetFromPool(RevisedFish pooledObject) => pooledObject.gameObject.SetActive(true);
    private void OnDestroyFromPool(RevisedFish pooledObject) => Destroy(pooledObject.gameObject);
}
