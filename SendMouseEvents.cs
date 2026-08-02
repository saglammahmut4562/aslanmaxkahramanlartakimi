using System;

namespace UnityEngine
{
	// Token: 0x020000D3 RID: 211
	internal class SendMouseEvents
	{
		// Token: 0x060007B2 RID: 1970 RVA: 0x00012258 File Offset: 0x00010458
		[NotRenamed]
		private static void DoSendMouseEvents(int mouseUsed, int skipRTCameras)
		{
			Vector3 mousePosition = Input.mousePosition;
			int allCamerasCount = Camera.allCamerasCount;
			if (SendMouseEvents.m_Cameras == null || SendMouseEvents.m_Cameras.Length != allCamerasCount)
			{
				SendMouseEvents.m_Cameras = new Camera[allCamerasCount];
			}
			Camera.GetAllCameras(SendMouseEvents.m_Cameras);
			if (mouseUsed == 0)
			{
				foreach (Camera camera in SendMouseEvents.m_Cameras)
				{
					if (skipRTCameras == 0 || !(camera.targetTexture != null))
					{
						if (camera.pixelRect.Contains(mousePosition))
						{
							GUILayer guilayer = (GUILayer)camera.GetComponent(typeof(GUILayer));
							if (guilayer)
							{
								GUIElement guielement = guilayer.HitTest(mousePosition);
								if (guielement)
								{
									SendMouseEvents.m_CurrentHit[0].target = guielement.gameObject;
									SendMouseEvents.m_CurrentHit[0].camera = camera;
								}
								else
								{
									SendMouseEvents.m_CurrentHit[0].target = null;
									SendMouseEvents.m_CurrentHit[0].camera = null;
								}
							}
							if (camera.eventMask != 0)
							{
								RaycastHit raycastHit;
								if (camera.farClipPlane > 0f && Physics.Raycast(camera.ScreenPointToRay(mousePosition), out raycastHit, camera.farClipPlane, camera.cullingMask & camera.eventMask & -5))
								{
									if (raycastHit.rigidbody)
									{
										SendMouseEvents.m_CurrentHit[1].target = raycastHit.rigidbody.gameObject;
										SendMouseEvents.m_CurrentHit[1].camera = camera;
									}
									else
									{
										SendMouseEvents.m_CurrentHit[1].target = raycastHit.collider.gameObject;
										SendMouseEvents.m_CurrentHit[1].camera = camera;
									}
								}
								else if (camera.farClipPlane > 0f && Physics2D.GetRayIntersectionNonAlloc(camera.ScreenPointToRay(mousePosition), SendMouseEvents.m_MouseRayHits2D, camera.farClipPlane, camera.cullingMask & camera.eventMask & -5) == 1)
								{
									SendMouseEvents.m_CurrentHit[1].camera = camera;
									if (SendMouseEvents.m_MouseRayHits2D[0].rigidbody)
									{
										SendMouseEvents.m_CurrentHit[1].target = SendMouseEvents.m_MouseRayHits2D[0].rigidbody.gameObject;
									}
									else
									{
										SendMouseEvents.m_CurrentHit[1].target = SendMouseEvents.m_MouseRayHits2D[0].collider.gameObject;
									}
								}
								else if (camera.clearFlags == CameraClearFlags.Skybox || camera.clearFlags == CameraClearFlags.Color)
								{
									SendMouseEvents.m_CurrentHit[1].target = null;
									SendMouseEvents.m_CurrentHit[1].camera = null;
								}
							}
						}
					}
				}
			}
			for (int j = 0; j < 2; j++)
			{
				SendMouseEvents.SendEvents(j, SendMouseEvents.m_CurrentHit[j]);
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00012568 File Offset: 0x00010768
		private static void SendEvents(int i, SendMouseEvents.HitInfo hit)
		{
			bool mouseButtonDown = Input.GetMouseButtonDown(0);
			bool mouseButton = Input.GetMouseButton(0);
			if (mouseButtonDown)
			{
				if (hit)
				{
					SendMouseEvents.m_MouseDownHit[i] = hit;
					SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseDown");
				}
			}
			else if (!mouseButton)
			{
				if (SendMouseEvents.m_MouseDownHit[i])
				{
					if (SendMouseEvents.HitInfo.Compare(hit, SendMouseEvents.m_MouseDownHit[i]))
					{
						SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseUpAsButton");
					}
					SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseUp");
					SendMouseEvents.m_MouseDownHit[i] = default(SendMouseEvents.HitInfo);
				}
			}
			else if (SendMouseEvents.m_MouseDownHit[i])
			{
				SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseDrag");
			}
			if (SendMouseEvents.HitInfo.Compare(hit, SendMouseEvents.m_LastHit[i]))
			{
				if (hit)
				{
					hit.SendMessage("OnMouseOver");
				}
			}
			else
			{
				if (SendMouseEvents.m_LastHit[i])
				{
					SendMouseEvents.m_LastHit[i].SendMessage("OnMouseExit");
				}
				if (hit)
				{
					hit.SendMessage("OnMouseEnter");
					hit.SendMessage("OnMouseOver");
				}
			}
			SendMouseEvents.m_LastHit[i] = hit;
		}

		// Token: 0x0400035B RID: 859
		private static SendMouseEvents.HitInfo[] m_LastHit = new SendMouseEvents.HitInfo[]
		{
			default(SendMouseEvents.HitInfo),
			default(SendMouseEvents.HitInfo)
		};

		// Token: 0x0400035C RID: 860
		private static SendMouseEvents.HitInfo[] m_MouseDownHit = new SendMouseEvents.HitInfo[]
		{
			default(SendMouseEvents.HitInfo),
			default(SendMouseEvents.HitInfo)
		};

		// Token: 0x0400035D RID: 861
		private static RaycastHit2D[] m_MouseRayHits2D = new RaycastHit2D[] { default(RaycastHit2D) };

		// Token: 0x0400035E RID: 862
		private static SendMouseEvents.HitInfo[] m_CurrentHit = new SendMouseEvents.HitInfo[]
		{
			default(SendMouseEvents.HitInfo),
			default(SendMouseEvents.HitInfo)
		};

		// Token: 0x0400035F RID: 863
		private static Camera[] m_Cameras;

		// Token: 0x020000D4 RID: 212
		private struct HitInfo
		{
			// Token: 0x060007B4 RID: 1972 RVA: 0x0001270C File Offset: 0x0001090C
			public void SendMessage(string name)
			{
				this.target.SendMessage(name, null, SendMessageOptions.DontRequireReceiver);
			}

			// Token: 0x060007B5 RID: 1973 RVA: 0x0001271C File Offset: 0x0001091C
			public static bool Compare(SendMouseEvents.HitInfo lhs, SendMouseEvents.HitInfo rhs)
			{
				return lhs.target == rhs.target && lhs.camera == rhs.camera;
			}

			// Token: 0x060007B6 RID: 1974 RVA: 0x0001274C File Offset: 0x0001094C
			public static implicit operator bool(SendMouseEvents.HitInfo exists)
			{
				return exists.target != null && exists.camera != null;
			}

			// Token: 0x04000360 RID: 864
			public GameObject target;

			// Token: 0x04000361 RID: 865
			public Camera camera;
		}
	}
}
