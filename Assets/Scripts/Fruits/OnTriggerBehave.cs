using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class OnTriggerBehave : MonoBehaviour
{
    [SerializeField]
    Animator animator = null;
    [SerializeField]
    public int pairElment = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public bool CheckPairCollection(int pairNumb)
    {
        if(pairNumb == pairElment) return true;
        return false;
    }

    public void TurnOffAnimation()
    {
        animator.SetBool("taken", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("taken", true);
        FruitSingleton.instance.PutFruit(this);
    }
}
