using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Temp_LoadNicknameData : MonoBehaviour
{
    public TMP_Text text;
    private void Start()
    {
        text.text = Data.Instance.nicknameBasket.ToString();
    }
}
