using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text timerText = null;
    public float startTime = 120f;
    private float time;

    private Overit3D overit3D = null;

    private Rigidbody rb;

    bool reset = false;

    public GameObject explosion;

    public GameObject finish;

    public GameObject finishPanel;

    private bool finished = false;
    private float finishedTime = 0;

    public bool gameRuns = false;

    

    // Use this for initialization
    void Start () {
        overit3D = GetComponent<Overit3D>();
        time = startTime;
        rb = gameObject.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        if (gameRuns && overit3D.firstClick)
        {
            if (!finish.GetComponent<Trigger>().triggered)
            {
                if (time <= 0f)
                {
                    timerText.text = "0";

                    if (!reset)
                    {
                        reset = true;
                        StartCoroutine("Reset");
                    }


                } else
                {
                    time -= Time.deltaTime;
                    timerText.text = time.ToString("f0");
                }
            } else
            {
                if (!finished)
                {
                    finishedTime = time;
                    Win();
                    finished = true;
                }

            }
        } else
        {
            timerText.text = "Ready...";
        } 
	}

    IEnumerator Reset()
    {
        rb.isKinematic = true;
        Instantiate(explosion, transform.position, transform.rotation);
        MeshRenderer[] meshes = overit3D.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer mesh in meshes)
        {
            mesh.enabled = false;
        }
        yield return new WaitForSeconds(3);
        rb.isKinematic = false;
        overit3D.ResetToastPosition();
        foreach (MeshRenderer mesh in meshes)
        {
            mesh.enabled = true;
        }
        time = startTime;
        reset = false;
        yield return null;
    }

    public void StartGame()
    {
        gameRuns = true;
    }


    public void Win()
    {
        rb.isKinematic = true;
        finishPanel.SetActive(true);
    }

    public void Again()
    {
        rb.isKinematic = false;
        overit3D.ResetToastPosition();
        overit3D.firstClick = false;
        time = startTime;
        reset = false;
        finished = false;
        finish.GetComponent<Trigger>().triggered = false;
    }

    public void RageQuit()
    {
        Application.Quit();
    }

}
