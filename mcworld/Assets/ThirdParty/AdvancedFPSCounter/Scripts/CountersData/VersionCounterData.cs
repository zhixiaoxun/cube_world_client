using System;
using System.Text;
using CodeStage.AdvancedFPSCounter.Labels;
using UnityEngine;
using System.Net.NetworkInformation;

namespace CodeStage.AdvancedFPSCounter.CountersData
{
    /// <summary>
    /// Shows additional version information.
    /// </summary>
    [Serializable]
    public class VersionCounterData : BaseCounterData
    {
        [HideInInspector]
        public string lastValue = "";

        [SerializeField]
        private bool svnModel = true;

        private bool inited;

        private string svnVersion = "";

        public string SvnVersion
        {
            get { return svnVersion; }
            set
            {
                if (svnVersion == value || !Application.isPlaying) return;
                svnVersion = value;
                if (!enabled) return;

                Refresh();
            }
        }

        internal VersionCounterData()
        {
            color = new Color32(85, 218, 102, 255);
            anchor = LabelAnchor.LowerLeft;
        }

        #region properties
        /// <summary>
        /// Shows CPU model name and maximum supported threads count.
        /// </summary>
        public bool SvnModel
        {
            get { return svnModel; }
            set
            {
                if (svnModel == value || !Application.isPlaying) return;
                svnModel = value;
                if (!enabled) return;

                Refresh();
            }
        }

        #endregion

        protected override void CacheCurrentColor()
        {
            colorCached = "<color=#" + AFPSCounter.Color32ToHex(color) + ">";
        }

        internal override void Activate()
        {
            if (!enabled || inited || !HasData()) return;
            base.Activate();

            inited = true;

            if (main.OperationMode == AFPSCounterOperationMode.Normal)
            {
                if (colorCached == null)
                {
                    colorCached = "<color=#" + AFPSCounter.Color32ToHex(color) + ">";
                }
            }

            if (text == null)
            {
                text = new StringBuilder();
            }
            else
            {
                text.Remove(0, text.Length);
            }

            UpdateValue();
        }

        internal override void Deactivate()
        {
            if (!inited) return;
            base.Deactivate();

            if (text != null) text.Length = 0;
            main.MakeDrawableLabelDirty(anchor);

            inited = false;
        }

        internal override void UpdateValue(bool force)
        {
            if (!inited && (HasData()))
            {
                Activate();
                return;
            }

            if (inited && (!HasData()))
            {
                Deactivate();
                return;
            }

            if (!enabled) return;

            text.Remove(0, text.Length);

            if (svnModel)
            {
                text.Append("CS: ").Append(svnVersion);
            }

            lastValue = text.ToString();

            if (main.OperationMode == AFPSCounterOperationMode.Normal)
            {
                text.Insert(0, colorCached);
                text.Append("</color>");
            }
            else
            {
                text.Length = 0;
            }

            dirty = true;
        }

        private bool HasData()
        {
            return svnModel;
        }
    }
}
