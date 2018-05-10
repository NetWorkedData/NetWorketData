﻿//=====================================================================================================================
//
// ideMobi copyright 2017 
// All rights reserved by ideMobi
//
//=====================================================================================================================

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SocialPlatforms;

using BasicToolBox;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Threading;

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public enum NWDNetworkState
    {
        Unknow,
        OnLine,
        OffLine,
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    /// <summary>
    /// NWD game data manager.
    /// The GameObject monobehaviour connection
    /// Use in game to acces to specific method and create a simulate singleton instance 
    /// The sigleton instance was connect to :
    /// Data Manager
    /// App Configuration
    /// Notification Center
    /// </summary>
    [ExecuteInEditMode] // We use this only in playmode so don't attribut ExecuteInEditMode
    public partial class NWDGameDataManager : MonoBehaviour
    {
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// SharedInstanceAsSingleton. Class to create a ShareInstance use as Singleton by All NWDGameDataManager
        /// Declare as private, all NWDGameDataManager instance call this class's shared instance.
        /// So, instances connected to private sharedinstance, if I don't create otehr instance, nobody can : I have a singleton simulation
        /// Of course it's a patch, but it's working!
        /// </summary>
        //-------------------------------------------------------------------------------------------------------------
        //[ExecuteInEditMode] // I do that to run this object in edit mode too (on scene)
        //-------------------------------------------------------------------------------------------------------------
        private class SharedInstanceAsSingleton
        {
            //-------------------------------------------------------------------------------------------------------------
            /// <summary>
            /// The instance's counter.
            /// </summary>
            static int InstanceCounter = 0;
            //-------------------------------------------------------------------------------------------------------------
            /// <summary>
            /// Log the counter of instance of this Class.
            /// </summary>
            static void InstanceCounterLog()
            {
                //Debug.LogVerbose ("(NWDGameDataManager SharedInstanceAsSingleton number of instance : "+InstanceCounter+")");
            }
            //-------------------------------------------------------------------------------------------------------------
            /// <summary>
            /// The shared instance as singleton.
            /// </summary>
            static public SharedInstanceAsSingleton kSharedInstaceAsSingleton = new SharedInstanceAsSingleton();
            //-------------------------------------------------------------------------------------------------------------
            /// <summary>
            /// The data manager.
            /// </summary>
            public NWDDataManager DataManager;
            /// <summary>
            /// The app configuration.
            /// </summary>
            public NWDAppConfiguration AppConfiguration;
            /// <summary>
            /// The notification center.
            /// </summary>
            public BTBNotificationManager NotificationCenter;
            //-------------------------------------------------------------------------------------------------------------
            /// <summary>
            /// The initialization state.
            /// </summary>
            private bool Initialized = false;
            //-------------------------------------------------------------------------------------------------------------
            /// <summary>
            /// The data need update state.
            /// </summary>
            public bool DataWasUpdated;
            //-------------------------------------------------------------------------------------------------------------
            /// <summary>
            /// Initializes a new instance of the <see cref="NetWorkedData.NWDGameDataManager+SharedInstanceAsSingleton"/> class.
            /// </summary>
            public SharedInstanceAsSingleton()
            {
                Interlocked.Increment(ref InstanceCounter);
                if (Initialized == false)
                {
                    // memorize the shared instance
                    DataManager = NWDDataManager.SharedInstance();
                    AppConfiguration = NWDAppConfiguration.SharedInstance();
                    NotificationCenter = BTBNotificationManager.SharedInstance();
                    // ready to launch database
                    DataManager.ConnectToDatabase();
                    // finish init
                    Initialized = true;
                    //Debug.LogVerbose ("NWDGameDataManager SharedInstanceAsSingleton InitInstance finished ("+InstanceCounter+")");
                }
                InstanceCounterLog();
            }
            //-------------------------------------------------------------------------------------------------------------
            /// <summary>
            /// Releases unmanaged resources and performs other cleanup operations before the
            /// <see cref="NetWorkedData.NWDGameDataManager+SharedInstanceAsSingleton"/> is reclaimed by garbage collection.
            /// </summary>
            ~SharedInstanceAsSingleton()
            {
                Interlocked.Decrement(ref InstanceCounter);
                //Debug.LogVerbose ("NWDGameDataManager SharedInstanceAsSingleton destroy ... IT NEVER POSSIBLE IN RUNTIME");
                if (NotificationCenter != null)
                {
                    NotificationCenter.RemoveAll();
                    NotificationCenter = null;
                }
                InstanceCounterLog();
            }
            //-------------------------------------------------------------------------------------------------------------
            /// <summary>
            /// Determines if Singleton is active.
            /// </summary>
            /// <returns><c>true</c> if is active; otherwise, <c>false</c>.</returns>
            public static bool IsActive()
            {
                return (kSharedInstaceAsSingleton != null);
            }
            //-------------------------------------------------------------------------------------------------------------
        }
        //-------------------------------------------------------------------------------------------------------------
        public BTBHealthGaugeComplex LoadingDatasGauge;
        //-------------------------------------------------------------------------------------------------------------
        public BTBHealthGaugeComplex OperationGauge;
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the data manager.
        /// </summary>
        /// <value>The data manager.</value>
        public NWDDataManager DataManager
        {
            get; private set;
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the app configuration.
        /// </summary>
        /// <value>The app configuration.</value>
        public NWDAppConfiguration AppConfiguration
        {
            get; private set;
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the notification center.
        /// </summary>
        /// <value>The notification center.</value>
        public BTBNotificationManager NotificationCenter
        {
            get; private set;
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="NetWorkedData.NWDGameDataManager"/> class.
        /// </summary>
        public NWDGameDataManager()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="NetWorkedData.NWDGameDataManager"/> is reclaimed by garbage collection.
        /// </summary>
        ~NWDGameDataManager()
        {
            //Debug.LogVerbose ("NWDGameDataManager Destroyed");
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Set needs the synchronize data to true.
        /// </summary>
        public void NeedSynchronizeData()
        {
            SharedInstanceAsSingleton.kSharedInstaceAsSingleton.DataWasUpdated = true;
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Player's account reference.
        /// </summary>
        /// <returns>The account reference.</returns>
        public string PlayerAccountReference()
        {
            return AppConfiguration.SelectedEnvironment().PlayerAccountReference;
        }
        //-------------------------------------------------------------------------------------------------------------
        public NWDErrorBlock ErrorDelegate;
        //-------------------------------------------------------------------------------------------------------------
        public void ErrorManagement(NWDError sError)
        {
            if (ErrorDelegate != null)
            {
                ErrorDelegate(sError);
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public NWDNetworkState NetworkStatut = NWDNetworkState.Unknow;
        //-------------------------------------------------------------------------------------------------------------
        // Test network connection
        public void NetworkStatutChange(NWDNetworkState sNewNetWorkStatut)
        {
            if (sNewNetWorkStatut != NetworkStatut)
            {
                NetworkStatut = sNewNetWorkStatut;
                if (NetworkStatut == NWDNetworkState.OffLine)
                {
                    BTBNotificationManager.SharedInstance().PostNotification(new BTBNotification(NWDNotificationConstants.K_NETWORK_OFFLINE, null));
                }
                else if (NetworkStatut == NWDNetworkState.OnLine)
                {
                    BTBNotificationManager.SharedInstance().PostNotification(new BTBNotification(NWDNotificationConstants.K_NETWORK_ONLINE, null));
                }
                else
                {
                    BTBNotificationManager.SharedInstance().PostNotification(new BTBNotification(NWDNotificationConstants.K_NETWORK_UNKNOW, null));
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public bool HasNetWork()
        {
            return NetworkStatut == NWDNetworkState.OnLine;
        }
        //-------------------------------------------------------------------------------------------------------------



        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// The unity singleton. (destroy other instance)
        /// </summary>
        private static NWDGameDataManager kUnitySingleton = null;
        /// <summary>
        /// The application stand by.
        /// </summary>
        private bool ApplicationStandBy = false;
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Raises the runtime method load event.
        /// </summary>
        [RuntimeInitializeOnLoadMethod]
        static void RuntimeInitializeOnLoad()
        {
            Debug.Log("NWDGameDataManager RuntimeInitializeOnLoad");
            UnitySingleton();
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity the singleton game object.
        /// </summary>
        /// <returns>The singleton game object.</returns>
        public static GameObject UnitySingletonGameObject()
        {
            return UnitySingleton().gameObject;
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        public static NWDGameDataManager UnitySingleton()
        {
            //Debug.Log("NWDGameDataManager UnitySingleton");
            if (kUnitySingleton == null)
            {

                // check real Singleton
                NWDGameDataManager[] tOtherInstances = FindObjectsOfType<NWDGameDataManager>();

                List<NWDGameDataManager> tOtherList = new List<NWDGameDataManager>();

                foreach (NWDGameDataManager tOtherInstance in tOtherInstances)
                {
                    tOtherList.Add(tOtherInstance);
                }
                if (tOtherList.Count == 0)
                {
                    // I need to create singleton
                    GameObject tObjToSpawn;
                    //spawn object
                    tObjToSpawn = new GameObject("NWDGameDataManagerUnitySingleton");
                    //Add Components
                    NWDGameDataManager tGameDataManager = tObjToSpawn.AddComponent<NWDGameDataManager>();
                    NWDUserNetWorkingScript tUserNetWorkingScript = tObjToSpawn.AddComponent<NWDUserNetWorkingScript>();
                    BTBHealthGaugeComplex tHealthGaugeComplex = tObjToSpawn.AddComponent<BTBHealthGaugeComplex>();
                    BTBHealthGaugeComplex tHealthGaugeComplexB = tObjToSpawn.AddComponent<BTBHealthGaugeComplex>();
                    tGameDataManager.LoadingDatasGauge = tHealthGaugeComplex;
                    tGameDataManager.OperationGauge = tHealthGaugeComplexB;
                    // keep k_Singleton
                    //kUnitySingleton = tObjToSpawn.GetComponent<NWDGameDataManager>();
                    kUnitySingleton = tGameDataManager;

                }
                else if (tOtherList.Count == 1)
                {
                    kUnitySingleton = tOtherList[0];
                }
                else
                {
                    while (tOtherList.Count > 0)
                    {
                        NWDGameDataManager tLast = tOtherList[0];
                        tOtherList.Remove(tLast);
#if UNITY_EDITOR
                        if (EditorApplication.isPlaying == true)
                        {
                            Debug.Log("NWDGameDataManager A");
                            Destroy(tLast.gameObject);
                        }
                        else
                        {
                            Debug.Log("NWDGameDataManager B");
                            DestroyImmediate(tLast.gameObject);
                        }
#else
                        Debug.Log("NWDGameDataManager C");
                    Destroy (tLast.gameObject);
#endif
                    }

                }


                //foreach (NWDGameDataManager tOtherInstance in tOtherInstances) {
                //  if (tOtherInstance.gameObject != tObjToSpawn) {
                //                    #if UNITY_EDITOR
                //                    if (EditorApplication.isPlaying == true)
                //                    {
                //                        Debug.Log("NWDGameDataManager A");
                //                        //DestroyImmediate (tOtherInstance.gameObject); // change with unity 2017.2.1f1
                //                        Destroy(tOtherInstance.gameObject);
                //                    }
                //                    else
                //                    {
                //                        Debug.Log("NWDGameDataManager B");
                //                        DestroyImmediate(tOtherInstance.gameObject); // change with unity 2017.2.1f1
                //                        //Destroy(tOtherInstance.gameObject);
                //                    }
                //                    #else
                //                    Debug.Log("NWDGameDataManager C");
                //      Destroy (tOtherInstance.gameObject);
                //      #endif
                //  }
                //}
            }
            return kUnitySingleton;
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Awake this instance.
        /// </summary>
        void Awake()
        {
            //Debug.Log("NWDGameDataManager Awake ()");
            //Debug.LogVerbose ("NWDGameDataManager Awake");
            //Check if there is already an instance
            if (kUnitySingleton == null)
            {
                //if not, set it to this.
                kUnitySingleton = this;
#if UNITY_EDITOR
                if (Application.isEditor && Application.isPlaying == true)
                {
                    DontDestroyOnLoad(gameObject);
                }
#else
                DontDestroyOnLoad (gameObject);
#endif
            }
            //If instance already exists:
            else if (kUnitySingleton != this)
            {
                //Destroy this, this enforces our singleton pattern so there can only be one instance.
#if UNITY_EDITOR
                DestroyImmediate(this.gameObject);
#else
                Destroy (this.gameObject);
#endif
            }


            // User networking?

        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// On enable. call every times is necessary.
        /// </summary>
        void OnEnable()
        {
            BTBNotificationManager.SharedInstance().AddObserver(this, NWDNotificationConstants.K_DATAS_START_LOADING, delegate (BTBNotification sNotification)
            {
                Debug.Log("NWD => NWDGameDataManager NOTIFICATION_DATAS_LOADEDNOTIFICATION_DATAS_START_LOADINGNOTIFIED ()");
                if (LoadingDatasGauge != null)
                {
                    LoadingDatasGauge.IsVisible = true;
                    LoadingDatasGauge.SetHorizontalValue(0.0F);
                }
            });

            BTBNotificationManager.SharedInstance().AddObserver(this, NWDNotificationConstants.K_DATAS_PARTIAL_LOADED, delegate (BTBNotification sNotification)
            {
                Debug.Log("NWD => NWDGameDataManager NOTIFICATION_DATAS_PARTIAL_LOADED NOTIFIED ()");
                if (LoadingDatasGauge != null)
                {
                    LoadingDatasGauge.IsVisible = true;
                    float tValue = (float)NWDTypeLauncher.ClassesDataLoaded / (float)NWDTypeLauncher.ClassesExpected;
                    //Debug.Log("DataLoader ASYNC () Gauge set to " + tValue);
                    LoadingDatasGauge.SetHorizontalValue(tValue);
                }
            });
            BTBNotificationManager.SharedInstance().AddObserver(this, NWDNotificationConstants.K_DATAS_LOADED, delegate (BTBNotification sNotification)
            {
                Debug.Log("NWD => NWDGameDataManager NOTIFICATION_DATAS_LOADED NOTIFIED ()");
                if (LoadingDatasGauge != null)
                {
                    LoadingDatasGauge.IsVisible = false;
                    //Gauge.Draw();
                }
            });
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Start this instance. Called once.
        /// </summary>
        void Start()
        {
            Debug.Log("NWDGameDataManager Start()");
            //Debug.LogVerbose ("NWDGameDataManager Start");
            DataManager = SharedInstanceAsSingleton.kSharedInstaceAsSingleton.DataManager;
            AppConfiguration = SharedInstanceAsSingleton.kSharedInstaceAsSingleton.AppConfiguration;
            NotificationCenter = SharedInstanceAsSingleton.kSharedInstaceAsSingleton.NotificationCenter;
            //Network is unknow at start
            NetworkStatutChange(NWDNetworkState.Unknow);

            if (NWDTypeLauncher.DataLoaded == false)
            {
                Debug.LogWarning("NWD => Datas ARE NOT LOADED ... load async now");
                StartCoroutine(NWDDataManager.SharedInstance().AsyncReloadAllObjects());
            }
            else
            {
                Debug.LogWarning("NWD => Datas ARE ALLREADY LOADED");
                if (LoadingDatasGauge != null)
                {
                    LoadingDatasGauge.IsVisible = false;
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------
        void OnGUI()
        {
            //Debug.Log ("NWDGameDataManager OnGUI()");
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Raises the destroy event.
        /// </summary>
        void OnDisable()
        {
            BTBNotificationManager.SharedInstance().RemoveObserverEveryWhere(this);
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Raises the destroy event.
        /// </summary>
        void OnDestroy()
        {
            
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Raises the application focus event.
        /// </summary>
        /// <param name="sHasFocus">If set to <c>true</c> s has focus.</param>
        void OnApplicationFocus(bool sHasFocus)
        {
            ApplicationStandBy = !sHasFocus;
            if (ApplicationStandBy == false)
            {
                //Debug.LogVerbose ("OnApplicationFocus Focus is ON");
            }
            else
            {
                //Debug.LogVerbose ("OnApplicationFocus Focus is OFF");
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Raises the application pause event.
        /// </summary>
        /// <param name="sPauseStatus">If set to <c>true</c> s pause status.</param>
        void OnApplicationPause(bool sPauseStatus)
        {
            ApplicationStandBy = sPauseStatus;
            if (ApplicationStandBy == false)
            {
                //Debug.LogVerbose ("OnApplicationPause Pause is OFF");
            }
            else
            {
                //Debug.LogVerbose ("OnApplicationPause Pause is ON");
            }
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
