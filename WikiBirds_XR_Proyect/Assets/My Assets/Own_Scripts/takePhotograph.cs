using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

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

    //Photo prefab
    public GameObject picture_;
    private GameObject spawnedPhoto;
    public Transform photoSpawnPoint_;
    bool isSocketActive = false;
    public void changeSocketActive(bool aux = false)
    {
        isSocketActive = aux;
        if (isSocketActive)
            spawnedPhoto.transform.tag = "Photo";
        else
            spawnedPhoto.transform.tag = "Untagged";
    }

    private void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(TakePhoto);
    }

    //private void Update()
    //{
    //    //INPUT ON KEYBOARD
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        //Take screenshot
    //        if (!viewingPhoto)
    //            StartCoroutine(CapturePhoto());
    //        else
    //            removePhoto();
    //    }
    //}

    // XR INPUT
    public void TakePhoto(ActivateEventArgs arg)
    {
        if (!isSocketActive)
            spawnedPhoto = Instantiate(picture_);

        isSocketActive = true;
        spawnedPhoto.SetActive(true);
        spawnedPhoto.transform.position = photoSpawnPoint_.position;
        StartCoroutine(CapturePhoto());
    }

    IEnumerator CapturePhoto()
    {
        yield return new WaitForEndOfFrame();
        showPhoto();
    }

    void showPhoto()
    {
        viewingPhoto = true;
        //photoFrame.SetActive(true);
        //photoDisplayArea.SetActive(true);

        RenderTexture renderTexture = new RenderTexture(width, height, depth);
        Rect rect = new Rect(0, 0, width, height);
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);

        camera_.targetTexture = renderTexture;
        camera_.Render();

        RenderTexture currentRenderTexture = RenderTexture.active;
        RenderTexture.active = renderTexture;
        texture.ReadPixels(rect, 0, 0);
        texture.Apply();

        camera_.targetTexture = null;
        RenderTexture.active = currentRenderTexture;
        Destroy(renderTexture);

        Sprite sprite = Sprite.Create(texture, rect, Vector2.zero);

        //photoDisplayArea.GetComponent<Image>().sprite = sprite;

        spawnedPhoto.GetComponent<MeshRenderer>().material.mainTexture = texture;
    }

    void removePhoto()
    {
        viewingPhoto = false;
        //photoFrame.SetActive(false);
        //photoDisplayArea.SetActive(false);
    }
}
