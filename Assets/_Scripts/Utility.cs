using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class Utility
{
    [MenuItem("Tools/Create Scriptable Objects")]
    public static void CreateScriptableObjects()
    {
        object[] loadedIcons = Resources.LoadAll("Icons", typeof(Sprite));
        Sprite[] icons = new Sprite[loadedIcons.Length];
        //this
        for (int x = 0; x < loadedIcons.Length; x++)
        {
            icons[x] = (Sprite)loadedIcons[x];
        }

        foreach (Sprite s in icons)
        {
            // https://stackoverflow.com/questions/64056647/making-a-tool-to-edit-scriptableobjects
            Collectible collectible = ScriptableObject.CreateInstance<Collectible>();
            string path = "Assets/Resources/Collectibles/" + s.name + ".asset";
            AssetDatabase.CreateAsset(collectible, path);
            collectible.Create(s);
            // Now flag the object as "dirty" in the editor so it will be saved
            EditorUtility.SetDirty(collectible);
            // And finally, prompt the editor database to save dirty assets, committing your changes to disk.
            AssetDatabase.SaveAssets();
        }
    }

}
