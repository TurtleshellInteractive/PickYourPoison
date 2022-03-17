using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float lookSpeed;
    public float walkSpeed;
    public float smoothTime = 0.3f;
    public float gravity = -9.8f;
    public float jumpHeight;
    public Camera cam;

    Vector2 rotation = Vector2.zero;
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelo = Vector2.zero;

    float camPitch = 0f;
    float velocityY = 0f;
    bool justJumped = false;
    bool canJump = true;
    CharacterController controller = null;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        mouselook();
        if (Input.GetButtonDown("Jump") && canJump)
        {
            jump();
        }
        moveUpdate();
        if (justJumped)
        {
            justJumped = false;
        }
    }

    void mouselook()
    {
        rotation = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        camPitch -= rotation.y * lookSpeed;
        camPitch = Mathf.Clamp(camPitch, -90.0f, 90.0f);
        cam.transform.localEulerAngles = Vector3.right * camPitch;

        transform.Rotate(Vector3.up * rotation.x * lookSpeed);
    }

    void moveUpdate()
    {
        Vector2 wasd = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        wasd.Normalize();
        currentDir = Vector2.SmoothDamp(currentDir,wasd,ref currentDirVelo,smoothTime);

        if (controller.isGrounded && !justJumped)
        {
            velocityY = 0f;
            canJump = true;
        }
        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);
    }

    void jump()
    {
        velocityY = jumpHeight;
        justJumped = true;
        canJump = false;
    }
}
