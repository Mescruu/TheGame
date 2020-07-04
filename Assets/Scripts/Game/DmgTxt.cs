using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DmgTxt : MonoBehaviour {
    public Animator animator;
    public Text dmgTxt;
    public AudioSource audioSource;
    public AudioClip[] audioClip;
    public Color[] dmgColor;
    public float SoundTime;
    
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        //  AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        // Destroy(gameObject, clipInfo[0].clip.length);
        Destroy(gameObject, SoundTime);
    }
    public void SetTxt(string txt, Color color,bool critic)
    {
        if (critic)
        {
            dmgTxt.text = txt;

        }
        else
        {
            dmgTxt.color = color;
            Debug.Log("change color");
            dmgTxt.text = txt;
        }
    }
    public void SetTxt(string txt, DmgType dmgType, bool critic)
    {
        Color color = Color.white;
   
                color = dmgColor[(int)dmgType];
         

        if (critic)
        {
            dmgTxt.color = color;
            Debug.Log("change color");
            dmgTxt.text = txt;
            Debug.Log((int)dmgType);

            audioSource.clip = audioClip[(int)dmgType];
            audioSource.Play();
        }
        else
        {
            dmgTxt.color = color;
            Debug.Log("change color");
            dmgTxt.text = txt;
        }
    }
}




