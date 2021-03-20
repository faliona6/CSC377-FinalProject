using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR;
using Valve.VR.InteractionSystem;

public class Gun : MonoBehaviour
{
    public SteamVR_Action_Boolean fireAction;
    //public ParticleSystem muzzleFlash;
    public Transform bulletShooter;

    private SimpleShoot simpleShoot;

    private Interactable interactable;

    public LineRenderer laser;
    public GameObject dot;

    public GameObject hitEnvironmentParticles, hitPlayerParticles;

    private bool isShooting = false;

    public AudioSource gunShotSfx;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        simpleShoot = GetComponentInChildren<SimpleShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        // If gun is grabbed
        if (interactable.attachedToHand != null)
        {
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;
            if (!isShooting && fireAction[source].stateDown)
            {
                FireBullet();
            }
            ShowLaser();
        }
        else
        {
            dot.SetActive(false);
            laser.gameObject.SetActive(false);
        }
    }

    private void ShowLaser()
    {
        laser.gameObject.SetActive(true);

        // Shoot raycast
        RaycastHit hit;
        if (Physics.Raycast(bulletShooter.position, bulletShooter.TransformDirection(Vector3.forward), out hit, 200f))
        {
            laser.SetPosition(0, bulletShooter.position);
            laser.SetPosition(1, hit.point);
            dot.SetActive(true);
            dot.transform.position = hit.point;
        }
        else
        {
            dot.SetActive(false);
            laser.SetPosition(0, bulletShooter.position);
            laser.SetPosition(1, bulletShooter.TransformDirection(Vector3.forward) * 100f);
        }
    }

    private void FireBullet()
    {
        Debug.Log("Shooting Gun");
        gunShotSfx.Play();
        //muzzleFlash.Play();
        //StartCoroutine(StopParticles());
        simpleShoot.Shoot();
        simpleShoot.gunAnimator.SetTrigger("Fire");

        // Shoot raycast
        RaycastHit hit;
        Debug.DrawRay(bulletShooter.position, bulletShooter.forward, Color.red, 3f);
        
        if (Physics.Raycast(bulletShooter.position, bulletShooter.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            GameObject particles = hitEnvironmentParticles;
            if (hit.transform.CompareTag("NPC"))
            {
                Debug.Log("Hit NPC");
                HandleNPCHit(hit.transform);
                particles = hitPlayerParticles;
            }
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("Hit Player");
                HandlePlayerHit(hit.transform);
                particles = hitPlayerParticles;
            }

            GameObject impactGO = Instantiate(particles, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }

    private IEnumerator stopShooting()
    {
        isShooting = true;
        yield return new WaitForSeconds(0.1f);
        isShooting = false;
    }

    private void HandleNPCHit(Transform npc)
    {
        GameLoopManager.Instance.player.changeHealth(-1);
    }

    private void HandlePlayerHit(Transform player)
    {
        player.Find("Mesh_LOD").gameObject.SetActive(false);
        GameLoopManager.Instance.PlayerHit();
    }

    IEnumerator StopParticles()
    {
        yield return new WaitForSeconds(0.2f);
        //muzzleFlash.Stop();
    }
}
