using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterController;

    [SerializeField]
    private float runSpeed = 10f;
    [SerializeField]
    private float moveSpeed = 10f;
    private float movHorizontal;
    [SerializeField]
    private Material[] playerColors;
    [SerializeField]
    private SkinnedMeshRenderer stickmanMeshRendered;
    [SerializeField]
    private Animator myAnim;

    [SerializeField]
    private int danceSteps;
    [SerializeField]
    private GameManager gameManager;
    public static bool ismoveStickMan;

   public enum gameStates
    {
        Start,Complete,Fail
    }
    [HideInInspector]
    public gameStates states;
    // Start is called before the first frame update
    void Start()
    {
       
        ismoveStickMan = false;
        states = gameStates.Start;

    }

    // Update is called once per frame
    void Update()
    {
        movHorizontal = Input.GetAxis("Horizontal");
        if (states == gameStates.Start && ismoveStickMan)
        {
            myAnim.SetTrigger("run");
            characterController.Move(Vector3.forward * runSpeed * Time.deltaTime + Vector3.right * moveSpeed * movHorizontal * Time.deltaTime);
            ClampPosition();
        }

      
    }
 
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("bluearea"))
        {
            stickmanMeshRendered.material = playerColors[0];
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.name.Equals("redarea"))
        {
            stickmanMeshRendered.material = playerColors[1];
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.tag.Equals("WrongMove"))
        {  other.gameObject.SetActive(false);
            danceSteps--;
            FindObjectOfType<GameManager>().DanceProgress(danceSteps);
            StartCoroutine(gameManager.ShowSadFace());
        }
        if (other.gameObject.tag.Equals("RightMove"))
        {
            other.gameObject.SetActive(false);
            danceSteps++;
            FindObjectOfType<GameManager>().DanceProgress(danceSteps);
               StartCoroutine(gameManager.ShowHappyFace());
        }
        if (other.gameObject.name.Equals("FinishPoint") && danceSteps>=5)
        {
            runSpeed = 0f;
            moveSpeed = 0f;
            gameManager.LevelComplete();
            myAnim.SetTrigger("breakdance");
        }
        if (other.gameObject.name.Equals("FinishPoint") && danceSteps <5)
        {
            runSpeed = 0f;
            moveSpeed = 0f;
            gameManager.LevelFail();
            myAnim.SetTrigger("defeat");
           
        }
    }

    public void ClampPosition()
    {
        Vector3 playerPosition = transform.position;
        playerPosition.x = Mathf.Clamp(playerPosition.x, -40f, 40f);
        transform.position = playerPosition;

    }

   
}
