using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{

    [SerializeField]
    private Text _coinText;
    [SerializeField]
    private AudioClip _coinCaugth;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("colidiou");

            if (Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.GetComponent<Player>();
                if(player != null)
                {
                    UImanager _uiManager = GameObject.Find("Canvas").GetComponent<UImanager>();
                    _uiManager.GetCoin();
                    AudioSource.PlayClipAtPoint(_coinCaugth, transform.position, 1f);
                    player._hasCoin = true;
                    Destroy(this.gameObject);
                }
            }
        }
    }
}

