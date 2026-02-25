using UnityEngine;

public class MoveBarrel : MonoBehaviour
{
    //90～200
    [SerializeField] private float RotetionLeft = 280 ;
    [SerializeField] private float RotetionRight = 180;

    //200になったか
    private bool MaxRotetion = false;
    //90になったか
    private bool MixRotetion = false;
    void Start()
    {
        MixRotetion = true;
        this.transform.eulerAngles = new Vector3(0, 0, 280);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.eulerAngles.z >= RotetionLeft)
        {
            MaxRotetion = true;
            MixRotetion = false;
        }
        else if (this.transform.eulerAngles.z <= RotetionRight)
        {
            MaxRotetion = false;
            MixRotetion = true;
        }
        //Debug.Log($"{this.transform.eulerAngles.z}/{this.name}");
        //Debug.Log(MaxRotetion);
        //Debug.Log(MixRotetion);
        if (MaxRotetion == true)
        {
            // 毎フレームX軸を中心に10°回転
            transform.Rotate(0, 0, -0.2f);
        }
        else if (MixRotetion == true)
        {
            // 毎フレームX軸を中心に10°回転
            transform.Rotate(0, 0, 0.2f);
        }
    }
}
