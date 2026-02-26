using UnityEngine;

public class GimmickBottonGenerator : MonoBehaviour
{
    [SerializeField] private GameObject gimmickBotton;
    private GameObject buttonPrefab;
    float span = 1.0f;
    float delta = 0;
    private bool btn = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {       
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            if (btn == false)
            {
                float randomX = Random.Range(-9, 9);
                float randomY = Random.Range(-5, 5);
                Vector3 spawnPos = new Vector3(randomX, randomY, 0);
                this.delta = 0;

                buttonPrefab = Instantiate(gimmickBotton);
                buttonPrefab.transform.position = spawnPos;

                btn = true;
            }
        }
    }

    public void WallCheack(bool gb)
    {
        Debug.Log("ギミックボックスがきえたよ");
    }
}
