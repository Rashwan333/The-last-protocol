using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ColliderHUD : MonoBehaviour
{

    public TMP_Text objectiveText;
    public float objectiveShowDuration = 5f;
    public float objectiveFadeSpeed = 1f;
    private float objectiveTimer;


    public int health = 20;
    public int maxHealth = 20;
    public Image healthBar;
    public TMP_Text healthText;


    public Image damageFlashImage;
    public float flashAlpha = 0.35f;
    public float flashFadeSpeed = 0.2f;


    public Image timerBar;
    public TMP_Text timerText;
    public float totalTime = 240f;

    private float timeLeft;
    private bool isDead = false;

    void Start()
    {
        objectiveTimer = objectiveShowDuration;
        timeLeft = totalTime;
        UpdateHealth();
        UpdateTimer();
    }

    void Update()
    {
        if (isDead) return;

        if (objectiveText != null)
        {
            if (objectiveTimer > 0f)
            {
                objectiveTimer -= Time.deltaTime;
            }
            else if (objectiveText.color.a > 0f)
            {
                Color c = objectiveText.color;
                c.a -= objectiveFadeSpeed * Time.deltaTime;
                if (c.a < 0f)
                {
                    c.a = 0f;
                }
                objectiveText.color = c;
            }
        }

        if (damageFlashImage != null)
        {
            if (damageFlashImage.color.a > 0f)
            {
                Color c = damageFlashImage.color;
                c.a -= flashFadeSpeed * Time.deltaTime;

                if (c.a < 0f)
                {
                    c.a = 0f;
                }

                damageFlashImage.color = c;
            }
        }

        timeLeft -= Time.deltaTime;
        UpdateTimer();

        if (timeLeft <= 0f)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag("Zombie"))
        {
            health -= 5;
            Debug.Log("current health:" + health);

            if (damageFlashImage != null)
            {
                Color c = damageFlashImage.color;
                c.a = flashAlpha;
                damageFlashImage.color = c;
            }

            UpdateHealth();

            if (health <= 0)
            {
                Die();
            }
        }
    }

    void UpdateHealth()
    {
        float percentage = (float)health / maxHealth;

        if (healthBar != null)
        {
            healthBar.fillAmount = percentage;
        }

        if (healthText != null)
        {
            healthText.text = "Health: " + Mathf.RoundToInt(percentage * 100f) + "%";
        }
    }

    void UpdateTimer()
    {
        float ratio = timeLeft / totalTime;

        if (timerBar != null)
        {
            timerBar.fillAmount = ratio;
        }

        if (timerText != null)
        {
            float secondsRemaining = Mathf.CeilToInt(timeLeft);
            timerText.text = "time left: " + secondsRemaining;
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("Game Over");
        FindObjectOfType<PanelsManager>().ShowLose();
    }
}