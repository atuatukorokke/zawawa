using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseAnimation : MonoBehaviour
{
    private bool check = true;
    [SerializeField] private Animator anim;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && check)
        {
            check = false;
            anim.SetBool("close", true);
        }
    }

    public void SceneClose()
    {
        SceneManager.LoadScene("SceneTest2");
    }
}
