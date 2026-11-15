using UnityEngine;
using UnityEngine.SceneManagement;
public class Health : MonoBehaviour
{
    public int health = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            health -= 5;
            Debug.Log("current health:" + health);

            if (health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        Debug.Log("Game Over");

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
