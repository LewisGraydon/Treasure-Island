using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GunRayShooter : MonoBehaviour
{

    public int gunDamage = 1;  
    public float fireRate = 0.25f;                                      
    public float weaponRange = 50f;                                    
    public float hitForce = 100f;                                       
    public Transform gunEnd;    //contains a reference to the empty game object where the bullet will be instanciated from

    private Camera fpsCam;                                              // Holds a reference to the first person camera
                                     
    private LineRenderer laserLine;                                     // Reference to the LineRenderer component which will display our laserline
    private float nextFire;                                             // Float to store the time the player will be allowed to fire again, after firing

    public GameObject Bullet;
    public int power = 1000;

	public GameObject Gun;

	public int maxAmmo = 72;
	public int clipSize = 8;
	public int currentAmmo = 24;
	public int ammoInMag = 8;

    public bool fullAuto = false;

	public Text playerAmmoUI;

    void Start()
    {
        fpsCam = GetComponentInParent<Camera>();
    }

    void RayCastFunction()
    {
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f)); //created a bullet from the centre of the screen

        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange)) //checks to see whether the ray has hit anything
        {

            ShootableTarget health = hit.collider.GetComponent<ShootableTarget>(); //if we hit the object it will get the ShootableTarget script from it, if it exists.

            if (health != null) //damages the enemy if there is a health script attached
            {
                health.Damage(gunDamage);
            }
        }
    }

    void Update()
    {
		if (Input.GetMouseButtonDown(1) && Time.time > nextFire && ammoInMag > 0 && fullAuto == false) //if the player right clicks, the time passed is greater than the delay, if there is ammo in the mag and if semi-automatic
        {
            nextFire = Time.time + fireRate;
         
                GameObject tempBullet;
                tempBullet = Instantiate(Bullet, gunEnd.position, transform.rotation) as GameObject;     //Creates the bullet

                Rigidbody tempRigidBody;
                tempRigidBody = tempBullet.GetComponent<Rigidbody>();
                tempRigidBody.AddForce(-transform.right * power);      //Determines the speed of the projectile

			ammoInMag--;   

			Destroy(tempBullet, 2.5f);      //Destroys the bullet 2.5s after firing it

            RayCastFunction(); //calls the raycast function to determine whether the bullet hit and deal its damage
        }

        if (Input.GetButton("Fire1") && Time.time > nextFire && ammoInMag > 0 && fullAuto == true) //if the player holds down right click, the time passed is greater than the delay, if there is ammo in the mag and if fully-automatic
        {
            nextFire = Time.time + fireRate;

            GameObject tempBullet;
            tempBullet = Instantiate(Bullet, gunEnd.position, transform.rotation) as GameObject;     //Creates the bullet

            Rigidbody tempRigidBody;
            tempRigidBody = tempBullet.GetComponent<Rigidbody>();
            tempRigidBody.AddForce(-transform.right * power);      //Determines the speed of the projectile

            ammoInMag--;

            Destroy(tempBullet, 2.5f);      //Destroys the bullet 2.5s after firing it

            RayCastFunction(); //calls the raycast function to determine whether the bullet hit and deal its damage
        }


        if (ammoInMag == 0) //reloads automatically once the ammo in the mag is 0
		{
			Reload();
		}


		if (Input.GetKeyDown(KeyCode.R)) //calls the reload function
		{
			Reload();
		}

        if(Input.GetKeyDown(KeyCode.Q)) //switches fire mode between semi-automatic and fully-automatic
        {
            fullAuto = !fullAuto;
        }

		playerAmmoUI.text = "Ammo: " + ammoInMag.ToString() + "/" + currentAmmo.ToString(); //displays the ammo in mag out of the total ammo remaining 
    }

	void Reload() //calls the reload function, restoring the ammo in mag by the current ammo
	{
		if (currentAmmo > clipSize && ammoInMag < clipSize)
		{
			Gun.GetComponent<Animation>().Play("Gun Reloading");
			currentAmmo = currentAmmo - clipSize + ammoInMag;
			ammoInMag = clipSize;
		}

		if (currentAmmo <= clipSize && currentAmmo != 0) 
		{
			Gun.GetComponent<Animation>().Play("Gun Reloading");
			ammoInMag = currentAmmo;
			currentAmmo = 0;
		}
	}
}