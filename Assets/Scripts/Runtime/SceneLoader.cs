using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Canvas transitionCanvas;
    [SerializeField] private VideoPlayer transitionVideo;
    [SerializeField] private CanvasGroup overlayCanvas;
    [Space] [Header("Scene Buttons")] 
    public Button menuButton;
    public Button checkPointButton;
    public Button beginnerButton;
    public Button advancedButton;

    private AsyncOperation currentSceneLoad;

    private void OnEnable()
    {
        transitionVideo.loopPointReached += OnVideoComplete;
    }

    private void OnDisable()
    {
        transitionVideo.loopPointReached -= OnVideoComplete;
    }

    private void OnVideoComplete(VideoPlayer source)
    {
        StartCoroutine(ActivateScene());
    }

    private Tween FadeCanvas()
    {
        return DOVirtual.Float(0f, 1f, .6f, value =>
            overlayCanvas.alpha = value).SetEase(Ease.Linear);
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (menuButton != null)
        {
            menuButton.onClick.AddListener((() => SwitchScenes(0)));
        }
        
        if (checkPointButton != null)
        {
            checkPointButton.onClick.AddListener((() => SwitchScenes(1)));
        }

        if (beginnerButton != null)
        {
            beginnerButton.onClick.AddListener((() => SwitchScenes(2)));
        }

        if (advancedButton)
        {
            advancedButton.onClick.AddListener((() => SwitchScenes(3)));
        }
    }

    private void SwitchScenes(int index)
    {
        StartCoroutine(WaitForVideo(index));
    }

    private IEnumerator WaitForVideo(int index)
    {
        yield return FadeCanvas().WaitForCompletion();
        currentSceneLoad = SceneManager.LoadSceneAsync(index);
        currentSceneLoad.allowSceneActivation = false;
        transitionCanvas.gameObject.SetActive(true);
        transitionVideo.Play();
        FadeCanvas().SetInverted();
    }
    private IEnumerator ActivateScene()
    {
        yield return FadeCanvas().WaitForCompletion();
        currentSceneLoad.allowSceneActivation = true;
        transitionCanvas.gameObject.SetActive(false);
        FadeCanvas().SetInverted();

    } 

}
