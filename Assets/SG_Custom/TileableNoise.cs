// using System.Reflection;
// using UnityEngine;

// namespace UnityEditor.ShaderGraph
// {
//     [Title("Procedural", "Noise", "Tileable Noise")]
//     public class TileableNoiseNode : CodeFunctionNode
//     {
//         public TileableNoiseNode()
//         {
//             name = "Tileable Noise";
//         }

//         public override string documentationURL
//         {
//             get { return ""; }
//         }

//         protected override MethodInfo GetFunctionToConvert()
//         {
//             return GetType().GetMethod("Unity_TileableNoise", BindingFlags.Static | BindingFlags.NonPublic);
//         }

//         static string Unity_TileableNoise(
//             [Slot(0, Binding.MeshUV0)] Vector2 UV,
//             [Slot(1, Binding.None, 20f, 20f, 20f, 20f)] Vector1 Scale,
//             [Slot(4, Binding.None)] out Vector1 Out)
//         {
//             return @"
// {
//     float t = 0.0;
//     for(int i = 0; i < 3; i++)
//     {
//         float freq = pow(2.0, float(i));
//         float amp = pow(0.5, float(3-i));
//         t += unity_tileNoise(UV*Scale/freq, Scale/freq)*amp;
//     }
//     Out = t;
// }
// ";
//         }

//         public override void GenerateNodeFunction(FunctionRegistry registry, GraphContext graphContext, GenerationMode generationMode)
//         {
//             registry.ProvideFunction("unity_noise_randomValue", s => s.Append(@"
// inline float unity_noise_randomValue (float2 uv)
// {
//     return frac(sin(dot(uv, float2(12.9898, 78.233)))*43758.5453);
// }"));

//             registry.ProvideFunction("unity_noise_interpolate", s => s.Append(@"
// inline float unity_noise_interpolate (float a, float b, float t)
// {
//     return (1.0-t)*a + (t*b);
// }
// "));

//             registry.ProvideFunction("unity_tileNoise", s => s.Append(@"
// inline float unity_tileNoise (float2 uv, float max)
// {
//     float2 i = floor(uv);
//     float2 f = frac(uv);
//     f = f * f * (3.0 - 2.0 * f);

//     uv = abs(frac(uv) - 0.5);
//     float2 c0 = i + float2(0.0, 0.0);
    
//     float2 c1 = i + float2(1.0, 0.0);
//     c1.x = int(c1.x) & int(max);
    
//     float2 c2 = i + float2(0.0, 1.0);
//     c2.y = int(c2.y) & int(max);

//     float2 c3 = i + float2(1.0, 1.0);
//     c3.x = int(c3.x) & int(max);
//     c3.y = int(c3.y) & int(max);

//     float r0 = unity_noise_randomValue(c0);
//     float r1 = unity_noise_randomValue(c1);
//     float r2 = unity_noise_randomValue(c2);
//     float r3 = unity_noise_randomValue(c3);

//     float bottomOfGrid = unity_noise_interpolate(r0, r1, f.x);
//     float topOfGrid = unity_noise_interpolate(r2, r3, f.x);
//     float t = unity_noise_interpolate(bottomOfGrid, topOfGrid, f.y);
//     return t;
// }"));

//             base.GenerateNodeFunction(registry, graphContext, generationMode);
//         }
//     }
// }