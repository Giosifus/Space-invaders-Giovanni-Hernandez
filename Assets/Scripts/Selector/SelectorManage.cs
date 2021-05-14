using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SelectorManage : MonoBehaviour
{
    public GameObject Transition;
    public Image Instructions;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void changeImageInst(Sprite img){
        Instructions.sprite = img;
    }
    public void SetGameDifficulty(int GameDifficulty){
        PlayerPrefs.SetInt("Difficulty", GameDifficulty);
        StartCoroutine(CambiarEscena());
    }

    IEnumerator CambiarEscena(){
        Transition.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Game");
    }
    public void EnableGameObject(GameObject Enable){
        Enable.SetActive(true);
    }
    public void DisableGameObject(GameObject Disable){
        Disable.SetActive(false);
    }
}
