using UnityEngine;

public static class SoundManager
{
    public enum Sound { Speed, Lava, Obstacle, Coin, Pop }
    public static void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
        soundGameObject.transform.SetParent(GameObject.FindGameObjectWithTag("SoundsUsed").transform);

    }
    private static AudioClip GetAudioClip(Sound sound)
    {
        GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        foreach (GameManager.SoundAudioClip soundAudioClip in gm.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }

        return null;
    }
}
