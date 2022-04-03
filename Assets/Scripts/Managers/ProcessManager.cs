using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/*
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
    private LevelConf levelConf;

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
                    break;
                case LvState.fighting:
                    // ��ʾ�����
                    //UIManager.Instance.SetMainPanelActive(true);
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


/*


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        levelConf.getLevelConf(level);
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
    /// <summary>
    /// �ؿ���ʼʱ ������ع��Ҫִ�еķ���
    /// </summary>
    private void LVStartCameraBackAction()
    {
        // �����⿪ʼ����
        SkySunManager.instance.StartCreatSun(levelConf.firstZombie);
        // ��ʼ��ʾUI��Ч
        UIManager.Instance.ShowLVStartEF();
        // �������ʬ
        ZombieManager.instance.ClearZombie();
        CurrLVState = LvState.fighting;

        // �ؿ���ʼʱ��Ҫ��������
        if (LVStartAction != null) LVStartAction();

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
        StageInLV += 1;
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
        // ��������
        CurrLV += 1;
        // ִ��һ��֮���Լ��Ƴ�ί��
        ZombieManager.instance.RemoveAllZombieDeadAction(OnAllZombieDeadAction);
    }

    /// <summary>
    /// ��Ϸ����
    /// </summary>
    public void GameOver()
    {
        StopAllCoroutines();
        // Ч��d
        //AudioManager.Instance.PlayEFAudio(GameManager.Instance.GameConf.ZombieEat);
        //AudioManager.Instance.PlayEFAudio(GameManager.Instance.GameConf.GameOver);

        isOver = true;
        // �߼�
        SkySunManager.instance.StopCreatSun();
        ZombieManager.instance.ClearZombie();

        // UI
        UIManager.Instance.GameOver();
    }
}

*/