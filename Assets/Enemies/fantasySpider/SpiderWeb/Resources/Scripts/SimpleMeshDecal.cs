using UnityEngine;
using System.Collections;
/*
 Individual decal script. Created by Tudor Nita (tudor.cgrats.com)
 Use the slider to adjust the ground "hug" distance. 
 Click Place when you're happy with the decal's position. 
 Hit reset to .... ummm, reset the decal.
 */
[ExecuteInEditMode]
public class SimpleMeshDecal : MonoBehaviour {
public float groundDist;
int vertCount;
public Mesh decalMesh;
RaycastHit hit;
float lastTime;
Vector3[] verts;
int[] tris;
Vector3[] normals;
Vector2[] newUv;
Vector3[] originalVerts;
bool placed;
bool init;
Transform TR;
public Mesh savedMesh;
public string meshName;
		
	public void Start()
	{
		if(!init)
		{
			decalMesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
			GetMeshInfo();
			TR = transform;
			init = true;
		}
	}
	
	public Mesh PlaceDecal()
	{
		for (int i = 0;i < vertCount;i++) 
		{
			Vector3 raycastPoint = transform.TransformPoint(verts[i]);
			Vector3 relativeUp = TR.TransformDirection(Vector3.up);
			if(Physics.Raycast(raycastPoint,-relativeUp, out hit,10))
			{	
				Vector3 newVertPos = transform.InverseTransformPoint(hit.point);
				verts[i] = new Vector3(newVertPos.x,newVertPos.y+groundDist,newVertPos.z);		
			}
		}
		
		if(!placed)
		{
			Mesh newMesh = new Mesh();
			newMesh.name = meshName;
			newMesh.vertices = verts;
			newMesh.triangles = tris;
			newMesh.normals = normals;
			newMesh.uv = newUv;
			GetComponent<MeshFilter>().sharedMesh = newMesh;
			decalMesh.RecalculateBounds();
			decalMesh.RecalculateNormals();
			placed = true;
			return newMesh;
		}
		else
		{
			savedMesh.vertices = verts;
			savedMesh.triangles = tris;
			savedMesh.normals = normals;
			savedMesh.uv = newUv;
			GetComponent<MeshFilter>().sharedMesh = savedMesh;
			decalMesh.RecalculateBounds();
			decalMesh.RecalculateNormals();
			placed = true;	
			return null;
		}
		//return null;
	}
	
	public void ResetDecal()
	{
		if(placed)
		{
			GetComponent<MeshFilter>().sharedMesh = decalMesh;
			GetMeshInfo();
			//placed = false;
		}
	}
	
	public void GetMeshInfo()
	{
		verts = decalMesh.vertices;
		tris = decalMesh.triangles;
		newUv = decalMesh.uv;
		normals = decalMesh.normals;
		vertCount = verts.Length;	
	}
	
	
}
