using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    public Button playButton;
    public Button optionButton;
    public Button quitButton;

    public GameObject loadingPannel;
    private int index;
    // Update is called once per frame
    private void Start()
    {
        
    }
    void Update()
    {
        playButton.onClick.AddListener(() => SetFunction(0));
        quitButton.onClick.AddListener(() => SetFunction(2));
    }

    void SetFunction(int index)
    {
        switch(index)
        {
            case 0:
                {
                    SceneManager.LoadScene(1);
                    loadingPannel.SetActive(true);
                    break;
                }
            case 1:
                {
                    //Hien thi Slider am thanh background
                    break;
                }
            case 2:
                {
                    Application.Quit();
                    break;
                }

        }
    }
}
