using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCache : MonoBehaviour
{
    public static SceneCache Instance { get; private set; }

    [Header("缓存配置")]
    public int maxCacheSize = 3; // 最大缓存场景数量
    public float cacheLifeTime = 300f; // 缓存生存时间（秒）

    private Dictionary<string, CachedScene> cachedScenes = new Dictionary<string, CachedScene>();

    private class CachedScene
    {
        public AsyncOperation loadOperation;
        public float cacheTime;
        public bool isActive;

        public CachedScene(AsyncOperation operation)
        {
            loadOperation = operation;
            cacheTime = Time.time;
            isActive = false;
        }
    }

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

    void Update()
    {
        CleanExpiredCache();
    }

    /// <summary>
    /// 缓存场景
    /// </summary>
    public void CacheScene(string sceneName, AsyncOperation loadOperation)
    {
        if (cachedScenes.ContainsKey(sceneName))
            return;

        // 检查缓存大小限制
        if (cachedScenes.Count >= maxCacheSize)
        {
            RemoveOldestCache();
        }

        cachedScenes[sceneName] = new CachedScene(loadOperation);
        Debug.Log($"场景已缓存: {sceneName}");
    }

    /// <summary>
        /// 检查场景是否已缓存
        /// </summary>
    public bool IsSceneCached(string sceneName)
    {
        return cachedScenes.ContainsKey(sceneName);
    }

    /// <summary>
        /// 激活已缓存的场景
        /// </summary>
    public bool ActivateCachedScene(string sceneName)
    {
        if (!cachedScenes.TryGetValue(sceneName, out CachedScene cached))
            return false;

        cached.loadOperation.allowSceneActivation = true;
        cached.isActive = true;

        Debug.Log($"激活缓存场景: {sceneName}");
        return true;
    }

    /// <summary>
        /// 清理过期缓存
        /// </summary>
    private void CleanExpiredCache()
    {
        var expiredScenes = new List<string>();

        foreach (var kvp in cachedScenes)
        {
            if (Time.time - kvp.Value.cacheTime > cacheLifeTime && !kvp.Value.isActive)
            {
                expiredScenes.Add(kvp.Key);
            }
        }

        foreach (string sceneName in expiredScenes)
        {
            RemoveCache(sceneName);
        }
    }

    /// <summary>
        /// 移除最旧的缓存
        /// </summary>
    private void RemoveOldestCache()
    {
        string oldestScene = null;
        float oldestTime = float.MaxValue;

        foreach (var kvp in cachedScenes)
        {
            if (!kvp.Value.isActive && kvp.Value.cacheTime < oldestTime)
            {
                oldestTime = kvp.Value.cacheTime;
                oldestScene = kvp.Key;
            }
        }

        if (oldestScene != null)
        {
            RemoveCache(oldestScene);
        }
    }

    /// <summary>
        /// 移除指定缓存
        /// </summary>
    private void RemoveCache(string sceneName)
    {
        if (cachedScenes.TryGetValue(sceneName, out CachedScene cached))
        {
            // 如果场景已加载但未激活，需要卸载
            if (cached.loadOperation.progress >= 0.9f && !cached.isActive)
            {
                SceneManager.UnloadSceneAsync(sceneName);
            }

            cachedScenes.Remove(sceneName);
            Debug.Log($"移除缓存场景: {sceneName}");
        }
    }

    /// <summary>
        /// 清理所有缓存
        /// </summary>
    public void ClearAllCache()
    {
        foreach (var sceneName in new List<string>(cachedScenes.Keys))
        {
            RemoveCache(sceneName);
        }
    }
}