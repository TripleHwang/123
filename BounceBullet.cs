using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBullet : MonoBehaviour
//
//바닥에 3번 튕기는 공 모양 탄막을 만듬(플레이어가 발사 가능)
//
{
    public float distance;
    public LayerMask isLayer;
    public float speed;
    private int collisionCount = 0;
    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        //바닥에 튕기는 공 같은 탄막
        rigid = GetComponent<Rigidbody2D>();
        Invoke("DestroyBullet",2);
        rigid.AddForce(transform.right * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //몬스터를 공격할 수 있고, 3번 튕기면 사라짐
        collisionCount++;
        if(collision.gameObject.tag == "Enemy")
        {
            OnAttack(collision.transform);
        }
        if(collisionCount >= 3)
        {
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    void OnAttack(Transform enemy)
    {
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
    }
}
