using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadRoad : MonoBehaviour
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
        if (col.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Textured Road");
            if (entrance)
            {
                col.gameObject.transform.position = new Vector3(10, 15, 5);
                col.gameObject.transform.rotation = Quaternion.identity;
            }
            else
            {
                col.gameObject.transform.position = new Vector3(20, 11, 68);
                col.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
        }
    }
}
