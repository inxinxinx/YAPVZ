using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum LvState
{
    start,
    fighting,
    end
}
public class ProcessManager : MonoBehaviour
{
    
    public static ProcessManager Instance;
    private LvState currLVState;
    //��ǰ�ؿ���Ϣ
    private int level = 1;
    private LevelConf levelConf = new LevelConf();

    private bool isOver;
    // ��ˢ�½�ʬ��
    private bool isUpdatingZombie;

    // �ؿ��еĽ׶� ����
    //private int stageInLV;

    private UnityAction LVStartAction;

    public LvState CurrLVState
    {
        get => currLVState;
        set
        {
            currLVState = value;
            switch (currLVState)
            {
                case LvState.start:
                    LVStart();
                    break;
                case LvState.fighting:
                    AudioManager.Instance.PlayEFAudio(LevelManager.instance.gameConf.ZombieComing);
                    // 20���Ժ�ˢһֻ��ʬ
                    UpdateZombie(3, 1);
                    break;
                case LvState.end:
                    break;
            }
        }
    }
    /*
    public int StageInLV
    {
        get => stageInLV;
        set
        {
            stageInLV = value;
            UIManager.Instance.UpdateStageNum(stageInLV - 1);
            if (stageInLV > 2)
            {
                // ɱ����ǰ�ؿ���ȫ����ʬ���ͽ�����һ��
                ZombieManager.Instance.AddAllZombieDeadAction(OnAllZombieDeadAction);
                CurrLVState = LVState.Over;
            }

        }
    }*/

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        levelConf.getLevelConf(level);
        CurrLVState = LvState.start;
    }
    // ��ʼ�ؿ�
    
    private void Update()
    {
        if (isOver) return;
        FSM();
    }

    public void FSM()
    {
        switch (CurrLVState)
        {
            case LvState.start:
                break;
            case LvState.fighting:
                // ˢ��ʬ
                // ���û����ˢ�½�ʬ����ˢ�½�ʬ
                if (isUpdatingZombie == false)
                {
                    // ��ʬˢ�µ�ʱ��
                    float updateTime = Random.Range(15 - levelConf.createZombieCD / 2, 20 - levelConf.createZombieCD / 2);
                    // ��ʬˢ�µ�����
                    int updateNum = Random.Range(1, 2 + level);
                    UpdateZombie(updateTime, updateNum);
                }
                break;
            case LvState.end:
                break;
        }
    }

    private void LVStart()
    {
        // �����⿪ʼ����
        SkySunManager.instance.StartCreatSun(levelConf.firstZombie);
        // ��ʼ��ʾUI��Ч
        UIManager.Instance.ShowLVStartEF();
        // �������ʬ
        ZombieManager.instance.ClearZombie();
        Invoke("StartFighting", levelConf.firstZombie);

        // �ؿ���ʼʱ��Ҫ��������
        if (LVStartAction != null) LVStartAction();
    }

    private void StartFighting()
    {
        currLVState = LvState.fighting;
    }

    /// <summary>
    /// ���½�ʬ
    /// </summary>
    private void UpdateZombie(float delay, int zombieNum)
    {
        StartCoroutine(DoUpdateZombie(delay, zombieNum));
    }

    IEnumerator DoUpdateZombie(float delay, int zombieNum)
    {
        isUpdatingZombie = true;
        yield return new WaitForSeconds(delay);
        ZombieManager.instance.UpdateZombie(zombieNum);
        ZombieManager.instance.ZombieStartMove();
        isUpdatingZombie = false;
    }
    /// <summary>
    /// ��ӹؿ���ʼ�¼��ļ�����
    /// </summary>
    public void AddLVStartActionLinstener(UnityAction action)
    {
        LVStartAction += action;
    }

    /// <summary>
    /// ��ȫ����ʬ����ʱ�������¼�
    /// </summary>
    private void OnAllZombieDeadAction()
    {
        // ִ��һ��֮���Լ��Ƴ�ί��
        ZombieManager.instance.RemoveAllZombieDeadAction(OnAllZombieDeadAction);
        // ����ʤ��
        winwinwin();
    }

    private void winwinwin()
    {
        StopAllCoroutines();
        AudioManager.Instance.PlayEFAudio(LevelManager.instance.gameConf.GameOver);

        isOver = true;
        // �߼�
        SkySunManager.instance.StopCreatSun();
        ZombieManager.instance.ClearZombie();

        //UIManager.Instance.Win();
    }

    /// <summary>
    /// ��Ϸ����
    /// </summary>
    public void GameOver()
    {
        StopAllCoroutines();
        // Ч��d
        AudioManager.Instance.PlayEFAudio(LevelManager.instance.gameConf.ZombieEat);
        AudioManager.Instance.PlayEFAudio(LevelManager.instance.gameConf.GameOver);

        isOver = true;
        // �߼�
        SkySunManager.instance.StopCreatSun();
        ZombieManager.instance.ClearZombie();

        // UI
        UIManager.Instance.ShowOverPanel();
    }
}
