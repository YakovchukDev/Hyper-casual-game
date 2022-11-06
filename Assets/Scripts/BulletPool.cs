using System.Collections.Generic;
using UnityEngine;
using Entities;

public class BulletPool : MonoBehaviour
{
    private Bullet _prefabBullet;
    private List<Bullet> _pool;
    private Transform _parent;

    public BulletPool(Bullet prefab, int startCount)
    {
        _prefabBullet = prefab;
        _parent = null;
        CreatePool(startCount);
    }
    public BulletPool(Bullet prefab, int startCount, Transform container)
    {
        _prefabBullet = prefab;
        _parent = container;
        CreatePool(startCount);
    }
    public Bullet GetFreeElement()
    {
        foreach(Bullet bullet in _pool)
        {
            if(!bullet.gameObject.activeInHierarchy)
            {
                bullet.transform.position = _parent.position;
                bullet.gameObject.SetActive(true);
                return bullet;
            }
        }
        return CreateObject(true);
    }
    private void CreatePool(int startCount)
    {
        _pool = new List<Bullet>(startCount);
        for(int i = 0; i < startCount; i++)
        {
            CreateObject();
        }
    }
    private Bullet CreateObject(bool isActiveDefault = false)
    {
        Bullet prefabCopy = Instantiate(_prefabBullet, _parent).GetComponent<Bullet>();
        prefabCopy.gameObject.SetActive(isActiveDefault);
        _pool.Add(prefabCopy);
        return prefabCopy;
    }
    
}
