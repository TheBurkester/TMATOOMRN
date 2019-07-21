using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int enemyHealth;
    public bool ranged;
    // Start is called before the first frame update
    void Start()
    {
        if(ranged == true)
        {
            enemyHealth = 3;
        }
        else
        {
            enemyHealth = 4;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void rangedBehaviour()
    {

    }
    private void meleeBehaviour()
    {

    }
}
