using System.Collections;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioMixer mixer;

	public Sound[] sounds;

	public enum MusicSet {Woods, Bathroom, Conductor, Ocean, Space, Nothing}

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.volume = s.volume;
			s.source.outputAudioMixerGroup = s.mixerGroup;
		}
	}

	public void Play(string sound)
	{
		Sound s = getSoundByName(sound);
		s.source.Play();
	}

	private Sound getSoundByName(string name)
	{
		Sound s = Array.Find(sounds, item => item.name == name);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found! Playing default sound.");
			return sounds[0];
		}
		return s;
	}

	public void PlaySet(MusicSet set)
	{
		// Fade Parameter:
		// StartFade(AudioMixer audioMixer, String exposedParameter, float duration, float targetVolume));
		switch(set)
		{
			case MusicSet.Woods:
				Sound[] woods = new Sound[2];
				woods[0] = getSoundByName("Train");
				woods[1] = getSoundByName("Crowd");
				foreach (var item in woods)
				{
					item.source.Play();
					StartCoroutine(FadeMixerGroup.StartFade(mixer, item.name + "Volume", 3f, 1f));
				}
				break;
			case MusicSet.Bathroom:
				Sound crowd = getSoundByName("Crowd");
				StartCoroutine(FadeMixerGroup.StartFade(mixer, crowd.name + "Volume", 0.5f, 0f));
				break;
			case MusicSet.Conductor:
				Sound conductor = getSoundByName("Conductor");
				Sound ocean = getSoundByName("Ocean");
				StartCoroutine(conductorQueue(conductor, ocean));
				break;
			case MusicSet.Ocean:
				crowd = getSoundByName("Crowd");
				StartCoroutine(FadeMixerGroup.StartFade(mixer, crowd.name + "Volume", 3f, 1f));
				break;
			case MusicSet.Space:
				crowd = getSoundByName("Crowd");
				ocean = getSoundByName("Ocean");
				StartCoroutine(FadeMixerGroup.StartFade(mixer, crowd.name + "Volume", 2f, 0f));
				StartCoroutine(FadeMixerGroup.StartFade(mixer, ocean.name + "Volume", 2f, 0f));
				Sound space = getSoundByName("Space");
				space.source.Play();
				StartCoroutine(FadeMixerGroup.StartFade(mixer, space.name + "Volume", 4f, 1f));
				break;
			case MusicSet.Nothing:
				Sound[] allSounds = new Sound[2];
				allSounds[0] = getSoundByName("Train");
				allSounds[1] = getSoundByName("Space");
				foreach (var item in allSounds)
				{
					StartCoroutine(FadeMixerGroup.StartFade(mixer, item.name + "Volume", 10f, 0f));
				}
				break;
			default:
				Debug.LogWarning(set + " is not a valid Music set.");
				break;
		}
	}

	IEnumerator conductorQueue(Sound conductor, Sound ocean)
	{
		conductor.source.Play();
		yield return new WaitForSeconds(48);
		//yield return new WaitForSeconds(10);

		ocean.source.Play();
		StartCoroutine(FadeMixerGroup.StartFade(mixer, ocean.name + "Volume", 3f, 1f));
	}

}
