 #ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

void CalculateMainLight_float(float3 WorldPos, out float3 Direction, out float3 Colour, out half DistanceAtten, out half ShadowAtten) {
	
#if SHADERGRAPH_PREVIEW
	Direction = float3(0.5, 0.5, 0);
	Colour = 1;
	DistanceAtten = 1;
	ShadowAtten = 1;
#else
	#if SHADOWS_SCREEN
		half4 clipPos = TransformWorldToHClip(WorldPos);
		half4 shadowCoord = ComputeScreenPos(clipPos);
	#else
		half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
	#endif
	
	Light mainLight = GetMainLight(0);
	Direction = mainLight.direction;
	Colour = mainLight.color;
    DistanceAtten = mainLight.distanceAttenuation;
    ShadowAtten = mainLight.shadowAttenuation;	
#endif
}

#endif