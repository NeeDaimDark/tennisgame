using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ball : MonoBehaviour
{  
    Vector3 initialPos;
    public string hitter;

    public int playerScore;
    public int botScore;
    public bool playing = true;
    public TextMeshProUGUI PlayerScore;
    public TextMeshProUGUI BotScore;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        playerScore = 0;
        botScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("wall"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            //transform.position = initialPos;
            Debug.Log("before reset");
            GameObject.Find("player").GetComponent<Player>().Reset();
            Debug.Log("after reset");
            if (playing)
            {
                if (hitter == "player")
                {
                    playerScore++;
                    PlayerScore.SetText(playerScore.ToString());
                }
                else if (hitter == "bot")
                {
                    botScore++;
                    BotScore.SetText(botScore.ToString());
                }
                playing = false;
            }

            else if (collision.transform.CompareTag("out"))
            {
               
        }
    }
    void OntriggerEnter(Collider other)
    {
        if(other.CompareTag("out") && playing)
            
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

                //transform.position = initialPos;
                Debug.Log("before reset");
                GameObject.Find("player").GetComponent<Player>().Reset();
                Debug.Log("after reset");
                if (playing)
                {
                    if (hitter == "player")
                    {
                        playerScore++;
                        PlayerScore.SetText(playerScore.ToString());
                    }
                    else if (hitter == "bot")
                    {
                        botScore++;
                        BotScore.SetText(botScore.ToString());
                    }
                    playing = false;
                }


            
            if (hitter == "player")
            {
                Debug.Log("bot scores");
                botScore++;
                BotScore.SetText(botScore.ToString());
            }
            else if (hitter == "bot")
            {
                Debug.Log("player scores");
                playerScore++;
                PlayerScore.SetText(playerScore.ToString());
            }
            playing=false;
        }
    }

}
