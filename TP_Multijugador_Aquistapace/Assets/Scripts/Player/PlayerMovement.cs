using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speedMovement = 5f;
    [SerializeField] private float speedRotation = 5f;
    [SerializeField] private float jumpForce = 5f;

    private Rigidbody rig;
    private Animator anim;
    private PhotonView view;

    // ---------------------------

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if(view.IsMine)
        {
            InputMovement();
        }
    }

    private void InputMovement()
    {
        float horizontal = Input.GetAxis("Horizontal") ;
        float vertical = Input.GetAxis("Vertical");

        Vector3 newPos = new Vector3(horizontal * speedMovement, 0, vertical * speedMovement) * Time.deltaTime;
        rig.MovePosition(this.transform.position + newPos);

        Vector3 movementDirection = new Vector3(horizontal, 0, vertical);
        movementDirection.Normalize();

        SetRotation(horizontal, vertical, movementDirection);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Salto");
        }

        SetAnimation(horizontal, vertical);
    }

    private void SetRotation(float hor, float ver, Vector3 direction)
    {
        if(direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, speedRotation);
        }
    }

    private void SetAnimation(float hor, float ver)
    {
        if (hor != 0 || ver != 0)
        {
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }
}
