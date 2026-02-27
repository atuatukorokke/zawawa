using UnityEngine;

public class GimmickBottonGenerator : MonoBehaviour
{
    [SerializeField] public GameObject gimmickBotton;
    //[SerializeField] public GameObject Wall;
    public Collider2D WallPrefab;

    private GameObject buttonPrefab;
    float span = 3.0f;
    float delta = 0;
    float deltaW = 0;
    private float wallTime = 3f;
    private bool btn = false;
    private bool stay = true;
    private GameObject spawnedBotton;

    //private void Start()
    //{
    //    WallPrefab = GetComponent<GameObject>();
    //}

    void Update()
    {       
        this.delta += Time.deltaTime;
        if (btn == false)
        {
            if (this.delta > this.span)
            {
                //GimmickBottonGenerator bottonGenerator = GetComponent<GimmickBottonGenerator>();
                float randomX = Random.Range(-9, 9);
                float randomY = Random.Range(-5, 5);
                Vector3 spawnPos = new Vector3(randomX, randomY, 0);
                spawnedBotton = Instantiate(gimmickBotton, spawnPos, Quaternion.identity);
                this.delta = 0;

                //buttonPrefab = Instantiate(gimmickBotton);
                //buttonPrefab.transform.position = spawnPos;

                GimmickBottonController controller = spawnedBotton.GetComponent<GimmickBottonController>();
                controller.bottonGenerator = this;

                btn = true;
            }
        }

        if (stay == false)
        {
            this.deltaW += Time.deltaTime;
            if (this.delta > this.wallTime)
            {
                WallPrefab.enabled=true;
                stay = true;
                btn = false;
                this.delta = 0;
            }
        }
    }

    //public void WallCheack(bool gb)
    //{
    //    Debug.Log("ギミックボックスがきえたよ");
    //}

    public void BottonRevive()
    {        
        stay = false;
        WallPrefab.enabled = false;
        //Debug.Log("!btn");
    }
}
