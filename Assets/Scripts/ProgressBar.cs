using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
    public float Fill = 0;
    public Image Mask;

    // Update is called once per frame
    void Update()
    {
        Mask.fillAmount = Fill;
    }
}
