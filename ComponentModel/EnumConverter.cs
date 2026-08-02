using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.ComponentModel
{
	// Token: 0x02000032 RID: 50
	public class EnumConverter : TypeConverter
	{
		// Token: 0x06000110 RID: 272 RVA: 0x0000539C File Offset: 0x0000359C
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(global::System.ComponentModel.Design.Serialization.InstanceDescriptor) || destinationType == typeof(Enum[]) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000053CC File Offset: 0x000035CC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != typeof(string) || value == null)
			{
				if (destinationType == typeof(global::System.ComponentModel.Design.Serialization.InstanceDescriptor) && value != null)
				{
					string text = base.ConvertToString(context, culture, value);
					if (this.IsFlags && text.IndexOf(",") != -1)
					{
						if (value is IConvertible)
						{
							Type underlyingType = Enum.GetUnderlyingType(this.type);
							object obj = ((IConvertible)value).ToType(underlyingType, culture);
							MethodInfo method = typeof(Enum).GetMethod("ToObject", new Type[]
							{
								typeof(Type),
								underlyingType
							});
							return new global::System.ComponentModel.Design.Serialization.InstanceDescriptor(method, new object[] { this.type, obj });
						}
					}
					else
					{
						FieldInfo field = this.type.GetField(text);
						if (field != null)
						{
							return new global::System.ComponentModel.Design.Serialization.InstanceDescriptor(field, null);
						}
					}
				}
				else if (destinationType == typeof(Enum[]) && value != null)
				{
					if (!this.IsFlags)
					{
						return new Enum[] { (Enum)Enum.ToObject(this.type, value) };
					}
					long num = Convert.ToInt64((Enum)value, culture);
					Array values = Enum.GetValues(this.type);
					long[] array = new long[values.Length];
					for (int i = 0; i < values.Length; i++)
					{
						array[i] = Convert.ToInt64(values.GetValue(i));
					}
					ArrayList arrayList = new ArrayList();
					bool flag = false;
					while (!flag)
					{
						flag = true;
						foreach (long num2 in array)
						{
							if ((num2 != 0L && (num2 & num) == num2) || num2 == num)
							{
								arrayList.Add(Enum.ToObject(this.type, num2));
								num &= ~num2;
								flag = false;
							}
						}
						if (num == 0L)
						{
							flag = true;
						}
					}
					if (num != 0L)
					{
						arrayList.Add(Enum.ToObject(this.type, num));
					}
					return arrayList.ToArray(typeof(Enum));
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (value is IConvertible)
			{
				Type underlyingType2 = Enum.GetUnderlyingType(this.type);
				if (underlyingType2 != value.GetType())
				{
					value = ((IConvertible)value).ToType(underlyingType2, culture);
				}
			}
			if (!this.IsFlags && !this.IsValid(context, value))
			{
				throw this.CreateValueNotValidException(value);
			}
			return Enum.Format(this.type, value, "G");
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005678 File Offset: 0x00003878
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || sourceType == typeof(Enum[]) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000056A8 File Offset: 0x000038A8
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = value as string;
				try
				{
					if (text.IndexOf(',') == -1)
					{
						return Enum.Parse(this.type, text, true);
					}
					long num = 0L;
					string[] array = text.Split(new char[] { ',' });
					foreach (string text2 in array)
					{
						Enum @enum = (Enum)Enum.Parse(this.type, text2, true);
						num |= Convert.ToInt64(@enum, culture);
					}
					return Enum.ToObject(this.type, num);
				}
				catch (Exception ex)
				{
					throw new FormatException(text + " is not a valid value for " + this.type.Name, ex);
				}
			}
			if (value is Enum[])
			{
				long num2 = 0L;
				foreach (Enum enum2 in (Enum[])value)
				{
					num2 |= Convert.ToInt64(enum2, culture);
				}
				return Enum.ToObject(this.type, num2);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000057EC File Offset: 0x000039EC
		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			return Enum.IsDefined(this.type, value);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000057FC File Offset: 0x000039FC
		private ArgumentException CreateValueNotValidException(object value)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "The value '{0}' is not a valid value for the enum '{1}'", new object[]
			{
				value,
				this.type.Name
			});
			return new ArgumentException(text);
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00005838 File Offset: 0x00003A38
		private bool IsFlags
		{
			get
			{
				return this.type.IsDefined(typeof(FlagsAttribute), false);
			}
		}

		// Token: 0x0400006B RID: 107
		private Type type;

		// Token: 0x0400006C RID: 108
		private TypeConverter.StandardValuesCollection stdValues;
	}
}
