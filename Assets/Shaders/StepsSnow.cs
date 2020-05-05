using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepsSnow : MonoBehaviour
{
    //public Camera camera;

    [Range(1, 20)]
    public float brushSize;
    [Range(0, 1)]
    public float brushStrength;

    private RenderTexture splatMap;

    public Shader drawShader;
    private Material snowMaterial, drawMaterial;
    public GameObject terrain;
    public Transform[] feet;

    //private RaycastHit hit;
    
    private RaycastHit groundHit;
    private int layerMask;

    private void Start()
    {
        layerMask = LayerMask.GetMask("Ground");
        drawMaterial = new Material(drawShader);
        snowMaterial = terrain.GetComponent<MeshRenderer>().material;
        drawMaterial.SetVector("_Color", Color.red);

        splatMap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        snowMaterial.SetTexture("_Splat", splatMap);
    }

    private void Update()
    {
        for (int i = 0; i < feet.Length; i++)
        {
            if (Physics.Raycast(feet[i].position, -Vector3.up, out groundHit, .5f, layerMask))
            {
                drawMaterial.SetVector("_Coordinate", new Vector4(groundHit.textureCoord.x, groundHit.textureCoord.y, 0, 0));
                drawMaterial.SetFloat("_Strength", brushStrength);
                drawMaterial.SetFloat("_Size", brushSize);
                RenderTexture temp = RenderTexture.GetTemporary(splatMap.width, splatMap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(splatMap, temp);
                Graphics.Blit(temp, splatMap, drawMaterial);
                RenderTexture.ReleaseTemporary(temp);
            }
        }
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, 256, 256), splatMap, ScaleMode.ScaleToFit, false, 1);
    }

}
