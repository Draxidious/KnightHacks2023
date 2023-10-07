using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.XR.Oculus;
using UnityEngine;

public class Interviewer : MonoBehaviour
{
    public GameObject head;
    public GameObject body;
    public GameObject left_hand;
    public GameObject right_hand;

    public Material source_mat;

    public CanvasRenderer mouth;

    public Sprite smile;
    public Sprite mouth_img;
    private Sprite current;
    public Sprite blink;
    public Sprite normal;
    public Sprite happy;
    public Sprite angry;

    // Start is called before the first frame update
    void Start()
    {
        Material shirt_color = new(source_mat);
        Material skin_color = new(source_mat);
        shirt_color.color = new Color(Random.value, Random.value, Random.value);
        body.GetComponent<MeshRenderer>().material = shirt_color;
        float val = Random.value;
        skin_color.color = new Color((75 + val * 165)/255, (55 + val * 150)/255, (10 + val * 120)/255);
        head.GetComponent<MeshRenderer>().material = skin_color;
        left_hand.GetComponent<MeshRenderer>().material = skin_color;
        right_hand.GetComponent<MeshRenderer>().material = skin_color;

        current = smile;
    }

    float eyetime = 0;
    float next_blink = 0;

    // Update is called once per frame
    void Update()
    {
        eyetime += Time.deltaTime;
        if (eyetime >= next_blink)
        {
            eyetime -= next_blink;
            Blink();
        }
    }

    private bool blinking = false;

    private void Blink()
    {
        blinking = !blinking;

        if (blinking)
        {

            return;
        }
    }

    private void SwapFace()
    {
        if (current == smile)
        {
            current = mouth_img;
            mouth.GetComponent<UnityEngine.UI.Image>().sprite = mouth_img;
        }
        else
        {
            current = smile;
            mouth.GetComponent<UnityEngine.UI.Image>().sprite = smile;
        }
    }
}
