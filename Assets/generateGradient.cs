using UnityEngine;
using UnityEngine.VFX;

public class generateGradient : MonoBehaviour
{
    public Gradient[] gradients;
    private VisualEffect vfx;


    private void Start()
    {


        vfx = GetComponent<VisualEffect>();
        if (vfx != null && gradients != null && gradients.Length > 0)
        {
            int randomIndex = Random.Range(0, gradients.Length);
            vfx.SetGradient("Gradient", gradients[randomIndex]);
        }
    }
}
