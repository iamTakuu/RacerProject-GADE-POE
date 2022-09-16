using System;
using UnityEngine;
[DefaultExecutionOrder(-90)]
public class EventsManager : MonoBehaviour
{
    public static EventsManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance !=null && Instance!=this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }
    public event Action ActivateCar;
    public void OnActivateCar() => ActivateCar?.Invoke();

    public event Action NextPoint;
    public void OnNextPoint() => NextPoint?.Invoke();

    public event Action StartGame;
    public void OnGameStart() => StartGame?.Invoke();

    public event Action EndGame;
    public void OnEndGame() => EndGame?.Invoke();
    public event Action DeactivateCar;
    public void OnDeactivateCar() => DeactivateCar?.Invoke();

    public event Action AddTime;
    public void OnAddTime() => AddTime?.Invoke();
}
