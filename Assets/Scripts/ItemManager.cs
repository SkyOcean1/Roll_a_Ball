using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public int itemScore = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);

            GameManager.instance.IncreaseScore(itemScore);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
