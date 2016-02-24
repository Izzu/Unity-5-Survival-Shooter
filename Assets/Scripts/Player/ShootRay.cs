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
    //int moveableMask;
    int shootableMask;
    LineRenderer gunLine;
    public GameObject player;
    private Color startColor;
    private GameObject anObject;


    void Awake ()
    {
        //moveableMask = LayerMask.GetMask ("Moveable");
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent <LineRenderer> ();
        energy = 100;
        InvokeRepeating("Recharge", 1, .5f);
    }


    void Update ()
    {
        //shootRay.origin = transform.position;
        //shootRay.direction = transform.forward;
               
        if (Input.GetButton("Fire1"))
        {
            shootRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            gunLine.enabled = true;
            gunLine.SetPosition(0, transform.position);
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                gunLine.SetColors(Color.cyan, Color.cyan);
                gunLine.material.color = Color.cyan;
                anObject = shootHit.collider.gameObject;
                gunLine.SetPosition(1, shootHit.point);

                //Vector3 mousePoint = Camera.main.WorldToScreenPoint(shootHit.transform.position);
                //Vector3 offset = shootHit.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mousePoint.z));

                if (energy > 0 && shootHit.collider.tag == "Moveable")
                {
                    HighlightGreen(anObject);
                    //Vector3 currScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mousePoint.z);
                    //Vector3 currPos = Camera.main.ScreenToWorldPoint(currScreenPoint);
                    //shootHit.transform.position = currPos;
                    
                    shootHit.transform.parent = player.transform;
                    //shootHit.transform.position = Vector3.Lerp(shootHit.transform.position, new Vector3(pos.x, pos.y, shootHit.transform.position.z), 0.5f * Time.deltaTime);
                    Shoot();
                }
                else if (energy > 0)
                {
                    gunLine.SetColors(Color.red, Color.red);
                    gunLine.material.color = Color.red;
                    HighlightRed(anObject);
                    Shoot();
                }
            }
        }
        else
        {
            gunLine.enabled = false;
            anObject.transform.parent = null;
            unHighlight(anObject);
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

    void HighlightGreen(GameObject anObject)
    {
        if (anObject.GetComponent<Light>() == null)
        {
            Light aLight = anObject.AddComponent<Light>();
            aLight.color = Color.cyan;
        }
    }

    void HighlightRed(GameObject anObject)
    {
        if (anObject.GetComponent<Light>() == null)
        {
            Light aLight = anObject.AddComponent<Light>();
            aLight.color = Color.red;
        }
    }

    void unHighlight(GameObject anObject)
    {
        if(anObject.GetComponent<Light>() != null)
            Destroy(anObject.GetComponent<Light>());
    }
}
