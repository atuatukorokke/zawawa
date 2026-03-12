using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

// 背景テクスチャをスクロールさせるスクリプト
// UI(Image)でも、SpriteRenderer / MeshRenderer でも動くようにしてある
public class BackGroundMover : MonoBehaviour
{
    // テクスチャオフセットは0～1でループするので最大値は1
    private const float k_maxLength = 1f;

    // マテリアルのメインテクスチャプロパティ名
    private const string k_propName = "_MainTex";

    // テクスチャがスクロールするスピード
    // x = 横スクロール速度
    // y = 縦スクロール速度
    [SerializeField]
    private Vector2 m_offsetSpeed;

    // 使用するマテリアルの参照
    private Material m_copiedMaterial;

    void Start()
    {
        // -----------------------------
        // どのコンポーネントからマテリアルを取得するか
        // -----------------------------

        // UIのImageが付いている場合
        Image image = GetComponent<Image>();
        if (image != null)
        {
            m_copiedMaterial = image.material;
        }
        else
        {
            // SpriteRendererやMeshRendererなどのRendererを取得
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                m_copiedMaterial = renderer.material;
            }
        }

        // マテリアルが取得できていない場合はエラー
        Assert.IsNotNull(m_copiedMaterial);
    }

    void Update()
    {
        // ゲームが停止中（ポーズなど）は動かさない
        if (Time.timeScale == 0f) return;

        // -----------------------------
        // テクスチャのオフセットを計算
        // -----------------------------
        // Mathf.Repeatで0～1の範囲でループさせる
        // これにより背景が無限にスクロールしているように見える

        float x = Mathf.Repeat(Time.time * m_offsetSpeed.x, k_maxLength);
        float y = Mathf.Repeat(Time.time * m_offsetSpeed.y, k_maxLength);

        Vector2 offset = new Vector2(x, y);

        // マテリアルのテクスチャオフセットを変更
        m_copiedMaterial.SetTextureOffset(k_propName, offset);
    }

    void OnDestroy()
    {
        // オブジェクト破壊時に参照を解放
        // （メモリ管理のため）
        m_copiedMaterial = null;
    }
}