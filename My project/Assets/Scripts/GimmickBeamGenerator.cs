using UnityEngine;

public class GimmickBeamGenerator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject gimmickBeam;
    float span = 3.0f;
    float delta = 0;
    private GameObject spawnedObject;
    private bool fire = false;

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if (fire == false) 
        {
            if (this.delta > this.span)
            {
                this.delta = 0;
                Vector3 spawnPos = new Vector3(9, -5, 0);
                spawnedObject = Instantiate(gimmickBeam, spawnPos, Quaternion.identity);
                // 出現位置を指定して生成

                GimmickBeamController controller = spawnedObject.GetComponent<GimmickBeamController>(); 
                controller.generator = this;


                // 初期スケール設定
                spawnedObject.transform.localScale = new Vector3(1f, 1f, 0f);

                //画面内に存在するビームは1つとする
                fire = true;
            } 
        }
        
        
    }

    public void Revive()
    {
        fire = false;
    }
}
