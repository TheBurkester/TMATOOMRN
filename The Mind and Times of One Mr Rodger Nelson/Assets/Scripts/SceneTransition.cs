using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransition : MonoBehaviour
{
    public Animator animationController;
    public GameObject mainText;
    public GameObject startButton;

    private float animCounter = 0.0f;
    private float animMax = 1.5f;
    private bool animGo = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(animGo == true)
        {
            animCounter += Time.deltaTime;
        }
        if (animCounter >= animMax)
        {
            SceneManager.LoadScene(1);
        }
    }
    public void MainScene()
    {
        animationController.SetBool("CanAnimate", true);
        startButton.SetActive(false);
        mainText.SetActive(false);
        animGo = true;

    }
}
