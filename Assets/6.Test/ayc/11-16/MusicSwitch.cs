using System;
using UnityEngine;
using UnityEngine.UI;

public class MusicSwitch : MonoBehaviour
{
    public ToggleSoundImage[] toggleSoundImages;

    private void Start()
    {
        foreach (ToggleSoundImage tsi in toggleSoundImages) tsi.setupListener();
    }

    [Serializable]
    public class ToggleSoundImage {
        public Button btnMusic;
        public Image imgWhenButtonDown;
        private Color clrOfAlphaControl;
        private bool bMusicOn = true;

        public void toggleImage() {
            clrOfAlphaControl = imgWhenButtonDown.color;

            clrOfAlphaControl.a = Convert.ToInt32(bMusicOn);
            bMusicOn = !bMusicOn;

            imgWhenButtonDown.color = clrOfAlphaControl;
        }

        public void setupListener() {
            btnMusic.onClick.AddListener(toggleImage);
        }
    }
}