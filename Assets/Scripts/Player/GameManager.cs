using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{   
   
    public GameObject finishPoint,Player;
    float maxDistance;
    [SerializeField]
    private Slider LevelProgressSlider;
    [SerializeField]
    private Slider danceBar;
    [SerializeField]
    private GameObject startPanel,failPanel,completePanel;
    [SerializeField]
    private GameObject[] particles;
    [SerializeField]
    private GameObject cam1,cam2;
    [SerializeField]
    private GameObject happyEmoji, SadEmoji;
    // Start is called before the first frame update
    void Start()
    {
      
        maxDistance = ReturnDistance();
    }
    public void SetProgress(float p)
    {
        LevelProgressSlider.value = p;
    }
    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.z <= maxDistance && Player.transform.position.z <= finishPoint.transform.position.z)
        {
            float distance = 1.032f - (ReturnDistance() / maxDistance);
            SetProgress(distance);
        }
    }
    public float ReturnDistance()
    {
        return Vector3.Distance(Player.transform.position, finishPoint.transform.position);
    }

    public void DanceProgress(int value)
    {
        danceBar.value = value;
    }

   public void PlayGame()
    {
        startPanel.SetActive(false);
        PlayerController.ismoveStickMan = true;
    }

    public void LevelComplete()
    {
        for(int i=0; i < particles.Length; i++)
        {
            particles[i].SetActive(true);
        }
        cam1.SetActive(false);
        cam2.SetActive(true);
        completePanel.SetActive(true);
    }

    public void LevelFail()
    {
        failPanel.SetActive(true);
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

    public IEnumerator ShowHappyFace()
    {
        happyEmoji.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        happyEmoji.SetActive(false);
    }
   public IEnumerator ShowSadFace()
    {
        SadEmoji.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        SadEmoji.SetActive(false);
    }
}
