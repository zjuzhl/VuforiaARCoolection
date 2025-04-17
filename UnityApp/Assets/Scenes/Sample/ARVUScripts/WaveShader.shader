Shader "Custom/WaterWave" 
{
    Properties 
    {
        _MainTex ("mainTex", 2D) = "white" {}
    }
 
    SubShader 
    {
        Pass
        {
            ZTest Always
            Cull Off
            ZWrite Off
            Fog { Mode off }
 
            CGPROGRAM

            #pragma vertex vert_img // UnityCG.cginc�ж�����vert_img����, ��vertex��texcoord�����˴���, ���v2f_img�е�pos��uv
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _A; // ˮ���ʵ�ˮƽ�������
            float _w1; // ˮ�����沨�ν�Ƶ��(ֵԽ��, ����Խ��)
            float _w2; // ˮ���ʵ�ˮƽ������Ƶ��(ֵԽ��, ˮ���ʵ���Խ��)
            float _t; // ˮ������ʱ��
            float2 _o; // ˮ����������
            float _waveDist; // ˮ����������
            float _waveWidth; // ˮ�����(ˮ���ڴ���ʱ, �󲨻���ʧ)

            fixed4 frag(v2f_img i) : SV_Target // ˮ��uv����ļ��㲻���ڶ�����ɫ���н���, ��Ϊ������Ķ���ֻ����Ļ��4���Ƕ���
            {
                float2 vec = i.uv - _o.xy;
                vec.x *= _ScreenParams.x / _ScreenParams.y; // ������Ļ����Ƚ�������
                float radius = length(vec); // ���벨���ĵİ뾶����
                float leng = abs(radius - _waveDist);
                float offset = 0;
                if (leng < _waveWidth)
                {
                    offset = _A * sin(_w1 * radius - _w2 * _t) * (1 - leng / _waveWidth);
                }
                return tex2D(_MainTex, i.uv + offset * 0.707); // offset��һά��, uv�Ƕ�ά��, ��Ҫ���Ը���2, ������0.707
            }

            ENDCG
        }
    }

    Fallback off
}