using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CommonVideoPlayer : MonoBehaviour
{
    public VideoPlayer videoplayer;
    public Sprite[] play_Pause;
    public Sprite[] VolOn_VolOff;
    public Button play_Pause_Btn;
    public Button VolumeBtn;

    public AudioSource vAudio;
    public Slider volSlider;
    public Slider seekBarslider;
    private bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vAudio.volume = volSlider.value;
        if (volSlider.value > 0.01f && vAudio.mute == false)
        {
            OnVolumeOn();
        }
        else
        {
            OnVolumeOff();
        }

        if (!canMove && videoplayer.isPlaying)
        {
            seekBarslider.value = (float)videoplayer.frame / (float)videoplayer.frameCount;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Rig"))
        {
            videoplayer.Play();
        }
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag.Equals("Rig"))
        {
            videoplayer.Stop();
        }
        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void onplayButtonpressed()
    {
        if(play_Pause_Btn.image.sprite == play_Pause[1])
        {
            playBtn();
        }
        else
        {
            pauseBtn();
        }
    }


    private void playBtn()
    {
        videoplayer.Play();
        play_Pause_Btn.image.sprite = play_Pause[0];
    }

    private void pauseBtn()
    {
        videoplayer.Pause();
        play_Pause_Btn.image.sprite = play_Pause[1];
    }

    public void PointerDownOnSeekBar()
    {
        //stop slider value updation
        canMove = true;
    }

    public void PointerUpOnSeekBar()
    {
        float frame = (float)seekBarslider.value * (float)videoplayer.frameCount;
        videoplayer.frame = (long)frame;
        //resume slider value updation
        canMove = false;
    }


    public void VolumeBtnClick()
    {
        if (vAudio.mute == false)
        {
            volSlider.interactable = false;
            vAudio.mute = true;
            OnVolumeOff();

        }
        else
        {
            volSlider.interactable = true;
            vAudio.mute = false;
            OnVolumeOn();

        }
    }

    void OnVolumeOn()
    {
        VolumeBtn.GetComponent<Image>().sprite = VolOn_VolOff[0];
    }

    void OnVolumeOff()
    {
        VolumeBtn.GetComponent<Image>().sprite = VolOn_VolOff[1];
    }
}
