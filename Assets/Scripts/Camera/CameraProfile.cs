using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.Audio;
public class CameraProfile : MonoBehaviour {

    //Ustawienia profilu kamery w postprocesingu
    public PostProcessingProfile ppProfile;

    //Insensywność  efektu bloom - aktualna i poprzednia
    public float bloomIntensity;
    public float oldBloomIntensity;

    //Intensywność aberacji chromatycznej - aktualna i poprzednia
    public float ChAberrationIntensity;
    public float oldChAberrationIntensity;

    //Powrót do poprzednich wartości
    public bool BackToPrevValueBloom;
    public bool BackToPrevValueChAberration;

    //Jak szybko ma nastąpić powrót do poprzednich wartości
    private float TimeToBackToPrevValueBloom;
    private float TimeToBackToPrevValueChAberration;

    //Ustawienia efektu Bloom
    private BloomModel.Settings bloomSettings;
    //Ustawienia aberacji chromatycznej
    private ChromaticAberrationModel.Settings ChAberrationSettings;

    //Dźwięk - komponenty
    public AudioSource audioSource;
    public AudioClip ChAberrationaudioClip;
    public AudioMixer audioMixer;

    //Wysokość tonu
    public float MasterPitch;

    // Use this for initialization
    void Start()
    {
        //Ustawienie ustawień efektów
        bloomSettings = ppProfile.bloom.settings;
        ChAberrationSettings = ppProfile.chromaticAberration.settings;

        //Wstępne ustawienie poprzednich intensywności
        oldBloomIntensity = bloomSettings.bloom.intensity;
        oldChAberrationIntensity = ChAberrationSettings.intensity;

        //Ustawienie dźwięku
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = 0;

        //Ustawienie początkowe intensywności
        bloomSettings.bloom.intensity = 0.5f;
        ChAberrationSettings.intensity = 0f;
        ppProfile.bloom.settings = bloomSettings;
        ppProfile.chromaticAberration.settings = ChAberrationSettings;
        audioMixer.SetFloat("MasterPitch", 1f);
            }

    void Update () {
        //Zmiana ustawień
        audioMixer.GetFloat("MasterPitch", out MasterPitch);
        bloomSettings.bloom.intensity= bloomIntensity;
        ChAberrationSettings.intensity = ChAberrationIntensity;

        //Powrót do poprzednich wartości efektu bloom
        if (BackToPrevValueBloom)
        {
            if (TimeToBackToPrevValueBloom <= 0)
            {
                bloomIntensity = oldBloomIntensity;
             //   Debug.Log("bloomIntensity = oldBloomIntensity; = " + bloomIntensity + " = " + oldBloomIntensity);
                BackToPrevValueBloom = false;
            }
            else
            {
                TimeToBackToPrevValueBloom -= Time.deltaTime*5f;
                ChangeBloomAtRuntime(TimeToBackToPrevValueBloom);
            }
        }

        //Powrót do poprzednich efektów aberacji chromatycznej
        if (BackToPrevValueChAberration)
        {
            if (TimeToBackToPrevValueChAberration <= 0)
            {
                ChangeChAberrationAtRuntime(oldChAberrationIntensity,true);
                BackToPrevValueChAberration = false;
                audioSource.Stop();
            }
            else
            {
                TimeToBackToPrevValueChAberration -= Time.deltaTime;
                audioSource.volume = TimeToBackToPrevValueChAberration;
                ChangeChAberrationAtRuntime(TimeToBackToPrevValueChAberration, true);
            }
        }
    }

    //Zmiana intensywności efektu bloom
    public void ChangeBloomAtRuntime(float Intensity)
    {
        // zmiana intensywności w tymczasowej zmiennej wartości ustawień
        if (bloomIntensity < oldBloomIntensity)
        {
            bloomIntensity = oldBloomIntensity;
        }
        else
        {
            bloomIntensity = Intensity;
        }

        // ustaw ustawienia efektu bloom w aktualnym profilu na ustawienia tymczasowe ze zmienioną wartością
        ppProfile.bloom.settings = bloomSettings;
    }
    public void ChangeBloomAtRuntimeToPreviousSetting()
    {
        TimeToBackToPrevValueBloom = bloomIntensity - oldBloomIntensity;
        BackToPrevValueBloom = true;

        // ustaw ustawienia efektu bloom w aktualnym profilu na ustawienia poprzednie
    }
    public void ChangeChAberrationAtRuntime(float Intensity, bool changeMusic)
    {
        // zmiana intensywności w tymczasowej zmiennej wartości ustawień
        ChAberrationIntensity = Intensity;

        // ustaw ustawienia aberacji chromatycznej w aktualnym profilu na ustawienia tymczasowe ze zmienioną wartością
        ppProfile.chromaticAberration.settings = ChAberrationSettings;

        if (changeMusic)
        {
            if (MasterPitch <= 1)
            {
                audioMixer.SetFloat("MasterPitch", 1 - Intensity / 2);
            }
            else
            {
                audioMixer.SetFloat("MasterPitch", 1f);
            }
        }
    }

    public void ChangeChAberrationAtRuntimeToPreviousSetting()
    {
        TimeToBackToPrevValueChAberration = ChAberrationIntensity - oldChAberrationIntensity;
        BackToPrevValueChAberration = true;
        // ustaw ustawienia aberracji chromatycznej w aktualnym profilu na ustawienia poprzednie
    }
}
