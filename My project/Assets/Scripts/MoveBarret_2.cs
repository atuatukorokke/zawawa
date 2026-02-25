using UnityEngine;

public class MoveBarret_2 : MonoBehaviour
{
    //90～200
    [SerializeField] private float RotetionLeft = 180;
    [SerializeField] private float RotetionRight = 80;

    //200になったか
    private bool MaxRotetion = false;
    //90になったか
    private bool MixRotetion = true;
    void Start()
    {
        //MixRotetion = true;
        Debug.Log("asd");
        this.transform.eulerAngles = new Vector3(0, 0, 80);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.eulerAngles.z > RotetionLeft)
        {
            MaxRotetion = true;
            MixRotetion = false;
        }
        else if (this.transform.eulerAngles.z < RotetionRight)
        {
            MaxRotetion = false;
            MixRotetion = true;
        }
        //Debug.Log($"{this.transform.eulerAngles.z}/{this.name}");
        //Debug.Log($"{MaxRotetion}/Max");
        //Debug.Log($"{MixRotetion}/Mix");
        if (MaxRotetion == true)        // 右回転
        {
            // 毎フレームX軸を中心に10°回転
            transform.Rotate(0, 0, -0.2f);
        }
        else if (MixRotetion == true)   // 左回転
        {
            // 毎フレームX軸を中心に10°回転
            transform.Rotate(0, 0, 0.2f);
        }
    }
}

