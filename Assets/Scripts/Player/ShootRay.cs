using UnityEngine;
using UnityEngine.UI;

public class ShootRay : MonoBehaviour
{
    public Slider energySlider;
    public static float energy;
    public Text energyText;

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
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && energy > 0)
        {
                Shoot();
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


    void Shoot ()
    {
        energy = energy - 0.1f;
        energySlider.value = energy;
        energyText.text = "Energy: " + Mathf.RoundToInt(energy) + "/100";
        timer = 0f;

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHit, shootableMask))
        {
            if(shootHit.collider.gameObject.tag == "Moveable")
            {
                 //shootHit.transform = shootRay.GetPoint;
            }
        }
    }
}
