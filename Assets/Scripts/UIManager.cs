using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject countDown;
    [SerializeField] private CheckpointManager _manager;

    [Header("Checkpoint UI")] 
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text bonusTime;
    [SerializeField] private TMP_Text endText;

    private Vector3 bonusOgVector3;
    private bool timerActive;
    

    

    private void Update()
    {
        if (timerActive && _manager.timerOn)
        {
            timeText.text = $"Time Left: { Math.Round( _manager.timeRemaining, 2)}";
        }
    }
    private void OnEnable()
    {
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
        bonusOgVector3 = bonusTime.transform.position;
    }

    private void StartCountDown()
    {
        StartCoroutine(CountDown());
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
            countDown.transform.GetChild(i).DOScale(new Vector3(3, 3), 0.5f).OnComplete((() => countDown.transform.GetChild(i).gameObject.SetActive(false)));
            yield return new WaitForSeconds(0.5f); 
        }

        timerActive = true;
        EventsManager.Instance.OnActivateCar();
        timeText.gameObject.SetActive(true);
    }
}
