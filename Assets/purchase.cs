using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class purchase : MonoBehaviour {

    MakeTurret makeTurret;
    public Weapon turret;
    public Weapon destroyer;
    public Weapon freezer;

    void start()
    {
        makeTurret = MakeTurret.instance;
    }

    public void purchaseTurret()
    {
        makeTurret = MakeTurret.instance;
        Debug.Log("Turret purchased");
        makeTurret.SelectWeaponToBuild(turret);
    }

    public void purchaseDestroyer()
    {
        makeTurret = MakeTurret.instance;
        Debug.Log("Destroyer purchased");
        makeTurret.SelectWeaponToBuild(destroyer);
    }

    public void purchaseFreezer()
    {
        makeTurret = MakeTurret.instance;
        Debug.Log("Freezer purchased");
        makeTurret.SelectWeaponToBuild(freezer);
    }


}
