using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private GameObject _fish;
    [SerializeField] private IntVariable _fishCount;
    [SerializeField] private Boundery _boundery;
    [SerializeField] private ListGameObjectVariable _fishes;

    public void Initialize()
    {
        _fishes.value.Clear();

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

            RegisterFish(newFish);
        }
    }

    private void RegisterFish(GameObject newFish)
    {
        if (_fishes.value.Contains(newFish)) return;

        ManageBySpawnManager manage = newFish.AddComponent<ManageBySpawnManager>();
        manage.onDestroyUnityEvent.AddListener(() =>
        {
            if (!_fishes.value.Remove(newFish))
            {
                Debug.LogError("[SpawnManager] Can't remove fish from list<Fish>");
            }
        });
        _fishes.value.Add(newFish);
    }
}
