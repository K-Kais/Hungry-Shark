using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/IntVariable")]
public class IntVariable : ScriptableObject
{
    [SerializeField] private GameEvent _onValueChange;
    [SerializeField] private int _value;
    public int Value
    {
        get => _value;
        set
        {
            _value = value;
            if (_onValueChange != null) _onValueChange.Raise();
        }
    }
}
