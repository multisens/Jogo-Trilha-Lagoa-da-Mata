using UnityEngine;
using UnityEngine.Video;

public class Colisao_Video_Curiosidade : MonoBehaviour
{
    public GameObject Ambiente;
    [SerializeField] private VideoPlayer myVideoPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("peao"))
        {
            Ambiente.SetActive(false);
            myVideoPlayer.Stop();
            myVideoPlayer.Play();
        }
    }

    private void Start()
    {
        myVideoPlayer.loopPointReached += OnVideoLoopPointReached;
    }

    private void OnVideoLoopPointReached(VideoPlayer vp)
    {
        Ambiente.SetActive(true);
    }
}
