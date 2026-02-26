using UnityEngine;

public class OpenAnimation : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject Object;

    void Awake()
    {
        anim.SetBool("open", true);
    }

    public void OpenDelete()
    {
        Destroy(Object);
    }
}
