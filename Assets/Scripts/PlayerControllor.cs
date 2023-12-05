using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerControllor : MonoBehaviour
{
    public Rigidbody player_rb;
    public float playerMove = 8f; // �÷��̾� �̵� �ӵ�
    float jumpForce = 10f; // �÷��̾� ������
    float bounceForce = 5f; // ƨ��� ��
    bool isGrounded; // �ٴ� ����
    public float itemRotationSpeed = 50f;
    public string sceneToRestart;
    public Material skyboxMaterial;

    // Start is called before the first frame update
    void Start()
    {
        player_rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -30.0f, 0); // �߷��� ��
        player_rb.useGravity = true; // �߷� Ȱ��ȭ
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

    void OnCollisionEnter(Collision collision) // �ٴ� �÷����� �ε����� �� 
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
        if (other.CompareTag("Item")) // ������ �浹�� ���� ����
        {
            Debug.Log("������ �浹");

            GameManager.instance.IncreaseScore(10); // ���� ���� �Լ� ȣ��
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Death Zone")) // ������ �浹�� ���ó��
        {
            Debug.Log("������ �浹");
            Die();
        }

        if (other.CompareTag("Obstacle")) // ��ֹ� �浹�� ���ó��
        {
            Debug.Log("��ֹ� �浹");
            Die();
        }

        if (other.gameObject.CompareTag("Flag")) // ���� �������� �ε�
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
