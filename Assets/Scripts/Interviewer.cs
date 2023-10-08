using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.Oculus;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Interviewer : MonoBehaviour
{
    public GameObject head;
    public GameObject body;
    public GameObject left_hand;
    public GameObject right_hand;
    public GameObject left_hand_target;
    public GameObject right_hand_target;

    public Material source_mat;

    public CanvasRenderer mouth;
    public CanvasRenderer eyes;

    public Sprite blink_eyes;
    public Sprite normal_eyes;
    public Sprite happy_eyes;
    public Sprite angry_eyes;
    public Sprite default_eyes;

    public Sprite talk1_mouth;
    public Sprite talk2_mouth;
    public Sprite normal_mouth;
    public Sprite happy_mouth;
    public Sprite angry_mouth;
    public Sprite default_mouth;

    public GameObject head_object;
    private Vector3 head_default_position;

    // Start is called before the first frame update
    void Start()
    {
        Material shirt_color = new(source_mat);
        Material skin_color = new(source_mat);
        shirt_color.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        body.GetComponent<MeshRenderer>().material = shirt_color;
        float val = UnityEngine.Random.value;
        skin_color.color = new Color((75 + val * 165)/255, (55 + val * 150)/255, (10 + val * 120)/255);
        head.GetComponent<MeshRenderer>().material = skin_color;
        left_hand.GetComponent<MeshRenderer>().material = skin_color;
        right_hand.GetComponent<MeshRenderer>().material = skin_color;

        current_eyes = happy_eyes;
        eyes.GetComponent<UnityEngine.UI.Image>().sprite = happy_eyes;
        current_mouth = happy_mouth;
        mouth.GetComponent<UnityEngine.UI.Image>().sprite = happy_mouth;
        head_default_position = head_object.transform.position;
    }

    float eyetime = 0;
    float next_blink = 0;
    float talktime = 0;
    float next_talk_switch = 0;
    int current_talk = 0;
    float movetime = 0;

    // Update is called once per frame
    void Update()
    {
        eyetime += Time.deltaTime;
        if (eyetime >= next_blink)
        {
            eyetime -= next_blink;
            Blink();
        }

        if (talking)
        {
            talktime += Time.deltaTime;
            if (talktime >= next_talk_switch)
            {
                talktime -= next_talk_switch;
                next_talk_switch = UnityEngine.Random.value * 0.15f + 0.05f;
                
                if (++current_talk%2 == 0)
                    mouth.GetComponent<UnityEngine.UI.Image>().sprite = talk1_mouth;
                else
                    mouth.GetComponent<UnityEngine.UI.Image>().sprite = talk2_mouth;
            }
        }

        movetime += Time.deltaTime/2.5f;
        head_object.transform.position = head_default_position + new Vector3(Mathf.PerlinNoise(20, movetime) - 0.5f, (Mathf.PerlinNoise(40, movetime) - 0.5f) * 0.4f, Mathf.PerlinNoise(60, movetime) - 0.5f) * 0.075f;
    }

    private bool blinking = false;
    private Sprite current_eyes;

    private void Blink()
    {
        blinking = !blinking;

        if (blinking)
        {
            eyes.GetComponent<UnityEngine.UI.Image>().sprite = blink_eyes;
            next_blink = 0.2f;
            return;
        }
        eyes.GetComponent<UnityEngine.UI.Image>().sprite = current_eyes;
        next_blink = UnityEngine.Random.value * 7 + 0.5f;
    }

    public void SetEyes(int new_eyes)
    {
        Sprite choice = new_eyes switch
        {
            0 => happy_eyes,
            1 => normal_eyes,
            2 => angry_eyes,
            _ => default_eyes,
        };

        if (!blinking)
            eyes.GetComponent<UnityEngine.UI.Image>().sprite = choice;
        current_eyes = choice;
    }
    
    private bool talking = false;
    private Sprite current_mouth;

    private void SetTalking(bool am_i_gonna_talk_or_no)
    {
        if (talking != am_i_gonna_talk_or_no)
        {
            if (am_i_gonna_talk_or_no)
            {
                left_hand_target.transform.position += new Vector3(0, 0.15f, 0);
                right_hand_target.transform.position += new Vector3(0, 0.15f, 0);
            }
            else
            {
                left_hand_target.transform.position += new Vector3(0, -0.15f, 0);
                right_hand_target.transform.position += new Vector3(0, -0.15f, 0);
            }
        }

        talking = am_i_gonna_talk_or_no;

        if (talking)
        {
            eyes.GetComponent<UnityEngine.UI.Image>().sprite = talk1_mouth;
            next_talk_switch = UnityEngine.Random.value * 0.2f + 0.1f;
            return;
        }

        mouth.GetComponent<UnityEngine.UI.Image>().sprite = current_mouth;
        next_blink = UnityEngine.Random.value * 7 + 0.5f;
    }

    public void SetMouth(int new_mouth)
    {
        Sprite choice = new_mouth switch
        {
            0 => happy_mouth,
            1 => normal_mouth,
            2 => angry_mouth,
            _ => default_mouth,
        };

        if (!talking)
            mouth.GetComponent<UnityEngine.UI.Image>().sprite = choice;
        current_mouth = choice;
    }
}
