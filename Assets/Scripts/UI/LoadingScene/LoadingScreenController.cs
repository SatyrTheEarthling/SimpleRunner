using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

/*
 * Loading screen controller:
 *  - Looks for ProjectContext.Instance - this triggers its creation. Instead of add SceneContext on scene.
 *  - ASync load GameScene.
 *  - Updates loading progress bar and loading texts.
 *  - Await until player will press Play button, fires GameSignals.GameStarted and destroy itself. 
 */
public class LoadingScreenController : MonoBehaviour
{
    [SerializeField] private Button _startButon;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI _versionText;
    [SerializeField] private TextMeshProUGUI _loadingText;
    [SerializeField] private ProgressBarComponent _progressBar;

    private SignalBus _signalBus;

    private string[] _loadingStrings = new string[] {
        "ProjectContext...",
        "Textures...",
        "Shaders...",
        "Jokes...",
        "Obtacles...",
        "Scene...",
        "Done...",
    };

    private float[] _loadingProgresses = new float[] {
        0.05f,
        0.15f,
        0.25f,
        0.35f,
        0.45f,
        0.65f,
        1f,
    };

    private async void Awake()
    {
        Debug.Assert(_loadingStrings.Length == _loadingProgresses.Length);

        _startButon.onClick.AddListener(HideLoadingScreen);
        DontDestroyOnLoad(gameObject);
        _versionText.text = $"Version: {Application.version}";

        await UniTask.NextFrame();

        SetLoadingProgressState(0);

        Debug.Log("LoadingScreenManager: Start initing context");
        var context = ProjectContext.Instance;
        Debug.Log("LoadingScreenManager: Initing context finished");
        _signalBus = context.Container.Resolve<SignalBus>();
        await UniTask.Delay(100);


        var sceneLoadingOperation = SceneManager.LoadSceneAsync(1);

        for (int i = 1; i < _loadingStrings.Length - 1; i++)
        {
            SetLoadingProgressState(i);
            await UniTask.Delay(500);
        }

        await sceneLoadingOperation;

        Debug.Log("LoadingScreenManager: Scene loaded");

        SetLoadingProgressState(_loadingStrings.Length - 1);

        _startButon.gameObject.SetActive(true);

        Debug.Log("LoadingScreenManager: Game started. wait for play button");
    }

    private void SetLoadingProgressState(int n)
    {
        Debug.Assert(n < _loadingStrings.Length);

        _progressBar.SetValue(_loadingProgresses[n]);
        _loadingText.text = _loadingStrings[n];
    }

    private void HideLoadingScreen()
    {
        _signalBus.Fire<GameSignals.GameStarted>();

        _canvasGroup.interactable = false;
        _canvasGroup.DOFade(0, 0.3f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}

