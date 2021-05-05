using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTown : MonoBehaviour
{
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
            SceneManager.LoadScene("Textured Town");
            col.gameObject.transform.position = new Vector3(12.5f, 15, -20);
            col.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
        }
    }
}
