using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temp_UI : MonoBehaviour
{
    public Button Button_CancelNickname;

    public InputField InputField_Nickname;

    private void Start()
    {
        Button_CancelNickname.onClick.AddListener(OnCancelNickname);
    }
    private void OnCancelNickname()
    {
        InputField_Nickname.text = "";
    }
}