using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D PlayerRigidbody;
    [SerializeField] private GameObject GameWonPanel;
    [SerializeField] private GameObject GameLostPanel;

    private float speed;
    private bool isGameOver = false; 

    void Update()
    {
        if(isGameOver)
        {
            Time.timeScale = 0;
            return;
        }
        else
        {
            Time.timeScale = 1;
        }

        if(Input.GetAxis("Horizontal") > 0)
        {
            PlayerRigidbody.velocity = new Vector2(speed, 0f); 
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            PlayerRigidbody.velocity = new Vector2(-speed, 0f);
        }

        else if (Input.GetAxis("Vertical") > 0)
        {
            PlayerRigidbody.velocity = new Vector2(0f, speed);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            PlayerRigidbody.velocity = new Vector2(0f, -speed);
        }

        else if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            PlayerRigidbody.velocity = new Vector2(0f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Door"))
        {
            Debug.Log("Welcome!!"); 

            GameWonPanel.SetActive(true);

            isGameOver = true;
        }
        else if(collision.CompareTag("FakeDoor"))
        {
            Debug.Log("Bazinga!!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("BaBye!!");

            GameLostPanel.SetActive(true);

            isGameOver = true;
        }
    }

    public void RestartGame()
    {
        isGameOver = false;         

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);       //or direct pass the buildIndex: SceneManager.LoadScene(0)
    }

    public void ExitGame()
    {
        Application.Quit();

        Debug.Log("QuitGame"); 
    }
}
