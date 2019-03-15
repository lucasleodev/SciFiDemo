using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    [SerializeField]
    private Text _ammoUI;
    [SerializeField]
    private GameObject _coin;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateAmmo(int ammo)
    {
        _ammoUI.text = "AMMO: " + ammo;
    }

    public void GetCoin()
    {
        _coin.SetActive(true);
    }

    public void GiveCoin()
    {
        _coin.SetActive(false);
    }

}
