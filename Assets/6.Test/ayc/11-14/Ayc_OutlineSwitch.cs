using System.Collections;
using UnityEngine;

public class Ayc_OutlineSwitch : MonoBehaviour
{
    // 자식 중 하위에 있는 아웃라인이 위쪽에 표시되므로
    // 클릭했을 때를 제외하고(OFF) 하위 아웃라인만 제어한다. 하위 아웃라인은 흰색으로 한다.

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
            // 내림
            Text_currentRecreateTime.text = Mathf.Floor(currentRecreateTime).ToString() + "초 남았습니다.";
            currentRecreateTime -= Time.deltaTime;
        }
    }

    // 마우스를 올려뒀을 때 -> 흰색 -> ON
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


    // 마우스로 클릭했을 때 -> 노란색 -> OFF
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

    // 자원 재생성 시 Default(기본)값으로 설정
    private IEnumerator SetResource()
    {
        yield return new WaitForSeconds(CameraRay.Instance.resourceRecreateTime);
        Mesh_currentRecreateTime.enabled = false;
        Mesh_upperOutline.enabled = true;
        isClick = false;
        currentRecreateTime = CameraRay.Instance.resourceRecreateTime;
    }


    // 올려둔 마우스를 땠을 때 -> 노란색 -> OFF
    private void OnMouseExit()
    {
        Mesh_lowerOutline.enabled = false;
    }
}
