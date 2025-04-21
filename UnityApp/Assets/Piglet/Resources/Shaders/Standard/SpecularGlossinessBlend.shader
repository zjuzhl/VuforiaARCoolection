﻿// This shader is written to implement a glTF material
// using the "specular glossiness" model (physically based
// rendering).
//
// The specular-glossiness is an extension to the
// glTF described at:
//
// https://github.com/KhronosGroup/glTF/blob/master/extensions/2.0/Khronos/KHR_materials_pbrSpecularGlossiness/README.md

Shader "Piglet/SpecularGlossinessBlend"
{
    Properties
    {
        // The following property corresponds to the `material.doubleSided`
        // flag in glTF format [1].
        //
        // Possible values of this shader property are: 0 = Off, 1 = Front, 2 = Back
        // (see [2]). A value of 2 (Back) is used when `material.doubleSided` is false,
        // and 0 (Off) is used when `material.doubleSided` is true.
        //
        // [1]: https://www.khronos.org/registry/glTF/specs/2.0/glTF-2.0.html#_material_doublesided
        // [2]: https://docs.unity3d.com/ScriptReference/Rendering.CullMode.html

        [Enum(UnityEngine.Rendering.CullMode)] _Cull("Cull", Int) = 2

        // The following properties correspond to basic glTF material properties
        // described at:
        //
        // https://github.com/KhronosGroup/glTF/tree/master/specification/2.0#reference-material

        _normalTexture ("Normal Map", 2D) = "bump" {}

        _occlusionTexture ("Occlusion Map", 2D) = "white" {}

        _emissiveTexture ("Emission Map", 2D) = "black" {}
        _emissiveFactor ("Emissive Factor", Color) = (1, 1, 1, 1)

        // The following properties correspond to specular-glossiness
        // model properties described at:
        //
        // https://github.com/KhronosGroup/glTF/blob/master/extensions/2.0/Khronos/KHR_materials_pbrSpecularGlossiness/README.md

        _diffuseFactor ("Diffuse Factor", Color) = (1, 1, 1, 1)
        _diffuseTexture ("Diffuse Texture", 2D) = "white" {}

        _specularFactor ("Specular Factor", Color) = (1, 1, 1, 1)
        _glossinessFactor ("Glossiness Factor", Range(0,1)) = 1.0
        _specularGlossinessTexture ("Specular Glossiness Texture", 2D) = "white" {}

        // Boolean shader properties.
        //
        // _runtime: "Is this a runtime glTF import?"
        // _linear: "Is the Unity Editor/Player in linear rendering mode?"
        //
        // In the case where both `_runtime` and `_linear`
        // are true (i.e. equal to 1.0), we need to undo the
        // Linear -> sRGB color conversions that UnityWebRequestTexture
        // incorrectly performs on linear textures (e.g. the normal texture).
        // (`UnityWebRequestTexture` assumes that all input
        // textures are sRGB-encoded and does not have a `linear` parameter like
        // `Texture2D.LoadImage`.)
        //
        // Piglet uses `UnityWebRequestTexture` to load textures during
        // runtime glTF imports because it does not stall the main Unity
        // thread during PNG/JPG decompression like `Texture2D.LoadImage`
        // does. In the case of Editor glTF imports, we do not need to
        // make any color corrections because Piglet uses `Texture2D.LoadImage`
        // to load the textures instead.

        _runtime ("Runtime", Int) = 0.0
        _linear ("Linear", Int) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Cull [_Cull]
        LOD 200

        // Add a preliminary shader pass that writes to the Z-buffer but
        // doesn't render any geometry.
        //
        // Without this pass, semi-transparent triangles will be drawn
        // in whatever order they appear in the mesh, rather than
        // in their proper depth-sorted order.  This happens because
        // transparent shaders don't write to the Z-buffer. (The
        // Z-buffer-based approach to depth-culling only works correctly
        // for opaque geometry.)
        //
        // For further background/discussion, see the following links:
        //
        // (1) https://forum.unity.com/threads/render-mode-transparent-doesnt-work-see-video.357853/#post-2315934
        // (2) https://answers.unity.com/questions/609021/how-to-fix-transparent-rendering-problem.html

        Pass { ColorMask 0 }

        CGPROGRAM
        #pragma target 3.0
        #pragma surface surf StandardSpecular alpha:fade noshadow nolightmap nofog nometa nolppv
        #include "SpecularGlossiness.cginc"
        ENDCG
    }
    FallBack "Diffuse"
}
