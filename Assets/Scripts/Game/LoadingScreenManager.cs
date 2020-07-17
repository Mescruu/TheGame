using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{


    [Header("Loading Visuals")] //wizualizacje podczas ładowania
    public Image loadingIcon;
    public Image loadingDoneIcon;
    public Text loadingText;
    public Image progressBar;
    public Image fadeOverlay;

    [Header("Timing Settings")] //ustawienie czasu zanim zacznie się ładować
    public float waitOnLoadEnd = 0.25f;
    public float fadeDuration = 0.25f;
    public float FakeLoadingDuration = 1f;

    [Header("Loading Settings")] //ładowanie sceny
    public LoadSceneMode loadSceneMode = LoadSceneMode.Single; //wybranie typ ładowanej sceny
    public ThreadPriority loadThreadPriority; //priorytet wątków. Niższy priorytet oznacza, że operacja w tle będzie wykonywana rzadziej i zajmie mniej czasu, ale będzie postępować wolniej.

    [Header("Other")]
    public AudioListener audioListener; //odtwarzacz

    AsyncOperation operation;
    Scene currentScene;

    //animacje
    public Animator anim;
    public Animator ParticleAnim;
    public Animator fadeAnim;

    public int[] prevBorders;
    public SoundHolder soundholder;
    public KeyMenager KeyMenager;
    public static int sceneToLoad = -1;
    //Indeks sceny ładowania. 
    static int loadingSceneIndex = 2;
    public int[] StageBorders;
    public GameObject[] LoadingPics;
    public static int FromSceneIndex;

    public void StageInfo()
    {
        //sprawdza z którego stage'a laduje i potem czy jest to menu
        if (sceneToLoad == 0)
        {
            if (prevBorders[0] > sceneToLoad || prevBorders[1] < sceneToLoad)
            {
               if (sceneToLoad == 0)
                {
                    LoadingPics[0].SetActive(true);  //ustawia obrazek stage'a z ktorego laduje
                }
            }
            Destroy(soundholder.gameObject);
            Destroy(KeyMenager.gameObject); //jezeli ładuje menu to usuwa keyMenager i soundholder ponieważ te dwie rzeczy są załadowane w menu zawsze
        }

        if (sceneToLoad >= StageBorders[0] && sceneToLoad < StageBorders[1])       //zalezne od Stage zmienia utwory;
        {
            soundholder.IdSong = 1;

            Debug.Log("change song");
            soundholder.IdSong = 1;
            PlayerPrefs.SetInt("beforeBorders0", StageBorders[0]);
            PlayerPrefs.SetInt("beforeBorders1", StageBorders[1]);
            LoadingPics[0].SetActive(true);
        }
        if (prevBorders[0] > sceneToLoad || prevBorders[1] < sceneToLoad)
        {
            soundholder.changeSong = true;
            Debug.Log("change song");
        }
        if (FromSceneIndex == 0)
        {
            fadeAnim.gameObject.SetActive(true);
            fadeAnim.SetBool("FadeOut", false);
        }
       
    }
    public static void LoadScene(int levelNum, int FromScene) //ładowanie sceny
    {
        FromSceneIndex = FromScene;
        Application.backgroundLoadingPriority = ThreadPriority.High;
        sceneToLoad = levelNum;
        SceneManager.LoadScene(loadingSceneIndex);
    }

    void Start()
    {
        soundholder = GameObject.Find("SoundHolder").GetComponent<SoundHolder>();
        KeyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();
        soundholder.isloading = true;

        if (PlayerPrefs.HasKey("beforeBorders0")) //sprawdza czy została przekroczona granica stage'ow
        {
            prevBorders[0] = PlayerPrefs.GetInt("beforeBorders0");
            prevBorders[1] = PlayerPrefs.GetInt("beforeBorders1");
        }
        else
        {
            prevBorders[0] = 0;
            prevBorders[1] = 0;
        }

            StageInfo();

     Cursor.lockState = CursorLockMode.Locked; //Zablokowanie kursora myszy.
     Cursor.visible = false;//Ukrycie kursora.

        if (sceneToLoad < 0)
            return;

        currentScene = SceneManager.GetActiveScene();
        StartCoroutine(LoadAsync(sceneToLoad, anim));

        Debug.Log("Loading....");
    }

    private IEnumerator LoadAsync(int levelNum, Animator anim)
    {
        ShowLoadingVisuals(); //wyswietlenie wizualizacji sceny

        yield return new WaitForSeconds(FakeLoadingDuration); //czekanie aż zacznie się ładować
        yield return null;

        StartOperation(levelNum);

        float lastProgress = 0f;

        // operacja nie aktywuje automatycznie sceny, więc utknęła na 0,9
        while (DoneLoading() == false)
        {
            yield return null;

            if (Mathf.Approximately(operation.progress, lastProgress) == false) //progressbar
            {
                progressBar.fillAmount = operation.progress;
                lastProgress = operation.progress;
            }
        }
      
        if (loadSceneMode == LoadSceneMode.Additive)
            audioListener.enabled = false;

        ShowCompletionVisuals();  //pokaż zakonczenie ładowania

        yield return new WaitForSeconds(waitOnLoadEnd);

        if (levelNum == 0)
        {
            fadeAnim.gameObject.SetActive(true);
            FakeLoadingDuration = fadeDuration;
            fadeAnim.SetBool("FadeOut", true);

        }
        yield return new WaitForSeconds(fadeDuration);

        soundholder.isloading = false; //kończenie łądowania dla soundholdera

        if (loadSceneMode == LoadSceneMode.Additive)
            SceneManager.UnloadScene(currentScene.name);
        else
        {
            operation.allowSceneActivation = true;

        }

    }

    private void StartOperation(int levelNum) //ładowanie w tle następnego poziomu
    {
        Application.backgroundLoadingPriority = loadThreadPriority;
        operation = SceneManager.LoadSceneAsync(levelNum, loadSceneMode);

        if (loadSceneMode == LoadSceneMode.Single)
            operation.allowSceneActivation = false;
    }

    private bool DoneLoading()
    {
      return (loadSceneMode == LoadSceneMode.Additive && operation.isDone) || (loadSceneMode == LoadSceneMode.Single && operation.progress >= 0.9f);
    }

    void ShowLoadingVisuals() //wyswietlenie paska ładowania itd
    {
        loadingIcon.gameObject.SetActive(true);
        loadingDoneIcon.gameObject.SetActive(false);

        progressBar.fillAmount = 0f;
        loadingText.text = "LOADING";

    }

    void ShowCompletionVisuals() //pokazanie, że ładowanie się zakończyło
    {
        loadingIcon.gameObject.SetActive(false);
        loadingDoneIcon.gameObject.SetActive(true);

        progressBar.fillAmount = 1f;
        loadingText.text = "DONE";
        anim.SetBool("FadeOut", true);
        ParticleAnim.SetBool("FadeOut", true);
    }
}