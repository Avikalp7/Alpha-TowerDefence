using UnityEngine;
using System.Collections;

public class MakeTurret : MonoBehaviour {

	public static MakeTurret instance;
    private Weapon weaponToBuild;

    void Awake ()
	{
        if (instance != null)
            return;
		instance = this;
	}

	public GameObject standardTurretPrefab;
    public GameObject destroyerPrefab;
    public GameObject freezerPrefab;
    public GameObject buildEffect;


    public bool CanBuild { get { return weaponToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= weaponToBuild.cost; } }

    //void Start ()
    //{
    //	weapon = standardTurretPrefab;
    //}

 //   public Weapon GetWeaponToBuild ()
	//{
	//	return weaponToBuild;
	//}

    //public void SetWeaponToBuild(GameObject weapon)
    //{
    //    weaponToBuild = weapon;
    //}

    public void SelectWeaponToBuild(Weapon weapon)
    {
        weaponToBuild = weapon;
    }

    public void BuildWeaponOn(Node node)
    {
        if (PlayerStats.Money < weaponToBuild.cost)
        {
            Debug.Log("Not Enough Money!");
            return;
        }
        GameObject weapon = (GameObject) Instantiate(weaponToBuild.prefab, node.transform.position + node.positionOffset, Quaternion.identity);
        node.weapon = weapon;

        GameObject effect = (GameObject)Instantiate(buildEffect, node.transform.position + node.positionOffset, Quaternion.identity);
        Destroy(effect, 5f);
        PlayerStats.Money -= weaponToBuild.cost;
    }

}
