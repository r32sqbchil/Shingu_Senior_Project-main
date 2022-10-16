using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_DestroyText : MonoBehaviour
{
    public GameObject destroyText;

    public void PayResource(int _cost1, int _cost2, int _cost3, int _cost4, int _cost5)
    {
        // 코스트가 오버 될 경우 return;
        if (
        ResourceSystem.Instance.resourceElements[0].resourceValue < _cost1 ||
        ResourceSystem.Instance.resourceElements[1].resourceValue < _cost2 ||
        ResourceSystem.Instance.resourceElements[2].resourceValue < _cost3 ||
        ResourceSystem.Instance.resourceElements[3].resourceValue < _cost4 ||
        ResourceSystem.Instance.resourceElements[4].resourceValue < _cost5)
        {
            destroyText.SetActive(true);
            CameraRay.Instance.isEditing = false;
            return;
        }
        else
        {
            ResourceSystem.Instance.resourceElements[0].resourceValue -= _cost1;
            ResourceSystem.Instance.resourceElements[1].resourceValue -= _cost2;
            ResourceSystem.Instance.resourceElements[2].resourceValue -= _cost3;
            ResourceSystem.Instance.resourceElements[3].resourceValue -= _cost4;
            ResourceSystem.Instance.resourceElements[4].resourceValue -= _cost5;
            CameraRay.Instance.isEditing = true;
            CameraRay.Instance.BringObject();
        }

        ResourceSystem.Instance.InsertResource();
    }
}
