using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip[] soundtracks;
    private AudioSource audio;
    private int songIndex = 0;
    public AudioClip CurrentSong => soundtracks[songIndex];
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        audio.Stop();
        DontDestroyOnLoad(gameObject);
        NextSong();
    }   

    private void NextSong()
    {
        songIndex = (songIndex + 1) % soundtracks.Length;
        audio.PlayOneShot(CurrentSong);
        StartCoroutine(SongEndTimer(CurrentSong.length));
    }

    private IEnumerator SongEndTimer(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        NextSong();
    }
}