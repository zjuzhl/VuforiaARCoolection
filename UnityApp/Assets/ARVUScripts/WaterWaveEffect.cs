using UnityEngine;

[RequireComponent(typeof(Camera))]  // ��Ļ������Чһ�㶼��Ҫ���������
public class WaterWaveEffect : MonoBehaviour
{
    public float A = 0.01f; // ˮ���ʵ�ˮƽ�������
    public float w1 = 60; // ˮ�����沨�ν�Ƶ��(ֵԽ��, ����Խ��)
    public float w2 = 30; // ˮ���ʵ�ˮƽ������Ƶ��(ֵԽ��, ˮ���ʵ���Խ��)
    public float waveWidth = 0.3f; // ˮ�����(ˮ������ɢʱ, �󲨻���ʧ)
    public float waveSpeed = 0.3f; // ˮ���������ٶ�
    private float waveTime; // ˮ������ʱ��
    private Vector4 waveCenter; // ˮ������
    private Material waveMaterial; // ˮ������
    private bool enabledWave = false; // ˮ������


    private float timeCount = 1.5f;
    private float timeSpace = 1.6f;

    private void Awake()
    {
        waveMaterial = new Material(Shader.Find("Custom/WaterWave"));
        waveMaterial.hideFlags = HideFlags.DontSave;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            waveCenter = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height); // ��Ļ�����һ��
            enabledWave = true;
            waveTime = 0;
        }

        //if (timeCount < timeSpace) {
        //    timeCount += Time.deltaTime;
        //    if (timeCount >= timeSpace) {
        //        timeCount = 0;
        //        waveCenter = new Vector2(0.5f, 0.5f); // ��Ļ�����һ��
        //        enabledWave = true;
        //        waveTime = 0;
        //    }
        //}
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (enabledWave)
        {
            SetWaveMaterialParams();
            Graphics.Blit(source, destination, waveMaterial);
            waveTime += Time.deltaTime;
            if (waveTime > 2 / waveSpeed)
            { // ˮ����������Ļ����, ����ˮ����Ч
                enabledWave = false;
            }
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

    private void SetWaveMaterialParams()
    { // ����ˮ�����ʲ���
        waveMaterial.SetFloat("_A", A); // ˮ���ʵ�ˮƽ�������
        waveMaterial.SetFloat("_w1", w1); // ˮ�����沨�ν�Ƶ��(ֵԽ��, ����Խ��)
        waveMaterial.SetFloat("_w2", w2); // ˮ���ʵ�ˮƽ������Ƶ��(ֵԽ��, ˮ���ʵ���Խ��)
        waveMaterial.SetFloat("_t", waveTime); // ˮ������ʱ��
        waveMaterial.SetVector("_o", waveCenter); // ˮ������
        waveMaterial.SetFloat("_waveDist", waveTime * waveSpeed); // ˮ����������
        waveMaterial.SetFloat("_waveWidth", waveWidth); // ˮ�����(ˮ���ڴ���ʱ, �󲨻���ʧ)
    }
}