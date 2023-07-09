using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject shootPrefab;

    private Vector3 mousePosition;

    private int bulletsWeapon;
    [SerializeField] private Image[] bulletsHUD; 
    [SerializeField] private Image ammoBullet, ammoBulletEmpty; 

    //Audio
    [SerializeField] private AudioSource shootAudio, reloadAudio;


    private void Awake()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bulletsWeapon = 7;
    }

    private void Update()
    {
        if(!PauseMenu.Instance.IsPause && GameManager.Instance.TimeLifeGame >= 0f)
        {
            Input.GetAxis("Vertical");

            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //MoveWeapon
            weapon.transform.position = new Vector2(mousePosition.x + 3f, mousePosition.y - 4f);

            Shoot();
            Reload();
        }
    }


    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && bulletsWeapon > 0)
        {
            shootAudio.Play();
            bulletsHUD[bulletsWeapon - 1].sprite = ammoBulletEmpty.sprite;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (hit)
            {
                GameObject parentTarget = hit.collider.gameObject;
                hit.transform.GetComponent<Ducks>().DucksDamage(1);
                Instantiate(shootPrefab, new Vector3(mousePosition.x, mousePosition.y, 0), Quaternion.identity, parentTarget.transform);

            } else {
                Instantiate(shootPrefab, new Vector3(mousePosition.x, mousePosition.y, 0), Quaternion.identity);
            }

            bulletsWeapon--;
        }
    }

    private void Reload()
    {

        if (Input.GetMouseButtonDown(1) && bulletsWeapon < 7)
        {
            weapon.GetComponent<Animator>().SetTrigger("Reload");
            reloadAudio.Play();

            for(int i = 1; i <= 7; i++)
            {
                bulletsHUD[i - 1].sprite = ammoBullet.sprite;
            }

            bulletsWeapon = 7;
        }
    }

}

