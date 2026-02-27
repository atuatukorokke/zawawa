using UnityEngine;

public class GimmickBottonController : MonoBehaviour
{
    [SerializeField] public GameObject botton;
    //[SerializeField] public Collider2D WallC;
    public GimmickBottonGenerator bottonGenerator;
    
    //private bool stay = true;
    //private float wallTime = 3f;            //いったん３
    //float delta = 0;

    //private GameObject WallTest;

    private void Start()
    {
        //WallC = GetComponent<Collider2D>();
        //WallTest = GameObject.Find("Wall");     //.Find修正予定
        //Debug.Log(WallTest.name);
        
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            //GimmickBottonGenerator bottonGenerator = GetComponent<GimmickBottonGenerator>();
            bottonGenerator.BottonRevive();
            Destroy(this.gameObject);

            //Debug.Log("dest");
        }
    }

    private void Update()
    {
        //if(stay == false)
        //{
        //    this.delta += Time.deltaTime;
        //    if (this.delta > this.wallTime)
        //    {
        //        Wall.SetActive(true);
        //        this.delta = 0;
        //        stay = true;
        //    }
        //}
    }
    //Num = Random.Range(1, 4)
    // {"wall"+Num}
}
