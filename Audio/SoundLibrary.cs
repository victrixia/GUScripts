﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundLibrary : MonoBehaviour {

    [SerializeField]
    SoundGroup[] soundGroups;

    Dictionary<string, AudioClip[]> groupDictionary = new Dictionary<string, AudioClip[]> ();

    void Awake () {
        foreach (SoundGroup group in soundGroups) {
            groupDictionary.Add (group.groupID, group.sounds);
        }
    }

    public AudioClip GetClipFromName (string name) {
        if (groupDictionary.ContainsKey (name)) {
            AudioClip[] sounds = groupDictionary[name];
            return sounds[Random.Range (0, sounds.Length)];
        }
        return null;
    }

    [System.Serializable]
    public class SoundGroup {
        public string groupID;
        public AudioClip[] sounds;
    }

}