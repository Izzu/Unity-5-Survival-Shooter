using UnityEngine;
using System.Collections;

public class CameraChange : MonoBehaviour {

    public GameObject player;

	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetButton("R"))
        {
            transform.position = player.GetComponent<Rigidbody>().position + transform.TransformDirection(new Vector3(1.2f, 0.82f, -0.83f));
        }
        else if(Input.GetButtonDown("L"))
        {
            transform.position = player.GetComponent<Rigidbody>().position + transform.TransformDirection(new Vector3(-0.459f, 0.82f, -0.83f));
        }
        else if(Input.GetButtonDown("F"))
        {
            transform.position = player.GetComponent<Rigidbody>().position + transform.TransformDirection(new Vector3(0.302f, 0.82f, -0.198f));
        }
	}
}
