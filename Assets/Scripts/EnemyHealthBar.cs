using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;
    [SerializeField] private Image fill;

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }

    private void setHealthBarPosition()
    {
        transform.parent.rotation = camera.transform.rotation;
        target.position = target.position;
    }

    private void showHealthBar()
    {
        if(slider.value == 1)
        {
            GetComponentInParent<Canvas>().enabled = false;
        }
        else
        {
            GetComponentInParent<Canvas>().enabled = true;
        }
    }

    private void changeHealthBarColor()
    {
        if(slider.value <= .3f)
        {
            fill.GetComponent<Image>().color = new Color32(203, 35, 43, 255);
        }
    }

    private void Start() 
    {
        fill.GetComponent<Image>().color = new Color32(143, 221, 151, 255);  
    }

    // Update is called once per frame
    private void Update()
    {
        setHealthBarPosition();
        showHealthBar();
        changeHealthBarColor();
    }
}
