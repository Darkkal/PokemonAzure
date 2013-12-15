float4x4 World;
float4x4 View;
float4x4 Projection;
sampler ColorTextureSampler : register(s0);

// TODO: add effect parameters here.

struct VertexShaderInput
{
    float4 Position : POSITION0;
	float3 Normal	: NORMAL0;
	float2 TexCoord1	: TEXCOORD0;
	float2 TexCoord2	: TEXCOORD1; 

    // TODO: add input channels such as texture
    // coordinates and vertex colors here.
};

struct VertexShaderSimpleInput
{
	float4 Position : POSITION0;
	float3 Normal	: NORMAL0;
	float2 TexCoord1	: TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float2 TexCoord1 : TEXCOORD0;
	float2 TexCoord2	: TEXCOORD1; 

    // TODO: add vertex shader outputs such as colors and texture
    // coordinates here. These values will automatically be interpolated
    // over the triangle, and provided as input to your pixel shader.
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

    output.TexCoord1 = input.TexCoord1;
	output.TexCoord2 = input.TexCoord2;

    return output;
}

VertexShaderOutput SimpleVertexShaderFunction(VertexShaderSimpleInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

    output.TexCoord1 = input.TexCoord1;
	output.TexCoord2 = input.TexCoord1;

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    float4 Color = tex2D(ColorTextureSampler, input.TexCoord2);
	if(Color.a < 0.1f) //this statement clops (I am keeping this typo in for my own amusement)
	{				   //the pixel to be drawn if the transparency is below the threshold
		clip(-1);
	}
	return Color;
}

technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.

        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}

technique Technique2
{
    pass Pass1
    {
        // TODO: set renderstates here.

        VertexShader = compile vs_2_0 SimpleVertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
