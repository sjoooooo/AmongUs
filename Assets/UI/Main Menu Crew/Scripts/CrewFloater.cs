using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewFloater : MonoBehaviour
{
    // 소환해서 날려보낼 크루원의 프리펩 
    [SerializeField]
    private GameObject prefab;

    // 크루원의 이미지를 바꿔줄때 사용할 스프라이트를 담고있는 배열
    [SerializeField]
    private List<Sprite> sprites;

    // 떠다니는 크루원이 중복되지 않게 하기위한 bool
    private bool[] crewStates = new bool[12];

    // 크루원을 소환하는 간격을 줄 프로퍼티
    private float timer = 0.5f;

    // 중심으로부터 소환될 위치를 지정할 프로퍼티 11
    private float distance = 6.0f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i< 12; i++)
        {
            SpawnFloatingCrew((EPlayerColor)i,Random.Range(0.0f, distance));
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0.0f)
        {
            SpawnFloatingCrew((EPlayerColor)Random.Range(0.0f, 12.0f), distance);
            timer = 1.0f;
        }
      
    }

    public void SpawnFloatingCrew(EPlayerColor playerColor, float dist)
    {
        if(!crewStates[(int)playerColor])
        {
            crewStates[(int)playerColor] = true;

            // 0 에서 360의 랜덤한 숫자
            float angle = Random.Range(0.0f, 360.0f);
            // 중심으로 부터 원형의 방향을 돌아가며 가르키는 벡터
            Vector3 spawnPos = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f) * distance;
            Vector3 direction = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f);
            float floatingSpeed = Random.Range(1.0f, 4.0f);
            float rotateSpeed = Random.Range(-3.0f, 3.0f);

            var crew = Instantiate(prefab, spawnPos, Quaternion.identity).GetComponent<FloatingCrew>();
            crew.SetFloatingCrew(sprites[Random.Range(0, sprites.Count)], playerColor, direction, floatingSpeed,
                rotateSpeed, Random.Range(0.5f, 1.0f));

        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var crew = collision.GetComponent<FloatingCrew>();
        if(crew != null)
        {
            crewStates[(int)crew.playerColor] = false;
            Destroy(crew.gameObject);
        }
    }
}
