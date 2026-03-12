using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearClose : MonoBehaviour
{
    public void SceneClose()
    {
        SceneManager.LoadScene("aStartScene");
    }
}
