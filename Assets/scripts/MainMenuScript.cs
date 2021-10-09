using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject ducky;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // continually look towards rubber ducky
        transform.LookAt(ducky.transform.localPosition - transform.position, Vector3.up);
    }

    public void LoadFlushed()
    {
        SceneManager.LoadSceneAsync("Flushed");
    }
}
