using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("---MAIN OBJECTS")]
    [SerializeField] private GameObject Platform;
    [SerializeField] private GameObject Rim;
    [SerializeField] private GameObject RimBigger;
    [SerializeField] private GameObject[] SpeciesSpawnPoints;
    [SerializeField] private AudioSource[] Audios;
    [SerializeField] private ParticleSystem[] Effects;
    SceneManager scene;

    [Header("---UI OBJECTS")]
    [SerializeField] private Image[] missionDuties;
    [SerializeField] private Sprite missionOkDuties;
    [SerializeField] private int pointBall;
    [SerializeField] private GameObject[] Panels;
    [SerializeField] private TextMeshProUGUI LevelName;
    int basketCounter;
    float fingerPosX;


    void Start() 
    {
        LevelName.text = "LEVEL : "+ SceneManager.GetActiveScene().name;
        BallControl();
        Invoke("SpeciesSpawn", 3);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

    }

    void SpeciesSpawn()
    {

        int randomNumber = Random.Range(0, SpeciesSpawnPoints.Length-1);
        RimBigger.transform.position = SpeciesSpawnPoints[randomNumber].transform.position;
        RimBigger.SetActive(true);
    }

    void Movement()
    {
        if (Time.timeScale != 0)
        {
            if(Input.touchCount>0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 TouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));

                switch(touch.phase)
                {
                    case TouchPhase.Began:
                        fingerPosX = TouchPosition.x - Platform.transform.position.x;
                        break;
                    case TouchPhase.Moved:
                        if(TouchPosition.x - fingerPosX > -4.6 && TouchPosition.x - fingerPosX < 4.6)
                        {
                            Platform.transform.position = Vector3.Lerp(Platform.transform.position, new Vector3(TouchPosition.x - fingerPosX - .6f, Platform.transform.position.y, Platform.transform.position.z), 5f);

                        }
                        break;
                }
            }


            if (Platform.transform.position.x > -1.75 )
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    Platform.transform.position = Vector3.Lerp(Platform.transform.position, new Vector3(Platform.transform.position.x - .6f, Platform.transform.position.y, Platform.transform.position.z), 0.1f);
                }
            }
            if (Platform.transform.position.x < 1.75)
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    Platform.transform.position = Vector3.Lerp(Platform.transform.position, new Vector3(Platform.transform.position.x + .6f, Platform.transform.position.y, Platform.transform.position.z), 0.1f);
                }
            }
        }
    }

    void BallControl()
    {
        for (int i = 0; i < pointBall; i++)
        {
            missionDuties[i].gameObject.SetActive(true);
        }
        
    }

    public void Basket(Vector3 Position)
    {
        
        basketCounter++;
        missionDuties[basketCounter - 1].sprite = missionOkDuties;
        Effects[0].transform.position = Position;
        Effects[0].gameObject.SetActive(true);
        
        Audios[1].Play();

        if (basketCounter == pointBall)
        {
            Win();
        }
    }

    void Win()
    {
        Audios[2].Play();
        Panels[1].SetActive(true);
        PlayerPrefs.SetInt("LEVEL", PlayerPrefs.GetInt("LEVEL")+1);
        Time.timeScale = 0;
    }

    public void Lose()
    {
        Audios[3].Play();
        Panels[2].SetActive(true);
        Time.timeScale = 0;
    }

    public void ButtonControl(string Value)
    {
        switch (Value)
        {
            case "Pause":
                Time.timeScale = 0;
                Panels[0].SetActive(true);
                break;
            case "Resume":
                Time.timeScale = 1;
                Panels[0].SetActive(false);
                break;
            case "Again":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
                Time.timeScale = 1;
                break;
            case "Next":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
                break;
            case "Settings":
                break;
            case "Quit":
                Application.Quit();
                break;


        }
    }


    public void MakeBasketBigger(Vector3 position)
    {
        Effects[1].transform.position = position;
        Effects[1].gameObject.SetActive(true);
        Audios[0].Play();
        Rim.transform.localScale = new Vector3(55f, 55f, 55f);
    }



}
