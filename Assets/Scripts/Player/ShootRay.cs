using UnityEngine;
using UnityEngine.UI;

public class ShootRay : MonoBehaviour
{
    public Slider energySlider;
    public static float energy;
    public Text energyText;
    public float range = 100f;
    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int moveableMask;
    LineRenderer gunLine;
    public GameObject player;
    private Color startColor;


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
        //shootRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(shootRay, out shootHit, range, moveableMask))
        {
            GameObject anObject = shootHit.collider.gameObject;
            gunLine.SetPosition(0, transform.position);
            gunLine.SetPosition(1, shootHit.point);
            Highlight(anObject);

            if (Input.GetButton("Fire1") && energy > 0)
            {
                shootHit.transform.parent = player.transform;
                //shootHit.transform.position = new Vector3(Input.mousePosition.x, 0f, 0f);
                //shootHit.transform.position = shootHit.point;
                Shoot();
            }
            else
            {
                anObject.transform.parent = null;
                unHighlight(anObject);
                Recharge();
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
    }

    void Highlight(GameObject anObject)
    {
        if(anObject.GetComponent<Light>() == null)
            anObject.AddComponent<Light>();
        //startColor = anObject.GetComponent<Renderer>().material.color;
        //anObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

    void unHighlight(GameObject anObject)
    {
        if(anObject.GetComponent<Light>() != null)
            Destroy(anObject.GetComponent<Light>());
        //anObject.GetComponent<Renderer>().material.color = Color.black;
    }
}
