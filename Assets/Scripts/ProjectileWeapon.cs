using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{
    public float launchForce;
    public float fireRate;
    float timeToFire = 0; 

    public GameObject projectile;
    public Transform shotPoint;

    public string Button = "Fire1";

    private void Update()
    {
        Vector2 weaponPosition = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - weaponPosition;
        transform.right = direction;
        if (fireRate == 0)
        {
            if (Input.GetButtonDown(Button))
            {
                StartCoroutine(Shoot());
            }
        }
        else
        {
            if (Input.GetButton(Button) && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                StartCoroutine(Shoot());
            }
        }

    }

    IEnumerator Shoot()
    {
        Debug.Log("Grenade Shooting");
        GameObject newProj = Instantiate(projectile, shotPoint.position, shotPoint.rotation);
        newProj.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;

        yield return new WaitForSeconds(0.02f);
    }
}
