using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    [Header("加载配置")]
    public float minLoadingTime = 1.0f; // 最小加载时间，避免闪烁
    public bool preloadNextScene = true; // 是否预加载下一个场景

    // 事件系统
    public static event Action<string> OnSceneLoadStart;
    public static event Action<float> OnSceneLoadProgress;
    public static event Action<string> OnSceneLoadComplete;
    public static event Action<string> OnSceneLoadFailed;

    private AsyncOperation currentLoadOperation;
    private bool isLoading = false;

    void Awake()
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

    /// <summary>
    /// 异步加载场景（推荐使用）
    /// </summary>
    public async Task<bool> LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
        if (isLoading)
        {
            Debug.LogWarning($"场景正在加载中，无法加载新场景: {sceneName}");
            return false;
        }

        isLoading = true;
        OnSceneLoadStart?.Invoke(sceneName);

        try
        {
            // 预处理：清理内存
            await PreLoadProcess();

            // 开始加载
            currentLoadOperation = SceneManager.LoadSceneAsync(sceneName, mode);
            currentLoadOperation.allowSceneActivation = false;

            float startTime = Time.time;

            // 监控加载进度
            while (!currentLoadOperation.isDone)
            {
                float progress = Mathf.Clamp01(currentLoadOperation.progress / 0.9f);
                OnSceneLoadProgress?.Invoke(progress);

                // 当加载完成且满足最小加载时间时，激活场景
                if (currentLoadOperation.progress >= 0.9f && Time.time - startTime >= minLoadingTime)
                {
                    currentLoadOperation.allowSceneActivation = true;
                }

                await Task.Yield();
            }

            // 后处理
            await PostLoadProcess(sceneName);

            OnSceneLoadComplete?.Invoke(sceneName);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"场景加载失败: {sceneName}, 错误: {e.Message}");
            OnSceneLoadFailed?.Invoke(sceneName);
            return false;
        }
        finally
        {
            isLoading = false;
            currentLoadOperation = null;
        }
    }

    /// <summary>
    /// 同步加载场景（仅用于小场景）
    /// </summary>
    public bool LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
        if (isLoading)
        {
            Debug.LogWarning($"场景正在加载中，无法加载新场景: {sceneName}");
            return false;
        }

        try
        {
            OnSceneLoadStart?.Invoke(sceneName);
            SceneManager.LoadScene(sceneName, mode);
            OnSceneLoadComplete?.Invoke(sceneName);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"同步加载场景失败: {sceneName}, 错误: {e.Message}");
            OnSceneLoadFailed?.Invoke(sceneName);
            return false;
        }
    }

    /// <summary>
    /// 预加载处理
    /// </summary>
    private async Task PreLoadProcess()
    {
        // 强制垃圾回收
        System.GC.Collect();

        // 清理资源缓存
        Resources.UnloadUnusedAssets();

        // 等待一帧，确保清理完成
        await Task.Yield();
    }

    /// <summary>
    /// 后加载处理
    /// </summary>
    private async Task PostLoadProcess(string sceneName)
    {
        // 预加载下一个可能的场景
        if (preloadNextScene)
        {
            string nextScene = GetNextSceneName(sceneName);
            if (!string.IsNullOrEmpty(nextScene))
            {
                _ = PreloadSceneAsync(nextScene);
            }
        }

        await Task.Yield();
    }

    /// <summary>
    /// 预加载场景到内存
    /// </summary>
    public async Task PreloadSceneAsync(string sceneName)
    {
        if (SceneCache.Instance.IsSceneCached(sceneName))
            return;

        var preloadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        preloadOperation.allowSceneActivation = false;

        while (!preloadOperation.isDone && preloadOperation.progress < 0.9f)
        {
            await Task.Yield();
        }

        SceneCache.Instance.CacheScene(sceneName, preloadOperation);
    }

    /// <summary>
    /// 获取下一个场景名称（可根据项目需求自定义）
    /// </summary>
    private string GetNextSceneName(string currentScene)
    {
        // 这里可以根据项目的场景跳转逻辑来实现 比如使用ScriptableObject配置好做动态读取
        // 示例：简单的场景序列
        switch (currentScene)
        {
            //case "MainMenu": return "GameScene";
            //case "GameScene": return "ResultScene";
            default: return null;
        }
    }
}