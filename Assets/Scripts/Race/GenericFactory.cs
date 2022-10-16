using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class GenericFactory<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected List<T>  prefabs;
    private int _fabIndex;
    
    /// <summary>
    /// Spawn an instance at the given Position.
    /// </summary>
    /// <param name="pos">A Vector3 Point</param>
    /// <returns>MonoBehaviour Prefab</returns>
    protected T GetInstance(Vector3 pos)
    {
        var inst = prefabs[_fabIndex++];
        return Instantiate(inst, pos, quaternion.identity);
    }
}
