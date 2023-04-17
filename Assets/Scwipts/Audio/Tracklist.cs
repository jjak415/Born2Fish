using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Tracklist")]
public class Tracklist : ScriptableObject
{

    public AudioClip[] Tracks = new AudioClip[0];

}
