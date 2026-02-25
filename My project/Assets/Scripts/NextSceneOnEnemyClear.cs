using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneOnEnemyClear : MonoBehaviour
{
    // シーン内のEnemyの数を数える変数
    int enemyCount;

    void Start()
    {
        // シーン内のEnemyタグが付いたオブジェクトを数える
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    // Enemytest.csから呼ばれる敵が消えた通知
    public void EnemyDefeated()
    {
        // 敵が倒されたのでカウントを減らす
        enemyCount--;
        Debug.Log("EnemyDefeated()が呼ばれた！");

        // 敵が全て倒されたかチェック
        if (enemyCount <= 0)
        {
            LoadNextScene();
        }
    }

    // 次のシーンを読み込む関数
    void LoadNextScene()
    {
        Debug.Log("LoadNextScene()が呼ばれた！");

        // 現在のシーン名を取得
        string current = SceneManager.GetActiveScene().name;

        // 例) PlayerScene -> PlayerScene1
        // 数字部分を取り出す
        string numberText = System.Text.RegularExpressions.Regex.Match(current, @"\d+").Value;

        // 数字を整数に変換して、次のシーン名を作る
        if (int.TryParse(numberText, out int num))
        {
            int nextNum = num + 1; // 次の番号
            string nextScene = current.Replace(num.ToString(), nextNum.ToString());

            // 次のシーンを読み込む
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            // シーン名に数字がない場合はエラーを出す
            Debug.LogError("シーン名に数字が含まれていません");
        }
    }
}
