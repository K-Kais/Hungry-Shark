using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private GameObject _fish;
    [SerializeField] private IntVariable _fishCount;
    [SerializeField] private Boundery _boundery;
    [SerializeField] private ListBoidVariable _fishesList;

    public void Initialize()
    {
        _fishesList.boidMovements.Clear();

        RandomInstantiateFish(_fishCount.Value);
    }
    private void RandomInstantiateFish(int fishCount)
    {
        for (int i = 0; i < fishCount; i++)
        {
            float xPos = Random.Range(-_boundery.XLimit, _boundery.XLimit);
            float yPos = Random.Range(-_boundery.YLimit, _boundery.YLimit);
            float direction = Random.Range(0f, 360f);
            Quaternion rotationFish = Quaternion.Euler(0, 90f, 0);

            GameObject newFish = Instantiate(_fish, new Vector3(xPos, yPos, 0), Quaternion.Euler(Vector3.forward * direction) * rotationFish);

            RegisterFish(newFish.GetComponent<BoidMovement>());
        }
    }

    private void RegisterFish(BoidMovement newFish)
    {
        if (_fishesList.boidMovements.Contains(newFish)) return;

        ManageBySpawnManager manage = newFish.gameObject.AddComponent<ManageBySpawnManager>();
        manage.onDestroyUnityEvent.AddListener(() =>
        {
            if (!_fishesList.boidMovements.Remove(newFish))
            {
                Debug.LogError("[SpawnManager] Can't remove fish from list<Fish>");
            }
        });
        _fishesList.boidMovements.Add(newFish);
    }
}
