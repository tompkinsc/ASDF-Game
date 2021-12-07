using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugAnsController : MonoBehaviour
{
    public ChangeQuestion cq;
    public string answer;
    public Text ans;
    public bool isShown = true;

    // Start is called before the first frame update
    void Start()
    {
        ans.gameObject.SetActive(false);
    }

    public void UpdateAnswer()
    {
        answer = cq.correctAns;
        ans.text = answer;
    }

    public void showAnswer(bool show)
    {
        if(show)
        {
            ans.gameObject.SetActive(true);
        }
        else
        {
            ans.gameObject.SetActive(false);
        }
    }
}
