using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    private bool check = true;
    [SerializeField] private Animator[] anim;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && check)
        {
            check = false;
            anim[0].SetBool("clear", true);
        }
    }

    public void ClearClose()
    {
        anim[1].SetBool("close", true);
    }

    public void SceneClose()
    {
        SceneManager.LoadScene("StartScene");
    }
}
