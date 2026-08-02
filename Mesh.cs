using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000099 RID: 153
	public sealed class Mesh : Object
	{
		// Token: 0x0600067B RID: 1659 RVA: 0x000110C4 File Offset: 0x0000F2C4
		public Mesh()
		{
			Mesh.Internal_Create(this);
		}

		// Token: 0x0600067C RID: 1660
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] Mesh mono);

		// Token: 0x0600067D RID: 1661
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void Clear([DefaultValue("true")] bool keepVertexLayout);

		// Token: 0x0600067E RID: 1662 RVA: 0x000110D4 File Offset: 0x0000F2D4
		[ExcludeFromDocs]
		public void Clear()
		{
			bool flag = true;
			this.Clear(flag);
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600067F RID: 1663
		public extern bool isReadable
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000680 RID: 1664
		internal extern bool canAccess
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000681 RID: 1665
		// (set) Token: 0x06000682 RID: 1666
		public extern Vector3[] vertices
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000683 RID: 1667
		// (set) Token: 0x06000684 RID: 1668
		public extern Vector3[] normals
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000685 RID: 1669
		// (set) Token: 0x06000686 RID: 1670
		public extern Vector4[] tangents
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000687 RID: 1671
		// (set) Token: 0x06000688 RID: 1672
		public extern Vector2[] uv
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000689 RID: 1673
		// (set) Token: 0x0600068A RID: 1674
		public extern Vector2[] uv2
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x000110EC File Offset: 0x0000F2EC
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x000110F4 File Offset: 0x0000F2F4
		public Vector2[] uv1
		{
			get
			{
				return this.uv2;
			}
			set
			{
				this.uv2 = value;
			}
		}

		// Token: 0x0600068D RID: 1677
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_bounds(out Bounds value);

		// Token: 0x0600068E RID: 1678
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_bounds(ref Bounds value);

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00011100 File Offset: 0x0000F300
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x00011118 File Offset: 0x0000F318
		public Bounds bounds
		{
			get
			{
				Bounds bounds;
				this.INTERNAL_get_bounds(out bounds);
				return bounds;
			}
			set
			{
				this.INTERNAL_set_bounds(ref value);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000691 RID: 1681
		// (set) Token: 0x06000692 RID: 1682
		public extern Color[] colors
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000693 RID: 1683
		// (set) Token: 0x06000694 RID: 1684
		public extern Color32[] colors32
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000695 RID: 1685
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void RecalculateBounds();

		// Token: 0x06000696 RID: 1686
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void RecalculateNormals();

		// Token: 0x06000697 RID: 1687
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void Optimize();

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000698 RID: 1688
		// (set) Token: 0x06000699 RID: 1689
		public extern int[] triangles
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600069A RID: 1690
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern int[] GetTriangles(int submesh);

		// Token: 0x0600069B RID: 1691
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetTriangles(int[] triangles, int submesh);

		// Token: 0x0600069C RID: 1692
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern int[] GetIndices(int submesh);

		// Token: 0x0600069D RID: 1693
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetIndices(int[] indices, MeshTopology topology, int submesh);

		// Token: 0x0600069E RID: 1694
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern MeshTopology GetTopology(int submesh);

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600069F RID: 1695
		public extern int vertexCount
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060006A0 RID: 1696
		// (set) Token: 0x060006A1 RID: 1697
		public extern int subMeshCount
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060006A2 RID: 1698
		[WrapperlessIcall]
		[Obsolete("Use SetTriangles instead. Internally this function will convert the triangle strip to a list of triangles anyway.")]
		[MethodImpl(4096)]
		public extern void SetTriangleStrip(int[] triangles, int submesh);

		// Token: 0x060006A3 RID: 1699
		[WrapperlessIcall]
		[Obsolete("Use GetTriangles instead. Internally this function converts a list of triangles to a strip, so it might be slow, it might be a mess.")]
		[MethodImpl(4096)]
		public extern int[] GetTriangleStrip(int submesh);

		// Token: 0x060006A4 RID: 1700
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void CombineMeshes(CombineInstance[] combine, [DefaultValue("true")] bool mergeSubMeshes, [DefaultValue("true")] bool useMatrices);

		// Token: 0x060006A5 RID: 1701 RVA: 0x00011124 File Offset: 0x0000F324
		[ExcludeFromDocs]
		public void CombineMeshes(CombineInstance[] combine, bool mergeSubMeshes)
		{
			bool flag = true;
			this.CombineMeshes(combine, mergeSubMeshes, flag);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001113C File Offset: 0x0000F33C
		[ExcludeFromDocs]
		public void CombineMeshes(CombineInstance[] combine)
		{
			bool flag = true;
			bool flag2 = true;
			this.CombineMeshes(combine, flag2, flag);
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060006A7 RID: 1703
		// (set) Token: 0x060006A8 RID: 1704
		public extern BoneWeight[] boneWeights
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060006A9 RID: 1705
		// (set) Token: 0x060006AA RID: 1706
		public extern Matrix4x4[] bindposes
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060006AB RID: 1707
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void MarkDynamic();

		// Token: 0x060006AC RID: 1708
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void UploadMeshData(bool markNoLogerReadable);

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060006AD RID: 1709
		public extern int blendShapeCount
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060006AE RID: 1710
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern string GetBlendShapeName(int index);

		// Token: 0x060006AF RID: 1711
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern int GetBlendShapeIndex(string blendShapeName);
	}
}
