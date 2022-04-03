using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ������Ч����
    /// </summary>
    public void PlayEFAudio(AudioClip clip)
    {
        // �Ӷ���ػ�ȡһ����Ч����
        EFAudio ef = PoolManager.Instance.GetObj(LevelManager.instance.gameConf.EFAudio).GetComponent<EFAudio>();
        ef.Init(clip);
    }

}
