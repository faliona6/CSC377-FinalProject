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

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        simpleShoot = GetComponentInChildren<SimpleShoot>();
    }

    // Update is called once per frame
    void Update()
    {

        if (fireAction.stateDown)
        {
            Debug.Log("shooting");
        }
        // If gun is grabbed
        if (interactable.attachedToHand != null)
        {
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;
            if (fireAction[source].stateDown)
            {
                FireBullet();
            }
        }
    }

    private void FireBullet()
    {
        Debug.Log("Shooting Gun");
        //muzzleFlash.Play();
        //StartCoroutine(StopParticles());
        simpleShoot.Shoot();
        simpleShoot.gunAnimator.SetTrigger("Fire");

        // Shoot raycast
        RaycastHit hit;
        Debug.DrawRay(bulletShooter.position, bulletShooter.forward, Color.red, 3f);
        
        if (Physics.Raycast(bulletShooter.position, bulletShooter.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag("NPC"))
            {
                Debug.Log("Hit NPC");
                HandleNPCHit(hit.transform);
            }
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("Hit Player");
                HandlePlayerHit(hit.transform);
            }
        }
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
