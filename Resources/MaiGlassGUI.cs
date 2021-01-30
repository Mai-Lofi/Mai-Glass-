﻿#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class MaiGlassGUI : ShaderGUI
{
    private Font VrchatFont = (Font)Resources.Load(@"segoesc");
    private Texture2D bannerTex = Resources.Load<Texture2D>("bg");
    private Texture2D konimg = Resources.Load<Texture2D>("gg");
    private Texture2D maiTitle = Resources.Load<Texture2D>("Mai_Glass");    
/*
    private MaterialProperty _Color;
    private MaterialProperty MASK;
    private MaterialProperty MASKD;
    private MaterialProperty DTYPE;

    private MaterialProperty _DissolveTex;
    private MaterialProperty _ps;
    private MaterialProperty _glowr;
    private MaterialProperty _maskpow;
    private MaterialProperty _dspow;
    private MaterialProperty _Tile;
    private MaterialProperty _mspeed;
    */
    private MaterialProperty _GlassTint;
    private MaterialProperty _smooth;
    private MaterialProperty _CubeMap;
    private MaterialProperty _mattalic;
    private MaterialProperty _IndexofRefraction;
    private MaterialProperty _ChromaticAberration;
    private MaterialProperty _Opacity;
    private MaterialProperty _emission;
    private MaterialProperty _EmissionStregth;
    private MaterialProperty _NormalTex;
    private MaterialProperty _NormalStrength;
    private MaterialProperty _NormalPan;
    private MaterialProperty _texcoord;
    private MaterialProperty _NoisePan;
    private MaterialProperty _NoiseMapSize;
    private MaterialProperty _NoiseTexture;
    private MaterialProperty _MaiSwitch;
    private MaterialProperty _CubeMapEnable;
    private MaterialProperty __dirty;



    private void DrawBanner(string text1, string text2, string URL)
    {
        GUIStyle rateTxt = new GUIStyle { font = VrchatFont };
        rateTxt.alignment = TextAnchor.LowerRight;
        rateTxt.normal.textColor = new Color(0.9f, 0.9f, 0.9f);
        rateTxt.fontSize = 12;
        rateTxt.padding = new RectOffset(0, 1, 0, 1);

        GUIStyle title = new GUIStyle(rateTxt);
        title.normal.textColor = new Color(1f, 1f, 1f);
        title.alignment = TextAnchor.MiddleCenter;
        title.fontSize = 18;

        EditorGUILayout.BeginVertical("GroupBox");
        var rect = GUILayoutUtility.GetRect(0, int.MaxValue, 35, 35);
        EditorGUI.DrawPreviewTexture(rect, bannerTex, null, ScaleMode.ScaleAndCrop);

        EditorGUI.LabelField(rect, text2, rateTxt);
        EditorGUI.LabelField(rect, text1, title);

        if (GUI.Button(rect, "", new GUIStyle()))
        {
            Application.OpenURL(URL);
        }

        EditorGUILayout.EndVertical();
    }

        private void MaiBannor(string text1, string text2, string URL)
    {
        GUIStyle rateTxt = new GUIStyle { font = VrchatFont };
        rateTxt.alignment = TextAnchor.LowerRight;
        rateTxt.normal.textColor = new Color(0.9f, 0.9f, 0.9f);
        rateTxt.fontSize = 12;
        rateTxt.padding = new RectOffset(0, 1, 0, 1);

        GUIStyle title = new GUIStyle(rateTxt);
        title.normal.textColor = new Color(1f, 1f, 1f);
        title.alignment = TextAnchor.MiddleCenter;
        title.fontSize = 18;

        EditorGUILayout.BeginVertical("GroupBox");
        var rect = GUILayoutUtility.GetRect(0, int.MaxValue, 35, 35);
        EditorGUI.DrawPreviewTexture(rect, konimg, null, ScaleMode.ScaleAndCrop);

        EditorGUI.LabelField(rect, text2, rateTxt);
        EditorGUI.LabelField(rect, text1, title);

        if (GUI.Button(rect, "", new GUIStyle()))
        {
            Application.OpenURL(URL);
        }

        EditorGUILayout.EndVertical();
    }

    public override void OnGUI(MaterialEditor editor, MaterialProperty[] properties)
    {
        Material material = editor.target as Material;

        DrawBanner("Dissolve Mirror Shader", "Open Discord", "https://discord.gg/5PHerbf");

        FindProperties(properties);

        EditorGUILayout.BeginVertical("GroupBox");
        //var rect = GUILayoutUtility.GetRect(0, int.MaxValue, 35, 35);//
        //EditorGUI.DrawPreviewTexture(rect, bannerTex, null, ScaleMode.ScaleAndCrop);//
        Header("Main Glass Settings");

        editor.ShaderProperty(_GlassTint, MakeLabel(_GlassTint)); //maiadd
        editor.ShaderProperty(_smooth, "Smoothness"); //maiadd
        editor.ShaderProperty(_mattalic, "Mattalic (keep low)"); //maiadd


        editor.ShaderProperty(_ChromaticAberration, MakeLabel(_ChromaticAberration)); //maiadd
        editor.ShaderProperty(_IndexofRefraction, MakeLabel(_IndexofRefraction)); //maiadd
        
        MaiSub("Normal");

        editor.ShaderProperty(_NormalStrength, "Normal Strength"); //maiadd
        editor.ShaderProperty(_NormalTex, "Normal Texture"); //maiadd
        editor.ShaderProperty(_NormalPan, "Normal Panning"); //maiadd    

        MaiSub("Reflections"); 

        editor.ShaderProperty(_Opacity, "Opacity"); //maiadd 
        editor.ShaderProperty(_CubeMapEnable, "Enable Cube map"); //maiadd          
        editor.ShaderProperty(_CubeMap, "Reflection Cubemap"); //maiadd

        MaiSub("Emission"); 
        editor.ShaderProperty(_EmissionStregth, "Emission Strength"); //maiadd  
        editor.ShaderProperty(_emission, "Emission Color"); //maiadd 

        MaiSub("Noise");
        editor.ShaderProperty(_MaiSwitch, "Noise Toggle (Req normal strength)"); //maiadd         
        editor.ShaderProperty(_NoiseTexture, "Noise Texture"); //maiadd 
        editor.ShaderProperty(_NoiseMapSize, "Noise Map Size"); //maiadd 
        editor.ShaderProperty(_NoisePan, "Noise Pan Speed"); //maiadd 


        EditorGUILayout.EndVertical();


        DrawBanner("Info", "Open Patreon", "https://www.patreon.com/Mai_Lofi");

        DrawCredits();

        MaiBannor("Dissolve Mirror Shader", "Open Discord", "https://discord.gg/5PHerbf");

       // GUI.DrawTexture(new Rect(10, 10, 60, 60), konimg, ScaleMode.ScaleToFit, true, 10.0F);

        //GUILayout.Box(konimg);
    }

    private static GUIContent MakeLabel(MaterialProperty property, string tooltip = null)
    {
        GUIContent staticLabel = new GUIContent();
        staticLabel.text = property.displayName;
        staticLabel.tooltip = tooltip;
        return staticLabel;
    }

    private void FindProperties(MaterialProperty[] properties)
    {
        /*
        _Color = FindProperty("_Color", properties);
        MASK = FindProperty("MASK", properties);
        MASKD = FindProperty("MASKD", properties);
        DTYPE = FindProperty("DTYPE", properties);
        _DissolveTex = FindProperty("_DissolveTex", properties);
        _glowr = FindProperty("_glowr", properties);
        _ps = FindProperty("_ps", properties);
        _maskpow = FindProperty("_maskpow", properties);
        _dspow = FindProperty("_dspow", properties);
        _Tile = FindProperty("_Tile", properties);
        _mspeed = FindProperty("_mspeed", properties);
        */
        //mai-add
        _GlassTint = FindProperty("_GlassTint", properties);
        _smooth = FindProperty("_smooth", properties);
        _CubeMap = FindProperty("_CubeMap", properties);
        _mattalic = FindProperty("_mattalic", properties);
        _IndexofRefraction = FindProperty("_IndexofRefraction", properties);
        _ChromaticAberration = FindProperty("_ChromaticAberration", properties);
        _Opacity = FindProperty("_Opacity", properties);
        _emission = FindProperty("_emission", properties);
        _EmissionStregth = FindProperty("_EmissionStregth", properties);
        _NormalTex = FindProperty("_NormalTex", properties);
        _NormalStrength = FindProperty("_NormalStrength", properties);
        _NormalPan = FindProperty("_NormalPan", properties);
        _texcoord = FindProperty("_texcoord", properties);
        _NoiseMapSize = FindProperty("_NoiseMapSize", properties);
        _NoisePan = FindProperty("_NoisePan", properties);
        _NoiseTexture = FindProperty("_NoiseTexture", properties);
        _MaiSwitch = FindProperty("_MaiSwitch", properties);
        _CubeMapEnable = FindProperty("_CubeMapEnable", properties);
        __dirty = FindProperty("__dirty", properties);

    }

    private void DrawCredits()
    {
        EditorGUILayout.BeginVertical("GroupBox");

        var TextStyle = new GUIStyle { font = VrchatFont, fontSize = 15, fontStyle = FontStyle.Italic };
        GUILayout.Label("Shader by:", TextStyle);
        GUILayout.Space(2);
        GUILayout.Label("MAIiii", TextStyle);
        GUILayout.Space(6);
        GUILayout.Label("Than:", TextStyle);
        GUILayout.Space(2);
        GUILayout.Label("just meeeee", TextStyle);

        EditorGUILayout.EndVertical();
    }

    private void Header(string name)
    {
        var Style = new GUIStyle { font = VrchatFont, fontSize = 18, fontStyle = FontStyle.Italic, alignment = TextAnchor.MiddleLeft };
        GUILayout.Label(name, Style);
        GUILayout.Space(5);
    }
    private void MaiSub(string name)
    {
        var Style = new GUIStyle { font = VrchatFont, fontSize = 15, fontStyle = FontStyle.Italic, alignment = TextAnchor.MiddleLeft };
        GUILayout.Space(3);
        var rect = GUILayoutUtility.GetRect(0, int.MaxValue, 6, 35);
        EditorGUI.DrawPreviewTexture(rect, bannerTex, null, ScaleMode.ScaleAndCrop);
        GUILayout.Space(2);
        GUILayout.Label(name, Style);
        GUILayout.Space(2);
    }
}
#endif