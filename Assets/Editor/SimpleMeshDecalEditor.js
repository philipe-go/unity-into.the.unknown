@CustomEditor (SimpleMeshDecal)
/*
	Custom inspector for the SimpleMeshDecal script. Created by Tudor Nita (tudor.cgrats.com)
*/
public class SimpleMeshDecalEditor extends Editor {

	function OnInspectorGUI () 
	{
		target.groundDist = EditorGUILayout.Slider("Ground Dist.",target.groundDist,0.05f,0.3f);
		if(GUILayout.Button("Place Decal"))
		{
			if(!target.savedMesh)
			{
				var newMesh: Mesh =	target.PlaceDecal();
				AssetDatabase.CreateAsset(newMesh,"Assets/_SMD/Resources/Meshes/NewEditorMeshes/"+target.meshName+".Asset");
				AssetDatabase.SaveAssets();
				target.savedMesh = AssetDatabase.LoadAssetAtPath("Assets/_SMD/Resources/Meshes/NewEditorMeshes/"+target.meshName+".Asset",Mesh);
			}
			else
			target.PlaceDecal();
				
		}
		if(GUILayout.Button("Reset Decal Position"))
		{
			target.ResetDecal();
		}
		//DrawDefaultInspector();
	}

}