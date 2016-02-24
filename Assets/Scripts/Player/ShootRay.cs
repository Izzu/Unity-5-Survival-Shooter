using UnityEngine;
using UnityEngine.UI;

public class ShootRay : MonoBehaviour
{
    public Slider energySlider;
    public static float energy;
    public Text energyText;
    public float range = 1000f;
    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    LineRenderer gunLine;


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunLine = GetComponent <LineRenderer> ();
        energy = 100;
        InvokeRepeating("Recharge", 1, .5f);
    }


    void Update ()
    {
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            gunLine.SetPosition(0, transform.position);
            gunLine.SetPosition(1, shootHit.point);
            if (Input.GetButton("Fire1") && energy > 0)
            {
                Shoot();
            }
        }
    }

    void Recharge()
    {
        if (energy < 100 && !Input.GetButton("Fire1"))
        {
            energy = energy + 1f;
            energySlider.value = energy;
            energyText.text = "Energy: " + Mathf.RoundToInt(energy) + "/100";
        }
    }


    void Shoot()
    {
        energy = energy - 0.5f;
        energySlider.value = energy;
        energyText.text = "Energy: " + Mathf.RoundToInt(energy) + "/100";

        /*if (Physics.Raycast(shootRay, out shootHit, Mathf.Infinity, shootableMask))
        {
            ///EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }*/
    }
}
