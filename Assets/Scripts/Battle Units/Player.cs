using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Unit
{
    //Inventory and equipment
    public List<GameObject> inventory = new List<GameObject>();

    //UI things
    DialogueHandler speaking;
    InventoryHandler backpack;

    //diable movement if interacting
    public bool talking = false;
    bool invCheck = false;
    public bool battling = false;
    public string mobName;
    public int mobLevel;
    public Camera cam;
    public string lastScene;
    private float speed = 10f;
    private float rSpeed = 2f;
    public bool wonBattle = false;

    //Animation timer
    float animTimer = 0;
    AudioSource noise;

    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        anim = this.gameObject.GetComponent<Animator>();
        rig = this.gameObject.GetComponent<Rigidbody>();
        speaking = this.gameObject.GetComponentInChildren<DialogueHandler>();
        backpack = this.gameObject.GetComponentInChildren<InventoryHandler>();
        noise = this.gameObject.GetComponent<AudioSource>();
    
        Stats s = gameObject.AddComponent<Stats>();
        s[StatTypes.LVL] = 1;
        GameObject jPrefab = Resources.Load<GameObject>("Jobs/Pointy");
        GameObject jInstance = Instantiate(jPrefab) as GameObject;
        jInstance.transform.SetParent(gameObject.transform);
        Job job = jInstance.GetComponent<Job>();
        job.Employ();
        job.LoadDefaultStats();
        gameObject.AddComponent<Status>();
        ExpHandler exp = gameObject.AddComponent<ExpHandler>();
        exp.Init(level);
        gameObject.AddComponent<Health>();
        gameObject.AddComponent<ActionPoints>();
    }

    // Update is called once per frame
    void Update()
    {
        animTimer -= Time.deltaTime;
        if (SceneManager.GetActiveScene().name != "BattleScene")
        {
            if (!talking && !invCheck && !battling)
            {
                Vector3 v = transform.forward*speed*Input.GetAxis("Vertical");
                v.y = rig.velocity.y;
                rig.velocity = v;
                if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0) 
                {
                    anim.SetFloat("walk", 1);
                } else {anim.SetFloat("walk", 0);}
                rig.angularVelocity = new Vector3(0.0f, Input.GetAxis("Horizontal") * rSpeed, 0.0f);
                
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //Debug.Log("I'll try to interact with something!");
                    Interact();
                    rig.angularVelocity = Vector3.zero;
                }
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    //Debug.Log("I'll try to open my inventory!");
                    Inventory();
                    rig.angularVelocity = Vector3.zero;
                }
            }
            else if (invCheck)
            {
                rig.velocity = Vector3.zero;
                anim.SetFloat("walk", 0);
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    //Debug.Log("I'll try to close my inventory!");
                    Inventory();
                    rig.angularVelocity = Vector3.zero;
                }
            }
            else if (talking)
            {
                rig.velocity = Vector3.zero;
                rig.angularVelocity = Vector3.zero;
                anim.SetFloat("walk", 0);
            }
            else 
            {
                rig.velocity = Vector3.zero;
                rig.angularVelocity = Vector3.zero;
                anim.SetFloat("walk", 0);
                anim.SetFloat("interact", 0);
            }
        }
        else
        {
            rig.velocity = Vector3.zero;
            rig.angularVelocity = Vector3.zero;
            anim.SetFloat("walk", 0);
        }
        if(animTimer <= 0)
        {
            anim.SetFloat("attack", 0);
        }
        if(wonBattle && !battling)
        {
            wonBattle = false;
        }
    }

    public void AttackAnim()
    {
        anim.SetFloat("attack", 1);
        animTimer = 0.5f;
        noise.Play();
    }

    public void Interact()
    {
        Collider[] interactive = Physics.OverlapSphere(this.transform.position, 4);
        foreach(var thing in interactive)
        {
            if(thing.gameObject.GetComponent<Interactable>() != null)
            {
                //chest
                if(thing.gameObject.GetComponent<Chest>() != null)
                {
                    Chest chest = thing.GetComponent<Chest>();
                    speaking.PresentDialogue(chest.Interact(this));
                    talking = true;
                    anim.SetFloat("interact", 1);
                }
                //NPC
                if(thing.gameObject.GetComponent<NPC>() != null)
                {
                    NPC npc = thing.GetComponent<NPC>();
                    speaking.PresentDialogue(npc.Interact(this));
                    talking = true;
                }
            }
        }
    }

    public void Inventory()
    {
        //Debug.Log("There should be " + inventory.Count + " items in here!");
        backpack.PresentInventory();
        invCheck = !invCheck;
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Monster")
        {
            if (invCheck)
            {
                backpack.PresentInventory();
                invCheck = !invCheck;
            }
            rig.angularVelocity = Vector3.zero;
            if (wonBattle)
            {
                Debug.Log("won battle");
                Destroy(col.gameObject);
                col.GetComponent<Enemy>().Defeated();
                wonBattle = false;
            }
            else
            {
                mobName = col.GetComponent<Enemy>().EnemyName;
                mobLevel = col.GetComponent<Stats>()[StatTypes.LVL];
                BattleData.StartLoc = transform.position;
                //Debug.Log("Stored position: " + transform.position);
                BattleData.StartRot = transform.rotation;
                takeTurn = true;
                StartBattle();
            }
        }
    }

    void StartBattle()
    {
        lastScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("BattleScene");
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
