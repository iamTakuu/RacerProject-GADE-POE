using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Cinemachine;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject countDown;
    [OptionalField][SerializeField] private CheckpointManager _manager;

    [Header("Checkpoint UI")] [OptionalField]
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text bonusTime;
    [SerializeField] private TMP_Text endText;

    [Header("Cameras")] 
    [SerializeField] private List<CinemachineVirtualCamera> virtualCameras;

    private int cameraIndex;

    private Vector3 bonusOgVector3;
    private bool timerActive;
    private bool isRace;
    
    private void Update()
    {
        if (timerActive && _manager.timerOn)
        {
            timeText.text = $"Time Left: { Math.Round( _manager.timeRemaining, 2)}";
        }
    }
    private void OnEnable()
    {
        if (FindObjectOfType<RaceManager>() != null)
        {
            isRace = true;
        }
        EventsManager.Instance.StartGame += StartCountDown;
        EventsManager.Instance.EndGame += GameOver;
        EventsManager.Instance.AddTime += AddTime;
    }

    private void OnDisable()
    {
        EventsManager.Instance.StartGame -= StartCountDown;
        EventsManager.Instance.EndGame -= GameOver;
        EventsManager.Instance.AddTime -= AddTime;
    }

    private void Start()
    {
        if (!isRace)
        {
            bonusOgVector3 = bonusTime.transform.position;
        }
    }

    private void StartCountDown()
    {
        StartCoroutine(isRace ?
            RaceCountDown() :
            CountDown());
    }

    private void AddTime()
    {
        
        ResetBonus(false, bonusOgVector3);
        bonusTime.text = $"Plus {_manager.timeToAdd}";

        bonusTime.transform.DOLocalMoveY(-0.3f, 1f).SetEase(Ease.Linear).OnComplete((
            () => ResetBonus(true, bonusOgVector3)));
        
    }

    private void ResetBonus(bool reset, Vector3 ogPos)
    {
        if (reset)
        {
            bonusTime.transform.position = ogPos;
            bonusTime.gameObject.SetActive(false);

        }
        else
        {
            bonusTime.gameObject.SetActive(true);
        }
    }

    private void GameOver()
    {
        StartCoroutine(ShowEndScreen(_manager.gameWon));
    }

    private IEnumerator ShowEndScreen(bool hasWon)
    {
        endText.gameObject.SetActive(true);
        if (hasWon)
        {
            timeText.DOColor(Color.cyan, 0.2f);
            timeText.transform.DOShakePosition(0.5f, 2f);
            endText.text = $"You rock!";
            endText.color = Color.green;
            endText.transform.DOScale(new Vector3(1.5f, 1.5f), 0.5f).SetEase(Ease.InBounce);
        }
        else
        {
            timeText.DOColor(Color.red, 0.2f);
            timeText.transform.DOShakePosition(0.5f, 2f);
            endText.text = $"Get Good.";
            endText.color = Color.red;
            endText.transform.DOScale(new Vector3(1.5f, 1.5f), 0.5f).SetEase(Ease.InBounce);
        }
        yield return new WaitForSeconds(0.3f);
    }
    
    
    private IEnumerator CountDown()
    {
        for (var i = 0; i < countDown.transform.childCount; i++)
        {
            countDown.transform.GetChild(i).gameObject.SetActive(true);
            countDown.transform.GetChild(i).DOScale(new Vector3(3, 3), 0.5f).OnComplete((() =>
            {
                countDown.transform.GetChild(i).gameObject.SetActive(false);
            }));
            yield return new WaitForSeconds(0.5f);
        }
        timerActive = true;
        EventsManager.Instance.OnActivateCar();
        timeText.gameObject.SetActive(true);
    }

    private IEnumerator RaceCountDown()
    {
        cameraIndex = 1;
        for (var i = 0; i < countDown.transform.childCount; i++)
        {
            countDown.transform.GetChild(i).gameObject.SetActive(true);
            countDown.transform.GetChild(i).DOScale(new Vector3(3, 3), 0.5f).OnComplete((() =>
            {
                countDown.transform.GetChild(i).gameObject.SetActive(false);
                if (i != 3) return;
                EventsManager.Instance.OnActivateCar();
            }));
            if (cameraIndex < 4)
            {
                virtualCameras[cameraIndex++].gameObject.SetActive(true);
            }

            //SwitchCam();
            yield return new WaitForSeconds(2f);
        }
        
    }

    private void SwitchCam()
    {
        // //Disable Current Camera
        virtualCameras[cameraIndex].gameObject.SetActive(false);
        //Enable Next Camera
        virtualCameras[++cameraIndex].gameObject.SetActive(true);
    }
}
