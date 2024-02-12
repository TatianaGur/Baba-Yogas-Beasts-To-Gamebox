using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsMoveAnim : MonoBehaviour
{
    private Animator anim;
    private bool isRelaxing;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        isRelaxing = GetComponent<PointsMoving>().isRelaxing;

        AnimationGoing();
    }

    private void AnimationGoing()
    {
        if (isRelaxing)
        {
            anim.SetBool("Going", false);
        }
        else
        {
            anim.SetBool("Going", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bed"))
        {
            anim.SetTrigger("Sleeping");
        }
        else if (coll.gameObject.CompareTag("Bottle"))
        {
            anim.SetTrigger("Drinking");
        }
        else if (coll.gameObject.CompareTag("Feeder"))
        {
            anim.SetTrigger("Eating");
        }
        else if (coll.gameObject.CompareTag("WC"))
        {
            anim.SetTrigger("Pee");
        }
    }
}
