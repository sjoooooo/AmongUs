using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCrew : MonoBehaviour
{
    // 크루원의 색상
    public EPlayerColor playerColor;

    // 떠다니는 크루원의 색상을 지정
    private SpriteRenderer spriteRenderer;

    // 크루원이 날아다니는 방향
    private Vector3 direction;

    // 크루원의 날아다니는 속도
    private float floatingSpeed;

    // 크루원의 회전 속도
    private float rotateSpeed;

    private void Awake()
    {
        // 스프라이트 렌더러 프로퍼티에 저장
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetFloatingCrew(Sprite sprite, EPlayerColor playerColor, Vector3 direction, float floatingSpeed, float rotateSpeed, float size)
    {
        this.playerColor = playerColor;
        this.direction = direction;
        this.floatingSpeed = floatingSpeed;
        this.rotateSpeed = rotateSpeed;

        spriteRenderer.sprite = sprite;
        spriteRenderer.material.SetColor("_PlayerColor", PlayerColor.GetColor(playerColor));

        transform.localScale = new Vector3(size, size, size);

        // 크기가 작은 크루원은 뒤로 크기가 큰 크루원은 앞으로 오게끔 sortingOrder로 설정
        spriteRenderer.sortingOrder = (int)Mathf.Lerp(1, 32767, size);
    }

    // Update is called once per frame
    void Update()
    {
        // 크루원을 이동시키고 회전시킴
        transform.position += direction * floatingSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0.0f, 0.0f, rotateSpeed));
        
    }
}
