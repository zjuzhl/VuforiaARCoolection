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

            #pragma vertex vert_img // UnityCG.cginc中定义了vert_img方法, 对vertex和texcoord进行了处理, 输出v2f_img中的pos和uv
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _A; // 水面质点水平波动振幅
            float _w1; // 水波截面波形角频率(值越大, 波纹越密)
            float _w2; // 水面质点水平波动角频率(值越大, 水波质点振动越快)
            float _t; // 水波传播时间
            float2 _o; // 水波中心坐标
            float _waveDist; // 水波传播距离
            float _waveWidth; // 水波宽度(水波在传播时, 后波会消失)

            fixed4 frag(v2f_img i) : SV_Target // 水波uv坐标的计算不能在顶点着色器中进行, 因为屏后处理的顶点只有屏幕的4个角顶点
            {
                float2 vec = i.uv - _o.xy;
                vec.x *= _ScreenParams.x / _ScreenParams.y; // 按照屏幕长宽比进行缩放
                float radius = length(vec); // 距离波中心的半径长度
                float leng = abs(radius - _waveDist);
                float offset = 0;
                if (leng < _waveWidth)
                {
                    offset = _A * sin(_w1 * radius - _w2 * _t) * (1 - leng / _waveWidth);
                }
                return tex2D(_MainTex, i.uv + offset * 0.707); // offset是一维的, uv是二维的, 需要除以根号2, 即乘以0.707
            }

            ENDCG
        }
    }

    Fallback off
}