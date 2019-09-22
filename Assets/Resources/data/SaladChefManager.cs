using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class SaladChefManager : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string str in importedAssets)
        {
            if (str.IndexOf("/orderdata.csv") != -1)
            {
                TextAsset data = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
                string assetfile = str.Replace(".csv", ".asset");
                Orderdata gm = AssetDatabase.LoadAssetAtPath<Orderdata>(assetfile);
                if (gm == null)
                {
                    gm = new Orderdata();
                    AssetDatabase.CreateAsset(gm, assetfile);
                }

                gm.allOrderType = CSVSerializer.Deserialize<Orderdata.OrderDetails>(data.text);

                EditorUtility.SetDirty(gm);
                AssetDatabase.SaveAssets();
#if DEBUG_LOG || UNITY_EDITOR
                Debug.Log("Reimported Asset: " + str);
#endif
            }
        }
    }
}
