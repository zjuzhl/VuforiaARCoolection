using UnityEngine;

public class DissolveController : MonoBehaviour
{
    [Header("�ܽ����")]
    [Range(0f, 1f)]
    public float dissolveAmount = 0f;
    public Vector3 dissolveDirection = Vector3.up;
    public float dissolveSpeed = 0.5f;

    [Header("��ԵЧ��")]
    public Color edgeColor = Color.red;
    public float edgeWidth = 0.1f;
    public float edgeIntensity = 2.0f;

    [Header("�Զ�����")]
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
        // ��ȡ����ʹ�ô˲��ʵ���Ⱦ��
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
            // �����ܽ����
            dissolveAmount += Time.deltaTime * dissolveSpeed * (isDissolving ? 1 : -1);

            // ���Ʒ�Χ
            if (dissolveAmount >= 1f)
            {
                dissolveAmount = 1f;
                dissolveInProgress = false;

                if (loop)
                {
                    // �Զ�����
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
                    // �Զ�ѭ��
                    isDissolving = true;
                    dissolveInProgress = true;
                }
            }

            UpdateMaterialProperties();
        }
    }

    // ���²�������
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

    // ��ʼ�ܽ⣨�������ϣ�
    public void StartDissolve()
    {
        isDissolving = true;
        dissolveInProgress = true;
    }

    // ��ʼ�ָ����������£�
    public void StartRestore()
    {
        isDissolving = false;
        dissolveInProgress = true;
    }

    // �л��ܽ�״̬
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

    // �����ܽ����
    public void SetDissolveAmount(float amount)
    {
        dissolveAmount = Mathf.Clamp01(amount);
        dissolveInProgress = false;
        UpdateMaterialProperties();
    }
}