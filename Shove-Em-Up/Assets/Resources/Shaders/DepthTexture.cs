using UnityEngine;

[ExecuteInEditMode]
public class DepthTexture : MonoBehaviour
{

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam)
            cam.depthTextureMode = DepthTextureMode.Depth;
        else
            Debug.LogError("No camera component found on the Game object");
    }

}
