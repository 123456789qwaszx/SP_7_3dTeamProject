using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
   
    private bool[] QC = new bool[10];
    public int i = 0;

    public void QuestCondition1() //Q1
    { 
        if(i < 1)
        {
            QC[0] = true;
        }
        else
        {
            return;
        }
    }
    public void QuestCondition2() //Q2
    {
        if (i < 2)
        {
            QC[1] = true;
        }
        else
        {
            return;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
