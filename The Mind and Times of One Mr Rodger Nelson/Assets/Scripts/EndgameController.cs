using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndgameController : MonoBehaviour
{
    [HideInInspector]
    public int enemiesLeft = 0;

    private float endCounter;
    private float endMax = 2.0f;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesLeft == 0)
        {
            SceneManager.LoadScene(2);
        }
    }
}
