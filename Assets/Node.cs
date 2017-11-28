using UnityEngine;

public class Node : MonoBehaviour {

	public Color hoverColor;
	public Vector3 positionOffset;

    public GameObject weapon;

	private Renderer rend;
	private Color startColor;

    MakeTurret makeTurret;

	void Start ()
	{
		rend = GetComponent<Renderer>();
		startColor = rend.material.color;
        makeTurret = MakeTurret.instance;
    }

	void OnMouseDown ()
	{
        if (!makeTurret.CanBuild)
            return;

		if (weapon != null)
		{
			Debug.Log("Can't build there! - TODO: Display on screen.");
			return;
		}

        //GameObject turretToBuild = makeTurret.GetWeaponToBuild();
        makeTurret.BuildWeaponOn(this);
		//turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
	}

	void OnMouseEnter ()
	{
        if (!makeTurret.CanBuild)
            return;
        if (makeTurret.HasMoney)
        {
            rend.material.color = hoverColor;
            Light light = (Light)this.GetComponent("Light");
            light.range = 25;
            light.enabled = true;
            
            //Component halo = GetComponent("Halo");
            //halo.GetType().GetProperty("size").SetValue(halo, 25, null);
            //halo.GetType().GetProperty("enabled").SetValue(halo, true, null);
        }
        else
            rend.material.color = Color.red;
	}

	void OnMouseExit ()
	{
		rend.material.color = startColor;
        //Component halo = GetComponent("Halo");
        //halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
        Light light = (Light)this.GetComponent("Light");
        //light.range = 25;
        light.enabled = false;
    }

}
