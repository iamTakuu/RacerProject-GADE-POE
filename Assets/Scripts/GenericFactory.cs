using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericFactory<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected List<T>  prefabs;
    protected T GetNewInstance()
    {
        var inst = prefabs[Random.Range(0, prefabs.Count)];
        return Instantiate(inst);
    }
}
