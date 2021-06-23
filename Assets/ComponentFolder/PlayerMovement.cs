using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController character;
    Rigidbody rb;
    ParticleSystem gunSource;
    BoxCollider groundCheck;
    Transform aimTarget;
    Transform cam;
    Transform torso;
    Transform shoulderTarget;

    public float movementSpeed;
    public float sprintSpeed;
    public float aerialSpeed;
    public float jumpForce;
    public float turnSmooting;
    public float gravityForce;
    float turnSmoothVelocity;

    float directionY;
    Vector3 directionalVelocity;
    bool isGrounded = true;

    void OnValidate()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        character = this.gameObject.GetComponentInChildren<CharacterController>();
        cam = Camera.main.transform;
        torso = GameObject.Find("mechChest").transform;
        aimTarget = GameObject.Find("AimTargetPoint").transform;
        shoulderTarget = GameObject.Find("ShoulderTarget").transform;
        gunSource = this.gameObject.GetComponentInChildren<ParticleSystem>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 movemnt = Vector3.zero;
        Vector3 aerialMovemnt = Vector3.zero;

        RaycastHit jumpRay;
        if (Physics.Raycast(character.transform.position, character.transform.TransformDirection(Vector3.down), out jumpRay, 0.1f))
        {
            isGrounded = true;
            Debug.DrawRay(character.transform.position, character.transform.TransformDirection(Vector3.down) * 0.1f, Color.green);
        }
        else
        {
            isGrounded = false;
            Debug.DrawRay(character.transform.position, character.transform.TransformDirection(Vector3.down) * 0.1f, Color.red);
        }

        if (dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmooting);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : movementSpeed;
            if (isGrounded)
            {
                movemnt = moveDir * speed * Time.deltaTime;
                directionalVelocity = movemnt;
            }
            aerialMovemnt = moveDir * aerialSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            directionY = jumpForce * Time.deltaTime;
            if (dir.magnitude <= 0.1f)
            {
                directionalVelocity = Vector3.zero;
            }
        }
        else if (!isGrounded)
        {
            directionY -= gravityForce * Time.deltaTime;
            movemnt = directionalVelocity + aerialMovemnt;
        }
        else if (isGrounded)
            directionY = 0;

        movemnt.y = directionY;

        character.Move(movemnt);

        RaycastHit camRay;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out camRay, 1000f))
        {
            Debug.DrawRay(cam.transform.position, cam.transform.forward * 1000f, Color.green);
            aimTarget.position = camRay.point;
        }
        else
        {
            Debug.DrawRay(cam.transform.position, cam.transform.forward * 100, Color.yellow);
            aimTarget.position = cam.transform.position + cam.transform.forward * 100;
        }
        torso.LookAt(aimTarget, transform.up);
        shoulderTarget.position = torso.position + torso.right;

        gunSource.transform.LookAt(aimTarget, transform.up);
        if (Input.GetButton("Fire1"))
        {
            gunSource.Emit(1);
        }
    }
}
