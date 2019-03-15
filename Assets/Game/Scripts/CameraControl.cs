using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    [SerializeField]
    private float _mouseSensibility = 3.5f;
    private ParticleSystem _gun_muzzle;
    [SerializeField]
    private GameObject _hitmakerPrefab;
    [SerializeField]
    private AudioSource _gun_sound;
    [SerializeField]
    private int _currentAmmo;
    [SerializeField]
    private int _maxAmmo = 50;
    [SerializeField]
    private float _reloadTime = 3f;
    private bool _isReloading = false;

    [SerializeField]
    private GameObject _arma;

    private UImanager _uiManager;

    // Use this for initialization
    void Start()
    {
        _currentAmmo = _maxAmmo;
        Cursor.lockState = CursorLockMode.Locked;
        //_gun_muzzle = GetComponentInChildren<ParticleSystem>();
        //_gun_muzzle.Stop();
        _arma.SetActive(false);
        _uiManager = GameObject.Find("Canvas").GetComponent<UImanager>();
    }

    // Update is called once per frame
    void Update()
    {
        LookY();
        CursorLogic();
        Shooting();
        ReloadAmmo();
        //_uiManager.UpdateAmmo(_currentAmmo);
    }

    void LookY()
    {
        float _yAxis = Input.GetAxis("Mouse Y");
        Vector3 rotation = transform.localEulerAngles;
        float rotationY = rotation.x - _yAxis * _mouseSensibility;
        rotation.x = rotationY;
        transform.localEulerAngles = new Vector3(rotation.x, 0, 0);
    }

    void CursorLogic()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    void Shooting()
    {
        if(_arma.activeInHierarchy)
        {
            if (Input.GetMouseButton(0) && _currentAmmo > 0)
            {
                _currentAmmo--;
                _uiManager.UpdateAmmo(_currentAmmo);
                if (Input.GetMouseButton(0) && _currentAmmo == 0)
                {
                    _gun_muzzle.Stop();
                    _gun_sound.Stop();
                }
                else
                {
                    _gun_muzzle.Play();
                    if (!_gun_sound.isPlaying)
                    {
                        _gun_sound.Play();
                    }
                    Ray originRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                    RaycastHit hitInfo;
                    if (Physics.Raycast(originRay, out hitInfo, Mathf.Infinity))
                    {
                        Instantiate(_hitmakerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                        Debug.Log("You hit something!! About this object=>" + hitInfo.transform.name);

                        Destructable _obj = hitInfo.transform.GetComponent<Destructable>();
                            if(_obj != null)
                            {
                                _obj.DestroyCrate();
                            }
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                _gun_muzzle.Stop();
                _gun_sound.Stop();
            }
        }
        else
        {
            Debug.Log("arma nao habilitada");
        }
    }

    void ReloadAmmo ()
    {
        if(Input.GetKeyDown(KeyCode.R) && !_isReloading)
        {
            StartCoroutine(tempoReload());
        }
    }

    IEnumerator tempoReload()
    {
        _isReloading = true;
       yield return new WaitForSeconds(_reloadTime);
        _currentAmmo = _maxAmmo;
        _isReloading = false;
        _uiManager.UpdateAmmo(_currentAmmo);
    }

    public void EnableGun()
    {
        _arma.SetActive(true);
        _gun_muzzle = GetComponentInChildren<ParticleSystem>();
        _gun_muzzle.Stop();
        _uiManager.UpdateAmmo(_currentAmmo);
    }
}
