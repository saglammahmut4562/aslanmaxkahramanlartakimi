using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000A9 RID: 169
	public sealed class ParticleSystem : Component
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060006E3 RID: 1763
		public extern int particleCount
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700015B RID: 347
		// (set) Token: 0x060006E4 RID: 1764
		public extern bool enableEmission
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060006E5 RID: 1765
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetParticles(ParticleSystem.Particle[] particles, int size);

		// Token: 0x060006E6 RID: 1766
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern int GetParticles(ParticleSystem.Particle[] particles);

		// Token: 0x060006E7 RID: 1767
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Internal_Play();

		// Token: 0x060006E8 RID: 1768
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern bool Internal_IsAlive();

		// Token: 0x060006E9 RID: 1769 RVA: 0x000112C0 File Offset: 0x0000F4C0
		[ExcludeFromDocs]
		public void Play()
		{
			bool flag = true;
			this.Play(flag);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x000112D8 File Offset: 0x0000F4D8
		public void Play([DefaultValue("true")] bool withChildren)
		{
			if (withChildren)
			{
				ParticleSystem[] particleSystems = ParticleSystem.GetParticleSystems(this);
				foreach (ParticleSystem particleSystem in particleSystems)
				{
					particleSystem.Internal_Play();
				}
			}
			else
			{
				this.Internal_Play();
			}
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00011320 File Offset: 0x0000F520
		public bool IsAlive([DefaultValue("true")] bool withChildren)
		{
			if (withChildren)
			{
				ParticleSystem[] particleSystems = ParticleSystem.GetParticleSystems(this);
				foreach (ParticleSystem particleSystem in particleSystems)
				{
					if (particleSystem.Internal_IsAlive())
					{
						return true;
					}
				}
				return false;
			}
			return this.Internal_IsAlive();
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001136C File Offset: 0x0000F56C
		internal static ParticleSystem[] GetParticleSystems(ParticleSystem root)
		{
			if (!root)
			{
				return null;
			}
			List<ParticleSystem> list = new List<ParticleSystem>();
			list.Add(root);
			ParticleSystem.GetDirectParticleSystemChildrenRecursive(root.transform, list);
			return list.ToArray();
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x000113A8 File Offset: 0x0000F5A8
		private static void GetDirectParticleSystemChildrenRecursive(Transform transform, List<ParticleSystem> particleSystems)
		{
			foreach (object obj in transform)
			{
				Transform transform2 = (Transform)obj;
				ParticleSystem component = transform2.gameObject.GetComponent<ParticleSystem>();
				if (component != null)
				{
					particleSystems.Add(component);
					ParticleSystem.GetDirectParticleSystemChildrenRecursive(transform2, particleSystems);
				}
			}
		}

		// Token: 0x020000AA RID: 170
		public struct Particle
		{
			// Token: 0x1700015C RID: 348
			// (set) Token: 0x060006EE RID: 1774 RVA: 0x00011428 File Offset: 0x0000F628
			public Vector3 position
			{
				set
				{
					this.m_Position = value;
				}
			}

			// Token: 0x1700015D RID: 349
			// (set) Token: 0x060006EF RID: 1775 RVA: 0x00011434 File Offset: 0x0000F634
			public float lifetime
			{
				set
				{
					this.m_Lifetime = value;
				}
			}

			// Token: 0x1700015E RID: 350
			// (set) Token: 0x060006F0 RID: 1776 RVA: 0x00011440 File Offset: 0x0000F640
			public float startLifetime
			{
				set
				{
					this.m_StartLifetime = value;
				}
			}

			// Token: 0x1700015F RID: 351
			// (set) Token: 0x060006F1 RID: 1777 RVA: 0x0001144C File Offset: 0x0000F64C
			public float size
			{
				set
				{
					this.m_Size = value;
				}
			}

			// Token: 0x17000160 RID: 352
			// (set) Token: 0x060006F2 RID: 1778 RVA: 0x00011458 File Offset: 0x0000F658
			public Color32 color
			{
				set
				{
					this.m_Color = value;
				}
			}

			// Token: 0x040002D1 RID: 721
			private Vector3 m_Position;

			// Token: 0x040002D2 RID: 722
			private Vector3 m_Velocity;

			// Token: 0x040002D3 RID: 723
			private Vector3 m_AnimatedVelocity;

			// Token: 0x040002D4 RID: 724
			private Vector3 m_AxisOfRotation;

			// Token: 0x040002D5 RID: 725
			private float m_Rotation;

			// Token: 0x040002D6 RID: 726
			private float m_AngularVelocity;

			// Token: 0x040002D7 RID: 727
			private float m_Size;

			// Token: 0x040002D8 RID: 728
			private Color32 m_Color;

			// Token: 0x040002D9 RID: 729
			private uint m_RandomSeed;

			// Token: 0x040002DA RID: 730
			private float m_Lifetime;

			// Token: 0x040002DB RID: 731
			private float m_StartLifetime;

			// Token: 0x040002DC RID: 732
			private float m_EmitAccumulator0;

			// Token: 0x040002DD RID: 733
			private float m_EmitAccumulator1;
		}
	}
}
