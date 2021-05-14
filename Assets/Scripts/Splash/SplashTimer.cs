using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SplashTimer : MonoBehaviour
{

    VideoPlayer SplashVid;
    public string NameVideo;
    // Start is called before the first frame update

    void Start(){
        SplashVid = gameObject.GetComponent<VideoPlayer>();
        StartCoroutine(PlayVid());
    }
    IEnumerator PlayVid(){
        SplashVid.url = Application.streamingAssetsPath + "/" + NameVideo + ".mp4";
        yield return new WaitForSeconds(1);
        SplashVid.loopPointReached += CheckOver;
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene("Selector");
    }
}
