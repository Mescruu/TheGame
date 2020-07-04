using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class CutScene : MonoBehaviour {

    public GameObject Player;
    private Game_Master gm;
    public PlayableDirector playableDirector;
    public BoxCollider2D boxCollider2D;
    public ChangePostProcesingSetting changePost;
    // Use this for initialization
    void Start () {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();
        if(boxCollider2D!=null)
        {
            playableDirector.Play();
        }
    }

    // Update is called once per frame
    void Update () {
        
        if (playableDirector.time>=playableDirector.duration-0.1f)
        {
            playableDirector.Stop();
            Destroy(gameObject);
        }

	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playableDirector.gameObject.SetActive(true);
            if (changePost!=null)
            {
                changePost.ChangeCamSet = true;
            }
        }
    }
}
