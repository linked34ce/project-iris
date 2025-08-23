using System.Collections;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour, ISceneLoader
{
    [SerializeField] private Animator _transitionAnimation;

    [SerializeField]
    [Range(2.0f, 7.0f)]
    private float _transitionDuration = 1f;

    [SerializeField]
    [Range(0.0f, 5.0f)]
    private float _delay = 0.5f;

    [SerializeField] private bool _isAutoTransitionEnabled = true;
    [SerializeField] private bool _showProgressBar = false;

    [SerializeField] private Canvas _crossfadeCanvas;
    [SerializeField] private Canvas _loadingCanvas;
    [SerializeField] private Slider _loadingBar;
    [SerializeField] private TMP_Text _loadingText;

    private AsyncOperation _loadOperation;

    void Awake()
    {
        _crossfadeCanvas.gameObject.SetActive(true);
    }

    public void LoadScene(string name)
    {
        StartCoroutine(LoadLevel(name));
    }

    public void ReloadScene()
    {
        var acrtivesceneindex = SceneManager.GetActiveScene().name;
        StartCoroutine(LoadLevel(acrtivesceneindex));
    }

    private IEnumerator LoadLevel(string name)
    {
        yield return new WaitForSeconds(_delay);
        _transitionAnimation.SetTrigger("Start");
        yield return new WaitForSeconds(_transitionDuration);
        StartCoroutine(LoadAsync(name));
    }

    private IEnumerator LoadAsync(string name)
    {
        _loadOperation = SceneManager.LoadSceneAsync(name);
        _loadOperation.allowSceneActivation = _isAutoTransitionEnabled;
        if (_showProgressBar)
        {
            _loadingCanvas.enabled = true;
        }

        while (!_loadOperation.isDone)
        {
            InLoading();
            yield return null;
        }
    }

    private void InLoading()
    {
        float progress = Mathf.Clamp01(_loadOperation.progress / .9f);
        _loadingBar.value = progress;
        _loadingText.text = progress * 100f + " %";

        if (!_loadOperation.allowSceneActivation)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _loadOperation.allowSceneActivation = true;
            }
        }
    }

}
