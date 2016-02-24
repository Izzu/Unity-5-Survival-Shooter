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
    int moveableMask;
    LineRenderer gunLine;


    void Awake ()
    {
        moveableMask = LayerMask.GetMask ("Moveable");
        gunLine = GetComponent <LineRenderer> ();
        energy = 100;
        InvokeRepeating("Recharge", 1, .5f);
    }


    void Update ()
    {
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, moveableMask))
        {
            gunLine.SetPosition(0, transform.position);
            gunLine.SetPosition(1, shootHit.point);
            if (Input.GetButton("Fire1") && energy > 0)
            {
                shootHit.transform.position = shootHit.point;
                Shoot();
            }
            else
                Recharge();
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
    }
}
