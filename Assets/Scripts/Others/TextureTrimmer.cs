using UnityEngine;
using UnityEditor;
using System.IO;

public class TextureTrimmer : EditorWindow
{
    [MenuItem("Tools/Trim Selected Sprites")]
    static void TrimSelected()
    {
        foreach (var obj in Selection.objects)
        {
            if (obj is not Texture2D texture) continue;

            string path = AssetDatabase.GetAssetPath(texture);
            TextureImporter importer = (TextureImporter)AssetImporter.GetAtPath(path);

            // Face textura readable temporar
            importer.isReadable = true;
            importer.SaveAndReimport();

            Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            Color[] pixels = tex.GetPixels();
            int w = tex.width, h = tex.height;

            // Gaseste marginile sprite-ului efectiv
            int minX = w, maxX = 0, minY = h, maxY = 0;
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                    if (pixels[y * w + x].a > 0.05f)
                    {
                        if (x < minX) minX = x;
                        if (x > maxX) maxX = x;
                        if (y < minY) minY = y;
                        if (y > maxY) maxY = y;
                    }

            if (minX > maxX || minY > maxY) continue;

            // Taie la dimensiunea efectiva
            int newW = maxX - minX + 1;
            int newH = maxY - minY + 1;
            Texture2D trimmed = new Texture2D(newW, newH);
            trimmed.SetPixels(tex.GetPixels(minX, minY, newW, newH));
            trimmed.Apply();

            // Salveaza
            File.WriteAllBytes(path, trimmed.EncodeToPNG());
            AssetDatabase.ImportAsset(path);

            Debug.Log($"Trimmed: {obj.name} → {newW}x{newH}");
        }

        AssetDatabase.Refresh();
        Debug.Log("Done!");
    }
}