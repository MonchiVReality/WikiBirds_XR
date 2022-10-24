using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class takePhotograph : MonoBehaviour
{
    [Header("Photo Taker")]
    [SerializeField] private GameObject photoDisplayArea;
    [SerializeField] private GameObject photoFrame;
    [SerializeField] private Camera camera_;

    int height = 1024;
    int width = 1024;
    int depth = 24;

    private bool viewingPhoto;

    private void Start()
    {  
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Take screenshot
            if (!viewingPhoto)
                StartCoroutine(CapturePhoto());
            else
                removePhoto();
        }
    }

    IEnumerator CapturePhoto()
    {
        yield return new WaitForEndOfFrame();
        showPhoto();
    }

    void showPhoto()
    {
        viewingPhoto = true;
        photoFrame.SetActive(true);
        photoDisplayArea.SetActive(true);

        RenderTexture renderTexture = new RenderTexture(width, height, depth);
        Rect rect = new Rect(0, 0, width, height);
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);

        GetComponent<Camera>().targetTexture = renderTexture;
        GetComponent<Camera>().Render();

        RenderTexture currentRenderTexture = RenderTexture.active;
        RenderTexture.active = renderTexture;
        texture.ReadPixels(rect, 0, 0);
        texture.Apply();

        GetComponent<Camera>().targetTexture = null;
        RenderTexture.active = currentRenderTexture;
        Destroy(renderTexture);

        Sprite sprite = Sprite.Create(texture, rect, Vector2.zero);

        photoDisplayArea.GetComponent<Image>().sprite = sprite;

    }

    void removePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
        photoDisplayArea.SetActive(false);
    }
}
