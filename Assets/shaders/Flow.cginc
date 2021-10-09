#if !defined(FLOW_INCLUDED)
#define FLOW_INCLUDED

// flow with a distorition along u coordinate and a flow sign in v
float3 FlowUVW (float2 uv, float2 flowVector, float2 jump, float flowOffset, float time, float noise, float waviness, float tiling, bool flowB) {
	float phaseOffset = flowB ? 0.5 : 0;
	float progress = frac(time + phaseOffset);
	float3 uvw;
	uvw.x = uv.x - (flowVector * (progress + flowOffset) + phaseOffset + (time + noise - progress) * jump.x) * waviness;
	uvw.y = uv.y + time;
	uvw.xy *= tiling;
	uvw.z = 1 - abs(1 - 2 * progress);
	return uvw;
}

#endif