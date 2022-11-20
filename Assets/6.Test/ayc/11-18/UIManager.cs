using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject startScene;
    public GameObject mainScene;
    public GameObject defaultAfterStart;
    public GameObject signIn;
    public GameObject settings;
    public GameObject bg;

    public TMP_InputField IF_nickname;
    public TMP_InputField IF_age;

    public Text Txt_setNickname;
    public Text Txt_level;

    public Button Btn_start;
    public Button Btn_startScene_joinUs;
    public Button Btn_signIn_joinUs;
    public Button Btn_cancel;
    public Button Btn_settings;

    private bool isOnSetting = false;

    private void Start()
    {
        IF_nickname.characterLimit = 8;
        IF_age.characterLimit = 2;

        IF_nickname.onValueChanged.AddListener
            ((nickname) => IF_nickname.text = Regex.Replace(nickname, @"[^0-9A-Za-z¤¡-¤¾°¡-ÆR]", ""));

        IF_age.onValueChanged.AddListener
            ((age) => IF_age.text = Regex.Replace(age, @"[^0-9]", ""));

        Btn_start.onClick.AddListener(OnClick_Start);
        Btn_startScene_joinUs.onClick.AddListener(OnClick_StartScene_JoinUs);
        Btn_signIn_joinUs.onClick.AddListener(OnClick_SignIn_JoinUs);
        Btn_cancel.onClick.AddListener(OnClick_Cancel);
        Btn_settings.onClick.AddListener(OnClick_Settings);
    }

    public void OnClick_Start()
    {
        startScene.SetActive(false);
        mainScene.SetActive(true);
        defaultAfterStart.SetActive(true);
    }

    public void OnClick_StartScene_JoinUs()
    {
        signIn.SetActive(true);
    }

    public void OnClick_SignIn_JoinUs()
    {
        DataContainer.Instance.nickname = IF_nickname.text;
        startScene.SetActive(false);
        signIn.SetActive(false);
        mainScene.SetActive(true);
        defaultAfterStart.SetActive(true);
        Txt_setNickname.text = DataContainer.Instance.nickname;
    }

    public void OnClick_Cancel()
    {
        IF_nickname.text = string.Empty;
        IF_age.text = string.Empty;

        signIn.SetActive(false);
    }

    public void OnClick_Settings()
    {
        settings.SetActive(!(isOnSetting));
        isOnSetting = !(isOnSetting);
    }

    public void OnClick_WorldMap()
    {
        bg.SetActive(false);
    }
}
