using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheBehaviour : MonoBehaviour
{
    public Animator playerAnim;
   // public GameObject hitMarker;
   // public AudioSource soundSource;
    public float swingRate;
    public bool _canSwing;


    public int comboCount;
    private bool isAttacking;
    private int attackIndex;

    public float comboTimeWindow = 1.5f;
    private float lastAttackTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }


    }

    public void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            lastAttackTime = Time.time;
            comboCount = 1;
            attackIndex = 1;
        }
        else if (Time.time - lastAttackTime < comboTimeWindow)
        {
            comboCount++;
            if (comboCount == 2)
            {
                attackIndex = 2;
            }
        }
        else
        {
            comboCount = 1;
            attackIndex = 1;
        }

        // Call the appropriate attack function based on the attackIndex
        if (attackIndex == 1) // hit1
        {
            playerAnim.Play("hit1");
          //  soundSource.Play();
          
        }
        else if (attackIndex == 2) // hit2
        {
            playerAnim.Play("hit2");
           // soundSource.Play();
            
        }

    }

    public void EndAttack()
    {
        playerAnim.SetBool("Return", true);
        isAttacking = false;
        comboCount = 0;
        attackIndex = 0;
    }
}
