using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAlert : MonoBehaviour
{
    


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Collider2D col = Physics2D.OverlapCircle(transform.position, 5);
            Debug.Log("Clicked");
            if(col.GetComponent<IdleWander>() != null)
            {
                var npc = col.GetComponent<IdleWander>();
                npc._isAlerted = true;
                Debug.Log("Happened");
            }
        }
    }
}
