using System;
using Tweens;
using Unity.VisualScripting;
using UnityEngine;

public class WaterLevels : MonoBehaviour
{
    // private Vector3 moveDir;
    // public Transform waterPlane;
    public float targetY;
    public float dur = 5f;

    public bool isMove = false;
    public bool isLvl2 = false;
    public bool isLvl3 = false;
    public bool isLvl4 = false;
    // private var tween = new PositionTween{};
    public bool isPaused;

    private TweenInstance inst = null;
    public static WaterLevels Instance;
    [SerializeField] private HUD hud;
    private void Awake()
    {
        Instance = this;
    }

    // public Plane Water;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Water = GetComponent<Plane>();
        isMove = false;
        isLvl2 = false;
        isLvl3 = false;
        isLvl4 = false;
        isPaused = false;
        targetY = 1f;

    }

    // Update is called once per frame
    void Update()
    {
        if (isMove && inst == null)
        {
            inst = moveToLvl(targetY, dur);
            Debug.Log(targetY);
        }
        else if (isLvl2)
        {
            targetY = 2.5f;
            dur = 10f;
        }
        else if (isLvl3)
        {
            targetY = 5f;
            dur = 7f;
        }
        else if (isLvl4)
        {
            targetY = 6.5f;
            dur = 5f;
        }

        if (transform.position.y < targetY && inst != null &&  inst.isPaused && isMove)
        {
            isPaused = false;
            inst = moveToLvl(targetY, dur);
            //Debug.Log(targetY);
        }
        
        if (isPaused)
        {
            inst.isPaused = true;
        }
        


    }

    TweenInstance moveToLvl(float y, float dur = 5)
    {
        var tween = new PositionTween
        {
            to = new Vector3(transform.position.x, y, transform.position.z),
            duration = dur,
            onEnd = (instance) =>
            {
                isPaused = true;
            },
        };
        TweenInstance inst = gameObject.AddTween(tween);
        return inst;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHead"))
        {
            hud.gameEnd(false);
        }
    }
}
