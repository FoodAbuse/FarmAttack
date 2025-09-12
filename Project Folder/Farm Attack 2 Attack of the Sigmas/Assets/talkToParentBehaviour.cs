using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talkToParentBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DoHop()
    {
        transform.GetComponentInParent<MarshmellowAIBehaviour>().doHop = true;
    }
    public void StopHop()
    {
        transform.GetComponentInParent<MarshmellowAIBehaviour>().doHop = false;
    }
    public void CrabSlam()
    {
        transform.GetComponentInParent<crabEnemyBehaviour>().startSlam_ = true;
        FindObjectOfType<cameraShakeBehaviour>().ShakeCamera(.25f);
    }
}
