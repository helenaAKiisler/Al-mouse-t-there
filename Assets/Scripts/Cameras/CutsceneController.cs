using UnityEngine;
using UnityEngine.Playables;

public class CutsceneController : MonoBehaviour
{
    PlayableDirector director;
    public bool isTunnelExit;
    private PlayerMovement player;
        
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerMovement>();
        director=GetComponent<PlayableDirector>();       
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isTunnelExit)
        {
            director.Play();
        }
        
        
    }
}
