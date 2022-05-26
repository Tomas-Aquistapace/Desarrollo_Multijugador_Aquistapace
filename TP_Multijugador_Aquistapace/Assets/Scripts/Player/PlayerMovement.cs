using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speedMovement = 5f;
    [SerializeField] private float speedRotation = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Vector3[] rayPositions = new Vector3[5];
    private float rayDistance = 0.3f;

    [Header("Particles")]
    [SerializeField] private ParticleSystem dustParticle;
    [SerializeField] private GameObject starParticlePref;
    [SerializeField] private GameObject ArrowToPlayer;

    [Header("Score")]
    [SerializeField] private int score = 0;
    public UI_PlayerScore playerScore;

    private Rigidbody rig;
    private Collider coll;
    private Animator anim;
    private PhotonView view;

    private Transform cam;

    private bool canMove = false;

    [SerializeField] private bool isGrounded = true;
    private bool auxGrounded = true;

    // ---------------------------

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        anim = GetComponentInChildren<Animator>();
        view = GetComponent<PhotonView>();

        cam = FindObjectOfType<Camera>().transform;

        ArrowToPlayer.SetActive(view.IsMine);
    }

    private void Update()
    {
        if(view.IsMine && canMove)
        {
            InputMovement();
            InputJump();

            ArrowToPlayer.transform.LookAt(cam);
        }
    }

    private void FixedUpdate()
    {
        isGrounded = CheckAllRays();

        if (isGrounded)
        {
            anim.SetBool("IsFell", true);

            if(!auxGrounded)
            {
                dustParticle.Play();
                anim.SetBool("IsFalling", false);
                anim.SetBool("IsJumping", false);

                auxGrounded = isGrounded;
            }
            return;
        }

        anim.SetBool("IsFell", false);
        auxGrounded = isGrounded;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            var particle = Instantiate(starParticlePref);
            particle.transform.localPosition = collision.GetContact(0).point;

            Destroy(particle.gameObject, 1f);
        }
    }

    // ----------------------------------

    private void InputMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 newPos = new Vector3(horizontal * speedMovement, 0, vertical * speedMovement) * Time.deltaTime;
        rig.MovePosition(this.transform.position + newPos);

        Vector3 movementDirection = new Vector3(horizontal, 0, vertical);
        movementDirection.Normalize();

        SetRotation(horizontal, vertical, movementDirection);

        SetAnimation(horizontal, vertical);
    }

    private void InputJump()
    {
        // Check if press the button
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            dustParticle.Stop();

            anim.SetBool("IsJumping", true);
            anim.SetBool("IsFalling", false);
        }
        else if(!isGrounded)
        {
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsFalling", true);
        }
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

    private bool CheckAllRays()
    {
        for (int i = 0; i < rayPositions.Length; i++)
        {
            if(Physics.Raycast(this.transform.position + rayPositions[i], Vector3.down, rayDistance))
            {
                return true;
            }
        }
        return false;
    }

    // ----------------------------------

    public void EarnPoint()
    {
        score += 1;
        playerScore.SetScore(score.ToString());
    }

    public void PushUp()
    {
        rig.AddForce(Vector3.up * (jumpForce / 2), ForceMode.Impulse);
        dustParticle.Stop();

        anim.SetBool("IsJumping", true);
        anim.SetBool("IsFalling", false);
    }

    public void SmashPlayer()
    {
        anim.SetTrigger("Smashed");
    }

    public void ActivatePlayer(bool state)
    {
        rig.isKinematic = !state;
        canMove = state;
        coll.enabled = state;
    }
}
