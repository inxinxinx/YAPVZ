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
    //当前关卡信息
    private int level = 1;
    private LevelConf levelConf;

    private bool isOver;
    // 在刷新僵尸中
    private bool isUpdatingZombie;

    // 关卡中的阶段 波数
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
                    // 显示主面板
                    //UIManager.Instance.SetMainPanelActive(true);
                    // 20秒以后刷一只僵尸
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
                // 杀掉当前关卡的全部僵尸，就进入下一天
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
    // 开始关卡
    
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
                // 刷僵尸
                // 如果没有在刷新僵尸，则刷新僵尸
                if (isUpdatingZombie == false)
                {
                    // 僵尸刷新的时间
                    float updateTime = Random.Range(15 - levelConf.createZombieCD / 2, 20 - levelConf.createZombieCD / 2);
                    // 僵尸刷新的数量
                    int updateNum = Random.Range(1, 2 + level);
                    UpdateZombie(updateTime, updateNum);
                }
                break;
            case LvState.end:
                break;
        }
    }
    /// <summary>
    /// 关卡开始时 摄像机回归后要执行的方法
    /// </summary>
    private void LVStartCameraBackAction()
    {
        // 让阳光开始创建
        SkySunManager.instance.StartCreatSun(levelConf.firstZombie);
        // 开始显示UI特效
        UIManager.Instance.ShowLVStartEF();
        // 清理掉僵尸
        ZombieManager.instance.ClearZombie();
        CurrLVState = LvState.fighting;

        // 关卡开始时需要做的事情
        if (LVStartAction != null) LVStartAction();

    }
    /// <summary>
    /// 更新僵尸
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
    /// 添加关卡开始事件的监听者
    /// </summary>
    public void AddLVStartActionLinstener(UnityAction action)
    {
        LVStartAction += action;
    }

    /// <summary>
    /// 当全部僵尸死亡时触发的事件
    /// </summary>
    private void OnAllZombieDeadAction()
    {
        // 更新天数
        CurrLV += 1;
        // 执行一次之后，自己移除委托
        ZombieManager.instance.RemoveAllZombieDeadAction(OnAllZombieDeadAction);
    }

    /// <summary>
    /// 游戏结束
    /// </summary>
    public void GameOver()
    {
        StopAllCoroutines();
        // 效果d
        //AudioManager.Instance.PlayEFAudio(GameManager.Instance.GameConf.ZombieEat);
        //AudioManager.Instance.PlayEFAudio(GameManager.Instance.GameConf.GameOver);

        isOver = true;
        // 逻辑
        SkySunManager.instance.StopCreatSun();
        ZombieManager.instance.ClearZombie();

        // UI
        UIManager.Instance.GameOver();
    }
}

*/