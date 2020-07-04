Shader "Custom/2dInvert"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Threshold ("Threshold", Range(0., 1.)) = 0
		_Threshold2 ("Threshold2", Range(0., 1.)) = 0
		_Brightness ("Brightness", Range(-1., 1.)) = 0

	   _TintColor1 ("Pigment Over the limit", Color)=(1.,1.,1.,1)
	   _TintColor2 ("InvertColor Limit", Color)=(1.,1.,1.,1)
	   _TintColor3 ("Pigment Under the limit", Color)=(1.,1.,1.,1)

	   _TintMaskInputColor ("Tint MaskInput", Color)=(1.,1.,1.,1)
	   	_TintMaskOutputColor ("Tint MaskOutput", Color)=(1.,1.,1.,1)
		  _TintBeltInputColor ("Tint BeltInput", Color)=(1.,1.,1.,1)
	   	_TintBeltOutputColor ("Tint BeltOutput", Color)=(1.,1.,1.,1)
			  _TintBeltInputColor2 ("Tint BeltInput2", Color)=(1.,1.,1.,1)
	   	_TintBeltOutputColor2 ("Tint BeltOutput2", Color)=(1.,1.,1.,1)
			  _TintBeltInputColor3 ("Tint BeltInput3", Color)=(1.,1.,1.,1)
	   	_TintBeltOutputColor3 ("Tint BeltOutput3", Color)=(1.,1.,1.,1)
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha
 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
           
            #include "UnityCG.cginc"
 
            sampler2D _MainTex;
            float _Threshold;
			float _Threshold2;

			fixed4 _TintColor1;

			fixed4 _TintColor2;
			fixed4 _TintColor3;

 			fixed4 _TintMaskInputColor;
			fixed4 _TintMaskOutputColor;

			fixed4 _TintBeltInputColor;
			fixed4 _TintBeltOutputColor;
			
			fixed4 _TintBeltInputColor2;
			fixed4 _TintBeltOutputColor2;

			fixed4 _TintBeltInputColor3;
			fixed4 _TintBeltOutputColor3;

			float _Brightness;

            fixed4 frag (v2f_img i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
			
				if(all(col.rgb==_TintMaskInputColor.rgb))
				{
				col.rgb = _TintMaskOutputColor.rgb;
                return col;
				}
				if(all(col.rgb==_TintBeltInputColor.rgb))
				{
				col.rgb = _TintBeltOutputColor.rgb;
                return col;
				}
				if(all(col.rgb==_TintBeltInputColor2.rgb))
				{
				col.rgb = _TintBeltOutputColor2.rgb;
                return col;
				}
				if(all(col.rgb==_TintBeltInputColor3.rgb))
				{
				col.rgb = _TintBeltOutputColor3.rgb;
                return col;
				}
				else
				{
					if(all(col.rgb>=_TintColor2.rgb))
					{
					col.rgb = abs(_Threshold - col.rgb)*(_TintColor1+_Brightness);
					return col;
					}
					else
					{
						if(all(col.rgb<_TintColor2.rgb))
						{
						col.rgb = abs(_Threshold2 - col.rgb)*_TintColor3;
						return col;
						}
					}
				}
				

				
				return col;
              
            }
            ENDCG
        }
    }
}