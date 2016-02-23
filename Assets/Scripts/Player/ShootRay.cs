using UnityEngine;
using UnityEngine.UI;

public class ShootRay : MonoBehaviour
{
    //public int damagePerShot = 20;
    //public float timeBetweenBullets = 0.15f;
    //public float range = 100f;
    public Slider energySlider;
    public static float energy;
    public Text energyText;

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    //ParticleSystem gunParticles;
    LineRenderer gunLine;
    //AudioSource gunAudio;
    //Light gunLight;
    //float effectsDisplayTime = 0.2f;


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        //gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        //gunAudio = GetComponent<AudioSource> ();
        //gunLight = GetComponent<Light> ();
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
        
        /*if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        } */   
    }

    void Recharge()
    {
        if (energy < 20 && !Input.GetButton("Fire1"))
        {
            energy = energy + 1f;
            energySlider.value = energy;
            energyText.text = "Energy: " + Mathf.RoundToInt(energy) + "/100";
        }
    }


    /*public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }*/


    void Shoot ()
    {
        energy = energy - 0.1f;
        energySlider.value = energy;
        energyText.text = "Energy: " + Mathf.RoundToInt(energy) + "/100";

        timer = 0f;

        //gunAudio.Play ();

        //gunLight.enabled = true;

        //gunParticles.Stop ();
        //gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHit, shootableMask))
        {
            //EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            /*if(enemyHealth != null)
            {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }*/
            if(shootHit.collider.gameObject.tag == "Moveable")
            {
                 //shootHit.transform = shootRay.GetPoint;
            }

            //gunLine.SetPosition (1, shootHit.point);
        }
        /*else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }*/
    }
}
