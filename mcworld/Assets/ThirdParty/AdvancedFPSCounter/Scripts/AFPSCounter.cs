using CodeStage.AdvancedFPSCounter.CountersData;
using CodeStage.AdvancedFPSCounter.Labels;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CodeStage.AdvancedFPSCounter
{
    using UnityEngine;

    /// <summary>
    /// Allows to see frames per second counter, memory usage counter and some simple hardware information right in running app on any device.<br/>
    /// Just use GameObject->Create Other->Code Stage->Advanced FPS Counter (wooh, pretty long, yeah?) menu item and you're ready to go!
    /// </summary>
    [AddComponentMenu("")] // sorry, but you shouldn't add it via Component menu, read above comment please
    public class AFPSCounter : MonoBehaviour
    {
        private const string CONTAINER_NAME = "Advanced FPS Counter";

        internal static string NEW_LINE = Environment.NewLine;

        private static AFPSCounter instance;

        /// <summary>
        /// Frames Per Second counter.
        /// </summary>
        public FPSCounterData fpsCounter = new FPSCounterData();

        /// <summary>
        /// Mono or heap memory counter.
        /// </summary>
        public MemoryCounterData memoryCounter = new MemoryCounterData();

        /// <summary>
        /// Device hardware info.<br/>
        /// Shows CPU name, cores (threads) count, GPU name, total VRAM, total RAM, screen DPI and screen size.
        /// </summary>
        public DeviceInfoCounterData deviceInfoCounter = new DeviceInfoCounterData();

        /// <summary>
        /// Version info.<br/>
        /// Shows svn version.
        /// </summary>
        public VersionCounterData versionInfoCounter = new VersionCounterData();

        /// <summary>
        /// Used to enable / disable plugin at runtime. Set to KeyCode.None to disable.
        /// </summary>
        public KeyCode hotKey = KeyCode.BackQuote;

        /// <summary>
        /// Allows to keep Advanced FPS Counter game object on new level (scene) load.
        /// </summary>
        public bool keepAlive = true;

        /// <summary>
        /// Show info in a line.
        /// </summary>
        public bool oneLine = true;

        private bool obsoleteEnabled = true;

        [SerializeField]
        private AFPSCounterOperationMode operationMode = AFPSCounterOperationMode.Normal;

        [SerializeField]
        private bool forceFrameRate;

        [SerializeField]
        [Range(-1, 200)]
        private int forcedFrameRate = -1;

        [SerializeField]
        private Vector2 anchorsOffset = new Vector2(5, 5);

        [SerializeField]
        private Font labelsFont;

        [SerializeField]
        [Range(0, 100)]
        private int fontSize;

        [SerializeField]
        [Range(0f, 10f)]
        private float lineSpacing = 1;

        internal DrawableLabel[] labels;

        private int anchorsCount;
        private int cachedVSync = -1;
        private int cachedFrameRate = -1;
        private bool inited;

        #region editor stuff
#if UNITY_EDITOR

        private const string MENU_PATH = "GameObject/Create Other/Code Stage/Advanced FPS Counter %&#F";

        [UnityEditor.MenuItem(MENU_PATH, false)]
        private static void AddToScene()
        {
            AFPSCounter counter = (AFPSCounter)FindObjectOfType(typeof(AFPSCounter));
            if (counter != null)
            {
                if (counter.IsPlacedCorrectly())
                {
                    if (UnityEditor.EditorUtility.DisplayDialog("Remove Advanced FPS Counter?", "Advanced FPS Counter already exists in scene and placed correctly. Dou you wish to remove it?", "Yes", "No"))
                    {
                        DestroyImmediate(counter.gameObject);
                    }
                }
                else
                {
                    if (counter.MayBePlacedHere())
                    {
                        int dialogResult = UnityEditor.EditorUtility.DisplayDialogComplex("Fix existing Game Object to work with Adavnced FPS Counter?", "Advanced FPS Counter already exists in scene and placed onto empty Game Object \"" + counter.name + "\".\nDo you wish to let plugin configure and use this Game Object further? Press Delete to remove plugin from scene at all.", "Fix", "Delete", "Cancel");

                        switch (dialogResult)
                        {
                            case 0:
                                counter.FixCurrentGameObject();
                                break;
                            case 1:
                                DestroyImmediate(counter);
                                break;
                        }
                    }
                    else
                    {
                        int dialogResult = UnityEditor.EditorUtility.DisplayDialogComplex("Move existing Adavnced FPS Counter to own Game Object?", "Looks like Advanced FPS Counter plugin is already exists in scene and placed incorrectly on Game Object \"" + counter.name + "\".\nDo you wish to let plugin move itself onto separate configured Game Object \"" + CONTAINER_NAME + "\"? Press Delete to remove plugin from scene at all.", "Move", "Delete", "Cancel");
                        switch (dialogResult)
                        {
                            case 0:
                                GameObject go = new GameObject(CONTAINER_NAME);
                                go.layer = 5;
                                AFPSCounter newCounter = go.AddComponent<AFPSCounter>();
                                UnityEditor.EditorUtility.CopySerialized(counter, newCounter);

                                DestroyImmediate(counter);
                                break;
                            case 1:
                                DestroyImmediate(counter);
                                break;
                        }
                    }
                }
            }
            else
            {
                GameObject go = new GameObject(CONTAINER_NAME);
                go.layer = 5;
                go.AddComponent<AFPSCounter>();
            }
        }

        private bool MayBePlacedHere()
        {
            return (gameObject.GetComponentsInChildren<Component>().Length == 2 &&
                    transform.childCount == 0 &&
                    transform.parent == null);
        }

        private void FixCurrentGameObject()
        {
            gameObject.name = CONTAINER_NAME;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
            tag = "Untagged";
            gameObject.layer = 5;
            gameObject.isStatic = false;
        }
#endif
        #endregion

        /// <summary>
        /// Allows to control %AFPSCounter from code. %AFPSCounter instance will be spawned if not exists.
        /// </summary>
        public static AFPSCounter Instance
        {
            get
            {
                if (instance == null)
                {
                    AFPSCounter counter = (AFPSCounter)FindObjectOfType(typeof(AFPSCounter));
                    if (counter != null && counter.IsPlacedCorrectly())
                    {
                        instance = counter;
                    }
                    else
                    {
                        GameObject go = new GameObject(CONTAINER_NAME);
                        go.layer = 5;
                        go.AddComponent<AFPSCounter>();
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Use it to change %AFPSCounter operation mode.
        /// </summary>
        /// Disabled: removes labels and stops all internal processes except Hot Key listener.<br/>
        /// Background: removes labels keeping counters alive. May be useful for hidden performance monitoring and benchmarking. Hot Key has no effect in this mode.<br/>
        /// Normal: shows labels and runs all internal processes as usual.
        public AFPSCounterOperationMode OperationMode
        {
            get { return operationMode; }
            set
            {
                if (operationMode == value || !Application.isPlaying) return;
                operationMode = value;

                if (operationMode != AFPSCounterOperationMode.Disabled)
                {
                    if (operationMode == AFPSCounterOperationMode.Background)
                    {
                        for (int i = 0; i < anchorsCount; i++)
                        {
                            labels[i].Clear();
                        }
                    }
                    fpsCounter.UpdateValue();
                    memoryCounter.UpdateValue();
                    deviceInfoCounter.UpdateValue();
                    versionInfoCounter.UpdateValue();

                    OnEnable();
                }
                else
                {
                    OnDisable();
                }
            }
        }

        /// <summary>
        /// This is deprecated property. Use #OperationMode property instead.
        /// </summary>
        [Obsolete("Use AFPSCounter.Instance.OperationMode instead of AFPSCounter.Instance.enabled!")]
        public new bool enabled
        {
            get { return obsoleteEnabled; }
            set
            {
                if (obsoleteEnabled == value || !Application.isPlaying) return;
                obsoleteEnabled = value;

                if (obsoleteEnabled)
                {
                    operationMode = AFPSCounterOperationMode.Normal;
                    OnEnable();
                }
                else
                {
                    operationMode = AFPSCounterOperationMode.Disabled;
                    OnDisable();
                }
            }
        }

        /// <summary>
        /// Allows to see how your game performs on specified frame rate.<br/>
        /// <strong>\htmlonly<font color="7030A0">IMPORTANT:</font>\endhtmlonly this option disables VSync while enabled!</strong>
        /// </summary>
        /// Useful to check how physics performs on slow devices for example.
        public bool ForceFrameRate
        {
            get { return forceFrameRate; }
            set
            {
                if (forceFrameRate == value || !Application.isPlaying) return;
                forceFrameRate = value;
                if (operationMode == AFPSCounterOperationMode.Disabled) return;

                RefreshForcedFrameRate();
            }
        }

        /// <summary>
        /// Desired frame rate for ForceFrameRate option, does not guarantee selected frame rate.
        /// Set to -1 to render as fast as possible in current conditions.
        /// </summary>
        public int ForcedFrameRate
        {
            get { return forcedFrameRate; }
            set
            {
                if (forcedFrameRate == value || !Application.isPlaying) return;
                forcedFrameRate = value;
                if (operationMode == AFPSCounterOperationMode.Disabled) return;

                RefreshForcedFrameRate();
            }
        }


        /// <summary>
        /// Pixel offset for anchored labels. Automatically applied to all 4 corners.
        /// </summary>
        public Vector2 AnchorsOffset
        {
            get { return anchorsOffset; }
            set
            {
                if (anchorsOffset == value || !Application.isPlaying) return;
                anchorsOffset = value;
                if (operationMode == AFPSCounterOperationMode.Disabled || labels == null) return;

                for (int i = 0; i < anchorsCount; i++)
                {
                    labels[i].ChangeOffset(anchorsOffset);
                }
            }
        }

        /// <summary>
        /// Font to render labels with.
        /// </summary>
        public Font LabelsFont
        {
            get { return labelsFont; }
            set
            {
                if (labelsFont == value || !Application.isPlaying) return;
                labelsFont = value;
                if (operationMode == AFPSCounterOperationMode.Disabled || labels == null) return;

                for (int i = 0; i < anchorsCount; i++)
                {
                    labels[i].ChangeFont(labelsFont);
                }
            }
        }

        /// <summary>
        /// The font size to use (for dynamic fonts).
        /// </summary>
        /// If this is set to a non-zero value, the font size specified in the font importer is overridden with a custom size. This is only supported for fonts set to use dynamic font rendering. Other fonts will always use the default font size.
        public int FontSize
        {
            get { return fontSize; }
            set
            {
                if (fontSize == value || !Application.isPlaying) return;
                fontSize = value;
                if (operationMode == AFPSCounterOperationMode.Disabled || labels == null) return;

                for (int i = 0; i < anchorsCount; i++)
                {
                    labels[i].ChangeFontSize(fontSize);
                }
            }
        }

        /// <summary>
        /// Space between lines.
        /// </summary>
        public float LineSpacing
        {
            get { return lineSpacing; }
            set
            {
                if (Math.Abs(lineSpacing - value) < 0.001f || !Application.isPlaying) return;
                lineSpacing = value;
                if (operationMode == AFPSCounterOperationMode.Disabled || labels == null) return;

                for (int i = 0; i < anchorsCount; i++)
                {
                    labels[i].ChangeLineSpacing(lineSpacing);
                }
            }
        }

        // preventing direct instantiation =P
        private AFPSCounter() { }

        /// <summary>
        /// Use it to completely dispose %AFPSCounter.
        /// </summary>
        public void Dispose()
        {
            if (instance == this) instance = null;
            Destroy(gameObject);
        }

        #region Unity callbacks
        private void Awake()
        {
            if (instance != null && instance.keepAlive)
            {
                Destroy(gameObject);
                return;
            }

            if (!IsPlacedCorrectly())
            {
                Debug.LogWarning("Advanced FPS Counter is placed in scene incorrectly and will be auto-destroyed! Please, use \"GameObject->Create Other->Code Stage->Advanced FPS Counter\" menu to correct this!");
                Destroy(this);
                return;
            }

            fpsCounter.Init(this);
            memoryCounter.Init(this);
            deviceInfoCounter.Init(this);
            versionInfoCounter.Init(this);

            deviceInfoCounter.ipport = Network.player.ipAddress;

            instance = this;
            DontDestroyOnLoad(gameObject);

            anchorsCount = Enum.GetNames(typeof(LabelAnchor)).Length;
            labels = new DrawableLabel[anchorsCount];

            for (int i = 0; i < anchorsCount; i++)
            {
                labels[i] = new DrawableLabel((LabelAnchor)i, anchorsOffset, labelsFont, fontSize, lineSpacing);
            }


        }

        public void ChangeNetStatusInfo(string msg)
        {
            deviceInfoCounter.ipport = msg;
            deviceInfoCounter.UpdateValue();
        }

        public void ChangeServerNetStatusInfo(string msg)
        {
            deviceInfoCounter.serverstatus = msg;
            deviceInfoCounter.UpdateValue();
        }
        public void SetNetStatsString(string msg)
        {
            deviceInfoCounter.statsString = msg;
            deviceInfoCounter.UpdateValue();
        }

        private void Start()
        {
            inited = true;
            #region EditorChecks
#if UNITY_EDITOR
            Camera[] cameras = Camera.allCameras;
            int len = cameras.Length;

            float highestCameraDeph = float.MinValue;
            float highestSuitableCameraDeph = float.MinValue;

            for (int i = 0; i < len; i++)
            {
                Camera cam = cameras[i];
                if (cam.depth > highestCameraDeph)
                {
                    highestCameraDeph = cam.depth;
                }

                GUILayer guiLayer = cameras[i].GetComponent<GUILayer>();
                if (guiLayer != null && guiLayer.enabled)
                {
                    // checking if AFPSCounter's layer in the camera's culling mask
                    if ((cam.cullingMask & (1 << gameObject.layer)) != 0)
                    {
                        if (cam.depth > highestSuitableCameraDeph)
                        {
                            highestSuitableCameraDeph = cam.depth;
                        }
                    }
                }
            }

            if (len == 0 || Math.Abs(highestCameraDeph - highestSuitableCameraDeph) > 0.0001f)
            {
                Debug.LogWarning("Please check you have camera and your top-most (highest depth) camera\nhas enabled GUILayer and has layer " + LayerMask.LayerToName(gameObject.layer) + " in the camera's culling mask!");
            }
#endif
            #endregion
        }

        private void Update()
        {
            if (!inited) return;

            if (hotKey != KeyCode.None)
            {
                if (Input.GetKeyDown(hotKey))
                {
                    SwitchCounter();
                }
            }
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (!inited) return;

            if (!keepAlive)
            {
                Dispose();
            }
            else
            {
                if (fpsCounter.Enabled)
                {
                    if (fpsCounter.ShowMinMax && fpsCounter.resetMinMaxOnNewScene) fpsCounter.ResetMinMax();
                    if (fpsCounter.ShowAverage && fpsCounter.resetAverageOnNewScene) fpsCounter.ResetAverage();
                }
            }
        }

        private void OnEnable()
        {
            if (operationMode == AFPSCounterOperationMode.Disabled) return;
            ActivateCounters();
            Invoke("RefreshForcedFrameRate", 0.5f);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            if (!inited) return;

            DeactivateCounters();
            if (IsInvoking("RefreshForcedFrameRate")) CancelInvoke("RefreshForcedFrameRate");
            RefreshForcedFrameRate(true);

            for (int i = 0; i < anchorsCount; i++)
            {
                labels[i].Clear();
            }

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnDestroy()
        {
            if (!inited) return;

            fpsCounter.Dispose();
            memoryCounter.Dispose();
            deviceInfoCounter.Dispose();
            versionInfoCounter.Dispose();

            if (labels != null)
            {
                for (int i = 0; i < anchorsCount; i++)
                {
                    labels[i].Dispose();
                }

                Array.Clear(labels, 0, anchorsCount);
                labels = null;
            }
            inited = false;
        }
        #endregion

        private bool IsPlacedCorrectly()
        {
            return (gameObject.GetComponentsInChildren<Component>().Length == 2 &&
                    transform.childCount == 0 &&
                    transform.parent == null);
        }

        internal void MakeDrawableLabelDirty(LabelAnchor anchor)
        {
            if (operationMode == AFPSCounterOperationMode.Normal)
            {
                labels[(int)anchor].dirty = true;
            }
        }

        internal void UpdateTexts()
        {
            if (operationMode != AFPSCounterOperationMode.Normal) return;

            bool anyContentPresent = false;

            if (versionInfoCounter.Enabled)
            {
                DrawableLabel label = labels[(int)versionInfoCounter.Anchor];
                if (oneLine)
                {
                    if (label.newText.Length > 0) label.newText.Append(" ");
                }
                else
                {
                    if (label.newText.Length > 0) label.newText.Append(NEW_LINE);
                }
                label.newText.Append(versionInfoCounter.text);
                label.dirty |= versionInfoCounter.dirty;
                versionInfoCounter.dirty = false;

                anyContentPresent = true;
            }

            if (fpsCounter.Enabled)
            {
                DrawableLabel label = labels[(int)fpsCounter.Anchor];
                if (oneLine)
                {
                    if (label.newText.Length > 0) label.newText.Append(" ");
                }
                else
                {
                    if (label.newText.Length > 0) label.newText.Append(NEW_LINE);
                }

                label.newText.Append(fpsCounter.text);
                label.dirty |= fpsCounter.dirty;
                fpsCounter.dirty = false;

                anyContentPresent = true;
            }

            if (memoryCounter.Enabled)
            {
                DrawableLabel label = labels[(int)memoryCounter.Anchor];
                if (oneLine)
                {
                    if (label.newText.Length > 0) label.newText.Append(" ");
                }
                else
                {
                    if (label.newText.Length > 0) label.newText.Append(NEW_LINE);
                }
                label.newText.Append(memoryCounter.text);
                label.dirty |= memoryCounter.dirty;
                memoryCounter.dirty = false;

                anyContentPresent = true;
            }

            if (deviceInfoCounter.Enabled)
            {
                DrawableLabel label = labels[(int)deviceInfoCounter.Anchor];
                if (oneLine)
                {
                    if (label.newText.Length > 0) label.newText.Append(" ");
                }
                else
                {
                    if (label.newText.Length > 0) label.newText.Append(NEW_LINE);
                }
                label.newText.Append(deviceInfoCounter.text);
                label.dirty |= deviceInfoCounter.dirty;
                deviceInfoCounter.dirty = false;

                anyContentPresent = true;
            }

            if (anyContentPresent)
            {
                for (int i = 0; i < anchorsCount; i++)
                {
                    labels[i].CheckAndUpdate();
                }
            }
            else
            {
                for (int i = 0; i < anchorsCount; i++)
                {
                    labels[i].Clear();
                }
            }
        }

        private IEnumerator UpdateFPSCounter()
        {
            while (true)
            {
                float previousUpdateTime = Time.time;
                int previousUpdateFrames = Time.frameCount;

                yield return new WaitForSeconds(fpsCounter.UpdateInterval);

                float timeElapsed = Time.time - previousUpdateTime;
                int framesChanged = Time.frameCount - previousUpdateFrames;

                // flooring FPS
                int fps = (int)(framesChanged / (timeElapsed / Time.timeScale));

                fpsCounter.newValue = fps;
                fpsCounter.UpdateValue(false);
                UpdateTexts();
            }
        }

        private IEnumerator UpdateMemoryCounter()
        {
            while (true)
            {
                memoryCounter.UpdateValue();
                UpdateTexts();
                yield return new WaitForSeconds(memoryCounter.UpdateInterval);
            }
        }

        private void SwitchCounter()
        {
            if (operationMode == AFPSCounterOperationMode.Disabled)
            {
                OperationMode = AFPSCounterOperationMode.Normal;
            }
            else if (operationMode == AFPSCounterOperationMode.Normal)
            {
                OperationMode = AFPSCounterOperationMode.Disabled;
            }
        }

        private void ActivateCounters()
        {
            fpsCounter.Activate();
            memoryCounter.Activate();
            deviceInfoCounter.Activate();
            versionInfoCounter.Activate();

            if (fpsCounter.Enabled || memoryCounter.Enabled || deviceInfoCounter.Enabled || versionInfoCounter.Enabled)
            {
                UpdateTexts();
            }
        }

        private void DeactivateCounters()
        {
            if (instance == null) return;

            fpsCounter.Deactivate();
            memoryCounter.Deactivate();
            deviceInfoCounter.Deactivate();
            versionInfoCounter.Deactivate();
        }

        private void RefreshForcedFrameRate()
        {
            RefreshForcedFrameRate(false);
        }

        private void RefreshForcedFrameRate(bool disabling)
        {
            if (forceFrameRate && !disabling)
            {
                if (cachedVSync == -1)
                {
                    cachedVSync = QualitySettings.vSyncCount;
                    cachedFrameRate = Application.targetFrameRate;
                    QualitySettings.vSyncCount = 0;
                }

                Application.targetFrameRate = forcedFrameRate;
            }
            else
            {
                if (cachedVSync != -1)
                {
                    QualitySettings.vSyncCount = cachedVSync;
                    Application.targetFrameRate = cachedFrameRate;
                    cachedVSync = -1;
                }
            }
        }

        internal static string Color32ToHex(Color32 color)
        {
            return color.r.ToString("x2") + color.g.ToString("x2") + color.b.ToString("x2") + color.a.ToString("x2");
        }
    }
}
