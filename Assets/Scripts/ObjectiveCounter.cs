using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ObjectiveCounter : MonoBehaviour
{
    GameObject[] enemies;
    GameObject[] nests;
    GameObject[] queen;

    [SerializeField] TMP_Text objective1;
    [SerializeField] TMP_Text objective2;
    [SerializeField] TMP_Text objective3;

    bool allTasksCompleted = false;
    bool objectiveOneDone = false;
    bool objectiveTwoDone = false;
    bool objectiveThreeDone = false;
    
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        nests = GameObject.FindGameObjectsWithTag("Nest");
        queen = GameObject.FindGameObjectsWithTag("Queen");
    }

    public void viewCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    void updateObjectives()
    {
        // Objective 1
        if(enemies.Length > 0)
        {
            objective1.text = "Terminate all pests";
        }
        else
        {
            objective1.text = "<s>Terminate all pests</s>";
            objectiveOneDone = true;
        }

        // Objective 2
        if(nests.Length > 0)
        {
            objective2.text = "Burn all nests";
        }
        else
        {
            objective2.text = "<s>Burn all nests</s>";
            objectiveTwoDone = true;
        }

        // Objective 3
        if(queen.Length > 0)
        {
            objective3.text = "Kill the Queen";
        }
        else
        {
            objective3.text = "<s>Kill the Queen</s>";
            objectiveThreeDone = true;
        }
    }

    void isTasksDone()
    {
        if(objectiveOneDone == true && objectiveTwoDone == true && objectiveThreeDone == true)
        {
            allTasksCompleted = true;
            Cursor.lockState = CursorLockMode.None;
            viewCredits();
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        nests = GameObject.FindGameObjectsWithTag("Nest");
        queen = GameObject.FindGameObjectsWithTag("Queen");
        updateObjectives();
        isTasksDone();
    }
}
