using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    Overit3D overit3D = null;
    public GameObject panel = null;

    private void Awake()
    {
        overit3D = GameObject.FindGameObjectWithTag("Player").GetComponent<Overit3D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panel.SetActive(!panel.activeSelf);
            overit3D.ControlOnOff(!panel.activeSelf);
            //print(panel.activeSelf.ToString());
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

	public void Stuck()
    {
        overit3D.ResetToastPosition();
    }
}
