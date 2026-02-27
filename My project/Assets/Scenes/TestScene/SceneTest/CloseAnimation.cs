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

    // アニメーションイベントで呼ぶ
    public void SceneClose()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextIndex);
    }
}