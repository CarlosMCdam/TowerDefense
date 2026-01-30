using UnityEngine;
using UnityEditor;
using System.IO;

public class TexturizarSlices : EditorWindow
{
    Texture2D spriteSheet;

    [MenuItem("Tools/Texturizar Slices")]
    static void Init()
    {
        TexturizarSlices window = (TexturizarSlices)EditorWindow.GetWindow(typeof(TexturizarSlices));
        window.titleContent = new GUIContent("Texturizar Slices");
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Extraer sprites sliceados a texturas PNG", EditorStyles.boldLabel);
        spriteSheet = (Texture2D)EditorGUILayout.ObjectField("Sprite Sheet", spriteSheet, typeof(Texture2D), false);

        if (spriteSheet == null)
        {
            EditorGUILayout.HelpBox("Arrastra aquí una textura sliceada (Sprite Mode: Multiple).", MessageType.Info);
            return;
        }

        if (GUILayout.Button("Exportar sprites a PNG"))
        {
            ExportSlices(spriteSheet);
        }
    }

    void ExportSlices(Texture2D texture)
    {
        string sourcePath = AssetDatabase.GetAssetPath(texture);
        string directory = Path.GetDirectoryName(sourcePath);
        string exportDir = Path.Combine(directory, texture.name + "_Texturas");

        if (!Directory.Exists(exportDir))
            Directory.CreateDirectory(exportDir);

        // Guardar configuración original
        TextureImporter importer = AssetImporter.GetAtPath(sourcePath) as TextureImporter;
        bool originalReadable = importer.isReadable;
        TextureImporterType originalType = importer.textureType;
        SpriteImportMode originalSpriteMode = importer.spriteImportMode;
        var originalCompression = importer.textureCompression;

        // Hacer legible temporalmente
        if (!importer.isReadable)
        {
            importer.isReadable = true;
            importer.textureCompression = TextureImporterCompression.Uncompressed;
            importer.SaveAndReimport();
        }

        // Obtener los sprites sliceados
        Object[] assets = AssetDatabase.LoadAllAssetRepresentationsAtPath(sourcePath);
        int exportedCount = 0;

        foreach (var asset in assets)
        {
            if (asset is Sprite sprite)
            {
                // Evitar redondeos peligrosos
                int x = Mathf.FloorToInt(sprite.rect.x);
                int y = Mathf.FloorToInt(sprite.rect.y);
                int w = Mathf.CeilToInt(sprite.rect.width);
                int h = Mathf.CeilToInt(sprite.rect.height);

                // Evitar errores de borde fuera de textura
                x = Mathf.Clamp(x, 0, texture.width - 1);
                y = Mathf.Clamp(y, 0, texture.height - 1);
                w = Mathf.Clamp(w, 1, texture.width - x);
                h = Mathf.Clamp(h, 1, texture.height - y);

                // Crear textura nueva
                Texture2D newTex = new Texture2D(w, h, TextureFormat.RGBA32, false);
                Color[] pixels = sprite.texture.GetPixels(x, y, w, h);
                newTex.SetPixels(pixels);
                newTex.Apply();

                // Exportar PNG
                byte[] pngData = newTex.EncodeToPNG();
                string filePath = Path.Combine(exportDir, sprite.name + ".png");
                File.WriteAllBytes(filePath, pngData);

                // Configurar textura exportada
                AssetDatabase.ImportAsset(filePath, ImportAssetOptions.ForceUpdate);
                TextureImporter newImporter = AssetImporter.GetAtPath(filePath) as TextureImporter;
                if (newImporter != null)
                {
                    newImporter.textureType = TextureImporterType.Default;
                    newImporter.filterMode = FilterMode.Point;
                    newImporter.textureCompression = TextureImporterCompression.Uncompressed;
                    newImporter.mipmapEnabled = false;
                    newImporter.alphaIsTransparency = true;
                    newImporter.SaveAndReimport();
                }

                exportedCount++;
            }
        }

        // Restaurar configuración original
        importer.isReadable = originalReadable;
        importer.textureType = originalType;
        importer.spriteImportMode = originalSpriteMode;
        importer.textureCompression = originalCompression;
        importer.SaveAndReimport();

        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Exportación completa ?",
            $"{exportedCount} sprites exportados en:\n{exportDir}", "Aceptar");
    }
}
