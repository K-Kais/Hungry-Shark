using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Float Variable")]
public class FloatVariable : ScriptableObject
{
    [SerializeField] private GameEvent _onValueChange;
    [SerializeField] private float _value;
    public float Value
    {
        get { return _value; }
        set
        {
            _value = value;
            if( _onValueChange != null ) _onValueChange.Raise();
        }
    }
}
