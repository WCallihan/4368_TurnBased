using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//miscellaneous script made for the buttons without dedacated scripts so they can still have sound effects
public class PlaySoundClip : MonoBehaviour {
    
    public void PlaySound(AudioClip clip) {
		AudioHelper.PlayClip2D(clip);
	}
}