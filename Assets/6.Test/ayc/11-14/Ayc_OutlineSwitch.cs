using System.Collections;
using UnityEngine;

public class Ayc_OutlineSwitch : MonoBehaviour
{
    // �ڽ� �� ������ �ִ� �ƿ������� ���ʿ� ǥ�õǹǷ�
    // Ŭ������ ���� �����ϰ�(OFF) ���� �ƿ����θ� �����Ѵ�. ���� �ƿ������� ������� �Ѵ�.

    public MeshRenderer Mesh_upperOutline;
    public MeshRenderer Mesh_lowerOutline;
  
    public float currentRecreateTime;

    public TextMesh Text_currentRecreateTime;
    public MeshRenderer Mesh_currentRecreateTime;

    private bool isClick;

    private void Start()
    {
        currentRecreateTime = CameraRay.Instance.resourceRecreateTime;
    }

    private void Update()
    {
        if(isClick)
        {
            Mesh_currentRecreateTime.enabled = true;
            // ����
            Text_currentRecreateTime.text = Mathf.Floor(currentRecreateTime).ToString() + "�� ���ҽ��ϴ�.";
            currentRecreateTime -= Time.deltaTime;
        }
    }

    // ���콺�� �÷����� �� -> ��� -> ON
    private void OnMouseEnter()
    {
        if(!CameraRay.Instance.isEditing)
        {
            if(CameraMove.Instance.isMove == true)
            {
                Mesh_lowerOutline.enabled = true;
            }

        }
    }


    // ���콺�� Ŭ������ �� -> ����� -> OFF
    private void OnMouseDown()
    {
        if(!CameraRay.Instance.isEditing)
        {
            isClick = true;
            Mesh_upperOutline.enabled = false;
            Mesh_lowerOutline.enabled = false;
            StartCoroutine(SetResource());
        }
    }

    // �ڿ� ����� �� Default(�⺻)������ ����
    private IEnumerator SetResource()
    {
        yield return new WaitForSeconds(CameraRay.Instance.resourceRecreateTime);
        Mesh_currentRecreateTime.enabled = false;
        Mesh_upperOutline.enabled = true;
        isClick = false;
        currentRecreateTime = CameraRay.Instance.resourceRecreateTime;
    }


    // �÷��� ���콺�� ���� �� -> ����� -> OFF
    private void OnMouseExit()
    {
        Mesh_lowerOutline.enabled = false;
    }
}
