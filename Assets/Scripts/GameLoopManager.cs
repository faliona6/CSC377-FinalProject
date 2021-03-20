using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.AI;


public class GameLoopManager : MonoBehaviour
{
    private static GameLoopManager _instance;
    public static GameLoopManager Instance { get { return _instance; } }

    public PlayerVR player;
    public Canvas winnersCanvas;
    public GameObject NPCPrefab;
    public GameObject PlayerPrefab;
    public int numNPCS = 30;

    private TextMeshProUGUI winnersText;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        player.deathEvent += () => OnPlayerVRDeath();
        winnersText = winnersCanvas.transform.Find("WinnerText").GetComponent<TextMeshProUGUI>();

        InitializeNPCs();
        InitializePlayer();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void InitializeNPCs()
    {
        for (int i = 0; i < numNPCS; i++)
        {
            Vector3 pos;
            if (RandomPoint(Vector3.zero, 100, out pos))
            {
                Debug.Log("hello");
                GameObject npc = Instantiate(NPCPrefab, pos, NPCPrefab.transform.rotation);
                Material m = npc.transform.GetComponentInChildren<SkinnedMeshRenderer>().materials[0];
                Vector3 c = getRandColor();
                m.SetColor("Albedo", new Color(c.x, c.y, c.z));
            }
        }
    }

    private void InitializePlayer()
    {
        Vector3 pos;
        if (RandomPoint(Vector3.zero, 100, out pos))
        {
            GameObject p = Instantiate(PlayerPrefab, pos, NPCPrefab.transform.rotation);
        }
    }

    private Vector3 getRandColor()
    {
        float r = UnityEngine.Random.Range(0.5f, 1.0f);
        float g = UnityEngine.Random.Range(0.5f, 1.0f);
        float b = UnityEngine.Random.Range(0.5f, 1.0f);

        return new Vector3(r, g, b);
    }

    public void PlayerHit()
    {
        winnersCanvas.gameObject.SetActive(true);
        winnersCanvas.enabled = true;
        winnersText.text = "VR Player Wins!";
    }

    private void OnPlayerVRDeath()
    {
        winnersCanvas.gameObject.SetActive(true);
        winnersCanvas.enabled = true;
        winnersText.text = "Keyboard Player Wins!";
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        while (true)
        {
            Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }




}
