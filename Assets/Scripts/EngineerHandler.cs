using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class EngineerHandler : MonoBehaviour
{
    #region Singleton
    internal static EngineerHandler instance = null;
    void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }
    }
    #endregion

    #region Character Attributesd
    private BoxCollider bcol;
    internal Animator anim;
    public bool canMove = true;
    internal bool engineerMove = true; //variable to be used by the GameManager.cs in order to control whether the Engineer Robot can move
    private Rigidbody rb;
    internal static float speed = 2.4f;
    public float rateOfTurn = 5000f;
    private Vector3 pos;
    private Quaternion rot;
    private bool movefwd = true;

    #endregion

    #region Others
    [Space]
    [Header("Sphere Game Object")]
    [SerializeField] internal GameObject sphere;
    [Tooltip("Sphere Tag is the string to be parsed into the script. Need to be the same as the tag of the Sphere GameObject")]
    public string sphereTag = "PlayerSphere";

    [Space]
    [Tooltip("This inspector variable is to check if the box to be carried is being parsed to the Engineer Script when triggered")]
    public GameObject boxToCarry; //This inspector variable is to check if the box to be carried is being parsed to the Engineer Script when triggered
    #endregion

    #region SFX 
    [Space]
    [Header("SFX")]
    private AudioSource sfx;
    [SerializeField] private AudioClip[] stepSFX;
    [SerializeField] private AudioClip pickItemSFX;
    [SerializeField] private AudioClip releaseItemSFX;
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip turnSFX;
    [SerializeField] private AudioClip pushSFX;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bcol = GetComponent<BoxCollider>();
        bcol.enabled = false;
        anim = GetComponent<Animator>();
        sphere = GameObject.FindGameObjectWithTag(sphereTag);
        sfx = GetComponent<AudioSource>();
        anim.SetFloat("WalkSpeed", speed);
    }

    void Update()
    {
        if (engineerMove)
        {
            if (Input.GetButtonDown("Action3")) { TakeObject(); }
            if (!anim.GetBool("CarryObject"))
            {
                if (Input.GetButtonDown("Action4")) { Action(); }
                if (Input.GetButtonDown("Action1"))
                {
                    Jump();
                    FindObjectOfType<AIUI>().ShowText($"You can not jump, Sir. You are very heavy. Try to ask the {GameManager.sphereName} to jump for you.");
                }
                if (Input.GetButtonDown("Action2")) { Punch(); }
            }
        }
    }

    void LateUpdate()
    {
        if (engineerMove)
        {
            Move();
        }
        else
        {
            anim.SetBool("Movement", false);
            anim.SetBool("TurnLeft", false);
            anim.SetBool("TurnRight", false);
        }
    }

    public void TakeObject()
    {
        if (anim.GetBool("CarryObject"))
        {
            StartCoroutine(CantMove(3.0f));
            anim.SetBool("CarryObject", false);
            if (boxToCarry)
            {
                boxToCarry.GetComponent<PuzzleCubes>().OnRelease();
            }
        }
        else
        {
            StartCoroutine(CantMove(3.0f));
            if (boxToCarry)
            {
                anim.SetTrigger("TakeObject");
                anim.SetBool("CarryObject", true);
            }
            else
            {
                FindObjectOfType<AIUI>().ShowText("There is nothing to pick up");
            }
        }
    }

    public void Action()
    {
        anim.SetTrigger("Action");
    }

    public void Jump()
    {
        anim.SetTrigger("Jump");
    }

    public void Punch()
    {
        anim.SetTrigger("Punch");
    }

    public void Move()
    {
        if (canMove)
        {
            ForwardMovement();

            LateralMovement();

            if (Input.GetButtonUp("Vertical"))
            {
                anim.SetBool("Movement", false);
            }
            if (Input.GetButtonUp("Horizontal"))
            {
                movefwd = true;
                anim.SetBool("TurnLeft", false);
                anim.SetBool("TurnRight", false);
            }
        }
        else
        {
            anim.SetBool("Movement", false);

            LateralMovement();
        }
    }

    public void ForwardMovement()
    {
        //if (Input.GetButton("Vertical") && Input.GetAxis("Vertical") >= 0)
        if (Input.GetAxis("Vertical") > 0.1)
        {
            if (movefwd)
            {
                anim.SetBool("Movement", true);

                pos = transform.position + (transform.forward * (Input.GetAxis("Vertical") * speed * Time.deltaTime));
                rb.MovePosition(pos);

                rot = transform.rotation * Quaternion.Euler(Vector3.up * (rateOfTurn * Input.GetAxis("Horizontal") * Time.deltaTime));
                rb.MoveRotation(rot);
            }
        }
        else
        {
            anim.SetBool("Movement", false);
        }
    }

    public void LateralMovement()
    {
        //if (!anim.GetBool("Movement") && Input.GetButton("Horizontal"))
        if (!anim.GetBool("Movement"))
        {
            movefwd = false;

            if (Input.GetAxis("Horizontal") > 0.1f)
            {
                anim.SetBool("TurnRight", true);
                anim.SetBool("TurnLeft", false);
            }
            else if (Input.GetAxis("Horizontal") < -0.1f)
            {
                anim.SetBool("TurnLeft", true);
                anim.SetBool("TurnRight", false);
            }
            else if (Input.GetButton("Vertical"))
            {
                anim.SetBool("TurnLeft", false);
                anim.SetBool("TurnRight", false);
                movefwd = true;
            }

            else
            {
                anim.SetBool("TurnLeft", false);
                anim.SetBool("TurnRight", false);
                movefwd = true;
            }

            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.up * 45), 1);
        }
    }


    IEnumerator CantMove(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
        StopCoroutine("CantMove");
        yield return null;
    }

    #region Animation Events
    public void PickItem()
    {
        if (boxToCarry)
        {
            boxToCarry.GetComponent<PuzzleCubes>().OnCarry();
            bcol.enabled = true;
        }
    }

    public void Pick()
    {
        sfx.PlayOneShot(pickItemSFX);
    }

    public void ReleaseItem()
    {
        bcol.enabled = false;
        sfx.PlayOneShot(releaseItemSFX);
    }

    public void JumpSFX()
    {
        sfx.PlayOneShot(jumpSFX);
    }

    public void Step()
    {
        sfx.PlayOneShot(stepSFX[Random.Range(0, stepSFX.Length)], 0.09f);
    }

    public void Turn()
    {
        sfx.PlayOneShot(turnSFX, 0.09f);
    }

    public void Finger()
    {
        sfx.PlayOneShot(pushSFX);
    }
    #endregion
}
