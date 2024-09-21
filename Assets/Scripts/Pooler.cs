using System;
using System.Collections.Generic;
using UnityEngine;

public static class Pooler
{
    private static Dictionary<string, Pool> pools = new Dictionary<string, Pool>();
    private static GameObject obj;
    public static GameObject Spawn(GameObject go, Vector3 pos, Quaternion rot)
    {
        
        string key = go.name.Replace("(Clone)", "");
        if (pools.ContainsKey(key))
        {
            if (pools[key].inactive.Count == 0)
            {
                obj = GameObject.Instantiate(go, pos, rot);
            }
            else
            {
                obj = pools[key].inactive.Pop();
                obj.transform.position = pos;
                obj.transform.rotation = rot;
                obj.SetActive(true);
            }
        }
        else
        {
            GameObject newParent = new GameObject($"{key}_POOL");
            obj = GameObject.Instantiate(go, pos, rot, newParent.transform);
            Pool newPool = new Pool(newParent);
            pools.Add(key, newPool);
        }
        return obj;  // Return the spawned or reactivated GameObject
    }

    public static void Despawn(GameObject go)
    {
        string key = go.name.Replace("(Clone)", "");
        if (pools.ContainsKey(key))
        {
            pools[key].inactive.Push(go);
            
            go.SetActive(false);
        }
        else
        {
            GameObject newParent = new GameObject($"{key}_POOL");
            Pool newPool = new Pool(newParent);
            go.transform.SetParent(newParent.transform);
            pools.Add(key, newPool);
            pools[key].inactive.Push(go);
            go.SetActive(false);
        }
    }

    public static void ClearPools()
{
    foreach (var pool in pools.Values)
    {
        foreach (var obj in pool.inactive)
        {
            if (obj != null)
            {
                GameObject.Destroy(obj);
            }
        }
        GameObject.Destroy(pool.parent);
    }
    pools.Clear();
}
}
