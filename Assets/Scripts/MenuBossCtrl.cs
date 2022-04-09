using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBossCtrl : MonoBehaviour
{
    Vector3 rot = Vector3.zero;
    float rotSpeed = 40f;
    Animator anim;
    public float SphereAttackSpeed = 100f;
    public float SphereWalkSpeed = 20f;

    public float awarenessRange = 50f;
    public float attackRange = 30f;
    public float attackpoint = 15f;

    public Transform playerTransform;
    AudioSource audio;

    public bool attackingplayer = true;
    public bool setplayerloc = true;
    public bool canbeshot = false;
    bool returnpos, qwerty;
    public GameObject oposGO, noposGO;
    Vector3 ppos;

    // Use this for initialization
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        gameObject.transform.eulerAngles = rot;
    }
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        audio = GetComponent<AudioSource>();

        InvokeRepeating("OpenShell", 0f, 10f);
        InvokeRepeating("CloseShell", 5f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    
}
