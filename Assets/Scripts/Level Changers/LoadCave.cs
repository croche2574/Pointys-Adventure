using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCave : MonoBehaviour
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
            SceneManager.LoadScene("Cave");
            col.gameObject.transform.position = new Vector3(10, 0.5f, 1.5f);
            col.gameObject.transform.rotation = Quaternion.identity;
        }
    }
}
