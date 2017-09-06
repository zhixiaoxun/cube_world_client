﻿using System;
using System.Text;
using UnityEngine;
using System.Runtime.InteropServices;

namespace CodeStage.AdvancedFPSCounter.CountersData
{
	/// <summary>
	/// Shows memory usage data.
	/// </summary>
	[Serializable]
	public class MemoryCounterData: BaseCounterData
	{
		public const int MEMORY_DIVIDER = 1048576; // 1024^2

		private const string COROUTINE_NAME = "UpdateMemoryCounter";
		private const string TEXT_START = "<color=#{0}><b>";
		private const string LINE_START_TOTAL = "MEM (total): ";
		private const string LINE_START_ALLOCATED = "MEM (alloc): ";
		private const string LINE_START_MONO = "MEM (mono): ";
		private const string LINE_END = " MB";
		private const string TEXT_END = "</b></color>";

		/// <summary>
		/// Last total memory readout.
		/// </summary>
		/// In megs if #PreciseValues is false, in bytes otherwise.
		/// @see TotalReserved
		[HideInInspector]
		public uint lastTotalValue = 0;

		/// <summary>
		/// Last allocated memory readout.
		/// </summary>
		/// In megs if #PreciseValues is false, in bytes otherwise.
		/// @see Allocated
		[HideInInspector]
		public uint lastAllocatedValue = 0;

		/// <summary>
		/// Last Mono memory readout.
		/// </summary>
		/// In megs if #PreciseValues is false, in bytes otherwise.
		/// @see MonoUsage
		[HideInInspector]
		public long lastMonoValue = 0;

		[SerializeField]
		[Range(0.1f, 10f)]
		private float updateInterval = 1f;

		[SerializeField]
		private bool preciseValues;

		[SerializeField]
		private bool totalReserved = true;

		[SerializeField]
		private bool allocated = true;

		[SerializeField]
		private bool monoUsage = true;

		private bool inited;

		internal MemoryCounterData()
		{
			color = new Color32(234, 238, 101, 255);
		}

		/// <summary>
		/// Counter's value update interval.
		/// </summary>
		public float UpdateInterval
		{
			get { return updateInterval; }
			set
			{
				if (Math.Abs(updateInterval - value) < 0.001f || !Application.isPlaying) return;
				updateInterval = value;
				if (!enabled) return;

				RestartCoroutine();
			}
		}

		/// <summary>
		/// Allows to output memory usage more precisely thus using more system resources.
		/// </summary>
		public bool PreciseValues
		{
			get { return preciseValues; }
			set
			{
				if (preciseValues == value || !Application.isPlaying) return;
				preciseValues = value;
				if (!enabled) return;

				Refresh();
			}
		}

		/// <summary>
		/// Allows to see private memory amount reserved for application. This memory can’t be used by other applications.
		/// </summary>
		public bool TotalReserved
		{
			get { return totalReserved; }
			set
			{
				if (totalReserved == value || !Application.isPlaying) return;
				totalReserved = value;
				if (!totalReserved) lastTotalValue = 0;
				if (!enabled) return;

				Refresh();
			}
		}

		/// <summary>
		/// Allows to see amount of memory, currently allocated by application.
		/// </summary>
		public bool Allocated
		{
			get { return allocated; }
			set
			{
				if (allocated == value || !Application.isPlaying) return;
				allocated = value;
				if (!allocated) lastAllocatedValue = 0;
				if (!enabled) return;

				Refresh();
			}
		}

		/// <summary>
		/// Allows to see amount of memory, allocated by managed Mono objects, 
		/// such as UnityEngine.Object and everything derived from it for example.
		/// </summary>
		public bool MonoUsage
		{
			get { return monoUsage; }
			set
			{
				if (monoUsage == value || !Application.isPlaying) return;
				monoUsage = value;
				if (!monoUsage) lastMonoValue = 0;
				if (!enabled) return;

				Refresh();
			}
		}

		protected override void CacheCurrentColor()
		{
			colorCached = String.Format(TEXT_START, AFPSCounter.Color32ToHex(color));
		}

		internal override void Activate()
		{
            if (!enabled || inited || !HasData()) return;
            base.Activate();
            inited = true;

            lastTotalValue = 0;
            lastAllocatedValue = 0;
            lastMonoValue = 0;

            if (main.OperationMode == AFPSCounterOperationMode.Normal)
            {
                if (colorCached == null)
                {
                    colorCached = String.Format(TEXT_START, AFPSCounter.Color32ToHex(color));
                }

                if (text == null)
                {
                    text = new StringBuilder(200);
                }
                else
                {
                    text.Length = 0;
                }

                text.Append(colorCached);

                if (totalReserved)
                {
                    if (preciseValues)
                    {
                        text.Append(LINE_START_TOTAL).AppendFormat("{0:F}", 0).Append(LINE_END);
                    }
                    else
                    {
                        text.Append(LINE_START_TOTAL).Append(0).Append(LINE_END);
                    }
                }

                if (allocated)
                {
                    if (oneLine)
                    {
                        if (text.Length > 0) text.Append(" ");
                    }
                    else
                    {
                        if (text.Length > 0) text.Append(AFPSCounter.NEW_LINE);
                    }

                    if (preciseValues)
                    {
                        text.Append(LINE_START_ALLOCATED).AppendFormat("{0:F}", 0).Append(LINE_END);
                    }
                    else
                    {
                        text.Append(LINE_START_ALLOCATED).Append(0).Append(LINE_END);
                    }
                }

                if (monoUsage)
                {
                    if (oneLine)
                    {
                        if (text.Length > 0) text.Append(" ");
                    }
                    else
                    {
                        if (text.Length > 0) text.Append(AFPSCounter.NEW_LINE);
                    }
                    if (preciseValues)
                    {
                        text.Append(LINE_START_MONO).AppendFormat("{0:F}", 0).Append(LINE_END);
                    }
                    else
                    {
                        text.Append(LINE_START_MONO).Append(0).Append(LINE_END);
                    }
                }

                text.Append(TEXT_END);
                dirty = true;
            }

            main.StartCoroutine(COROUTINE_NAME);
        }

