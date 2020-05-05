using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class MazePlayerHandler : MonoBehaviour
{
    #region Singleton
    public static MazePlayerHandler instance;
    public void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }
    }
    #endregion

    #region Movement Attributes 
    private float horizontal;
    private float vertical;
    private Vector3 movement;
    public float speed;
    private Vector3 input;
    #endregion
    
    private Rigidbody rb;

    #region SFX 
    [Space]
    [Header("SFX")]
    private AudioSource sfx;
    [SerializeField] private AudioClip collisionSFX;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sfx = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        input = new Vector3(horizontal, 0, vertical);
        movement = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * input;
        rb.AddForce(movement * speed * Time.deltaTime, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "mazeFloor")
        sfx.PlayOneShot(collisionSFX);
    }
}
