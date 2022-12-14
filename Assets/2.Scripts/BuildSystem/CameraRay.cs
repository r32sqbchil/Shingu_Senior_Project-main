using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CameraRay : Singleton<CameraRay>
{
    // 추가한 코드
    public GameObject TempUI_InteractionPrevent;
    // 기본세팅 건드리지 말것
    private GameObject prefab;
    public const int w = 13;
    public const int h = 13;
    public bool[,] isTrue = new bool[w, h];
    public int pickScaleX;
    public int pickScaleZ;
    public GameObject Prefab { set { prefab = value; } }
    public int PickScaleX { set { pickScaleX = value; } }
    public int PickScaleZ { set { pickScaleZ = value; } }
    private int index;
    private Transform pickObject;
    private GameObject dummyGameObject;

    // 레이어 및 레이 세팅
    private int wallLayerMask = 1 << 6;
    private int unitLayerMask = 1 << 7;
    private int IgnoreLayerMask = 1 << 11;
    private Ray ray;
    private RaycastHit hit;

    // 조건 세팅
    public bool isEditing;
    public bool IsEditing { set { isEditing = value; } }
    public bool isObjectMoving;

    // 상태 표시 텍스트
    public Text statusText;

    // 빌딩 옵션 및 재료가공 페이지 세팅
    public GameObject constructionModel;
    public GameObject constructionEffect;
    public GameObject option;

    // 공터 메테리얼 세팅
    public Material aa;
    public Material bb;

    public GameObject castle;
    

    // 건설된 건물들을 저장할 리스트
    public List<GameObject> buildings = new List<GameObject>();

    
    private void Start()
    {
        for (int i = 0; i < w; i++)
            for (int j = 0; j < h; j++)
                if (i == w - 1 || j == h - 1)
                    isTrue[i, j] = true;
    }


    [System.Obsolete]
    private void Update()
    {
        Color alpha; alpha.a = 0.2f;
        Color alphaZero; alphaZero.a = 0f;

        if(isEditing)
        {
            statusText.text = "수정중";
            aa.color = new Color(aa.color.r, aa.color.g, aa.color.b, alpha.a);
            bb.color = new Color(bb.color.r, bb.color.g, bb.color.b, alpha.a);
        }
        else
        {
            statusText.text = "수정아님";
            aa.color = new Color(aa.color.r, aa.color.g, aa.color.b, alphaZero.a);
            bb.color = new Color(bb.color.r, bb.color.g, bb.color.b, alphaZero.a);
        }

        // 이전 개발자 버전
        //if(Input.GetKeyDown(KeyCode.E))
        //{
        //    if(!isEditing)
        //        isEditing = true;
        //    else
        //        isEditing = false;
        //}

        if(Input.GetMouseButtonDown(0))
            OnClick();

        if(isEditing)
            OnDrag();
    }

    private bool temp_coroutineCheck = false;
    public GameObject Parent_warningText;
    public Text warningText;
    IEnumerator Temp_YetCastle()
    {
        if (temp_coroutineCheck == false)
        {
            temp_coroutineCheck = true;
            Parent_warningText.SetActive(true);
            warningText.text = "개발 중인 지역이에요!";
            yield return new WaitForSeconds(1f);
            Parent_warningText.SetActive(false);
            temp_coroutineCheck = false;
        }
    }
    IEnumerator Temp_YetOpenCastle()
    {
        if (temp_coroutineCheck == false)
        {
            temp_coroutineCheck = true;
            Parent_warningText.SetActive(true);
            warningText.text = "미해금된 지역이에요!";
            yield return new WaitForSeconds(1f);
            Parent_warningText.SetActive(false);
            temp_coroutineCheck = false;
        }
    }


    // 안유찬이 추가함
    public Text TempText_BossStage;
    public Image TempImage_Boss;

    public Sprite[] Image_Bosses;

    public float resourceRecreateTime;

    private void DontCameraMove()
    {
        TempUI_InteractionPrevent.SetActive(true);
        CameraMove.Instance.isMove = false;
        castle.SetActive(true);
    }
    private void OnClick()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform.CompareTag("Sell"))
            {
                SellBuilding(hit.transform.parent.transform.parent.gameObject);
            }
            if(hit.transform.CompareTag("Move"))
            {
                pickObject.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
                Destroy(hit.transform.parent.gameObject);
                isObjectMoving = true;
            }
            if(!isEditing && CameraMove.Instance.isMove == true)
            {
                if (hit.transform.CompareTag("Resource"))
                {
                    Building building = hit.transform.gameObject.GetComponent<Building>();
                    building.StartCoroutine(building.GetResource(resourceRecreateTime));
                }

                if (hit.transform.CompareTag("Unit"))
                {
                    Building building = hit.transform.gameObject.GetComponent<Building>();
                    if (building.isCollect == false) return;
                    building.GetBuildingResource(building);
                    building.resource = 0;
                    building.isCollect = false;
                }

                if(hit.transform.CompareTag("Manufacture"))
                {
                    
                }

                if(hit.transform.CompareTag("Castle0"))
                {
                    TempText_BossStage.text = "피터팬 - 보스 스테이지";
                    TempImage_Boss.overrideSprite = Image_Bosses[0];
                    DontCameraMove();
                }

                if(hit.transform.CompareTag("Castle1"))
                {
                    if (Data.Instance.isStage[1] == true)
                    {
                        TempText_BossStage.text = "미녀와 야수 - 보스 스테이지";
                        TempImage_Boss.overrideSprite = Image_Bosses[1];
                        DontCameraMove();
                    }
                    else
                    {
                        StartCoroutine(Temp_YetOpenCastle());
                    }
                }

                if(hit.transform.CompareTag("Yet_Castle"))
                {
                    StartCoroutine(Temp_YetCastle());
                }
            }
        }
        
        
        if (Physics.Raycast(ray, out hit, float.MaxValue, IgnoreLayerMask))
            return;

        if (Physics.Raycast(ray, out hit, float.MaxValue, unitLayerMask))
        {
            if(isEditing)
            {
                if(pickObject != null) return;
                //if(pickObject.GetComponent<Building>().isConstruct) return;

                index = int.Parse(hit.transform.parent.name);
                int _x = Mathf.RoundToInt(hit.transform.localScale.x);
                int _z = Mathf.RoundToInt(hit.transform.localScale.z);
                pickScaleX = _x;
                pickScaleZ = _z;//(int)hit.transform.localScale.z;

                IsBool(index, false, pickScaleX, pickScaleZ);
                pickObject = hit.transform;

                Vector3 _position = hit.transform.position - new Vector3(0, -2.5f, 1);
                option.transform.localScale = new Vector3(1,1,1);
                Vector3 _option = new Vector3(option.transform.localScale.x / pickScaleX, 1, option.transform.localScale.x / pickScaleX);
                option.transform.localScale = _option;
                Instantiate(option, _position, option.transform.localRotation, hit.transform);
            }
        }
        else if (Physics.Raycast(ray, out hit, float.MaxValue, wallLayerMask)) 
            return;
    }

    public void BringObject()
    {
        if(isEditing)
        {
            if(!buildings.Contains(dummyGameObject)) Destroy(dummyGameObject);
            ResetBuildingsPosition();
            dummyGameObject = Instantiate(prefab, this.transform);
            dummyGameObject.transform.position += new Vector3(0, 100, 0);
            dummyGameObject.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
            pickObject = dummyGameObject.transform;
            isObjectMoving = true;
        }
    }

    [System.Obsolete]
    private void OnDrag()
    {
        if (pickObject == null) return;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, float.MaxValue, IgnoreLayerMask))
        {
            return;
        }

        if (Physics.Raycast(ray, out hit, float.MaxValue, wallLayerMask))
        {
            if (CheckBool(int.Parse(hit.transform.name), pickScaleX, pickScaleZ)) return;
            if (!isObjectMoving) return;

            pickObject.SetParent(hit.transform);
            pickObject.localPosition = new Vector3(-(pickScaleX - 1) * 0.5f, 1, (pickScaleZ - 1) * 0.5f);
        }

        if(Input.GetMouseButtonDown(1))
        {
            if (pickObject != null)
            {
                isObjectMoving = false;
                IsBool(int.Parse(pickObject.transform.parent.name), true, pickScaleX, pickScaleZ);
                pickObject.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
                ResetBuildingsPosition();
                if(buildings.Contains(pickObject.gameObject))
                {
                    isEditing = false;
                    pickObject = null;
                    return;
                }
                else
                {
                    isEditing = false;
                    pickObject.GetComponent<Building>().buildBuilding(constructionModel, constructionEffect);
                    buildings.Add(pickObject.gameObject);
                    ResetBuildingsPosition();
                    pickObject = null;
                }
            }
        }
    }

    private bool CheckBool(int index, int sizeX, int sizeZ)
    {
        for (int i = 1; i <= sizeX; i++)
        {
            for (int j = 1; j <= sizeZ; j++)
            {
                if (isTrue[index / h + i - 1, index % h + j - 1])
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void IsBool(int index, bool isBool, int sizeX, int sizeZ)
    {
        for (int i = 1; i <= sizeX; i++)
        {
            for (int j = 1; j <= sizeZ; j++)
            {
                isTrue[index / h + i - 1, index % h + j - 1] = isBool;
            }
        }
    }

    public void LockUnLock(int w, int h, bool _bool)
    {
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                if (i == w - 1 || j == h - 1)
                    isTrue[i, j] = _bool;
            }
        }
    }

    public void LockBlock(int _int)
    {
        if(_int <= w)
        {
            for (int i = _int; i < w; i++)
            {
                LockUnLock(i, i, true);
            }
        }
    }

    public void SellBuilding(GameObject building)
    {
        ResourceSystem.Instance.GetResource(ResourceType.childlikeEnergy, building.GetComponent<Building>().cost1 / 2);
        buildings.Remove(building);
        pickObject = null;
        Destroy(building);
    }

    public void ResetBuildingsPosition()
    {
        if(isEditing)
        {
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                        isTrue[i, j] = false;
            
            if(LevelSystem.Instance.level < 5)
            {
                LockBlock(LevelSystem.Instance.level + 9);
            }

            Start();

            for (int i = 0; i < buildings.Count; i++)
            {
                var LocalScale = buildings[i].gameObject.transform.localScale;
                IsBool(int.Parse(buildings[i].transform.parent.name), true, (int)LocalScale.x, (int)LocalScale.z);
                //Debug.Log(buildings[i].transform.parent.name+"의 x 크기를"+int.Parse(x)+"으로 바꿨습니다.");
            }
        }
    }
}