		internal override void Deactivate()
		{
			if (!inited) return;
			base.Deactivate();

			if (text != null) text.Length = 0;

			main.StopCoroutine(COROUTINE_NAME);
			main.MakeDrawableLabelDirty(anchor);

			inited = false;
		}
        
        internal override void UpdateValue(bool force)
		{
			if (!enabled) return;

			if (force)
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
			}

			if (totalReserved)
            {
                uint value = UnityEngine.Profiling.Profiler.GetTotalReservedMemory();
                uint divisionResult = 0;

				bool newValue;
				if (preciseValues)
				{
					newValue = (lastTotalValue != value);
				}
				else
				{
					divisionResult = value / MEMORY_DIVIDER;
					newValue = (lastTotalValue != divisionResult);
				}

				if (newValue || force)
				{
					if (preciseValues)
					{
						lastTotalValue = value;
					}
					else
					{
						lastTotalValue = divisionResult;
					}

					dirty = true;
				}
			}

			if (allocated)
			{
                uint value = UnityEngine.Profiling.Profiler.GetTotalAllocatedMemory();

                uint divisionResult = 0;

				bool newValue;
				if (preciseValues)
				{
					newValue = (lastAllocatedValue != value);
				}
				else
				{
					divisionResult = value / MEMORY_DIVIDER;
					newValue = (lastAllocatedValue != divisionResult);
				}

				if (newValue || force)
				{
					if (preciseValues)
					{
						lastAllocatedValue = value;
					}
					else
					{
						lastAllocatedValue = divisionResult;
					}

					dirty = true;
				}
			}

			if (monoUsage)
			{
				long monoMemory = GC.GetTotalMemory(false);
				long monoDivisionResult = 0;

				bool newValue;
				if (preciseValues)
				{
					newValue = (lastMonoValue != monoMemory);
				}
				else
				{
					monoDivisionResult = monoMemory / MEMORY_DIVIDER;
					newValue = (lastMonoValue != monoDivisionResult);
				}

				if (newValue || force)
				{
					if (preciseValues)
					{
						lastMonoValue = monoMemory;
					}
					else
					{
						lastMonoValue = monoDivisionResult;
					}
					
					dirty = true;
				}
			}
			if (dirty && main.OperationMode == AFPSCounterOperationMode.Normal)
			{
				bool needNewLine = false;

				text.Length = 0;
				text.Append(colorCached);

				if (totalReserved)
				{
					text.Append(LINE_START_TOTAL);

					if (preciseValues)
					{
						text.AppendFormat("{0:F}", lastTotalValue / (float)MEMORY_DIVIDER);
					}
					else
					{
						text.Append(lastTotalValue);
					}
					text.Append(LINE_END);
					needNewLine = true;
				}

				if (allocated)
				{
                    if (oneLine)
                    {
                        text.Append(" ");
                    }
                    else
                    {
                        if (needNewLine) text.Append(AFPSCounter.NEW_LINE);
                    }
					
					text.Append(LINE_START_ALLOCATED);

					if (preciseValues)
					{
						text.AppendFormat("{0:F}", lastAllocatedValue / (float)MEMORY_DIVIDER);
					}
					else
					{
						text.Append(lastAllocatedValue);
					}
					text.Append(LINE_END);
					needNewLine = true;
				} 

				if (monoUsage)
				{
                    if (oneLine)
                    {
                        text.Append(" ");
                    }
                    else
                    {
                        if (needNewLine) text.Append(AFPSCounter.NEW_LINE);
                    }
					//if (needNewLine) text.Append(AFPSCounter.NEW_LINE);
					text.Append(LINE_START_MONO);

					if (preciseValues)
					{
						text.AppendFormat("{0:F}", lastMonoValue / (float)MEMORY_DIVIDER);
					}
					else
					{
						text.Append(lastMonoValue);
					}

					text.Append(LINE_END);
				}

				text.Append(TEXT_END);
			}
		}

		private void RestartCoroutine()
		{
            main.StopCoroutine(COROUTINE_NAME);
            main.StartCoroutine(COROUTINE_NAME);
        }

		private bool HasData()
		{
			return totalReserved || allocated || monoUsage;
		}
	}
}