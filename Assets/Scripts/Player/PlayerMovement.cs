using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float rotateSpeed = 3f;
    Vector3 movement;
    Vector3 rotation;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float rotate = Input.GetAxis("Horizontal");
        float move = Input.GetAxisRaw("Vertical");

        Move(move);
        Turning(rotate);
        Animating(rotate, move);
    }

    void Move(float move)
    {
        movement.Set(0f, 0f, move);
        movement = movement.normalized * speed * Time.deltaTime;
        movement = transform.TransformDirection(movement);
        playerRigidbody.MovePosition(playerRigidbody.position + movement);
    }

    void Turning(float rotate)
    {
        rotation.Set(0f, rotate, 0f);
        rotation = rotation.normalized * rotateSpeed;
        Quaternion rotated = Quaternion.Euler(rotation);
        playerRigidbody.MoveRotation(playerRigidbody.rotation * rotated);
        
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }
}
