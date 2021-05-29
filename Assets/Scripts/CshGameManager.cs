using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CshGameManager : MonoBehaviour
{
    private static CshGameManager _instance;

    public float fuelCapacity;
    public float fuel;
    [HideInInspector] public bool thrustable;
    [HideInInspector] public bool isThrusting;

    public Image fuelImage;

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
    }
    
    // Start is called before the first frame update
    void Start()
    {
        thrustable = true;
    }

    void FixedUpdate()
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
}
