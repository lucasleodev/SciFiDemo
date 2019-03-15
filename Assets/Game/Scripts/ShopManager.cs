using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {

    [SerializeField]
    private GameObject _gun;
    [SerializeField]
    private AudioClip _getGun;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.GetComponent<Player>();
                if (player != null)
                {
                    if (player._hasCoin)
                    {
                        CameraControl _control = GameObject.Find("MouseLook").GetComponent<CameraControl>();
                        UImanager _uiManager = GameObject.Find("Canvas").GetComponent<UImanager>();
                        _uiManager.GiveCoin();
                        AudioSource.PlayClipAtPoint(_getGun, transform.position, 1f);
                        player._hasCoin = false;
                        _control.EnableGun();
                    }
                }
            }
        }
    }
}
