using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Data : Singleton<Data>
{
    public string nicknameBasket;
    //public Text nicknameField;
    //public TextMeshProUGUI nicknameShow;

    public AudioSource mainAudio;
    public AudioSource sfx;

    // audio
    public AudioClip titleClip;
    public AudioClip worldmapClip;
    public AudioClip selectunitClip;
    public AudioClip defenseClip;
    public AudioClip storeClip;
    public AudioClip gameclearClip;
    public AudioClip gameoverClip;

    public bool[] isStage;

    // sfx
    public AudioClip btnClip;
    public AudioClip scenechangeClip;

    //?߰?
    public bool isLogIn = false;

    public int[] temp_resourceValue;
}