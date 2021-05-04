using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzleRoomManager : MonoBehaviour
{
    public static PuzzleRoomManager Instance;
    public List<int> solution = new List<int>();
    public int state;
    public Transform[] rooms;
    [Tooltip("Corresponds to the number of runes\nto remember, *3. Default : 30. (So, 10 runes to remember.)")]public int difficulty = 30;

    private void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start()
    {
        for (int i = 3; i <= difficulty; i+=3)
        {
            solution.Add(Random.Range(i - 2, i));
        }
    }
    public void NextStep(int TPEntered)
    {
        if(TPEntered == solution[state])
        {
            state++;
        }
        else
        {
            state = 0;
        }
        StartCoroutine(Teleport());
    }
    IEnumerator Teleport()
    {
        PlayerMovement.Instance.canMove = false;
        yield return new WaitForSeconds(0.5f);
        PlayerMovement.Instance.transform.position = rooms[state].position;
        //PlayerMovement.Instance.transform.rotation = rooms[state].rotation;
        PlayerMovement.Instance.canMove = true;

    }
}
