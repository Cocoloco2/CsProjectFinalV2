using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heart_script : MonoBehaviour
{
    public Sprite fullHeart, halfHeart, emptyHeart;
    Image heartImage;

    private void Awake()
    {
        heartImage = GetComponent<Image>();
    }

    //determine what state the heart is in and assign a sprite to the state
    public void SetHeartImage(HeartStatus status) //recieves a value that can either be 0, 1 and 2 from 'Heartstatus'
    {
        switch (status) //switch statement to handle the different states
        {
            case HeartStatus.Empty:
                heartImage.sprite = emptyHeart; 
                break;
            case HeartStatus.Half:
                heartImage.sprite = halfHeart;
                break;
            case HeartStatus.Full:
                heartImage.sprite = fullHeart;
                break;
        }
        
        }
}

public enum HeartStatus //assign values corresponding to different states
{
Empty = 0,
Half = 1,
Full= 2,
}
