Shader "ProjectSynth/PumpedUpOverlayEffect"
{
    Properties
    {
        _Intensity ("Intensity", Range(0, 1)) = 1.0
        _MainColor ("Main Color", Color) = (1.0, 1.0, 1.0)
        _ScrollTexture ("Scroll Texture", 2D) = "white" {}
        _ScrollSpeed ("Scroll Speed", Float) = 3.0
        _BaseColor ("Base Color", Color) = (0.1, 0.5, 0.9, 1.0)
        _BaseColorIntensity ("Base Color Intensity", Range(0, 5)) = 2.0
        _AccentColor ("Accent Color", Color) = (0.4, 0.6, 0.6, 1.0)
        _AccentColorIntensity ("Accent Color Intensity", Range(0, 5)) = 0.5
        _PatternIntensity ("Pattern Intensity", Float) = 3.0
        _PulseSpeed ("Pulse Speed", Float) = 2.0
        _RadiusX ("Radius X", Float) = 0.8
        _RadiusY ("Radius Y", Float) = 0.4
        _EdgeWidth ("Edge Width", Float) = 0.7
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Intensity;
            float3 _MainColor;
            sampler2D _ScrollTexture;
            float4 _ScrollTexture_ST;
            float _ScrollSpeed;
            float4 _BaseColor;
            float _BaseColorIntensity;
            float4 _AccentColor;
            float _AccentColorIntensity;
            float _PatternIntensity;
            float _PulseSpeed;
            float _RadiusX;
            float _RadiusY;
            float _EdgeWidth;

            struct appdata { float4 vertex : POSITION; float2 uv : TEXCOORD0; };
            struct v2f { float2 uv : TEXCOORD0; float4 vertex : SV_POSITION; };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float3 computeScrollLayer(float2 uv)
            {
                float stepX = step(0.5, uv.x);
                float stepY = step(0.5, uv.y);

                float maskBL = (1.0 - stepX) * (1.0 - stepY);
                float maskBR = stepX * (1.0 - stepY);
                float maskTL = (1.0 - stepX) * stepY;
                float maskTR = stepX * stepY;

                float2 localBL = uv * 2.0;
                float2 localBR = float2(1.0 - (uv.x - 0.5) * 2.0, uv.y * 2.0);
                float2 localTL = float2(uv.x * 2.0, 1.0 - (uv.y - 0.5) * 2.0);
                float2 localTR = float2(1.0 - (uv.x - 0.5) * 2.0, 1.0 - (uv.y - 0.5) * 2.0);

                float2 scrollOffset = _Time.y * _ScrollSpeed * 0.01;

                float2 sampleBL = TRANSFORM_TEX(frac(localBL - scrollOffset), _ScrollTexture);
                float2 sampleBR = TRANSFORM_TEX(frac(localBR - scrollOffset), _ScrollTexture);
                float2 sampleTL = TRANSFORM_TEX(frac(localTL - scrollOffset), _ScrollTexture);
                float2 sampleTR = TRANSFORM_TEX(frac(localTR - scrollOffset), _ScrollTexture);

                float4 texBL = tex2D(_ScrollTexture, sampleBL);
                float4 texBR = tex2D(_ScrollTexture, sampleBR);
                float4 texTL = tex2D(_ScrollTexture, sampleTL);
                float4 texTR = tex2D(_ScrollTexture, sampleTR);

                float4 scrollLayer = texBL * maskBL + texBR * maskBR + texTL * maskTL + texTR * maskTR;

                return _MainColor * (scrollLayer.a);
            }

            float3 computePatternColor(float2 fragCoord)
            {
                float2 uv = fragCoord / _ScreenParams.xy;

                float pulse = 0.5 + 0.5 * sin(_Time.y * _PulseSpeed);

                float3 baseContribution = _BaseColor * _BaseColorIntensity;
                float3 accentContribution = _AccentColor * _PatternIntensity * pulse * _AccentColorIntensity;

                float3 scrollLayer = computeScrollLayer(uv);

                return baseContribution + accentContribution + scrollLayer;
            }

            float computeEllipseMask(float2 screenUv)
            {
                float2 centered = (screenUv - 0.5) * float2(_ScreenParams.x / _ScreenParams.y, 1.0);
                float dist = length(centered / float2(_RadiusX, _RadiusY));
                return smoothstep(1.0, 1.0 + _EdgeWidth, dist);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 fragCoord = i.uv * _ScreenParams.xy;

                float3 patternColor = computePatternColor(fragCoord);
                float3 combinedPattern = patternColor;

                float mask = computeEllipseMask(i.uv);

                fixed4 sceneColor = tex2D(_MainTex, i.uv);
                float3 finalColor = lerp(sceneColor.rgb, combinedPattern, mask * _Intensity);

                return fixed4(finalColor, sceneColor.a);
            }
            ENDCG
        }
    }
}
