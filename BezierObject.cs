using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierObject : MonoBehaviour
//
//베지에 곡선의 궤적으로 이동하는 곡선 공격 탄막 제작 https://drehzr.tistory.com/539 참조
//4개의 포인트 p1, p2, p3, p4를 정하고, 4개의 포인트를 곡선으로 거쳐 이동하는 물체
//
{
    // Start is called before the first frame update
    Vector2[] point = new Vector2[4];
    Animator anim;
    bool hit = false;
 
    [SerializeField] [Range(0, 1)] private float t = 0;
    [SerializeField] public float spd;
    [SerializeField] public float posA = 0.55f;
    [SerializeField] public float posB = 0.45f;
 
    public GameObject master;
    public GameObject enemy;

    public GameObject p1;

    public GameObject p2;
 
    void Start() {
        anim = GetComponent<Animator>();

        //곡선공격을 만들기 위해 각 Point의 궤적을 그림, 이 코드에서의 베지에 곡선은 3차 베지에 곡선
        point[0] = master.transform.position; // P0
        point[1] = p1.transform.position; // P1
        point[2] = p2.transform.position; // P2
        point[3] = enemy.transform.position; // P3
        Invoke("DestroyBullet",0.5f);
    }
 
    void FixedUpdate() {
        //t가 1이 될 때까지 물제를 발사
        if (t > 1) return;
        if (hit) return;
        t += (Time.fixedDeltaTime*spd);
        DrawTrajectory();
    }
 
    Vector2 PointSetting(Vector2 origin) {
        float x, y;

        //랜덤으로 뻗어나가는 효과(카이사 q를 생각하면 됨)을 위해 만들어놓았으나, 실사용은 하지 않음.
        //사용하려면 p1, p2를 x, y로 변경하면 됨
        x = posA * Mathf.Cos(Random.Range(0, 360) * Mathf.Deg2Rad)
            + origin.x;
        y = posB * Mathf.Sin(Random.Range(0, 360) * Mathf.Deg2Rad)
            + origin.y;
        return new Vector2(x, y);
    }
 
    void DrawTrajectory() {
        transform.position = new Vector2(
            FourPointBezier(point[0].x, point[1].x, point[2].x, point[3].x),
            FourPointBezier(point[0].y, point[1].y, point[2].y, point[3].y)
        );
        
    }
 
    void OnTriggerEnter2D(Collider2D collision) {
        //곡선 궤적 공격의 충돌처리
        if(collision.tag == "Platform" || collision.tag == "VomitDest"){
            DestroyBullet();
        }

        /*
        if (collision.gameObject == enemy) {
            hit = true;
            anim.SetTrigger("hit");

        }
        */
        
    }
 
    private float FourPointBezier(float a, float b, float c, float d) {
        //베지에 곡선의 공식, 이 공식을 통해 물체의 position이 바뀜
        return Mathf.Pow((1 - t), 3) * a
            + Mathf.Pow((1 - t), 2) * 3 * t * b
            + Mathf.Pow(t, 2) * 3 * (1 - t) * c
            + Mathf.Pow(t, 3) * d;
    }

    void DestroyBullet(){
        Destroy(gameObject);
    }
}
