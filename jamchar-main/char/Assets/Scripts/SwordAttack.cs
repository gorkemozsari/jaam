using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Animator anim;
    
    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    private static int noOfClicks = 0;
    float lastClickTime = 0;
    float maxComboDelay = 1;

    void Start() {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        /* if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8 && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            anim.SetBool("hit1", false);
        }
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8 && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            anim.SetBool("hit2", false);
        }
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8 && anim.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
        {
            anim.SetBool("hit3", false);
            noOfClicks = 0;
        }

        if(Time.time - lastClickTime > maxComboDelay)
        {
            noOfClicks = 0;
        }
        
        if(Time.time > nextFireTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnClick();
            }
        } */
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
    }

    void OnClick()
    {
        lastClickTime = Time.time;
        noOfClicks++;
        if(noOfClicks == 1)
        {
            anim.SetBool("hit1", true);
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

        if(noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.4 && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            anim.SetBool("hit1", false);
            anim.SetBool("hit2", true);
        }
        if(noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.4 && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            anim.SetBool("hit2", false);
            anim.SetBool("hit3", true);
        }
    }
}
