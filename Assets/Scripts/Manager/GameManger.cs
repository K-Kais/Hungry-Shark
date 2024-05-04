using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : Singleton<GameManger>
{
    [SerializeField] private GameObject _spawnManager;

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        StartGame();
    }
    private void StartGame()
    {
        Instantiate(_spawnManager);
        SpawnManager.Instance.Initialize();
    }
}
