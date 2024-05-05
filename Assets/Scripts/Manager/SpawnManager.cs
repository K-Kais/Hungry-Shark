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

        InstantiateFish(_fishCount.Value);
    }
    private void InstantiateFish(int fishCount)
    {
        ObjectPool.Instance.InitObjectPool();
        for (int i = 0; i < fishCount; i++) CreateRandomFish();
    }

    public GameObject CreateRandomFish()
    {
        Vector3 pos = RandomPos();
        float direction = Random.Range(0f, 360f);
        Quaternion rotationFish = Quaternion.Euler(0, 90f, 0);

        GameObject newFish = Instantiate(_fish, pos, Quaternion.Euler(Vector3.forward * direction) * rotationFish);
        RegisterFish(newFish.GetComponent<BoidMovement>());
        return newFish;
    }

    public Vector3 RandomPos()
    {
        float xPos;
        float yPos;

        float cameraX = Camera.main.transform.position.x;
        float cameraY = Camera.main.transform.position.y;

        float camXMin = cameraX - _boundery.XLimit;
        float camXMax = cameraX + _boundery.XLimit;
        float camYMin = cameraY - _boundery.YLimit;
        float camYMax = cameraY + _boundery.YLimit;

        float rand = Random.Range(0f, 1f);
        if (rand < 0.5f)
        {
            if (Random.Range(0f, 1f) < 0.5f) xPos = camXMin;
            else xPos = camXMax;

            yPos = Random.Range(camYMin, camYMax);
        }
        else
        {
            if (Random.Range(0f, 1f) < 0.5f) yPos = camYMin;
            else yPos = camYMax;

            xPos = Random.Range(camXMin, camXMax);
        }
        return new Vector3(xPos, yPos, 0);
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
