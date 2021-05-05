using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadForest : MonoBehaviour
{
    public bool entrance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Textured Forest");
            if (entrance)
            {
                col.gameObject.transform.position = new Vector3(2.75f, 15, -7);
                col.gameObject.transform.rotation = Quaternion.identity;
            }
            else
            {
                col.gameObject.transform.position = new Vector3(2.75f, 15, 107);
                col.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
        }
    }
}
