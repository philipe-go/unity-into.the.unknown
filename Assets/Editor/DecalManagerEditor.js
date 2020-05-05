@CustomEditor (DecalManager)
/*
	Custom inspector for the Decal Manager. Created by Tudor Nita (tudor.cgrats.com)
*/
public class DecalManagerEditor extends Editor {
var showOptions: boolean = true;
var showCombine: boolean = true;
var showUtils: boolean = true;
var options: String[] = ["Delete","Cancel"];
var index: int = 0;

	function OnInspectorGUI () 
	{	
	

				
		if(GUILayout.Button(GUIContent("Create New Decal","Place a new decal as a child of decalManager"),GUILayout.Height(30)))
		{
			Selection.activeObject =  target.NewDecal();

		}
		showOptions = EditorGUILayout.Foldout(showOptions,"Options:");
		if(showOptions)	
		{
			target.decalPrefab = EditorGUILayout.ObjectField("Decal Prefab",target.decalPrefab,GameObject,true,GUILayout.Height(19));
			target.material = EditorGUILayout.ObjectField("Opt. Material",target.material,Material,true,GUILayout.Height(19));
		}
		showUtils = EditorGUILayout.Foldout(showUtils,"Utilities:");
		if(showUtils)
			{
			if(GUILayout.Button(GUIContent("Reset All Decals","Resets all placed decals")))
			{
				Selection.activeObject =  target.ResetAllDecals();
			}
			EditorGUILayout.TextArea("Warning! The delete button removes all \ndecals/ decal groups and all your saved \ndecal meshes!",GUILayout.Height(50));
			if(GUILayout.Button(GUIContent("Delete All Decals","Warning, deletes all saved meshes as well and can not be undone!")))
			{
				
				Debug.Log("There are "+target.meshIndex.ToString()+" registered decal meshes");
				for(var i:int =0;i<target.meshIndex+1;i++)
				{
					AssetDatabase.DeleteAsset("Assets/_SMD/Resources/Meshes/NewEditorMeshes/simpleMeshDecal"+i.ToString()+".Asset");
				}
				target.DeleteAllDecals();
			}
		}
		showCombine = EditorGUILayout.Foldout(showCombine,"Mesh Combine options");
		if(showCombine)
		{
			target.triangleStrips = EditorGUILayout.Toggle("Triangle Strips",target.triangleStrips);
		}
	}

}