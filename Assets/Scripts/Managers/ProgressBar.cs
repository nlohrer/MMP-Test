using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour // pur die progress bar für abilities
{
    public float Fill = 0;
    public Image Mask;

    // Update is called once per frame
    void Update()
    {
        Mask.fillAmount = 1 - Fill;
    }
    
}
