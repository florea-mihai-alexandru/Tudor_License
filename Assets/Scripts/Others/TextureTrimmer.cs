using UnityEngine;
using UnityEditor;
using System.IO;

public class TextureTrimmer : EditorWindow
{
    private int targetWidth = 64;
    private int targetHeight = 64;
    private float alphaThreshold = 0.05f;
    private bool centerContent = true;

    [MenuItem("Tools/Texture Trimmer")]
    static void OpenWindow()
    {
        var window = GetWindow<TextureTrimmer>("Texture Trimmer");
        window.minSize = new Vector2(280, 220);
    }

    private void OnGUI()
    {
        GUILayout.Label("Trim Selected Sprites", EditorStyles.boldLabel);
        EditorGUILayout.Space(8);

        GUILayout.Label($"Sprite-uri selectate: {CountSelectedTextures()}", EditorStyles.miniLabel);
        EditorGUILayout.Space(8);

        targetWidth = EditorGUILayout.IntField("Target Width", targetWidth);
        targetHeight = EditorGUILayout.IntField("Target Height", targetHeight);

        EditorGUILayout.Space(4);
        alphaThreshold = EditorGUILayout.Slider("Alpha Threshold", alphaThreshold, 0f, 1f);

        EditorGUILayout.Space(4);
        centerContent = EditorGUILayout.Toggle("Centrare in canvas", centerContent);

        EditorGUILayout.Space(12);

        EditorGUILayout.HelpBox(
            "Gaseste continutul efectiv (non-transparent) al fiecarui sprite, " +
            "apoi il plaseaza pe un canvas de dimensiunea aleasa, centrat.\n" +
            "Daca continutul e mai mare decat Target Width/Height, va fi taiat la margini.",
            MessageType.Info);

        EditorGUILayout.Space(12);

        GUI.enabled = CountSelectedTextures() > 0 && targetWidth > 0 && targetHeight > 0;
        if (GUILayout.Button("Trim la dimensiunea selectata", GUILayout.Height(32)))
        {
            TrimSelected();
        }
        GUI.enabled = true;
    }

    private static int CountSelectedTextures()
    {
        int count = 0;
        foreach (var obj in Selection.objects)
            if (obj is Texture2D) count++;
        return count;
    }

    private void TrimSelected()
    {
        int processed = 0;

        foreach (var obj in Selection.objects)
        {
            if (obj is not Texture2D texture) continue;

            string path = AssetDatabase.GetAssetPath(texture);
            TextureImporter importer = (TextureImporter)AssetImporter.GetAtPath(path);

            bool wasReadable = importer.isReadable;
            if (!wasReadable)
            {
                importer.isReadable = true;
                importer.SaveAndReimport();
            }

            Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            Color[] pixels = tex.GetPixels();
            int w = tex.width, h = tex.height;

            // Gaseste marginile continutului efectiv (non-transparent)
            int minX = w, maxX = -1, minY = h, maxY = -1;
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (pixels[y * w + x].a > alphaThreshold)
                    {
                        if (x < minX) minX = x;
                        if (x > maxX) maxX = x;
                        if (y < minY) minY = y;
                        if (y > maxY) maxY = y;
                    }
                }
            }

            // Textura e complet transparenta, nimic de taiat
            if (maxX < minX || maxY < minY)
            {
                Debug.LogWarning($"Skip (complet transparent): {obj.name}");
                continue;
            }

            int contentW = maxX - minX + 1;
            int contentH = maxY - minY + 1;

            // Extrage doar continutul efectiv
            Color[] content = tex.GetPixels(minX, minY, contentW, contentH);

            // Creeaza canvas-ul final, complet transparent
            Texture2D result = new Texture2D(targetWidth, targetHeight, TextureFormat.RGBA32, false);
            Color[] blank = new Color[targetWidth * targetHeight];
            for (int i = 0; i < blank.Length; i++) blank[i] = new Color(0, 0, 0, 0);
            result.SetPixels(blank);

            // Calculeaza offsetul (centrat sau din coltul stanga-jos)
            int offsetX = centerContent ? (targetWidth - contentW) / 2 : 0;
            int offsetY = centerContent ? (targetHeight - contentH) / 2 : 0;

            // Copiaza pixel cu pixel, respectand limitele canvas-ului
            // (functioneaza corect si cand continutul e mai mare decat target -> se taie la margini)
            for (int y = 0; y < contentH; y++)
            {
                int destY = y + offsetY;
                if (destY < 0 || destY >= targetHeight) continue;

                for (int x = 0; x < contentW; x++)
                {
                    int destX = x + offsetX;
                    if (destX < 0 || destX >= targetWidth) continue;

                    result.SetPixel(destX, destY, content[y * contentW + x]);
                }
            }

            result.Apply();

            File.WriteAllBytes(path, result.EncodeToPNG());

            // Restaureaza setarea de isReadable daca a fost schimbata
            if (!wasReadable)
            {
                importer.isReadable = false;
            }

            AssetDatabase.ImportAsset(path);

            Debug.Log($"Trimmed: {obj.name} → continut {contentW}x{contentH} pe canvas {targetWidth}x{targetHeight}");
            processed++;
        }

        AssetDatabase.Refresh();
        Debug.Log($"Done! {processed} sprite-uri procesate.");
    }
}