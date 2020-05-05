using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class SphereHandler : MonoBehaviour
{
    #region Singleton
    internal static SphereHandler instance = null;
    void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }
    }
    #endregion

    #region Character Attributes
    internal CharacterController controller;
    internal Animator anim;
    [Header("Colliders of Sphere Body")]
    [Tooltip("Collider located on the body of the sphere so the game object can handle it during movements ")]
    [SerializeField] internal SphereCollider scol;
    [SerializeField] internal BoxCollider bcol;
    [Space]
    #endregion

    #region Movement Attributes
    private float speedOpen = 2.0f;
    private float speedClosed = 2.5f;
    private float jumpForce = 5.0f;
    private float verticalVelocity;
    internal bool sphereMove = true; //variable to control whether the Engineer or the Sphere sphereMove
    #endregion

    #region Follow Engineer Attributes
    [Header("Engineer Game Object")]
    [SerializeField] internal GameObject engineer;
    [SerializeField] private Transform engineerPos;
    [Tooltip("Engineer Tag is the string to be parsed into the script. Need to be the same as the tag of the Engineer GameObject")]
    private float followMaxDistance = 3.0f;
    private float followMinDistance = 0.2f;
    [SerializeField] private float followSpeed = 1.8f;
    #endregion

    #region SFX 
    [Space]
    [Header("SFX")]
    private AudioSource sfx;
    [SerializeField] private AudioClip stepSFX;
    [SerializeField] private AudioClip openSFX;
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip rotateSFX;
    [SerializeField] private AudioClip rollingSFX;
    #endregion

    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        engineer = GameObject.FindGameObjectWithTag(GameManager.engineerTag);
        engineerPos = GameObject.FindGameObjectWithTag(GameManager.engineerTag).transform;
        bcol.enabled = false;
        sfx = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (sphereMove)
        {
            if (controller.isGrounded)
            {
                if (Input.GetButtonUp("Action3")) { Open(); }
                if (Input.GetButtonUp("Action1")) { Jump(); }
                if (Input.GetButton("Action2")) { Attack(true); }
                if (Input.GetButtonUp("Action2")) { Attack(false); }
                if (Input.GetButton("Action4")) { Action(); }
            }
            else
            {
                verticalVelocity -= 10.0f * Time.deltaTime;
            }
        }
    }

    void FixedUpdate()
    {
        if (sphereMove)
        {
            if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0))
            {
                anim.SetBool("Movement", true);
                Move();
            }

            if ((Input.GetAxis("Horizontal") == 0) && (Input.GetAxis("Vertical") == 0))
            {
                controller.Move(Vector3.zero);
                anim.SetBool("Movement", false);
            }
        }
        else
        {
            FollowMove();
        }
    }

    public void Attack(bool cond)
    {
        controller.transform.rotation = Quaternion.identity;
        anim.SetBool("Attack", cond);
    }

    public void Action()
    {
        if (anim.GetBool("Open"))
        {
            controller.transform.rotation = Quaternion.identity;
            anim.SetTrigger("Action");
        }
    }

    public void Open()
    {
        controller.transform.rotation = Quaternion.identity;
        anim.SetBool("Open", !anim.GetBool("Open"));
        sfx.PlayOneShot(openSFX);
        if (anim.GetBool("Open"))
        {
            bcol.enabled = true;
        }
        else
        {
            bcol.enabled = false;
        }
    }

    public void Move()
    {
        if (!controller.isGrounded)
        {
            verticalVelocity -= 10.0f * Time.deltaTime;
        }
        else if (controller.isGrounded)
        {
            verticalVelocity = -9.8f * Time.deltaTime;
        }
        if (anim.GetBool("Open"))
        {
            controller.transform.rotation = Quaternion.identity;
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Vector3 move = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * input;
            controller.Move(move * Time.deltaTime * speedOpen);
            transform.rotation = Quaternion.LookRotation(transform.TransformDirection(input));
        }
        else if (!anim.GetBool("Open"))
        {
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Vector3 move = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * input;
            controller.Move(move * Time.deltaTime * speedClosed);
            transform.rotation = Quaternion.LookRotation(transform.TransformDirection(input));
        }
    }

    private void FollowMove()
    {
        if (FindObjectOfType<GameManager>().sphereOn)
        {
            followSpeed = EngineerHandler.speed;
            if (Vector3.Distance(transform.position, engineer.transform.position) >= followMaxDistance)
            {
                Vector3 follow = new Vector3((engineer.transform.position.x - transform.position.x), 0, (engineer.transform.position.z - transform.position.z));
                controller.Move(follow.normalized * Time.deltaTime * followSpeed);
                transform.LookAt(engineer.transform);
            }
            else if (Vector3.Distance(transform.position, engineer.transform.position) <= followMinDistance)
            {
                controller.Move(Vector3.zero);
            }
        }
    }


    IEnumerator Jump(float time)
    {
        yield return new WaitForSeconds(0.15f);
        controller.radius *= 2f;
        yield return new WaitForSeconds(time * 0.2f);
        controller.radius *= 0.5f;
        yield return new WaitForSeconds(time * 0.7f);
        StopCoroutine(Jump(time));
        yield return null;
    }

    #region Animation Events
    public void Jump()
    {
        controller.transform.rotation = Quaternion.identity;
        anim.SetTrigger("Jump");
        sfx.PlayOneShot(jumpSFX);
        StartCoroutine(Jump(3.0f));
    }

    public void Step()
    {
        sfx.PlayOneShot(stepSFX);
    }

    public void Rotate()
    {
        sfx.PlayOneShot(rotateSFX);
    }

    public void Rolling()
    {
        sfx.PlayOneShot(rollingSFX);
    }
    #endregion
}
