using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIController : MonoBehaviour
{
    public GameObject[] page;

    public Image blur;
    public GameObject set;

    public Button[] btn;
    /*
    0. Set - esc
    1. Set - exit
    2. Create Pop-Up - Y Btn;
    3. 타이틀 SETTING
    4. 월드맵 set
    5~8. 입장버튼
    */

    public Animator animator;
    private int levelToLoad;

    // =================================== Temp
    private int maxNicknameLength = 8;
    private string[] blankTexts;

    public Text Text_Nickname;
    

    //추가
    public GameObject title;
    public GameObject worldMap;
    void Start()
    {
        btn[0].onClick.AddListener(() => SetController(false));
        btn[1].onClick.AddListener(Exit);
        //btn[2].onClick.AddListener(Data.Instance.SaveNicknameData);
        btn[2].onClick.AddListener(() => FadeToLevel(1));
        btn[3].onClick.AddListener(() => SetController(true));
        btn[4].onClick.AddListener(() => SetController(true));
        btn[5].onClick.AddListener(() => FadeToLevelSceneChange(1));

        if (Data.Instance.isLogIn == true)
        {
            Data.Instance.isLogIn = false;
            title.SetActive(false);
            worldMap.SetActive(true);
        }
    }

    // Fade
    public InputField tempInputField_Nickname;


    public void FadeToLevel(int levelIndex)
    {
        switch (Text_Nickname.text)
        {
            case "":
                StartCoroutine(Temp_OverNicknameLength());
                return;

            case " ":
                StartCoroutine(Temp_OverNicknameLength());
                return;

            case "  ":
                StartCoroutine(Temp_OverNicknameLength());
                return;

            case "   ":
                StartCoroutine(Temp_OverNicknameLength());
                return;

            case "    ":
                StartCoroutine(Temp_OverNicknameLength());
                return;

            case "     ":
                StartCoroutine(Temp_OverNicknameLength());
                return;

            case "      ":
                StartCoroutine(Temp_OverNicknameLength());
                return;

            case "       ":
                StartCoroutine(Temp_OverNicknameLength());
                return;
        }
        Data.Instance.nicknameBasket = tempInputField_Nickname.text;


        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");

        // add

        Data.Instance.sfx.clip = Data.Instance.scenechangeClip;
        Data.Instance.sfx.Play();
    }

    private bool temp_coroutineCheck = false;
    public GameObject overNicknameLength;
    IEnumerator Temp_OverNicknameLength()
    {
        if (temp_coroutineCheck == false)
        {
            temp_coroutineCheck = true;
            overNicknameLength.SetActive(true);
            yield return new WaitForSeconds(1f);
            overNicknameLength.SetActive(false);
            temp_coroutineCheck = false;
        }
    }

    public void OnFadeComplete(int _int)
    {
        _int = levelToLoad;

        for (int i = 0; i < page.Length; i++)
        {
            page[i].gameObject.SetActive(false);
        }
        page[_int].gameObject.SetActive(true);

        animator.SetTrigger("FadeIn");
    }

    // _______________________________________________________

    public void FadeToLevelSceneChange(int levelIndex)
    {
        if (ResourceSystem.Instance.resourceElements[0].resourceValue >= 500)
        {
            ResourceSystem.Instance.resourceElements[0].resourceValue -= 500;
            ResourceSystem.Instance.GetResource(ResourceType.childlikeEnergy, -500);
            ResourceSystem.Instance.InsertResource();
        }
        else
        {
            Debug.Log(ResourceSystem.Instance.resourceElements[0].resourceValue);
            return;
        }
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOutSceneChange");

        Data.Instance.sfx.clip = Data.Instance.scenechangeClip;
        Data.Instance.sfx.Play();
    }

    public void OnFadeCompleteSceneChange()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    // ________________________________________________________

    public void SetController(bool _bool)
    {
        Data.Instance.sfx.clip = Data.Instance.btnClip;
        Data.Instance.sfx.Play();

        set.SetActive(_bool);
        blur.enabled = _bool;

        CameraMove.Instance.isMove = !(_bool);
    }

    public void Exit()
    {
        Data.Instance.sfx.clip = Data.Instance.btnClip;
        Data.Instance.sfx.Play();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}