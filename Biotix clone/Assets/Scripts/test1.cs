using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void shoot();
}

public class rangeAtack : MonoBehaviour, IWeapon
{
    int damage = 5;
    public void shoot()
    {
        print(damage);
    }
}

public class meleeAtack : MonoBehaviour, IWeapon
{
    int damage = 10;
    public void shoot()
    {
        print(damage);
    }
}
