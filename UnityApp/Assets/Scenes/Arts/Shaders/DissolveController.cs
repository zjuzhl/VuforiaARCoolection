using UnityEngine;

public class DissolveController : MonoBehaviour
{
    [Header("溶解控制")]
    [Range(0f, 1f)]
    public float dissolveAmount = 0f;
    public Vector3 dissolveDirection = Vector3.up;
    public float dissolveSpeed = 0.5f;

    [Header("边缘效果")]
    public Color edgeColor = Color.red;
    public float edgeWidth = 0.1f;
    public float edgeIntensity = 2.0f;

    [Header("自动播放")]
    public bool playOnStart = false;
    public bool loop = false;

    private Material[] materials;
    private bool isDissolving = false;
    private bool dissolveInProgress = false;

    private const string DISSOLVE_AMOUNT = "_DissolveAmount";
    private const string DISSOLVE_DIRECTION = "_DissolveDirection";
    private const string EDGE_COLOR = "_EdgeColor";
    private const string EDGE_WIDTH = "_EdgeWidth";
    private const string EDGE_INTENSITY = "_EdgeIntensity";

    void Start()
    {
        // 获取所有使用此材质的渲染器
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        materials = new Material[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            materials[i] = renderers[i].material;
            UpdateMaterialProperties();
        }

        if (playOnStart)
        {
            StartDissolve();
        }
    }

    void Update()
    {
        if (dissolveInProgress)
        {
            // 控制溶解进度
            dissolveAmount += Time.deltaTime * dissolveSpeed * (isDissolving ? 1 : -1);

            // 限制范围
            if (dissolveAmount >= 1f)
            {
                dissolveAmount = 1f;
                dissolveInProgress = false;

                if (loop)
                {
                    // 自动反向
                    isDissolving = false;
                    dissolveInProgress = true;
                }
            }
            else if (dissolveAmount <= 0f)
            {
                dissolveAmount = 0f;
                dissolveInProgress = false;

                if (loop && !isDissolving)
                {
                    // 自动循环
                    isDissolving = true;
                    dissolveInProgress = true;
                }
            }

            UpdateMaterialProperties();
        }
    }

    // 更新材质属性
    private void UpdateMaterialProperties()
    {
        foreach (Material mat in materials)
        {
            mat.SetFloat(DISSOLVE_AMOUNT, dissolveAmount);
            mat.SetVector(DISSOLVE_DIRECTION, dissolveDirection);
            mat.SetColor(EDGE_COLOR, edgeColor);
            mat.SetFloat(EDGE_WIDTH, edgeWidth);
            mat.SetFloat(EDGE_INTENSITY, edgeIntensity);
        }
    }

    // 开始溶解（从下往上）
    public void StartDissolve()
    {
        isDissolving = true;
        dissolveInProgress = true;
    }

    // 开始恢复（从上往下）
    public void StartRestore()
    {
        isDissolving = false;
        dissolveInProgress = true;
    }

    // 切换溶解状态
    public void ToggleDissolve()
    {
        if (dissolveInProgress)
        {
            isDissolving = !isDissolving;
        }
        else
        {
            StartDissolve();
        }
    }

    // 设置溶解进度
    public void SetDissolveAmount(float amount)
    {
        dissolveAmount = Mathf.Clamp01(amount);
        dissolveInProgress = false;
        UpdateMaterialProperties();
    }
}