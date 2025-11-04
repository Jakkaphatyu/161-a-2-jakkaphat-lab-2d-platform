using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField]
    private Slider healthSlider;

    [SerializeField]
    private Image fillImage;
    [SerializeField]
    private Color fullHealthColor = Color.green;
    [SerializeField]
    private Color zeroHealthColor = Color.red;

    private Character targetCharacter;

    private void Awake()
    {
        if (healthSlider == null)
        {
            healthSlider = GetComponent<Slider>();
        }


        targetCharacter = GetComponentInParent<Character>();

        if (targetCharacter != null)
        {
            Debug.LogError("HealthBar: Cannot find Character in parent!");
            return;
        }

        targetCharacter.OnHealthPctChanged += HandleHealthChanged;
    }

    private void Start()
    {
        healthSlider.minValue = 0f;
        healthSlider.maxValue = 1f;

        targetCharacter.Health = targetCharacter.Health;
    }

    private void OnDestroy()
    {
        if (targetCharacter != null)
        {
            targetCharacter.OnHealthPctChanged -= HandleHealthChanged;
        }
    }

    private void HandleHealthChanged(float percentage)
    {
        healthSlider.value = percentage;

        if (fillImage != null)
        {
            fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, percentage);
        }
    }
}