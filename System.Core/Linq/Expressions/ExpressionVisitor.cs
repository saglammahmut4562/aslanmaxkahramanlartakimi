using System;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x0200002A RID: 42
	internal abstract class ExpressionVisitor
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00005C58 File Offset: 0x00003E58
		protected virtual void Visit(Expression expression)
		{
			if (expression == null)
			{
				return;
			}
			switch (expression.NodeType)
			{
			case ExpressionType.Add:
			case ExpressionType.AddChecked:
			case ExpressionType.And:
			case ExpressionType.AndAlso:
			case ExpressionType.ArrayIndex:
			case ExpressionType.Coalesce:
			case ExpressionType.Divide:
			case ExpressionType.Equal:
			case ExpressionType.ExclusiveOr:
			case ExpressionType.GreaterThan:
			case ExpressionType.GreaterThanOrEqual:
			case ExpressionType.LeftShift:
			case ExpressionType.LessThan:
			case ExpressionType.LessThanOrEqual:
			case ExpressionType.Modulo:
			case ExpressionType.Multiply:
			case ExpressionType.MultiplyChecked:
			case ExpressionType.NotEqual:
			case ExpressionType.Or:
			case ExpressionType.OrElse:
			case ExpressionType.Power:
			case ExpressionType.RightShift:
			case ExpressionType.Subtract:
			case ExpressionType.SubtractChecked:
				this.VisitBinary((BinaryExpression)expression);
				break;
			case ExpressionType.ArrayLength:
			case ExpressionType.Convert:
			case ExpressionType.ConvertChecked:
			case ExpressionType.Negate:
			case ExpressionType.UnaryPlus:
			case ExpressionType.NegateChecked:
			case ExpressionType.Not:
			case ExpressionType.Quote:
			case ExpressionType.TypeAs:
				this.VisitUnary((UnaryExpression)expression);
				break;
			case ExpressionType.Call:
				this.VisitMethodCall((MethodCallExpression)expression);
				break;
			case ExpressionType.Conditional:
				this.VisitConditional((ConditionalExpression)expression);
				break;
			case ExpressionType.Constant:
				this.VisitConstant((ConstantExpression)expression);
				break;
			case ExpressionType.Invoke:
				this.VisitInvocation((InvocationExpression)expression);
				break;
			case ExpressionType.Lambda:
				this.VisitLambda((LambdaExpression)expression);
				break;
			case ExpressionType.ListInit:
				this.VisitListInit((ListInitExpression)expression);
				break;
			case ExpressionType.MemberAccess:
				this.VisitMemberAccess((MemberExpression)expression);
				break;
			case ExpressionType.MemberInit:
				this.VisitMemberInit((MemberInitExpression)expression);
				break;
			case ExpressionType.New:
				this.VisitNew((NewExpression)expression);
				break;
			case ExpressionType.NewArrayInit:
			case ExpressionType.NewArrayBounds:
				this.VisitNewArray((NewArrayExpression)expression);
				break;
			case ExpressionType.Parameter:
				this.VisitParameter((ParameterExpression)expression);
				break;
			case ExpressionType.TypeIs:
				this.VisitTypeIs((TypeBinaryExpression)expression);
				break;
			default:
				throw new ArgumentException(string.Format("Unhandled expression type: '{0}'", expression.NodeType));
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005E40 File Offset: 0x00004040
		protected virtual void VisitBinding(MemberBinding binding)
		{
			switch (binding.BindingType)
			{
			case MemberBindingType.Assignment:
				this.VisitMemberAssignment((MemberAssignment)binding);
				break;
			case MemberBindingType.MemberBinding:
				this.VisitMemberMemberBinding((MemberMemberBinding)binding);
				break;
			case MemberBindingType.ListBinding:
				this.VisitMemberListBinding((MemberListBinding)binding);
				break;
			default:
				throw new ArgumentException(string.Format("Unhandled binding type '{0}'", binding.BindingType));
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005EBC File Offset: 0x000040BC
		protected virtual void VisitElementInitializer(ElementInit initializer)
		{
			this.VisitExpressionList(initializer.Arguments);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005ECC File Offset: 0x000040CC
		protected virtual void VisitUnary(UnaryExpression unary)
		{
			this.Visit(unary.Operand);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005EDC File Offset: 0x000040DC
		protected virtual void VisitBinary(BinaryExpression binary)
		{
			this.Visit(binary.Left);
			this.Visit(binary.Right);
			this.Visit(binary.Conversion);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005F04 File Offset: 0x00004104
		protected virtual void VisitTypeIs(TypeBinaryExpression type)
		{
			this.Visit(type.Expression);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005F14 File Offset: 0x00004114
		protected virtual void VisitConstant(ConstantExpression constant)
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005F18 File Offset: 0x00004118
		protected virtual void VisitConditional(ConditionalExpression conditional)
		{
			this.Visit(conditional.Test);
			this.Visit(conditional.IfTrue);
			this.Visit(conditional.IfFalse);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005F40 File Offset: 0x00004140
		protected virtual void VisitParameter(ParameterExpression parameter)
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005F44 File Offset: 0x00004144
		protected virtual void VisitMemberAccess(MemberExpression member)
		{
			this.Visit(member.Expression);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005F54 File Offset: 0x00004154
		protected virtual void VisitMethodCall(MethodCallExpression methodCall)
		{
			this.Visit(methodCall.Object);
			this.VisitExpressionList(methodCall.Arguments);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005F70 File Offset: 0x00004170
		protected virtual void VisitList<T>(ReadOnlyCollection<T> list, Action<T> visitor)
		{
			foreach (T t in list)
			{
				visitor(t);
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005FC4 File Offset: 0x000041C4
		protected virtual void VisitExpressionList(ReadOnlyCollection<Expression> list)
		{
			this.VisitList<Expression>(list, new Action<Expression>(this.Visit));
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005FDC File Offset: 0x000041DC
		protected virtual void VisitMemberAssignment(MemberAssignment assignment)
		{
			this.Visit(assignment.Expression);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005FEC File Offset: 0x000041EC
		protected virtual void VisitMemberMemberBinding(MemberMemberBinding binding)
		{
			this.VisitBindingList(binding.Bindings);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005FFC File Offset: 0x000041FC
		protected virtual void VisitMemberListBinding(MemberListBinding binding)
		{
			this.VisitElementInitializerList(binding.Initializers);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000600C File Offset: 0x0000420C
		protected virtual void VisitBindingList(ReadOnlyCollection<MemberBinding> list)
		{
			this.VisitList<MemberBinding>(list, new Action<MemberBinding>(this.VisitBinding));
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006024 File Offset: 0x00004224
		protected virtual void VisitElementInitializerList(ReadOnlyCollection<ElementInit> list)
		{
			this.VisitList<ElementInit>(list, new Action<ElementInit>(this.VisitElementInitializer));
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000603C File Offset: 0x0000423C
		protected virtual void VisitLambda(LambdaExpression lambda)
		{
			this.Visit(lambda.Body);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000604C File Offset: 0x0000424C
		protected virtual void VisitNew(NewExpression nex)
		{
			this.VisitExpressionList(nex.Arguments);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000605C File Offset: 0x0000425C
		protected virtual void VisitMemberInit(MemberInitExpression init)
		{
			this.VisitNew(init.NewExpression);
			this.VisitBindingList(init.Bindings);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00006078 File Offset: 0x00004278
		protected virtual void VisitListInit(ListInitExpression init)
		{
			this.VisitNew(init.NewExpression);
			this.VisitElementInitializerList(init.Initializers);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00006094 File Offset: 0x00004294
		protected virtual void VisitNewArray(NewArrayExpression newArray)
		{
			this.VisitExpressionList(newArray.Expressions);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000060A4 File Offset: 0x000042A4
		protected virtual void VisitInvocation(InvocationExpression invocation)
		{
			this.VisitExpressionList(invocation.Arguments);
			this.Visit(invocation.Expression);
		}
	}
}
