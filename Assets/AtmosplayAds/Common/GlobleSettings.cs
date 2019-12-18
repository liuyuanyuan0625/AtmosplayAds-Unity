using System.IO;
using UnityEngine;
namespace ZPLAYAds.Common
{
    public class GlobleSettings : ScriptableObject
    {
        static GlobleSettings instance;

        // Disable warnings for unused values
#pragma warning disable 67
        [Header("Android")]
        [SerializeField]
        bool androidAutoload;
        [SerializeField]
        string androidChannelId = "";
        [SerializeField]
        string androidAppId = "";
        [SerializeField]
        string androidRewardVideoUnitId = "";
        [SerializeField]
        string androidInterstitialUnitId = "";
        [SerializeField]
        string androidBannerUnitId = "";


        [Header("IOS")]
        [SerializeField]
        bool iOSAutoload;
        [SerializeField]
        string iOSChannelId = "";
        [SerializeField]
        string iOSAppId = "";
        [SerializeField]
        string iOSRewardVideoUnitId = "";
        [SerializeField]
        string iOSInterstitialUnitId = "";
        [SerializeField]
        string iOSBannerUnitId = "";
#pragma warning restore 67


        public static GlobleSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load("GlobleSettingsDB") as GlobleSettings;
                    if (instance == null)
                    {
                        instance = CreateInstance<GlobleSettings>();
#if UNITY_EDITOR
                        string properPath = Path.Combine(Application.dataPath, "ZPLAYAds/Resources");
                        if (!Directory.Exists(properPath))
                        {
                            UnityEditor.AssetDatabase.CreateFolder("Assets/ZPLAYAds", "Resources");
                        }

                        string fullPath = Path.Combine(Path.Combine("Assets", "ZPLAYAds/Resources"), "GlobleSettingsDB.asset");
                        UnityEditor.AssetDatabase.CreateAsset(instance, fullPath);
#endif
                    }
                }
                return instance;
            }
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("Window/ZPLAYAds/Globle Settings")]
        public static void ZplayGlobleSettings()
        {
            UnityEditor.Selection.activeObject = Instance;
        }
#endif


        public static string GetChannelId
        {
            get
            {
#if UNITY_ANDROID
                return Instance.androidChannelId;
#elif UNITY_IPHONE
                return Instance.iOSChannelId;
#else
                return "";
#endif

            }
        }

        public static bool IsAutoload
        {
            get
            {
#if UNITY_ANDROID
                return Instance.androidAutoload;
#elif UNITY_IPHONE
                return Instance.iOSAutoload;
#else
                return false;
#endif
            }
        }

        public static string GetAppID
        {
            get
            {
#if UNITY_ANDROID
                return Instance.androidAppId;
#elif UNITY_IPHONE
                return Instance.iOSAppId;
#else
                return "";
#endif
            }
        }

        public static string GetRewardVideoUnitID
        {
            get
            {
#if UNITY_ANDROID
                return Instance.androidRewardVideoUnitId;
#elif UNITY_IPHONE
                return Instance.iOSRewardVideoUnitId;
#else
                return "";
#endif
            }
        }

        public static string GetInterstitialUnitID
        {
            get
            {
#if UNITY_ANDROID
                return Instance.androidInterstitialUnitId;
#elif UNITY_IPHONE
                return Instance.iOSInterstitialUnitId;
#else
                return "";
#endif
            }
        }
        public static string GetBannerUnitID
        {
            get
            {
#if UNITY_ANDROID
                return Instance.androidBannerUnitId;
#elif UNITY_IPHONE
                return Instance.iOSBannerUnitId;
#else
                return "";
#endif
            }
        }
    }
}
