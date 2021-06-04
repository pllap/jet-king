using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CshGameManager : MonoBehaviour
{
    private static CshGameManager _instance;

    [HideInInspector] public GameObject player;
    [HideInInspector] public float fuelCapacity = 2;
    [HideInInspector] public float fuel = 2;
    [HideInInspector] public bool thrustable;
    [HideInInspector] public bool isThrusting;

    public Image fuelImage;
    [HideInInspector] public GameObject canvasResetButton;
    [HideInInspector] public GameObject canvasEndingScreen;

    [HideInInspector] public bool isGameEnd = false;

    public static CshGameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(CshGameManager)) as CshGameManager;

                if (_instance == null)
                {
                    Debug.Log("Singleton 객체가 없습니다.");
                }
            }

            return _instance;
        }
    }
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        // 객체가 이미 존재하는 경우 새로 생긴 객체를 삭제합니다.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // Scene이 변경되어도 객체가 삭제되지 않도록 합니다.
        DontDestroyOnLoad(gameObject);

        this.canvasResetButton = GameObject.Find("CanvasResetButton");
        this.canvasEndingScreen = GameObject.Find("CanvasEndingScreen");
        this.canvasEndingScreen.gameObject.SetActive(false);
        
        this.player = GameObject.Find("Player");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        thrustable = true;
    }

    void FixedUpdate()
    {
        if (!isGameEnd)
        {
            calculateFuel();
        }
    }

    private void calculateFuel()
    {
        // 연료 감소
        if (isThrusting)
        {
            fuel -= 0.01f;
            if (fuel < 0)
            {
                fuel = 0;
                thrustable = false;
            }
        }
        // 연료 회복
        else
        {
            fuel += 0.02f;
            thrustable = true;
            if (fuel > fuelCapacity)
            {
                fuel = fuelCapacity;
            }
        }

        fuelImage.fillAmount = fuel / fuelCapacity;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SampleScene")
        {
            isGameEnd = false;
            fuelImage = GameObject.Find("Player").GetComponentsInChildren<Image>()[1];
            Debug.Log(fuelImage);
        }
    }

    public void endGame()
    {
        this.player.gameObject.transform.position = new Vector3(0, 0, 0);
        SceneManager.LoadScene("Title");
    }
}
