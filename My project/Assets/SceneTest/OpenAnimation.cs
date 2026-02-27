using UnityEngine;

public class OpenAnimation : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator anim;

    // 多重実行防止
    private bool opened = false;

    void Awake()
    {
        if (anim == null)
            anim = GetComponent<Animator>();

        // シーン開始と同時にOpen再生
        anim.SetBool("open", true);
    }

    // 🔥 アニメーションの最後に AnimationEvent で呼ぶ
    public void OpenDelete()
    {
        if (opened) return;
        opened = true;

        Destroy(gameObject);
    }
}
