using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
	// Token: 0x0200004C RID: 76
	[Serializable]
	public sealed class TimeZoneInfo : IEquatable<TimeZoneInfo>, IDeserializationCallback, ISerializable
	{
		// Token: 0x06000190 RID: 400 RVA: 0x00007074 File Offset: 0x00005274
		private TimeZoneInfo(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules, bool disableDaylightSavingTime)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (id == string.Empty)
			{
				throw new ArgumentException("id parameter is an empty string");
			}
			if (baseUtcOffset.Ticks % 600000000L != 0L)
			{
				throw new ArgumentException("baseUtcOffset parameter does not represent a whole number of minutes");
			}
			if (baseUtcOffset > new TimeSpan(14, 0, 0) || baseUtcOffset < new TimeSpan(-14, 0, 0))
			{
				throw new ArgumentOutOfRangeException("baseUtcOffset parameter is greater than 14 hours or less than -14 hours");
			}
			if (adjustmentRules != null && adjustmentRules.Length != 0)
			{
				TimeZoneInfo.AdjustmentRule adjustmentRule = null;
				foreach (TimeZoneInfo.AdjustmentRule adjustmentRule2 in adjustmentRules)
				{
					if (adjustmentRule2 == null)
					{
						throw new InvalidTimeZoneException("one or more elements in adjustmentRules are null");
					}
					if (baseUtcOffset + adjustmentRule2.DaylightDelta < new TimeSpan(-14, 0, 0) || baseUtcOffset + adjustmentRule2.DaylightDelta > new TimeSpan(14, 0, 0))
					{
						throw new InvalidTimeZoneException("Sum of baseUtcOffset and DaylightDelta of one or more object in adjustmentRules array is greater than 14 or less than -14 hours;");
					}
					if (adjustmentRule != null && adjustmentRule.DateStart > adjustmentRule2.DateStart)
					{
						throw new InvalidTimeZoneException("adjustment rules specified in adjustmentRules parameter are not in chronological order");
					}
					if (adjustmentRule != null && adjustmentRule.DateEnd > adjustmentRule2.DateStart)
					{
						throw new InvalidTimeZoneException("some adjustment rules in the adjustmentRules parameter overlap");
					}
					if (adjustmentRule != null && adjustmentRule.DateEnd == adjustmentRule2.DateStart)
					{
						throw new InvalidTimeZoneException("a date can have multiple adjustment rules applied to it");
					}
					adjustmentRule = adjustmentRule2;
				}
			}
			this.id = id;
			this.baseUtcOffset = baseUtcOffset;
			this.displayName = displayName ?? id;
			this.standardDisplayName = standardDisplayName ?? id;
			this.daylightDisplayName = daylightDisplayName;
			this.disableDaylightSavingTime = disableDaylightSavingTime;
			this.adjustmentRules = adjustmentRules;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000724C File Offset: 0x0000544C
		public TimeSpan BaseUtcOffset
		{
			get
			{
				return this.baseUtcOffset;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00007254 File Offset: 0x00005454
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000725C File Offset: 0x0000545C
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00007264 File Offset: 0x00005464
		public static TimeZoneInfo Local
		{
			get
			{
				if (TimeZoneInfo.local == null)
				{
					try
					{
						TimeZoneInfo.local = TimeZoneInfo.FindSystemTimeZoneByFileName("Local", "/etc/localtime");
					}
					catch
					{
						try
						{
							TimeZoneInfo.local = TimeZoneInfo.FindSystemTimeZoneByFileName("Local", Path.Combine(TimeZoneInfo.TimeZoneDirectory, "localtime"));
						}
						catch
						{
							throw new TimeZoneNotFoundException();
						}
					}
				}
				return TimeZoneInfo.local;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000196 RID: 406 RVA: 0x000072EC File Offset: 0x000054EC
		public bool SupportsDaylightSavingTime
		{
			get
			{
				return !this.disableDaylightSavingTime;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000197 RID: 407 RVA: 0x000072F8 File Offset: 0x000054F8
		public static TimeZoneInfo Utc
		{
			get
			{
				if (TimeZoneInfo.utc == null)
				{
					TimeZoneInfo.utc = TimeZoneInfo.CreateCustomTimeZone("UTC", new TimeSpan(0L), "UTC", "UTC");
				}
				return TimeZoneInfo.utc;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000732C File Offset: 0x0000552C
		private static string TimeZoneDirectory
		{
			get
			{
				if (TimeZoneInfo.timeZoneDirectory == null)
				{
					TimeZoneInfo.timeZoneDirectory = "/usr/share/zoneinfo";
				}
				return TimeZoneInfo.timeZoneDirectory;
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00007348 File Offset: 0x00005548
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName)
		{
			return TimeZoneInfo.CreateCustomTimeZone(id, baseUtcOffset, displayName, standardDisplayName, null, null, true);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00007358 File Offset: 0x00005558
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules)
		{
			return TimeZoneInfo.CreateCustomTimeZone(id, baseUtcOffset, displayName, standardDisplayName, daylightDisplayName, adjustmentRules, false);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00007368 File Offset: 0x00005568
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules, bool disableDaylightSavingTime)
		{
			return new TimeZoneInfo(id, baseUtcOffset, displayName, standardDisplayName, daylightDisplayName, adjustmentRules, disableDaylightSavingTime);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000737C File Offset: 0x0000557C
		public bool Equals(TimeZoneInfo other)
		{
			return other != null && other.Id == this.Id && this.HasSameRules(other);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000073A8 File Offset: 0x000055A8
		private static TimeZoneInfo FindSystemTimeZoneByFileName(string id, string filepath)
		{
			if (!File.Exists(filepath))
			{
				throw new TimeZoneNotFoundException();
			}
			byte[] array = new byte[16384];
			int num;
			using (FileStream fileStream = File.OpenRead(filepath))
			{
				num = fileStream.Read(array, 0, 16384);
			}
			if (!TimeZoneInfo.ValidTZFile(array, num))
			{
				throw new InvalidTimeZoneException("TZ file too big for the buffer");
			}
			TimeZoneInfo timeZoneInfo;
			try
			{
				timeZoneInfo = TimeZoneInfo.ParseTZBuffer(id, array, num);
			}
			catch (Exception ex)
			{
				throw new InvalidTimeZoneException(ex.Message);
			}
			return timeZoneInfo;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00007454 File Offset: 0x00005654
		public TimeZoneInfo.AdjustmentRule[] GetAdjustmentRules()
		{
			if (this.disableDaylightSavingTime)
			{
				return new TimeZoneInfo.AdjustmentRule[0];
			}
			return (TimeZoneInfo.AdjustmentRule[])this.adjustmentRules.Clone();
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00007478 File Offset: 0x00005678
		public override int GetHashCode()
		{
			int num = this.Id.GetHashCode();
			foreach (TimeZoneInfo.AdjustmentRule adjustmentRule in this.GetAdjustmentRules())
			{
				num ^= adjustmentRule.GetHashCode();
			}
			return num;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000074BC File Offset: 0x000056BC
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000074C4 File Offset: 0x000056C4
		public TimeSpan GetUtcOffset(DateTime dateTime)
		{
			if (this.IsDaylightSavingTime(dateTime))
			{
				TimeZoneInfo.AdjustmentRule applicableRule = this.GetApplicableRule(dateTime);
				return this.BaseUtcOffset + applicableRule.DaylightDelta;
			}
			return this.BaseUtcOffset;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00007500 File Offset: 0x00005700
		public bool HasSameRules(TimeZoneInfo other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.adjustmentRules == null != (other.adjustmentRules == null))
			{
				return false;
			}
			if (this.adjustmentRules == null)
			{
				return true;
			}
			if (this.BaseUtcOffset != other.BaseUtcOffset)
			{
				return false;
			}
			if (this.adjustmentRules.Length != other.adjustmentRules.Length)
			{
				return false;
			}
			for (int i = 0; i < this.adjustmentRules.Length; i++)
			{
				if (!this.adjustmentRules[i].Equals(other.adjustmentRules[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000075AC File Offset: 0x000057AC
		public bool IsDaylightSavingTime(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Local && this.IsInvalidTime(dateTime))
			{
				throw new ArgumentException("dateTime is invalid and Kind is Local");
			}
			if (this == TimeZoneInfo.Utc)
			{
				return false;
			}
			if (!this.SupportsDaylightSavingTime)
			{
				return false;
			}
			if ((dateTime.Kind == DateTimeKind.Local || dateTime.Kind == DateTimeKind.Unspecified) && this == TimeZoneInfo.Local)
			{
				return dateTime.IsDaylightSavingTime();
			}
			if (dateTime.Kind == DateTimeKind.Local && this != TimeZoneInfo.Utc)
			{
				return this.IsDaylightSavingTime(DateTime.SpecifyKind(dateTime.ToUniversalTime(), DateTimeKind.Utc));
			}
			TimeZoneInfo.AdjustmentRule applicableRule = this.GetApplicableRule(dateTime.Date);
			if (applicableRule == null)
			{
				return false;
			}
			DateTime dateTime2 = TimeZoneInfo.TransitionPoint(applicableRule.DaylightTransitionStart, dateTime.Year);
			DateTime dateTime3 = TimeZoneInfo.TransitionPoint(applicableRule.DaylightTransitionEnd, dateTime.Year + ((applicableRule.DaylightTransitionStart.Month >= applicableRule.DaylightTransitionEnd.Month) ? 1 : 0));
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				dateTime2 -= this.BaseUtcOffset;
				dateTime3 -= this.BaseUtcOffset + applicableRule.DaylightDelta;
			}
			return dateTime >= dateTime2 && dateTime < dateTime3;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00007700 File Offset: 0x00005900
		public bool IsInvalidTime(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return false;
			}
			if (dateTime.Kind == DateTimeKind.Local && this != TimeZoneInfo.Local)
			{
				return false;
			}
			TimeZoneInfo.AdjustmentRule applicableRule = this.GetApplicableRule(dateTime);
			DateTime dateTime2 = TimeZoneInfo.TransitionPoint(applicableRule.DaylightTransitionStart, dateTime.Year);
			return dateTime >= dateTime2 && dateTime < dateTime2 + applicableRule.DaylightDelta;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00007778 File Offset: 0x00005978
		public void OnDeserialization(object sender)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00007780 File Offset: 0x00005980
		public override string ToString()
		{
			return this.DisplayName;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00007788 File Offset: 0x00005988
		private TimeZoneInfo.AdjustmentRule GetApplicableRule(DateTime dateTime)
		{
			DateTime dateTime2 = dateTime;
			if (dateTime.Kind == DateTimeKind.Local && this != TimeZoneInfo.Local)
			{
				dateTime2 = dateTime2.ToUniversalTime() + this.BaseUtcOffset;
			}
			if (dateTime.Kind == DateTimeKind.Utc && this != TimeZoneInfo.Utc)
			{
				dateTime2 += this.BaseUtcOffset;
			}
			foreach (TimeZoneInfo.AdjustmentRule adjustmentRule in this.adjustmentRules)
			{
				if (adjustmentRule.DateStart > dateTime2.Date)
				{
					return null;
				}
				if (!(adjustmentRule.DateEnd < dateTime2.Date))
				{
					return adjustmentRule;
				}
			}
			return null;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00007840 File Offset: 0x00005A40
		private static DateTime TransitionPoint(TimeZoneInfo.TransitionTime transition, int year)
		{
			if (transition.IsFixedDateRule)
			{
				return new DateTime(year, transition.Month, transition.Day) + transition.TimeOfDay.TimeOfDay;
			}
			DateTime dateTime = new DateTime(year, transition.Month, 1);
			DayOfWeek dayOfWeek = dateTime.DayOfWeek;
			int num = 1 + (transition.Week - 1) * 7 + (transition.DayOfWeek - dayOfWeek) % 7;
			if (num > DateTime.DaysInMonth(year, transition.Month))
			{
				num -= 7;
			}
			return new DateTime(year, transition.Month, num) + transition.TimeOfDay.TimeOfDay;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000078F0 File Offset: 0x00005AF0
		private static bool ValidTZFile(byte[] buffer, int length)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < 4; i++)
			{
				stringBuilder.Append((char)buffer[i]);
			}
			return !(stringBuilder.ToString() != "TZif") && length < 16384;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00007948 File Offset: 0x00005B48
		private static int SwapInt32(int i)
		{
			return ((i >> 24) & 255) | ((i >> 8) & 65280) | ((i << 8) & 16711680) | (i << 24);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007970 File Offset: 0x00005B70
		private static int ReadBigEndianInt32(byte[] buffer, int start)
		{
			int num = BitConverter.ToInt32(buffer, start);
			if (!BitConverter.IsLittleEndian)
			{
				return num;
			}
			return TimeZoneInfo.SwapInt32(num);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00007998 File Offset: 0x00005B98
		private static TimeZoneInfo ParseTZBuffer(string id, byte[] buffer, int length)
		{
			int num = TimeZoneInfo.ReadBigEndianInt32(buffer, 20);
			int num2 = TimeZoneInfo.ReadBigEndianInt32(buffer, 24);
			int num3 = TimeZoneInfo.ReadBigEndianInt32(buffer, 28);
			int num4 = TimeZoneInfo.ReadBigEndianInt32(buffer, 32);
			int num5 = TimeZoneInfo.ReadBigEndianInt32(buffer, 36);
			int num6 = TimeZoneInfo.ReadBigEndianInt32(buffer, 40);
			if (length < 44 + num4 * 5 + num5 * 6 + num6 + num3 * 8 + num2 + num)
			{
				throw new InvalidTimeZoneException();
			}
			Dictionary<int, string> dictionary = TimeZoneInfo.ParseAbbreviations(buffer, 44 + 4 * num4 + num4 + 6 * num5, num6);
			Dictionary<int, TimeZoneInfo.TimeType> dictionary2 = TimeZoneInfo.ParseTimesTypes(buffer, 44 + 4 * num4 + num4, num5, dictionary);
			List<KeyValuePair<DateTime, TimeZoneInfo.TimeType>> list = TimeZoneInfo.ParseTransitions(buffer, 44, num4, dictionary2);
			if (dictionary2.Count == 0)
			{
				throw new InvalidTimeZoneException();
			}
			if (dictionary2.Count == 1 && dictionary2[0].IsDst)
			{
				throw new InvalidTimeZoneException();
			}
			TimeSpan timeSpan = new TimeSpan(0L);
			TimeSpan timeSpan2 = new TimeSpan(0L);
			string text = null;
			string text2 = null;
			bool flag = false;
			DateTime dateTime = DateTime.MinValue;
			List<TimeZoneInfo.AdjustmentRule> list2 = new List<TimeZoneInfo.AdjustmentRule>();
			for (int i = 0; i < list.Count; i++)
			{
				KeyValuePair<DateTime, TimeZoneInfo.TimeType> keyValuePair = list[i];
				DateTime key = keyValuePair.Key;
				TimeZoneInfo.TimeType value = keyValuePair.Value;
				if (!value.IsDst)
				{
					if (text != value.Name || timeSpan.TotalSeconds != (double)value.Offset)
					{
						text = value.Name;
						text2 = null;
						timeSpan = new TimeSpan(0, 0, value.Offset);
						list2 = new List<TimeZoneInfo.AdjustmentRule>();
						flag = false;
					}
					if (flag)
					{
						dateTime += timeSpan;
						DateTime dateTime2 = key + timeSpan + timeSpan2;
						if (dateTime2.Date == new DateTime(dateTime2.Year, 1, 1) && dateTime2.Year > dateTime.Year)
						{
							dateTime2 -= new TimeSpan(24, 0, 0);
						}
						DateTime dateTime3;
						if (dateTime.Month < 7)
						{
							dateTime3 = new DateTime(dateTime.Year, 1, 1);
						}
						else
						{
							dateTime3 = new DateTime(dateTime.Year, 7, 1);
						}
						DateTime dateTime4;
						if (dateTime2.Month >= 7)
						{
							dateTime4 = new DateTime(dateTime2.Year, 12, 31);
						}
						else
						{
							dateTime4 = new DateTime(dateTime2.Year, 6, 30);
						}
						TimeZoneInfo.TransitionTime transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1) + dateTime.TimeOfDay, dateTime.Month, dateTime.Day);
						TimeZoneInfo.TransitionTime transitionTime2 = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1) + dateTime2.TimeOfDay, dateTime2.Month, dateTime2.Day);
						if (transitionTime != transitionTime2)
						{
							list2.Add(TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(dateTime3, dateTime4, timeSpan2, transitionTime, transitionTime2));
						}
					}
					flag = false;
				}
				else
				{
					if (text2 != value.Name || timeSpan2.TotalSeconds != (double)value.Offset - timeSpan.TotalSeconds)
					{
						text2 = value.Name;
						timeSpan2 = new TimeSpan(0, 0, value.Offset) - timeSpan;
					}
					dateTime = key;
					flag = true;
				}
			}
			if (list2.Count == 0)
			{
				TimeZoneInfo.TimeType timeType = dictionary2[0];
				if (text == null)
				{
					text = timeType.Name;
					timeSpan = new TimeSpan(0, 0, timeType.Offset);
				}
				return TimeZoneInfo.CreateCustomTimeZone(id, timeSpan, id, text);
			}
			return TimeZoneInfo.CreateCustomTimeZone(id, timeSpan, id, text, text2, TimeZoneInfo.ValidateRules(list2).ToArray());
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00007D2C File Offset: 0x00005F2C
		private static List<TimeZoneInfo.AdjustmentRule> ValidateRules(List<TimeZoneInfo.AdjustmentRule> adjustmentRules)
		{
			TimeZoneInfo.AdjustmentRule adjustmentRule = null;
			foreach (TimeZoneInfo.AdjustmentRule adjustmentRule2 in adjustmentRules.ToArray())
			{
				if (adjustmentRule != null && adjustmentRule.DateEnd > adjustmentRule2.DateStart)
				{
					adjustmentRules.Remove(adjustmentRule2);
				}
				adjustmentRule = adjustmentRule2;
			}
			return adjustmentRules;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007D84 File Offset: 0x00005F84
		private static Dictionary<int, string> ParseAbbreviations(byte[] buffer, int index, int count)
		{
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < count; i++)
			{
				char c = (char)buffer[index + i];
				if (c != '\0')
				{
					stringBuilder.Append(c);
				}
				else
				{
					dictionary.Add(num, stringBuilder.ToString());
					for (int j = 1; j < stringBuilder.Length; j++)
					{
						dictionary.Add(num + j, stringBuilder.ToString(j, stringBuilder.Length - j));
					}
					num = i + 1;
					stringBuilder = new StringBuilder();
				}
			}
			return dictionary;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00007E1C File Offset: 0x0000601C
		private static Dictionary<int, TimeZoneInfo.TimeType> ParseTimesTypes(byte[] buffer, int index, int count, Dictionary<int, string> abbreviations)
		{
			Dictionary<int, TimeZoneInfo.TimeType> dictionary = new Dictionary<int, TimeZoneInfo.TimeType>(count);
			for (int i = 0; i < count; i++)
			{
				int num = TimeZoneInfo.ReadBigEndianInt32(buffer, index + 6 * i);
				byte b = buffer[index + 6 * i + 4];
				byte b2 = buffer[index + 6 * i + 5];
				dictionary.Add(i, new TimeZoneInfo.TimeType(num, b != 0, abbreviations[(int)b2]));
			}
			return dictionary;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00007E80 File Offset: 0x00006080
		private static List<KeyValuePair<DateTime, TimeZoneInfo.TimeType>> ParseTransitions(byte[] buffer, int index, int count, Dictionary<int, TimeZoneInfo.TimeType> time_types)
		{
			List<KeyValuePair<DateTime, TimeZoneInfo.TimeType>> list = new List<KeyValuePair<DateTime, TimeZoneInfo.TimeType>>(count);
			for (int i = 0; i < count; i++)
			{
				int num = TimeZoneInfo.ReadBigEndianInt32(buffer, index + 4 * i);
				DateTime dateTime = TimeZoneInfo.DateTimeFromUnixTime((long)num);
				byte b = buffer[index + 4 * count + i];
				list.Add(new KeyValuePair<DateTime, TimeZoneInfo.TimeType>(dateTime, time_types[(int)b]));
			}
			return list;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00007EDC File Offset: 0x000060DC
		private static DateTime DateTimeFromUnixTime(long unix_time)
		{
			DateTime dateTime = new DateTime(1970, 1, 1);
			return dateTime.AddSeconds((double)unix_time);
		}

		// Token: 0x04000160 RID: 352
		private const int BUFFER_SIZE = 16384;

		// Token: 0x04000161 RID: 353
		private TimeSpan baseUtcOffset;

		// Token: 0x04000162 RID: 354
		private string daylightDisplayName;

		// Token: 0x04000163 RID: 355
		private string displayName;

		// Token: 0x04000164 RID: 356
		private string id;

		// Token: 0x04000165 RID: 357
		private static TimeZoneInfo local;

		// Token: 0x04000166 RID: 358
		private string standardDisplayName;

		// Token: 0x04000167 RID: 359
		private bool disableDaylightSavingTime;

		// Token: 0x04000168 RID: 360
		private static TimeZoneInfo utc;

		// Token: 0x04000169 RID: 361
		private static string timeZoneDirectory;

		// Token: 0x0400016A RID: 362
		private TimeZoneInfo.AdjustmentRule[] adjustmentRules;

		// Token: 0x0400016B RID: 363
		private static List<TimeZoneInfo> systemTimeZones;

		// Token: 0x0200004D RID: 77
		[Serializable]
		public sealed class AdjustmentRule : IEquatable<TimeZoneInfo.AdjustmentRule>, IDeserializationCallback, ISerializable
		{
			// Token: 0x060001B2 RID: 434 RVA: 0x00007F00 File Offset: 0x00006100
			private AdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd)
			{
				if (dateStart.Kind != DateTimeKind.Unspecified || dateEnd.Kind != DateTimeKind.Unspecified)
				{
					throw new ArgumentException("the Kind property of dateStart or dateEnd parameter does not equal DateTimeKind.Unspecified");
				}
				if (daylightTransitionStart == daylightTransitionEnd)
				{
					throw new ArgumentException("daylightTransitionStart parameter cannot equal daylightTransitionEnd parameter");
				}
				if (dateStart.Ticks % 864000000000L != 0L || dateEnd.Ticks % 864000000000L != 0L)
				{
					throw new ArgumentException("dateStart or dateEnd parameter includes a time of day value");
				}
				if (dateEnd < dateStart)
				{
					throw new ArgumentOutOfRangeException("dateEnd is earlier than dateStart");
				}
				if (daylightDelta > new TimeSpan(14, 0, 0) || daylightDelta < new TimeSpan(-14, 0, 0))
				{
					throw new ArgumentOutOfRangeException("daylightDelta is less than -14 or greater than 14 hours");
				}
				if (daylightDelta.Ticks % 10000000L != 0L)
				{
					throw new ArgumentOutOfRangeException("daylightDelta parameter does not represent a whole number of seconds");
				}
				this.dateStart = dateStart;
				this.dateEnd = dateEnd;
				this.daylightDelta = daylightDelta;
				this.daylightTransitionStart = daylightTransitionStart;
				this.daylightTransitionEnd = daylightTransitionEnd;
			}

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x060001B3 RID: 435 RVA: 0x00008014 File Offset: 0x00006214
			public DateTime DateEnd
			{
				get
				{
					return this.dateEnd;
				}
			}

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000801C File Offset: 0x0000621C
			public DateTime DateStart
			{
				get
				{
					return this.dateStart;
				}
			}

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x060001B5 RID: 437 RVA: 0x00008024 File Offset: 0x00006224
			public TimeSpan DaylightDelta
			{
				get
				{
					return this.daylightDelta;
				}
			}

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000802C File Offset: 0x0000622C
			public TimeZoneInfo.TransitionTime DaylightTransitionEnd
			{
				get
				{
					return this.daylightTransitionEnd;
				}
			}

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x060001B7 RID: 439 RVA: 0x00008034 File Offset: 0x00006234
			public TimeZoneInfo.TransitionTime DaylightTransitionStart
			{
				get
				{
					return this.daylightTransitionStart;
				}
			}

			// Token: 0x060001B8 RID: 440 RVA: 0x0000803C File Offset: 0x0000623C
			public static TimeZoneInfo.AdjustmentRule CreateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd)
			{
				return new TimeZoneInfo.AdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd);
			}

			// Token: 0x060001B9 RID: 441 RVA: 0x0000804C File Offset: 0x0000624C
			public bool Equals(TimeZoneInfo.AdjustmentRule other)
			{
				return this.dateStart == other.dateStart && this.dateEnd == other.dateEnd && this.daylightDelta == other.daylightDelta && this.daylightTransitionStart == other.daylightTransitionStart && this.daylightTransitionEnd == other.daylightTransitionEnd;
			}

			// Token: 0x060001BA RID: 442 RVA: 0x000080C8 File Offset: 0x000062C8
			public override int GetHashCode()
			{
				return this.dateStart.GetHashCode() ^ this.dateEnd.GetHashCode() ^ this.daylightDelta.GetHashCode() ^ this.daylightTransitionStart.GetHashCode() ^ this.daylightTransitionEnd.GetHashCode();
			}

			// Token: 0x060001BB RID: 443 RVA: 0x00008108 File Offset: 0x00006308
			public void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001BC RID: 444 RVA: 0x00008110 File Offset: 0x00006310
			public void OnDeserialization(object sender)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0400016C RID: 364
			private DateTime dateEnd;

			// Token: 0x0400016D RID: 365
			private DateTime dateStart;

			// Token: 0x0400016E RID: 366
			private TimeSpan daylightDelta;

			// Token: 0x0400016F RID: 367
			private TimeZoneInfo.TransitionTime daylightTransitionEnd;

			// Token: 0x04000170 RID: 368
			private TimeZoneInfo.TransitionTime daylightTransitionStart;
		}

		// Token: 0x0200004E RID: 78
		private struct TimeType
		{
			// Token: 0x060001BD RID: 445 RVA: 0x00008118 File Offset: 0x00006318
			public TimeType(int offset, bool is_dst, string abbrev)
			{
				this.Offset = offset;
				this.IsDst = is_dst;
				this.Name = abbrev;
			}

			// Token: 0x060001BE RID: 446 RVA: 0x00008130 File Offset: 0x00006330
			public override string ToString()
			{
				return string.Concat(new object[] { "offset: ", this.Offset, "s, is_dst: ", this.IsDst, ", zone name: ", this.Name });
			}

			// Token: 0x04000171 RID: 369
			public readonly int Offset;

			// Token: 0x04000172 RID: 370
			public readonly bool IsDst;

			// Token: 0x04000173 RID: 371
			public string Name;
		}

		// Token: 0x0200004F RID: 79
		[Serializable]
		public struct TransitionTime : IEquatable<TimeZoneInfo.TransitionTime>, IDeserializationCallback, ISerializable
		{
			// Token: 0x060001BF RID: 447 RVA: 0x00008188 File Offset: 0x00006388
			private TransitionTime(DateTime timeOfDay, int month, int day)
			{
				this = new TimeZoneInfo.TransitionTime(timeOfDay, month);
				if (day < 1 || day > 31)
				{
					throw new ArgumentOutOfRangeException("day parameter is less than 1 or greater than 31");
				}
				this.day = day;
				this.isFixedDateRule = true;
			}

			// Token: 0x060001C0 RID: 448 RVA: 0x000081BC File Offset: 0x000063BC
			private TransitionTime(DateTime timeOfDay, int month)
			{
				if (timeOfDay.Year != 1 || timeOfDay.Month != 1 || timeOfDay.Day != 1)
				{
					throw new ArgumentException("timeOfDay parameter has a non-default date component");
				}
				if (timeOfDay.Kind != DateTimeKind.Unspecified)
				{
					throw new ArgumentException("timeOfDay parameter Kind's property is not DateTimeKind.Unspecified");
				}
				if (timeOfDay.Ticks % 10000L != 0L)
				{
					throw new ArgumentException("timeOfDay parameter does not represent a whole number of milliseconds");
				}
				if (month < 1 || month > 12)
				{
					throw new ArgumentOutOfRangeException("month parameter is less than 1 or greater than 12");
				}
				this.timeOfDay = timeOfDay;
				this.month = month;
				this.week = -1;
				this.dayOfWeek = (DayOfWeek)(-1);
				this.day = -1;
				this.isFixedDateRule = false;
			}

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x060001C1 RID: 449 RVA: 0x00008274 File Offset: 0x00006474
			public DateTime TimeOfDay
			{
				get
				{
					return this.timeOfDay;
				}
			}

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000827C File Offset: 0x0000647C
			public int Month
			{
				get
				{
					return this.month;
				}
			}

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x060001C3 RID: 451 RVA: 0x00008284 File Offset: 0x00006484
			public int Day
			{
				get
				{
					return this.day;
				}
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000828C File Offset: 0x0000648C
			public int Week
			{
				get
				{
					return this.week;
				}
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x060001C5 RID: 453 RVA: 0x00008294 File Offset: 0x00006494
			public DayOfWeek DayOfWeek
			{
				get
				{
					return this.dayOfWeek;
				}
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000829C File Offset: 0x0000649C
			public bool IsFixedDateRule
			{
				get
				{
					return this.isFixedDateRule;
				}
			}

			// Token: 0x060001C7 RID: 455 RVA: 0x000082A4 File Offset: 0x000064A4
			public static TimeZoneInfo.TransitionTime CreateFixedDateRule(DateTime timeOfDay, int month, int day)
			{
				return new TimeZoneInfo.TransitionTime(timeOfDay, month, day);
			}

			// Token: 0x060001C8 RID: 456 RVA: 0x000082B0 File Offset: 0x000064B0
			public void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001C9 RID: 457 RVA: 0x000082B8 File Offset: 0x000064B8
			public override bool Equals(object other)
			{
				return other is TimeZoneInfo.TransitionTime && this == (TimeZoneInfo.TransitionTime)other;
			}

			// Token: 0x060001CA RID: 458 RVA: 0x000082D8 File Offset: 0x000064D8
			public bool Equals(TimeZoneInfo.TransitionTime other)
			{
				return this == other;
			}

			// Token: 0x060001CB RID: 459 RVA: 0x000082E8 File Offset: 0x000064E8
			public override int GetHashCode()
			{
				return this.day ^ (int)this.dayOfWeek ^ this.month ^ (int)this.timeOfDay.Ticks ^ this.week;
			}

			// Token: 0x060001CC RID: 460 RVA: 0x00008314 File Offset: 0x00006514
			public void OnDeserialization(object sender)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001CD RID: 461 RVA: 0x0000831C File Offset: 0x0000651C
			public static bool operator ==(TimeZoneInfo.TransitionTime t1, TimeZoneInfo.TransitionTime t2)
			{
				return t1.day == t2.day && t1.dayOfWeek == t2.dayOfWeek && t1.isFixedDateRule == t2.isFixedDateRule && t1.month == t2.month && t1.timeOfDay == t2.timeOfDay && t1.week == t2.week;
			}

			// Token: 0x060001CE RID: 462 RVA: 0x000083A0 File Offset: 0x000065A0
			public static bool operator !=(TimeZoneInfo.TransitionTime t1, TimeZoneInfo.TransitionTime t2)
			{
				return !(t1 == t2);
			}

			// Token: 0x04000174 RID: 372
			private DateTime timeOfDay;

			// Token: 0x04000175 RID: 373
			private int month;

			// Token: 0x04000176 RID: 374
			private int day;

			// Token: 0x04000177 RID: 375
			private int week;

			// Token: 0x04000178 RID: 376
			private DayOfWeek dayOfWeek;

			// Token: 0x04000179 RID: 377
			private bool isFixedDateRule;
		}
	}
}
