using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia and Sohyun Yi

public class PuzzleCubes : MonoBehaviour
{
    #region Object attributes
    private EngineerHandler engineer;
    private Rigidbody rb;
    private Material mat;
    internal bool isCarried;
    #endregion

    [Space]
    [SerializeField] Transform grabPos;
    [Tooltip("Child GameObject of Engineer where the box will be attached")]
    public string engineerGrabPos = "GrabPos";
    [Space]
    [Tooltip("Tag on the Engineer Player")]
    public string engineerTag;
    [Space]
    public string switchTag = "switchForBox";

    public float constraintResetTimer = 3f;


    void Awake()
    {
        isCarried = false;
        rb = GetComponent<Rigidbody>();
        mat = GetComponent<Renderer>().material;
        engineer = FindObjectOfType<EngineerHandler>();
        engineerTag = GameManager.engineerTag;
        grabPos = GameObject.FindWithTag(engineerGrabPos).transform; //Assign Transform of child gameobject located on the hand of the Engineer Robot
    }

    private void Update()
    {
        if (isCarried)
        {
            transform.position = Vector3.Lerp(transform.position, grabPos.position, 1.2f);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == engineerTag)
        {
            engineer.boxToCarry = this.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == engineerTag)
        {
            engineer.boxToCarry = null;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == switchTag)
        {
            mat.SetColor("_EmissionColor", Color.green);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == switchTag)
        {
            mat.SetColor("_EmissionColor", Color.red);
        }
    }

    public void OnCarry()
    {
        isCarried = true;
        rb.useGravity = false;
        rb.isKinematic = true;
        transform.position = grabPos.transform.position;
        transform.SetParent(grabPos.transform);
        GetComponent<BoxCollider>().enabled = false;
    }

    public void OnRelease()
    {
        isCarried = false;
        rb.useGravity = true;
        rb.isKinematic = false;
        //rb.constraints = RigidbodyConstraints.FreezeRotation;
        //| RigidbodyConstraints.FreezePositionX
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        Invoke("ResetConstraints", constraintResetTimer);
        transform.SetParent(null); 
        engineer.boxToCarry = null;
        GetComponent<BoxCollider>().enabled = true;
    }

    private void ResetConstraints()
    {
        rb.constraints = RigidbodyConstraints.None;
    }
}
