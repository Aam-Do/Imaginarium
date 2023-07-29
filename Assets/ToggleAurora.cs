using UnityEngine;
using UnityEngine.VFX;

public class ToggleAurora : MonoBehaviour
{
    public int playerIndex = 0;
    public KinectInterop.JointType joint = KinectInterop.JointType.HandRight;
    public KinectInterop.JointType joint2 = KinectInterop.JointType.HandLeft;

    public GameObject spirit;
    public GameObject aurora;

    // Start is called before the first frame update
    void Start()
    {
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
                    aurora.SetActive(false);
                }
                else
                {
                    aurora.SetActive(true);
                    spiritVFX.enabled = false;
                    spawnPlantsScript.spawn = false;
                }
            }
            else
            {
                aurora.SetActive(true);
                spiritVFX.enabled = false;
                spawnPlantsScript.spawn = false;
            }
        }
    }
}
