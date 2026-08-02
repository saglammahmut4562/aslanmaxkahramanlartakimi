using System;
using System.Collections.ObjectModel;
using System.Text;

namespace System.Linq.Expressions
{
	// Token: 0x02000028 RID: 40
	internal class ExpressionPrinter : ExpressionVisitor
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x0000520C File Offset: 0x0000340C
		private ExpressionPrinter(StringBuilder builder)
		{
			this.builder = builder;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000521C File Offset: 0x0000341C
		private ExpressionPrinter()
			: this(new StringBuilder())
		{
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000522C File Offset: 0x0000342C
		public static string ToString(Expression expression)
		{
			ExpressionPrinter expressionPrinter = new ExpressionPrinter();
			expressionPrinter.Visit(expression);
			return expressionPrinter.builder.ToString();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005254 File Offset: 0x00003454
		public static string ToString(ElementInit init)
		{
			ExpressionPrinter expressionPrinter = new ExpressionPrinter();
			expressionPrinter.VisitElementInitializer(init);
			return expressionPrinter.builder.ToString();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000527C File Offset: 0x0000347C
		public static string ToString(MemberBinding binding)
		{
			ExpressionPrinter expressionPrinter = new ExpressionPrinter();
			expressionPrinter.VisitBinding(binding);
			return expressionPrinter.builder.ToString();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000052A4 File Offset: 0x000034A4
		private void Print(string str)
		{
			this.builder.Append(str);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000052B4 File Offset: 0x000034B4
		private void Print(object obj)
		{
			this.builder.Append(obj);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000052C4 File Offset: 0x000034C4
		private void Print(string str, params object[] objs)
		{
			this.builder.AppendFormat(str, objs);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000052D4 File Offset: 0x000034D4
		protected override void VisitElementInitializer(ElementInit initializer)
		{
			this.Print(initializer.AddMethod);
			this.Print("(");
			this.VisitExpressionList(initializer.Arguments);
			this.Print(")");
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005304 File Offset: 0x00003504
		protected override void VisitUnary(UnaryExpression unary)
		{
			ExpressionType nodeType = unary.NodeType;
			if (nodeType != ExpressionType.Convert && nodeType != ExpressionType.ConvertChecked)
			{
				if (nodeType == ExpressionType.Negate)
				{
					this.Print("-");
					this.Visit(unary.Operand);
					return;
				}
				if (nodeType == ExpressionType.UnaryPlus)
				{
					this.Print("+");
					this.Visit(unary.Operand);
					return;
				}
				if (nodeType != ExpressionType.ArrayLength && nodeType != ExpressionType.Not)
				{
					if (nodeType == ExpressionType.Quote)
					{
						this.Visit(unary.Operand);
						return;
					}
					if (nodeType != ExpressionType.TypeAs)
					{
						throw new NotImplementedException();
					}
					this.Print("(");
					this.Visit(unary.Operand);
					this.Print(" As {0})", new object[] { unary.Type.Name });
					return;
				}
			}
			this.Print("{0}(", new object[] { unary.NodeType });
			this.Visit(unary.Operand);
			this.Print(")");
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000540C File Offset: 0x0000360C
		private static string OperatorToString(BinaryExpression binary)
		{
			switch (binary.NodeType)
			{
			case ExpressionType.Add:
			case ExpressionType.AddChecked:
				return "+";
			case ExpressionType.And:
				return (!ExpressionPrinter.IsBoolean(binary)) ? "&" : "And";
			case ExpressionType.AndAlso:
				return "&&";
			case ExpressionType.Coalesce:
				return "??";
			case ExpressionType.Divide:
				return "/";
			case ExpressionType.Equal:
				return "=";
			case ExpressionType.ExclusiveOr:
				return "^";
			case ExpressionType.GreaterThan:
				return ">";
			case ExpressionType.GreaterThanOrEqual:
				return ">=";
			case ExpressionType.LeftShift:
				return "<<";
			case ExpressionType.LessThan:
				return "<";
			case ExpressionType.LessThanOrEqual:
				return "<=";
			case ExpressionType.Modulo:
				return "%";
			case ExpressionType.Multiply:
			case ExpressionType.MultiplyChecked:
				return "*";
			case ExpressionType.NotEqual:
				return "!=";
			case ExpressionType.Or:
				return (!ExpressionPrinter.IsBoolean(binary)) ? "|" : "Or";
			case ExpressionType.OrElse:
				return "||";
			case ExpressionType.Power:
				return "^";
			case ExpressionType.RightShift:
				return ">>";
			case ExpressionType.Subtract:
			case ExpressionType.SubtractChecked:
				return "-";
			}
			return null;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005580 File Offset: 0x00003780
		private static bool IsBoolean(Expression expression)
		{
			return expression.Type == typeof(bool) || expression.Type == typeof(bool?);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000055AC File Offset: 0x000037AC
		private void PrintArrayIndex(BinaryExpression index)
		{
			this.Visit(index.Left);
			this.Print("[");
			this.Visit(index.Right);
			this.Print("]");
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000055DC File Offset: 0x000037DC
		protected override void VisitBinary(BinaryExpression binary)
		{
			ExpressionType nodeType = binary.NodeType;
			if (nodeType != ExpressionType.ArrayIndex)
			{
				this.Print("(");
				this.Visit(binary.Left);
				this.Print(" {0} ", new object[] { ExpressionPrinter.OperatorToString(binary) });
				this.Visit(binary.Right);
				this.Print(")");
				return;
			}
			this.PrintArrayIndex(binary);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000564C File Offset: 0x0000384C
		protected override void VisitTypeIs(TypeBinaryExpression type)
		{
			ExpressionType nodeType = type.NodeType;
			if (nodeType != ExpressionType.TypeIs)
			{
				throw new NotImplementedException();
			}
			this.Print("(");
			this.Visit(type.Expression);
			this.Print(" Is {0})", new object[] { type.TypeOperand.Name });
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000056AC File Offset: 0x000038AC
		protected override void VisitConstant(ConstantExpression constant)
		{
			object value = constant.Value;
			if (value == null)
			{
				this.Print("null");
			}
			else if (value is string)
			{
				this.Print("\"");
				this.Print(value);
				this.Print("\"");
			}
			else if (!ExpressionPrinter.HasStringRepresentation(value))
			{
				this.Print("value(");
				this.Print(value);
				this.Print(")");
			}
			else
			{
				this.Print(value);
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005738 File Offset: 0x00003938
		private static bool HasStringRepresentation(object obj)
		{
			return obj.ToString() != obj.GetType().ToString();
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005750 File Offset: 0x00003950
		protected override void VisitConditional(ConditionalExpression conditional)
		{
			this.Print("IIF(");
			this.Visit(conditional.Test);
			this.Print(", ");
			this.Visit(conditional.IfTrue);
			this.Print(", ");
			this.Visit(conditional.IfFalse);
			this.Print(")");
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000057B0 File Offset: 0x000039B0
		protected override void VisitParameter(ParameterExpression parameter)
		{
			this.Print(parameter.Name ?? "<param>");
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000057CC File Offset: 0x000039CC
		protected override void VisitMemberAccess(MemberExpression access)
		{
			if (access.Expression == null)
			{
				this.Print(access.Member.DeclaringType.Name);
			}
			else
			{
				this.Visit(access.Expression);
			}
			this.Print(".{0}", new object[] { access.Member.Name });
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000582C File Offset: 0x00003A2C
		protected override void VisitMethodCall(MethodCallExpression call)
		{
			if (call.Object != null)
			{
				this.Visit(call.Object);
				this.Print(".");
			}
			this.Print(call.Method.Name);
			this.Print("(");
			this.VisitExpressionList(call.Arguments);
			this.Print(")");
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005890 File Offset: 0x00003A90
		protected override void VisitMemberAssignment(MemberAssignment assignment)
		{
			this.Print("{0} = ", new object[] { assignment.Member.Name });
			this.Visit(assignment.Expression);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000058C0 File Offset: 0x00003AC0
		protected override void VisitMemberMemberBinding(MemberMemberBinding binding)
		{
			this.Print(binding.Member.Name);
			this.Print(" = {");
			this.VisitList<MemberBinding>(binding.Bindings, new Action<MemberBinding>(this.VisitBinding));
			this.Print("}");
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005910 File Offset: 0x00003B10
		protected override void VisitMemberListBinding(MemberListBinding binding)
		{
			this.Print(binding.Member.Name);
			this.Print(" = {");
			this.VisitList<ElementInit>(binding.Initializers, new Action<ElementInit>(this.VisitElementInitializer));
			this.Print("}");
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005960 File Offset: 0x00003B60
		protected override void VisitList<T>(ReadOnlyCollection<T> list, Action<T> visitor)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (i > 0)
				{
					this.Print(", ");
				}
				visitor(list[i]);
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000059A4 File Offset: 0x00003BA4
		protected override void VisitLambda(LambdaExpression lambda)
		{
			if (lambda.Parameters.Count != 1)
			{
				this.Print("(");
				this.VisitList<ParameterExpression>(lambda.Parameters, new Action<ParameterExpression>(this.Visit));
				this.Print(")");
			}
			else
			{
				this.Visit(lambda.Parameters[0]);
			}
			this.Print(" => ");
			this.Visit(lambda.Body);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005A20 File Offset: 0x00003C20
		protected override void VisitNew(NewExpression nex)
		{
			this.Print("new {0}(", new object[] { nex.Type.Name });
			if (nex.Members != null && nex.Members.Count > 0)
			{
				for (int i = 0; i < nex.Members.Count; i++)
				{
					if (i > 0)
					{
						this.Print(", ");
					}
					this.Print("{0} = ", new object[] { nex.Members[i].Name });
					this.Visit(nex.Arguments[i]);
				}
			}
			else
			{
				this.VisitExpressionList(nex.Arguments);
			}
			this.Print(")");
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005AEC File Offset: 0x00003CEC
		protected override void VisitMemberInit(MemberInitExpression init)
		{
			this.Visit(init.NewExpression);
			this.Print(" {");
			this.VisitList<MemberBinding>(init.Bindings, new Action<MemberBinding>(this.VisitBinding));
			this.Print("}");
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005B2C File Offset: 0x00003D2C
		protected override void VisitListInit(ListInitExpression init)
		{
			this.Visit(init.NewExpression);
			this.Print(" {");
			this.VisitList<ElementInit>(init.Initializers, new Action<ElementInit>(this.VisitElementInitializer));
			this.Print("}");
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005B6C File Offset: 0x00003D6C
		protected override void VisitNewArray(NewArrayExpression newArray)
		{
			this.Print("new ");
			ExpressionType nodeType = newArray.NodeType;
			if (nodeType == ExpressionType.NewArrayInit)
			{
				this.Print("[] {");
				this.VisitExpressionList(newArray.Expressions);
				this.Print("}");
				return;
			}
			if (nodeType != ExpressionType.NewArrayBounds)
			{
				throw new NotSupportedException();
			}
			this.Print(newArray.Type);
			this.Print("(");
			this.VisitExpressionList(newArray.Expressions);
			this.Print(")");
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005BF8 File Offset: 0x00003DF8
		protected override void VisitInvocation(InvocationExpression invocation)
		{
			this.Print("Invoke(");
			this.Visit(invocation.Expression);
			if (invocation.Arguments.Count != 0)
			{
				this.Print(", ");
				this.VisitExpressionList(invocation.Arguments);
			}
			this.Print(")");
		}

		// Token: 0x040000E2 RID: 226
		private const string ListSeparator = ", ";

		// Token: 0x040000E3 RID: 227
		private StringBuilder builder;
	}
}
