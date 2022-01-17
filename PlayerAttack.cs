using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
//
//player의 하위 오브젝트인 bulletpos에서 bullet을 발사하는 코드
//마우스의 위치를 계산하여 해당 위치로 탄막(bullet)을 발사함
//
{
    public float shootDelay;
    private float shootDelayDeltaTime = 0;
    public GameObject bullet;
    public Transform pos;
    private string bulletKind;
    private GameObject instantBullet;
    private bool isLazer = false;
    // Start is called before the first frame update
    void Start()
    {
        bulletKind = bullet.name;
        Debug.Log(bulletKind);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float z = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,z);
        if(Input.GetMouseButton(0) && PlayerMove.underAttack == false)
        {
            if(bulletKind == "Lazercast")
            {
                Debug.Log(bulletKind);
                LazerShoot();
            }
            else if(bulletKind == "Bullet" || bulletKind == "BulletMass" || bulletKind == "Shotgun" || bulletKind == "Missile")
            {
                Reload();
                BulletShoot();                
            }
        }
        if(Input.GetMouseButtonUp(0) || PlayerMove.underAttack == true)
        {
            if(bulletKind == "Lazercast")
            {
                DestroyLazer();
            }
        }
    }
    void BulletShoot()
    {
        if(shootDelayDeltaTime < shootDelay){
            return;
        }
        instantBullet = (GameObject)Instantiate(bullet, pos.position, transform.rotation);
        shootDelayDeltaTime = 0;
    }

    void Reload()
    {
        shootDelayDeltaTime += Time.deltaTime;
    }

    void LazerShoot()
    {
        if(isLazer == false)
        {
            instantBullet = (GameObject)Instantiate(bullet, pos.position, transform.rotation);
            isLazer = true;
        }
        else
        {
            return;
        }
    }

    void DestroyLazer()
    {
        Debug.Log("Lazercut");
        Destroy(instantBullet);
        isLazer = false;
    }
}
