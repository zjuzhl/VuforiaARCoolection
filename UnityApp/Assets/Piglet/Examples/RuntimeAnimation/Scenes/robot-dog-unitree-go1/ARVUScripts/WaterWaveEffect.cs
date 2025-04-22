using UnityEngine;

[RequireComponent(typeof(Camera))]  // 屏幕后处理特效一般都需要绑定在像机上
public class WaterWaveEffect : MonoBehaviour
{
    public float A = 0.01f; // 水面质点水平波动振幅
    public float w1 = 60; // 水波截面波形角频率(值越大, 波纹越密)
    public float w2 = 30; // 水面质点水平波动角频率(值越大, 水波质点振动越快)
    public float waveWidth = 0.3f; // 水波宽度(水波在扩散时, 后波会消失)
    public float waveSpeed = 0.3f; // 水波传播的速度
    private float waveTime; // 水波传播时间
    private Vector4 waveCenter; // 水波中心
    private Material waveMaterial; // 水波材质
    private bool enabledWave = false; // 水波开关


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
            waveCenter = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height); // 屏幕坐标归一化
            enabledWave = true;
            waveTime = 0;
        }

        //if (timeCount < timeSpace) {
        //    timeCount += Time.deltaTime;
        //    if (timeCount >= timeSpace) {
        //        timeCount = 0;
        //        waveCenter = new Vector2(0.5f, 0.5f); // 屏幕坐标归一化
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
            { // 水波传播到屏幕外面, 结束水波特效
                enabledWave = false;
            }
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

    private void SetWaveMaterialParams()
    { // 设置水波材质参数
        waveMaterial.SetFloat("_A", A); // 水面质点水平波动振幅
        waveMaterial.SetFloat("_w1", w1); // 水波截面波形角频率(值越大, 波纹越密)
        waveMaterial.SetFloat("_w2", w2); // 水面质点水平波动角频率(值越大, 水波质点振动越快)
        waveMaterial.SetFloat("_t", waveTime); // 水波传播时间
        waveMaterial.SetVector("_o", waveCenter); // 水波中心
        waveMaterial.SetFloat("_waveDist", waveTime * waveSpeed); // 水波传播距离
        waveMaterial.SetFloat("_waveWidth", waveWidth); // 水波宽度(水波在传播时, 后波会消失)
    }
}