using UnityEngine;
using UnityEngine.SceneManagement;

public class Temp_Method : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    
    public void Editing()
    {
        CameraRay.Instance.isEditing = !CameraRay.Instance.isEditing;
    }
}
