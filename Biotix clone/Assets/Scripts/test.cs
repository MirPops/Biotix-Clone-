using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class test : MonoBehaviour
{
    IWeapon weapon1 = new meleeAtack();
    IWeapon weapon2 = new rangeAtack();

    private void Start()
    {
        weapon1.shoot();
        weapon2.shoot();
    }
}
