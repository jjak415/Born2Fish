using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public Tracklist Tracklist;
    //private AudioClip _activeTrack;
    private AudioSource _source;
    private int _lastPlayedTrack;
    private bool _musicActive = false;


    void Awake()
    {
        _source = GetComponentInChildren<AudioSource>();

        if (_source == null)
        {
            Debug.LogWarning("No AudioSource assigned! Not playing any music.");
            return;
        }


        if (Tracklist == null)
        {
            Debug.LogWarning("No tracklist assigned! Not playing any music.");
            return;
        }

        StartPlaylist();
    }

    void StartPlaylist()
    {
        _musicActive = true;

        _source.clip = GetNextTrack_Random();
        _source.Play();
    
    }

    private AudioClip GetNextTrack_Random()
    {
        // This does not take duplicates in account
        int randomTrack = Random.Range(0, Tracklist.Tracks.Length);
        return Tracklist.Tracks[randomTrack];
    }

    void Update()
    {
        if (!_source.isPlaying)
        {
            // Play a new track
            _source.clip = GetNextTrack_Random();
            _source.Play();
        }
    }
}
