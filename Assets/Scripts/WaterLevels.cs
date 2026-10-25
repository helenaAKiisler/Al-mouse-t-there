using Tweens;
using Unity.VisualScripting;
using UnityEngine;

public class WaterLevels : MonoBehaviour
{
    private Vector3 moveDir;
    public Transform waterPlane;
    private float targetY;

    public bool isLvl1 = false;
    public bool isLvl2 = false;
    public bool isLvl3 = false;
    public bool isLvl4 = false;

    

    public Plane Water;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Water = GetComponent<Plane>();
        isLvl1 = false;
        isLvl2 = false;
        isLvl3 = false;
        isLvl4 = false;
        // var tween = new PositionYTween(to = Vector3(transform.position.x));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Vector3 moveToLvl(int lvl)
    // {
    //     if (lvl == 1)
    //     {
    //         moveDir = new Vector3();
    //     }
    // }
}
