using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class CreateRoomUI : MonoBehaviour
{
    // 이미지를 담는 변수
    [SerializeField]
    private List<Image> crewImgs;

    [SerializeField]
    private List<Button> imposterCountButtons;

    [SerializeField]
    private List<Button> maxPlayerCountButtons;

    private CreateGameRoomData roomData;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < crewImgs.Count; i++)
        {
            Material materialInstance = Instantiate(crewImgs[i].material);
            crewImgs[i].material = materialInstance;
        }

        roomData = new CreateGameRoomData() { imposterCount = 1, maxPlayerCount = 10 };
        UpdateCrewImages();
    }

    public void UpdateImposterCount(int count)
    {
        roomData.imposterCount = count;

        for (int i = 0; i < imposterCountButtons.Count; i++)
        {
            if (i == count - 1)
            {
                imposterCountButtons[i].image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            else
            {
                imposterCountButtons[i].image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            }
        }

        int limitMaxPlayer = count == 1 ? 4 : count == 2 ? 7 : 9;
        if(roomData.maxPlayerCount < limitMaxPlayer)
        {
            UpdateMaxPlayerCount(limitMaxPlayer);
        }
        else
        {
            UpdateMaxPlayerCount(roomData.maxPlayerCount);
        }
        for(int i = 0; i<maxPlayerCountButtons.Count; i++)
        {
            var text = maxPlayerCountButtons[i].GetComponentInChildren<Text>();
            if (i<limitMaxPlayer - 4)
            {
                maxPlayerCountButtons[i].interactable = false;
                text.color = Color.gray;
            }
            else
            {
                maxPlayerCountButtons[i].interactable = true;
                text.color = Color.white;
            }
        }
    }

    public void UpdateMaxPlayerCount(int count)
    {
        roomData.maxPlayerCount = count;

        for (int i = 0; i< maxPlayerCountButtons.Count; i++)
        {
            if(i == count - 4 )
            {
                maxPlayerCountButtons[i].image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            else
            {
                maxPlayerCountButtons[i].image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            }
        }
        UpdateCrewImages();
    }

    // 크루원의 이미지를 랜덤으로 뽑아 빨간색으로 만들어줌
    private void UpdateCrewImages()
    {
        // 빨간색으로 바꾸기 전에 크루 이미지의 색을 모두 하얀색으로 바꿔줌
        for (int i = 0; i< crewImgs.Count; i++)
        {
            crewImgs[i].material.SetColor("_PlayerColor", Color.white);
        }

        int imposterCount = roomData.imposterCount;
        int idx = 0;
        while(imposterCount != 0)
        {
            if (idx >= roomData.maxPlayerCount)
            {
                idx = 0;
            }

            if(crewImgs[idx].material.GetColor("_PlayerColor") != Color.red && Random.Range(0,5) == 0)
            {
                crewImgs[idx].material.SetColor("_PlayerColor", Color.red);
                imposterCount--;
            }
            idx++;
        }

        // 설정한 플레이어 수만큼만 크루원 이미지 활성
        for (int i = 0; i< crewImgs.Count; i++)
        {
            if (i<roomData.maxPlayerCount)
            {
                crewImgs[i].gameObject.SetActive(true);
            }
            else
            {
                crewImgs[i].gameObject.SetActive(false);
            }
        }
    }

    public void CreateRoom()
    {
        var manager = AmongUsRoomManager.singleton;
        // 방 설정 작업 처리
        //
        //
        manager.StartHost();
    }
}

public class CreateGameRoomData
{
    public int imposterCount;
    public int maxPlayerCount;

}
