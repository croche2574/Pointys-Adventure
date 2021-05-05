using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public string EnemyName;

    private static int EnemyID = 0;
    public int thisID = -1;
    public static Dictionary<int, bool> enemiesDefeated;
    public List<Skill> availableSkills;
    AudioSource noise;

    private void Reset()
    {
        thisID = EnemyID;
        EnemyID++;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        rig = this.gameObject.GetComponent<Rigidbody>();
        Stats s = gameObject.AddComponent<Stats>();
        GameObject jPrefab = Resources.Load<GameObject>("Jobs/" + EnemyName);
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
        noise = this.gameObject.GetComponent<AudioSource>();
        if (enemiesDefeated.ContainsKey(thisID))
        {
            if (enemiesDefeated[thisID])
            {
                Destroy(this.gameObject);
            }
            else
            {
                enemiesDefeated.Add(thisID, false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("attack", 0);
    }

    public void AttackAnim()
    {
        anim.SetFloat("attack", 1);
        noise.Play();
    }

    private void Awake()
    {
        if(enemiesDefeated == null)
        {
            enemiesDefeated = new Dictionary<int, bool>();
        }
    }

    public void Defeated()
    {
        enemiesDefeated[thisID] = true;
    }
}
