using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000026 RID: 38
	public abstract class Expression
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00004D18 File Offset: 0x00002F18
		protected Expression(ExpressionType node_type, Type type)
		{
			this.node_type = node_type;
			this.type = type;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004D30 File Offset: 0x00002F30
		public ExpressionType NodeType
		{
			get
			{
				return this.node_type;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004D38 File Offset: 0x00002F38
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004D40 File Offset: 0x00002F40
		public override string ToString()
		{
			return ExpressionPrinter.ToString(this);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004D48 File Offset: 0x00002F48
		private static bool IsAssignableToParameterType(Type type, ParameterInfo param)
		{
			Type type2 = param.ParameterType;
			if (type2.IsByRef)
			{
				type2 = type2.GetElementType();
			}
			return type.GetNotNullableType().IsAssignableTo(type2);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004D7C File Offset: 0x00002F7C
		public static MethodCallExpression Call(Expression instance, MethodInfo method, params Expression[] arguments)
		{
			return Expression.Call(instance, method, arguments);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004D88 File Offset: 0x00002F88
		public static MethodCallExpression Call(Expression instance, MethodInfo method, IEnumerable<Expression> arguments)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			if (instance == null && !method.IsStatic)
			{
				throw new ArgumentNullException("instance");
			}
			if (method.IsStatic && instance != null)
			{
				throw new ArgumentException("instance");
			}
			if (!method.IsStatic && !instance.Type.IsAssignableTo(method.DeclaringType))
			{
				throw new ArgumentException("Type is not assignable to the declaring type of the method");
			}
			ReadOnlyCollection<Expression> readOnlyCollection = Expression.CheckMethodArguments(method, arguments);
			return new MethodCallExpression(instance, method, readOnlyCollection);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004E1C File Offset: 0x0000301C
		public static ConstantExpression Constant(object value)
		{
			if (value == null)
			{
				return new ConstantExpression(null, typeof(object));
			}
			return Expression.Constant(value, value.GetType());
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004E44 File Offset: 0x00003044
		public static ConstantExpression Constant(object value, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (value == null)
			{
				if (type.IsValueType && !type.IsNullable())
				{
					throw new ArgumentException();
				}
			}
			else if ((!type.IsValueType || !type.IsNullable()) && !value.GetType().IsAssignableTo(type))
			{
				throw new ArgumentException();
			}
			return new ConstantExpression(value, type);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004EC0 File Offset: 0x000030C0
		public static MemberExpression Field(Expression expression, FieldInfo field)
		{
			if (field == null)
			{
				throw new ArgumentNullException("field");
			}
			if (!field.IsStatic)
			{
				if (expression == null)
				{
					throw new ArgumentNullException("expression");
				}
				if (!expression.Type.IsAssignableTo(field.DeclaringType))
				{
					throw new ArgumentException("field");
				}
			}
			else if (expression != null)
			{
				throw new ArgumentException("expression");
			}
			return new MemberExpression(expression, field, field.FieldType);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004F40 File Offset: 0x00003140
		private static bool CanAssign(Type target, Type source)
		{
			return !(target.IsValueType ^ source.IsValueType) && source.IsAssignableTo(target);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004F60 File Offset: 0x00003160
		private static Expression CheckLambda(Type delegateType, Expression body, ReadOnlyCollection<ParameterExpression> parameters)
		{
			if (!delegateType.IsSubclassOf(typeof(Delegate)))
			{
				throw new ArgumentException("delegateType");
			}
			MethodInfo invokeMethod = delegateType.GetInvokeMethod();
			if (invokeMethod == null)
			{
				throw new ArgumentException("delegate must contain an Invoke method", "delegateType");
			}
			ParameterInfo[] parameters2 = invokeMethod.GetParameters();
			if (parameters2.Length != parameters.Count)
			{
				throw new ArgumentException(string.Format("Different number of arguments in delegate {0}", delegateType), "delegateType");
			}
			for (int i = 0; i < parameters2.Length; i++)
			{
				ParameterExpression parameterExpression = parameters[i];
				if (parameterExpression == null)
				{
					throw new ArgumentNullException("parameters");
				}
				if (!Expression.CanAssign(parameterExpression.Type, parameters2[i].ParameterType))
				{
					throw new ArgumentException(string.Format("Can not assign a {0} to a {1}", parameters2[i].ParameterType, parameterExpression.Type));
				}
			}
			if (invokeMethod.ReturnType == typeof(void) || Expression.CanAssign(invokeMethod.ReturnType, body.Type))
			{
				return body;
			}
			if (invokeMethod.ReturnType.IsExpression())
			{
				return Expression.Quote(body);
			}
			throw new ArgumentException(string.Format("body type {0} can not be assigned to {1}", body.Type, invokeMethod.ReturnType));
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005098 File Offset: 0x00003298
		public static Expression<TDelegate> Lambda<TDelegate>(Expression body, params ParameterExpression[] parameters)
		{
			return Expression.Lambda<TDelegate>(body, parameters);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000050A4 File Offset: 0x000032A4
		public static Expression<TDelegate> Lambda<TDelegate>(Expression body, IEnumerable<ParameterExpression> parameters)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			ReadOnlyCollection<ParameterExpression> readOnlyCollection = parameters.ToReadOnlyCollection<ParameterExpression>();
			body = Expression.CheckLambda(typeof(TDelegate), body, readOnlyCollection);
			return new Expression<TDelegate>(body, readOnlyCollection);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000050E4 File Offset: 0x000032E4
		private static IList<Expression> CreateArgumentList(IEnumerable<Expression> arguments)
		{
			if (arguments == null)
			{
				return arguments.ToReadOnlyCollection<Expression>();
			}
			return arguments.ToList<Expression>();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000050FC File Offset: 0x000032FC
		private static void CheckNonGenericMethod(MethodBase method)
		{
			if (method.IsGenericMethodDefinition || method.ContainsGenericParameters)
			{
				throw new ArgumentException("Can not used open generic methods");
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005120 File Offset: 0x00003320
		private static ReadOnlyCollection<Expression> CheckMethodArguments(MethodBase method, IEnumerable<Expression> args)
		{
			Expression.CheckNonGenericMethod(method);
			IList<Expression> list = Expression.CreateArgumentList(args);
			ParameterInfo[] parameters = method.GetParameters();
			if (list.Count != parameters.Length)
			{
				throw new ArgumentException("The number of arguments doesn't match the number of parameters");
			}
			for (int i = 0; i < parameters.Length; i++)
			{
				if (list[i] == null)
				{
					throw new ArgumentNullException("arguments");
				}
				if (!Expression.IsAssignableToParameterType(list[i].Type, parameters[i]))
				{
					if (!parameters[i].ParameterType.IsExpression())
					{
						throw new ArgumentException("arguments");
					}
					list[i] = Expression.Quote(list[i]);
				}
			}
			return list.ToReadOnlyCollection<Expression>();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000051D4 File Offset: 0x000033D4
		public static UnaryExpression Quote(Expression expression)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			return new UnaryExpression(ExpressionType.Quote, expression, expression.GetType());
		}

		// Token: 0x040000D0 RID: 208
		internal const BindingFlags PublicInstance = default(BindingFlags);

		// Token: 0x040000D1 RID: 209
		internal const BindingFlags NonPublicInstance = default(BindingFlags);

		// Token: 0x040000D2 RID: 210
		internal const BindingFlags PublicStatic = default(BindingFlags);

		// Token: 0x040000D3 RID: 211
		internal const BindingFlags AllInstance = default(BindingFlags);

		// Token: 0x040000D4 RID: 212
		internal const BindingFlags AllStatic = default(BindingFlags);

		// Token: 0x040000D5 RID: 213
		internal const BindingFlags All = default(BindingFlags);

		// Token: 0x040000D6 RID: 214
		private ExpressionType node_type;

		// Token: 0x040000D7 RID: 215
		private Type type;
	}
}
