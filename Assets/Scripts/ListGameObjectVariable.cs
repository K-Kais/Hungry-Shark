using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/List GameObject Variable")]
public class ListGameObjectVariable : ScriptableObject
{
    public List<GameObject> value = new List<GameObject>();
}
