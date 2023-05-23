using UnityEngine;
using UnityEngine.VFX;

public class CreateNewRenText : MonoBehaviour
{

    // public RenderTexture renderTexture;
    public int textureWidth = 512;
    public int textureHeight = 512;
    public RenderTextureFormat textureFormat = RenderTextureFormat.Default;

    // Start is called before the first frame update
    void Start()
    {
        // Create the RenderTexture
        RenderTexture renderTexture = new RenderTexture(textureWidth, textureHeight, 0, textureFormat);
        renderTexture.Create();

        // Assign the RenderTexture to the "SDF Texture" component if it exists
        SDFTexture sdfTexture = GetComponent<SDFTexture>();
        if (sdfTexture != null)
        {
            sdfTexture.sdf = renderTexture;
            Debug.Log(sdfTexture.sdf);
        }

        // GameObject particleSystem = transform.GetChild(1).gameObject;
        VisualEffect particleSystem = GetComponent<VisualEffect>();
        if (particleSystem != null)
        {
            particleSystem.SetTexture("SDF", renderTexture);
        }
    }

}
