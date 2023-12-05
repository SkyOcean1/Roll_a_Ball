using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerControllor : MonoBehaviour
{
    public Rigidbody player_rb;
    public float playerMove = 8f; // 플레이어 이동 속도
    float jumpForce = 10f; // 플레이어 점프력
    float bounceForce = 5f; // 튕기는 힘
    bool isGrounded; // 바닥 감지
    public float itemRotationSpeed = 50f;
    public string sceneToRestart;
    public Material skyboxMaterial;

    // Start is called before the first frame update
    void Start()
    {
        player_rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -30.0f, 0); // 중력의 힘
        player_rb.useGravity = true; // 중력 활성화
    }

    // Update is called once per frame
    void Update()
    {
        //player_rb.velocity -= player_rb.velocity * friction;

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }

        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        float playerMove_x = xInput * playerMove;
        float playerMove_y = yInput * playerMove;

        Vector3 newVelocity = new Vector3(playerMove_x, player_rb.velocity.y, playerMove_y);
        player_rb.velocity = newVelocity;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.2f);
    }

    void Jump()
    {
        bounceForce = 5f;
        player_rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }

    void OnCollisionEnter(Collision collision) // 바닥 플랫폼에 부딪혔을 때 
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            player_rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            bounceForce -= 0.7f;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Item")) // 아이템 충돌시 점수 증가
        {
            Debug.Log("아이템 충돌");

            GameManager.instance.IncreaseScore(10); // 점수 증가 함수 호출
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Death Zone")) // 데드존 충돌시 사망처리
        {
            Debug.Log("데드존 충돌");
            Die();
        }

        if (other.CompareTag("Obstacle")) // 장애물 충돌시 사망처리
        {
            Debug.Log("장애물 충돌");
            Die();
        }

        if (other.gameObject.CompareTag("Flag")) // 다음 스테이지 로딩
        {
            LoadNextStage();
        }
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void RestartSceneDelay(float delay)
    {
        Invoke("RestartScene", delay);
    }

    public void Die()
    {
        if (gameObject.name == "Player")
        {
            gameObject.SetActive(false);
            RestartSceneDelay(1f);
        }
    }

    void LoadNextStage()
    {
        SceneManager.LoadScene("Stage_02");
    }

}
