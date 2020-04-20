using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
// Replaces Unity terrain trees with prefab GameObject.
// http://answers.unity3d.com/questions/723266/converting-all-terrain-trees-to-gameobjects.html
[ExecuteInEditMode]
public class TreeReplacerS : EditorWindow
{
    [Header("Settings")]
    public GameObject tree;
    public GameObject tree1;
    public GameObject tree2;
    public GameObject tree3;
    public GameObject tree4;

    private List<GameObject> trees;

    [Header("References")]
    public Terrain _terrain;
    //============================================
    [MenuItem("Window/My/TreeReplacer")]
    static void Init()
    {
        TreeReplacerS window = (TreeReplacerS)GetWindow(typeof(TreeReplacerS));
    }
    void OnGUI()
    {
        _terrain = (Terrain)EditorGUILayout.ObjectField(_terrain, typeof(Terrain), true);

        tree = (GameObject)EditorGUILayout.ObjectField(tree, typeof(GameObject), true);
        tree1 = (GameObject)EditorGUILayout.ObjectField(tree1, typeof(GameObject), true);
        tree2 = (GameObject)EditorGUILayout.ObjectField(tree2, typeof(GameObject), true);
        tree3 = (GameObject)EditorGUILayout.ObjectField(tree3, typeof(GameObject), true);
        tree4 = (GameObject)EditorGUILayout.ObjectField(tree4, typeof(GameObject), true);

        trees = new List<GameObject>();
        trees.Add(tree);
        trees.Add(tree1);
        trees.Add(tree2);
        trees.Add(tree3);
        trees.Add(tree4);

        if (GUILayout.Button("Convert to objects"))
        {
            Convert();
        }
        if (GUILayout.Button("Clear generated trees"))
        {
            Clear();
        }
    }
    //============================================
    public void Convert()
    {
        TerrainData data = _terrain.terrainData;
        float width = data.size.x;
        float height = data.size.z;
        float y = data.size.y;
        // Create parent
        GameObject parent = GameObject.Find("TREES_GENERATED");
        if (parent == null)
        {
            parent = new GameObject("TREES_GENERATED");
        }
        // Create trees
        foreach (TreeInstance tree in data.treeInstances)
        {
            Vector3 position = new Vector3(tree.position.x * width, tree.position.y * y, tree.position.z * height);
            // Vector3 position = new Vector3(tree.position.x * data.detailWidth - (data.size.x / 2), tree.position.y * y - (data.size.y / 2), tree.position.z * data.detailHeight - (data.size.z / 2));
            Instantiate(trees[tree.prototypeIndex], position, Quaternion.identity, parent.transform);
            // Instantiate(_tree, position, Quaternion.identity, parent.transform);
        }
    }
    public void Clear()
    {
        DestroyImmediate(GameObject.Find("TREES_GENERATED"));
    }
}
