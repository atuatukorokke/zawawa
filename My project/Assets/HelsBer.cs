using UnityEngine;
using UnityEngine.UI;

public class HelsBer : MonoBehaviour
{
    public Image healthImage;
    public float MaxHP = 5.0f;
    private float HP = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            damage(1);
        }
    }
    public void damage(int damage)
    {
        HP -= damage;
        //healthImage.fillAmount = HP / MaxHP;
        Debug.Log(HP);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("衝突しました: " + collision.gameObject.name);
    }
}
