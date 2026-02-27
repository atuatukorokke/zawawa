using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseAnimation : MonoBehaviour
{
    private bool check = true;
    [SerializeField] private Animator anim;

    public void PlayClose()
    {
        if (!check) return;

        check = false;
        anim.SetBool("close", true);
    }

    public void SceneClose()
    {
        SceneManager.LoadScene("PlayerScene 2");
    }
}