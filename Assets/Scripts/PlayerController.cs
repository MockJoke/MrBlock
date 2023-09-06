using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D PlayerRigidbody;
    [SerializeField] private GameObject GameWonPanel;
    [SerializeField] private GameObject GameLostPanel;

    [SerializeField] private float rbSpeed;
    [SerializeField] private float transformSpeed;
    
    private bool isGameOver = false; 

    void Update()
    {
        if(isGameOver)
        {
            SetTimeScale(true);
            return;
        }
        else
        {
            SetTimeScale(false);
        }
    }

    private void FixedUpdate()
    {
        MoveUsingTransformPos();
        //MoveUsingRigidbody();
    }

    private void MoveUsingTransformPos()
    {
        if(Input.GetAxis("Horizontal") > 0)
        {
            transform.position += Vector3.right * Time.deltaTime * transformSpeed;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            transform.position += Vector3.left * Time.deltaTime * transformSpeed;
        }

        else if (Input.GetAxis("Vertical") > 0)
        {
            transform.position += Vector3.up * Time.deltaTime * transformSpeed;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            transform.position += Vector3.down * Time.deltaTime * transformSpeed;
        }
    }
    
    private void MoveUsingRigidbodyVelocity()
    {
        if(Input.GetAxis("Horizontal") > 0)
        {
            PlayerRigidbody.velocity = new Vector2(rbSpeed, 0f); 
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            PlayerRigidbody.velocity = new Vector2(-rbSpeed, 0f);
        }

        else if (Input.GetAxis("Vertical") > 0)
        {
            PlayerRigidbody.velocity = new Vector2(0f, rbSpeed);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            PlayerRigidbody.velocity = new Vector2(0f, -rbSpeed);
        }

        else if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            PlayerRigidbody.velocity = new Vector2(0f, 0f);
        }
    }

    #region Methods for collison detection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Fruit"))
        {
            Debug.Log("Welcome!!"); 

            GameWonPanel.SetActive(true);

            isGameOver = true;
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
    #endregion

    private void SetTimeScale(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
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
