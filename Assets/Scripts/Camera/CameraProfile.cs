using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.Audio;
public class CameraProfile : MonoBehaviour {

    public PostProcessingProfile ppProfile;

    public float bloomIntensity;
    public float oldBloomIntensity;

    public float ChAberrationIntensity;
    public float oldChAberrationIntensity;

    public bool BackToPrevValueBloom;
    public bool BackToPrevValueChAberration;

    private float TimeToBackToPrevValueBloom;
    private float TimeToBackToPrevValueChAberration;

    private BloomModel.Settings bloomSettings;
    private ChromaticAberrationModel.Settings ChAberrationSettings;

    public AudioSource audioSource;
    public AudioClip ChAberrationaudioClip;

    public AudioMixer audioMixer;

    public float MasterPitch;

    // Use this for initialization
    void Start()
    {
        bloomSettings = ppProfile.bloom.settings;
        ChAberrationSettings = ppProfile.chromaticAberration.settings;

        oldBloomIntensity = bloomSettings.bloom.intensity;
        oldChAberrationIntensity = ChAberrationSettings.intensity;

        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = 0;

        bloomSettings.bloom.intensity = 0.5f;
        ChAberrationSettings.intensity = 0f;

        ppProfile.bloom.settings = bloomSettings;
        ppProfile.chromaticAberration.settings = ChAberrationSettings;
        audioMixer.SetFloat("MasterPitch", 1f);

    }
    // Update is called once per frame
    void Update () {
        audioMixer.GetFloat("MasterPitch", out MasterPitch);
        Debug.Log(MasterPitch);

        bloomSettings.bloom.intensity= bloomIntensity;
        ChAberrationSettings.intensity = ChAberrationIntensity;

        if (BackToPrevValueBloom)
        {
            if (TimeToBackToPrevValueBloom <= 0)
            {
                bloomIntensity = oldBloomIntensity;
                Debug.Log("bloomIntensity = oldBloomIntensity; = " + bloomIntensity + " = " + oldBloomIntensity);
                BackToPrevValueBloom = false;
            }
            else
            {
                TimeToBackToPrevValueBloom -= Time.deltaTime*5f;
                ChangeBloomAtRuntime(TimeToBackToPrevValueBloom);

            }
        }

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

    public void ChangeBloomAtRuntime(float Intensity)
    {
        //change the intensity in the temporary settings variable
        if (bloomIntensity < oldBloomIntensity)
        {
            bloomIntensity = oldBloomIntensity;
        }
        else
        {
            bloomIntensity = Intensity;
        }

        //set the bloom settings in the actual profile to the temp settings with the changed value
        ppProfile.bloom.settings = bloomSettings;
    }
    public void ChangeBloomAtRuntimeToPreviousSetting()
    {
        TimeToBackToPrevValueBloom = bloomIntensity - oldBloomIntensity;
        BackToPrevValueBloom = true;

        //set the bloom settings in the actual profile to the temp settings with the changed value
    }
    public void ChangeChAberrationAtRuntime(float Intensity, bool changeMusic)
    {
        //change the intensity in the temporary settings variable
        ChAberrationIntensity = Intensity;

        //set the chromaticAberration settings in the actual profile to the temp settings with the changed value
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

        //set the chromaticAberration settings in the actual profile to the temp settings with the changed value
    }

}
