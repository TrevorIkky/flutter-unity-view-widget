﻿using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class NativeAPI
{
#if UNITY_IOS && !UNITY_EDITOR

    [DllImport("__Internal")]
    public static extern void OnUnityMessage(string message);

    [DllImport("__Internal")]
    public static extern void OnUnitySceneLoaded(string name, int buildIndex, bool isLoaded, bool IsValid);
#endif

    public static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
#if UNITY_ANDROID
        try
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.xraph.plugins.flutterunitywidget.UnityPlayerUtils");
                    jc.CallStatic("onUnitySceneLoaded", scene.name, scene.buildIndex, scene.isLoaded, scene.IsValid());
                }
                catch (Exception e)
                {
                    print(e.Message);
                }
#elif UNITY_IOS && !UNITY_EDITOR
        NativeAPI.OnUnitySceneLoaded(scene.name, scene.buildIndex, scene.isLoaded, scene.IsValid());
#endif
    }



    public static void ShowHostMainWindow()
    {
#if UNITY_ANDROID
        try
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.xraph.plugins.flutterunitywidget.OverrideUnityActivity");
            AndroidJavaObject overrideActivity = jc.GetStatic<AndroidJavaObject>("instance");
            overrideActivity.Call("showMainActivity");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
#elif UNITY_IOS && !UNITY_EDITOR
        // NativeAPI.showHostMainWindow();
#endif
    }

    public static void UnloadMainWindow()
    {
#if UNITY_ANDROID
        try
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.xraph.plugins.flutterunitywidget.OverrideUnityActivity");
            AndroidJavaObject overrideActivity = jc.GetStatic<AndroidJavaObject>("instance");
            overrideActivity.Call("unloadPlayer");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
#elif UNITY_IOS && !UNITY_EDITOR
        // NativeAPI.unloadPlayer();
#endif
    }


    public static void QuitUnityWindow()
    {
#if UNITY_ANDROID
        try
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.xraph.plugins.flutterunitywidget.OverrideUnityActivity");
            AndroidJavaObject overrideActivity = jc.GetStatic<AndroidJavaObject>("instance");
            overrideActivity.Call("quitPlayer");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
#elif UNITY_IOS && !UNITY_EDITOR
        // NativeAPI.quitPlayer();
#endif
    }


    public static void SendMessageToFlutter(string message)
    {
#if UNITY_ANDROID
            try
            {
                AndroidJavaClass jc = new AndroidJavaClass("com.xraph.plugins.flutterunitywidget.UnityPlayerUtils");
                jc.CallStatic("onUnityMessage", message);
            }
            catch (Exception e)
            {
                print(e.Message);
            }
#elif UNITY_IOS && !UNITY_EDITOR
        NativeAPI.OnUnityMessage(message);
#endif
    }
}
