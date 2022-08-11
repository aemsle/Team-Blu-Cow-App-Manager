using UnityEditor;
using UnityEngine;
using System.Collections;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Text;

// WIP Module asset creator
public class CreateModule : EditorWindow
{
    private string moduleNameBuffer;

    //     [MenuItem("Window/Module Creator")]
    //     public static void OpenWindow()
    //     {
    //         GetWindow<CreateModule>("Module Creator");
    //     }

    private void OnGUI()
    {
        GUILayout.Label("Module Name");
        moduleNameBuffer = GUILayout.TextField(moduleNameBuffer);
        if (GUILayout.Button("Create Module Script"))
        {
            CreateModuleScript();
        }

        if (GUILayout.Button("Create Module Prefab"))
        {
            CreateModulePrefab();
        }
    }

    private void CreateModuleScript()
    {
        //Author: APMIX
        if (moduleNameBuffer.Length == 0 || Type.GetType(moduleNameBuffer, false) != null)
        {
            return;
        }

        if (!AssetDatabase.IsValidFolder("Assets/Resources"))
            AssetDatabase.CreateFolder("Assets", "Resources");
        if (!AssetDatabase.IsValidFolder("Assets/Resources/ModuleScripts"))
            AssetDatabase.CreateFolder("Assets/Resources", "ModuleScripts");

        // remove whitespace and minus
        moduleNameBuffer = moduleNameBuffer.Replace(" ", "_");
        moduleNameBuffer = moduleNameBuffer.Replace("-", "_");

        string copyPath = $"Assets/Resources/ModuleScripts/{moduleNameBuffer}.cs";
        if (File.Exists(copyPath) == false)
        { // do not overwrite
            using (StreamWriter outfile =
                new StreamWriter(copyPath))
            {
                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine("using System.Collections;");
                outfile.WriteLine("");
                outfile.WriteLine("namespace blu");
                outfile.WriteLine("{");
                outfile.WriteLine(" public class " + moduleNameBuffer + " : Module");
                outfile.WriteLine(" {");
                outfile.WriteLine(" }");
                outfile.WriteLine("}");
            }//File written
        }
        AssetDatabase.Refresh();
    }

    private async void CreateModulePrefab()
    {
        Debug.Log("Creating Prefab...");
        if (moduleNameBuffer.Length == 0)
        {
            Debug.Log("Failed to create prefab");
            return;
        }

        if (!AssetDatabase.IsValidFolder("Assets/Resources"))
            AssetDatabase.CreateFolder("Assets", "Resources");
        if (!AssetDatabase.IsValidFolder("Assets/Resources/ModulePrefabs"))
            AssetDatabase.CreateFolder("Assets/Resources", "ModulePrefabs");

        GameObject newModule = new GameObject();

        Type type = Type.GetType($"blu.{moduleNameBuffer}");

        newModule.AddComponent(Type.GetType($"blu.{moduleNameBuffer}"));
        newModule.name = $"blu.{moduleNameBuffer}";
        Debug.Log("Attempting to save perfab...");

        while (EditorApplication.isCompiling)
        {
            await Task.Yield();
        }

        if (AssetDatabase.IsValidFolder("Assets/Resources/ModulePrefabs"))
            PrefabUtility.SaveAsPrefabAsset(newModule, "Assets/Resources/ModulePrefabs/" + newModule.name + ".prefab");

        DestroyImmediate(newModule);
    }
}