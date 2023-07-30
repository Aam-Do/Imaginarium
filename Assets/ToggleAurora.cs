using UnityEngine;
using UnityEngine.VFX;
using System.Collections;
using System.Collections.Generic;

public class ToggleAurora : MonoBehaviour
{
    public int playerIndex = 0;
    public KinectInterop.JointType joint = KinectInterop.JointType.HandRight;
    public KinectInterop.JointType joint2 = KinectInterop.JointType.HandLeft;

    public GameObject spirit;
    public GameObject aurora;

    // Duration of the fade-out effect in seconds
    public float fadeOutDuration = 3f;
    public float fadeInDuration = 3f;

    private bool isFading = false;

    private List<float> startAlphaValues = new List<float>();
    private List<Material> childMaterials = new List<Material>();

    // Start is called before the first frame update
    void Start()
    {
        // Get the materials from all children of the aurora GameObject
        Renderer[] auroraChildrenRenderers = aurora.GetComponentsInChildren<Renderer>();
        childMaterials = new List<Material>(auroraChildrenRenderers.Length);

        foreach (Renderer renderer in auroraChildrenRenderers)
        {
            Material material = renderer.material;
            childMaterials.Add(material);
            startAlphaValues.Add(material.GetFloat("_Alpha"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpawnPlants spawnPlantsScript = spirit.GetComponent<SpawnPlants>();
        VisualEffect spiritVFX = spirit.GetComponent<VisualEffect>();

        KinectManager manager = KinectManager.Instance;
        if (manager && manager.IsInitialized())
        {
            if (manager.IsUserDetected(playerIndex))
            {
                long userId = manager.GetUserIdByIndex(playerIndex);

                if (manager.IsJointTracked(userId, (int)joint) && manager.IsJointTracked(userId, (int)joint2))
                {
                    spawnPlantsScript.spawn = true;
                    spiritVFX.enabled = true;
                    if (!isFading && aurora.activeSelf == true)
                    {
                        StartCoroutine(FadeOutAurora());
                        isFading = true;
                    }
                }
                else
                {
                    if (!isFading && aurora.activeSelf == false)
                    {
                        StartCoroutine(FadeInAurora());
                        isFading = true;
                    }
                    spiritVFX.enabled = false;
                    spawnPlantsScript.spawn = false;
                }
            }
            else
            {
                    if (!isFading && aurora.activeSelf == false)
                    {
                        StartCoroutine(FadeInAurora());
                        isFading = true;
                    }
                spiritVFX.enabled = false;
                spawnPlantsScript.spawn = false;
            }
        }
    }

    // Coroutine for fading out the aurora
    private IEnumerator FadeOutAurora()
    {
        float startTime = Time.time;

        while (Time.time < startTime + fadeOutDuration)
        {
            float normalizedTime = (Time.time - startTime) / fadeOutDuration;

            // Update the alpha property of each child material
            for (int i = 0; i < childMaterials.Count; i++)
            {
                float newAlpha = Mathf.Lerp(startAlphaValues[i], 0f, normalizedTime);
                childMaterials[i].SetFloat("_Alpha", newAlpha);
            }

            yield return null;
        }

        // Ensure that the alpha value is set to 0 for all child materials
        foreach (Material material in childMaterials)
        {
            material.SetFloat("_Alpha", 0f);
        }

        // Deactivate the aurora GameObject
        aurora.SetActive(false);
        isFading = false;
    }
    private IEnumerator FadeInAurora()
    {
        float startTime = Time.time;

        while (Time.time < startTime + fadeInDuration)
        {
            float normalizedTime = (Time.time - startTime) / fadeInDuration;

            // Update the alpha property of each child material
            for (int i = 0; i < childMaterials.Count; i++)
            {
                float newAlpha = Mathf.Lerp(0f, startAlphaValues[i], normalizedTime);
                childMaterials[i].SetFloat("_Alpha", newAlpha);
            }

            yield return null;
        }

        // Ensure that the alpha value is set to the startAlphaValue for all child materials
        for (int i = 0; i < childMaterials.Count; i++)
        {
            childMaterials[i].SetFloat("_Alpha", startAlphaValues[i]);
        }

        // Reactivate the aurora GameObject
        aurora.SetActive(true);
        isFading = true;
    }
}
