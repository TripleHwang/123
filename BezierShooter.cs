using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierShooter : MonoBehaviour
//
//BezierObject를 발사하는 shooter의 코드
//
{
    // Start is called before the first frame update
    [SerializeField] public GameObject missile;
    [SerializeField] public GameObject target;
 
    [SerializeField] public float spd;
    [SerializeField] public int shot;

    [SerializeField] public GameObject p1;

    [SerializeField] public GameObject p2;

    Vector3 initTargetPosition;

    void Start(){
        initTargetPosition =  target.transform.localPosition;
    }
 
    public void Shot() {
        //코루틴 실행
        StartCoroutine(CreateMissile());
    }
 
    IEnumerator CreateMissile() {
        Debug.Log("Shoot");
        //골렘이 RangeAttack 상태로 들어갔을 때 베지에 곡선 공격을 하는 코루틴 발동
        int _shot = shot;
        while (_shot > 0) {
            _shot--;
            //BezierObject에서의 위치를 지정함.
            GameObject bullet = Instantiate(missile, transform);
            bullet.GetComponent<BezierObject>().master = gameObject;
            bullet.GetComponent<BezierObject>().enemy = target;
            bullet.GetComponent<BezierObject>().p1 = p1;
            bullet.GetComponent<BezierObject>().p2 = p2;
            bullet.GetComponent<BezierObject>().spd = spd;

            //입에서 나오는 것처럼 위치를 조정함(조정하지 않으면 가운데에서 나옴)
            if(target.transform.localPosition.y < 6.2f){
                target.transform.position += new Vector3(0, 0.3f, 0);
            }
            yield return new WaitForSeconds(0.03f);
        }
        target.transform.localPosition = initTargetPosition;
        yield return null;
    }
}
