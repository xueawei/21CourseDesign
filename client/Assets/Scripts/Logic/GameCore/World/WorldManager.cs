using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : BaseMgr<WorldManager>
{
    // 场景状态机
    enum LoadState
    {
        // 初始化状态
        Init,
        // 加载场景状态
        LoadScene,
        // 更新状态
        Update,
        // 等待状态
        Wait,
    }
    private LoadState mState;
    private string mLoadSceneName;


    // 初始化
    public void Init()
    {
        EnterState(LoadState.Init);
    }


    // 世界更新
    public void Update()
    {
        if (mState == LoadState.Init)
        {

        }

        if (mState == LoadState.LoadScene)
        {
            EnterState(LoadState.Wait);
            // "rpgpp_lt_scene_1.0"
            ResManager.Instance.LoadSceneAsync(mLoadSceneName, () =>
            {
                // 等待场景加载完成后，加载玩家到场景中
                LoadMainPlayer();
            });
            //ResManager.Instance.LoadScene(mLoadSceneName);
        }
    }


    // 世界管理中的加载场景
    public void LoadScene(string name)
    {
        mLoadSceneName = name;

        EnterState(LoadState.LoadScene);
    }


    // 改变当前的状态机
    private void EnterState(LoadState state)
    {
        mState = state;
    }

    private void LoadMainPlayer()
    {
        GameObject mainPlayer = ResManager.Instance.InstantiateGameObject("Assets/Res/Role/Peasant Nolant Blue(Free Version).prefab");
        if (mainPlayer == null)
        {
            Debug.LogError("Load Main Player Error");
        }

        mainPlayer.transform.position = new Vector3(63.0f, 22.23f, 43.0f);

        //mainPlayer.GetComponent<UnityEngine.Animator>().Play("metarig|Idle");
        mainPlayer.GetComponent<UnityEngine.Animator>().Play("metarig|Walk");
    }
}
