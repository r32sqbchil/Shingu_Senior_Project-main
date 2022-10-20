using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SET________TIMESCALE : Singleton<SET________TIMESCALE>
{
    //public bool[] selectSpeed;


    void Start()
    {
        //selectSpeed = new bool[4];

        //for(int i = 0; i < selectSpeed.Length; i++)
        //{
        //    selectSpeed[i] = false;
        //}

        //// 안하면 default가 timeScale 0.5f가 된다.
        //selectSpeed[3] = true;

    }

    //private int applySpeed;
    private void Update()
    {
        //for (int i = 0; i < selectSpeed.Length; i++)
        //{
        //    if(selectSpeed[i] == true)
        //    {
        //        applySpeed = i;
        //    }
        //}

        //for (int i = 0; i < selectSpeed.Length; i++)
        //{
        //    selectSpeed[i] = false;
        //}

        //switch(applySpeed)
        //{
        //    case 0:
        //        Time.timeScale = 0.5f;
        //        break;
        //    case 1:
        //        Time.timeScale = 1f;
        //        break;
        //    case 2:
        //        Time.timeScale = 2f;
        //        break;
        //    case 3:
        //        Time.timeScale = 3f;
        //        break;
        //}

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = 0.5f;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Time.timeScale = 1f;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            Time.timeScale = 2f;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            Time.timeScale = 3f;
        }
    }
}
