using UnityEngine;

public class GimmickBottonController : MonoBehaviour
{
    [SerializeField] private GameObject botton;
    //[SerializeField] private GameObject Wall;
    
    private bool stay = true;
    private float wallTime = 5f;
    float delta = 0;

    private GameObject WallTest;

    private void Start()
    {
        //GameObject wall = GetComponent<GameObject>();
        WallTest = GameObject.Find("Wall");
        Debug.Log(WallTest.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            WallTest.SetActive(false);
            stay = false;
        }
    }

    private void Update()
    {
        if(stay == false)
        {
            this.delta += Time.deltaTime;
            if (this.delta > this.wallTime)
            {
                WallTest.SetActive(true);
                this.delta = 0;
            }
        }
    }
}
