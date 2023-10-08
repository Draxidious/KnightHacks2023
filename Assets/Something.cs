using System.Collections;
using System.Collections.Generic;
using OpenAI;
using UnityEngine;

public class Something : MonoBehaviour
{
    [SerializeField] GameObject chat;
    ChatGPT comp;
    // Start is called before the first frame update
    void Start()
    {
    print(chat.GetType());
       comp = chat.GetComponent<ChatGPT>();
    }

    public void RunResponse()
    {
        waiter();
        comp.ResponseAfterUserInput();
    }

    IEnumerator waiter()
{



    yield return new WaitForSeconds(20);

   
}
}
