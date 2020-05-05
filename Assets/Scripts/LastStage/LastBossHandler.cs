using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//by Sidakpreet Singh

public class LastBossHandler : MonoBehaviour
{
    [SerializeField] private Transform canon;
    [SerializeField] private Transform turret;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject dieExplosion;
    [SerializeField] private GameObject dieWreck;
    private Vector3 aimTurret;
    private Vector3 smoothRotate;
    public int shoots;
    public int defaultShoots = 3;
    public int reloadTime = 3;
    public float shootTime = 3f;
    public float rotSpeed = 5f;
    public bool targetAcquired = false;
    private RaycastHit hit;
    private int healthBar = 6;
    private Vector3 hitpoint;
    public float rotX;
    //public float rotY;
    //public float rotZ;
    private float counter;
    private bool targetLocked;
    private bool reloadAmmo;

    public string name;
    [SerializeField] private Text healthBarTxt;
    [SerializeField] private Text ammoBarTxt;
    [SerializeField] private Text nameTxt;
    [SerializeField] private Text targetTxt;

    // Start is called before the first frame update
    void Start()
    {
        targetLocked = false;
        shoots = 0;
        nameTxt.text = name;
        ammoBarTxt.text = "";
        UpdateHealthBar();
    }

    private void Update()
    {
        if (targetAcquired)
        {
            Target();
            Aim();

            if (Time.time - counter > shootTime)
            {
                Shoot();
            }
        }

    }

    public void Begin()
    {
        reloadAmmo = true;
        Shoot();
        targetAcquired = true;
    }

    private void Target()
    {
        if (turret)
        {
            aimTurret = target.position - turret.position;
            aimTurret.y = 90f;
            smoothRotate = Vector3.Lerp(smoothRotate, aimTurret, Time.deltaTime * rotSpeed);
            turret.rotation = Quaternion.LookRotation(smoothRotate * rotX);
        }
    }


    internal void GetHit()
    {
        healthBar--;
        UpdateHealthBar();

        if (healthBar <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(dieExplosion, transform.position, Quaternion.identity);
        Instantiate(dieWreck, transform.position, Quaternion.identity);
        FindObjectOfType<MechHandler>().GetComponent<MechHandler>().enemies--;
        Destroy(gameObject);
    }

    void UpdateAmmoBar()
    {
        ammoBarTxt.text = "";
        ammoBarTxt.color = Color.green;
        for (int i = 0; i < shoots; i++)
        {
            ammoBarTxt.text += "|";
        }
    }

    void UpdateHealthBar()
    {
        healthBarTxt.text = "";
        for (int i = 0; i < healthBar; i++)
        {
            healthBarTxt.text += "|";
        }

        if (healthBar < 2)
        {
            healthBarTxt.color = Color.black;
        }
    }

    private void Aim()
    {
        Debug.DrawRay(canon.position, canon.TransformDirection(Vector3.forward) * 50f, Color.black, 1.5f);

        if (Physics.Raycast(canon.position, canon.TransformDirection(Vector3.forward), out hit, 50f))
        {

            if (hit.transform.tag == "Player")
            {
                Debug.DrawRay(canon.position, canon.TransformDirection(Vector3.forward) * 50f, Color.green, 1.5f);
                targetTxt.color = Color.green;
                targetTxt.text = "Target Locked";
            }
            else
            {
                targetTxt.color = Color.red;
                targetTxt.text = "no target";
            }
        }
    }

    void LockTarget()
    {
        if (Physics.Raycast(canon.position, canon.TransformDirection(Vector3.forward), out hit, 50f))
        {
            if (hit.transform.tag == "Player")
            {
                targetLocked = true;
                hitpoint = hit.point;
            }
            else
            {
                targetLocked = false;
            }
        }
    }

    void Shoot()
    {
        counter = Time.time;

        if ((shoots > 0) && (!reloadAmmo))
        {
            shoots--;
            UpdateAmmoBar();
            LockTarget();
            Instantiate(explosion, hitpoint, Quaternion.identity);

            if (targetLocked)
            {
                FindObjectOfType<MechHandler>().GetComponent<MechHandler>().GetHit();
            }

            if (shoots <= 0) reloadAmmo = true;
        }

        if (reloadAmmo)
        {
            StartCoroutine(Reload(reloadTime));
            Invoke("ResetAmmo", reloadTime);
        }
    }

    void ResetAmmo()
    {
        shoots = defaultShoots;
        UpdateAmmoBar();
        targetAcquired = true;
    }

    IEnumerator Reload(float time)
    {
        reloadAmmo = false;
        targetAcquired = false;
        float timer = 0;

        ammoBarTxt.text = "";
        ammoBarTxt.color = Color.blue;

        while (timer < time)
        {
            ammoBarTxt.text += ".";
            yield return new WaitForSeconds(1f);
            timer++;
        }

        yield return null;
        StopCoroutine(Reload(time));
    }
}
