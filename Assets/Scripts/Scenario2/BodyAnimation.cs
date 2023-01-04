using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetBadMoodAnimation(string triggerName) 
    {
        animator.SetTrigger(triggerName);

        if (triggerName == null)
        {
            int rand = Random.Range(1, 4);
            switch (rand)
            {
                case 1:
                    animator.SetTrigger("BadMood1");
                    break;
                case 2:
                    animator.SetTrigger("BadMood2");
                    break;
                case 3:
                    animator.SetTrigger("BadMood3");
                    break;
                default:
                    break;
            }
        }
    }
    public void SetGoodMoodAnimation(string triggerName)
    {
        animator.SetTrigger(triggerName);

        if (triggerName == null)
        {
            int rand = Random.Range(1, 4);
            switch (rand)
            {
                case 1:
                    animator.SetTrigger("GoodMood1");
                    break;
                case 2:
                    animator.SetTrigger("GoodMood2");
                    break;
                case 3:
                    animator.SetTrigger("GoodMood3");
                    break;
                default:
                    break;
            }
        }
    }
    public void SetNeutrallAnimation(string triggerName)
    {
        animator.SetTrigger(triggerName);

        if (triggerName==null)
        {
            int rand = Random.Range(1, 4);
            switch (rand)
            {
                case 1:
                    animator.SetTrigger("NeutralMood1");
                    break;
                case 2:
                    animator.SetTrigger("NeutralMood2");
                    break;
                case 3:
                    animator.SetTrigger("NeutralMood3");
                    break;
                default:
                    break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
