﻿/*
 * MetroSet UI - MetroSet UI Framework
 *
 * The MIT License (MIT)
 * Copyright (c) 2017 Narwin, https://github.com/N-a-r-w-i-n
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in the
 * Software without restriction, including without limitation the rights to use, copy,
 * modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
 * and to permit persons to whom the Software is furnished to do so, subject to the
 * following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */


using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using MetroSet_UI.Child;
using MetroSet_UI.Controls;
using MetroSet_UI.Native;

namespace MetroSet_UI.Design
{
	/// <summary>
	/// The below class designer is a part from : https://www.codeproject.com/Articles/38014/KRBTabControl after a few clean code clean up.
	/// </summary>
	public class MetroSetTabControlDesigner : ParentControlDesigner
	{
		#region Instance Members

		private DesignerVerbCollection _verbs;
		private IDesignerHost _designerHost;
		private IComponentChangeService _changeService;

		#endregion Instance Members

		#region Constructor

		#endregion Constructor

		#region Property

		public override DesignerVerbCollection Verbs
		{
			get
			{
				if (_verbs == null)
				{
					var addVerbs = new[]
					{
						new DesignerVerb("Add Tab", OnAddTab),
						new DesignerVerb("Remove Tab", OnRemoveTab)
					};

					_verbs = new DesignerVerbCollection();
					_verbs.AddRange(addVerbs);

					if (!(Control is MetroSetTabControl parentControl))
						return _verbs;
					switch (parentControl.TabPages.Count)
					{
						case 0:
							_verbs[1].Enabled = false;
							break;

						default:
							_verbs[1].Enabled = true;
							break;
					}
				}

				return _verbs;
			}
		}

		#endregion Property

		#region Override Methods

		public override void Initialize(IComponent component)
		{
			base.Initialize(component);

			_designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));
			_changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));

			// Update your designer verb whenever ComponentChanged event occurs.
			if (_changeService != null)
				_changeService.ComponentChanged += OnComponentChanged;
		}

		/// <summary>
		/// Override this method to remove unused or inappropriate properties.
		/// </summary>
		/// <param name="properties">Properties collection of the control.</param>
		protected override void PostFilterProperties(IDictionary properties)
		{
			properties.Remove("Margin");
			properties.Remove("ImeMode");
			properties.Remove("Padding");
			properties.Remove("Enabled");
			properties.Remove("RightToLeft");
			properties.Remove("RightToLeftLayout");
			properties.Remove("ApplicationSettings");
			properties.Remove("DataBindings");

			base.PostFilterProperties(properties);
		}

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if (m.Msg == (int)User32.Msgs.WM_NCHITTEST)
			{
				if (m.Result.ToInt32() == User32._HT_TRANSPARENT)
					m.Result = (IntPtr)User32._HTCLIENT;
			}
		}

		protected override bool GetHitTest(Point point)
		{
			var selectionService = (ISelectionService)GetService(typeof(ISelectionService));
			var selectedObject = selectionService?.PrimarySelection;
			if (selectedObject != null && selectedObject.Equals(Control))
			{
				var p = Control.PointToClient(point);

				var hti = new User32.TCHITTESTINFO(p, User32.TabControlHitTest.TCHT_ONITEM);

				var m = new Message
				{
					HWnd = Control.Handle,
					Msg = User32._TCM_HITTEST
				};

				var lParam = Marshal.AllocHGlobal(Marshal.SizeOf(hti));
				Marshal.StructureToPtr(hti, lParam, false);
				m.LParam = lParam;

				base.WndProc(ref m);
				Marshal.FreeHGlobal(lParam);

				if (m.Result.ToInt32() != -1)
					return hti.flags != User32.TabControlHitTest.TCHT_NOWHERE;
			}

			return false;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && _changeService != null)
				_changeService.ComponentChanged -= OnComponentChanged;

			base.Dispose(disposing);
		}

		#endregion Override Methods

		#region Helper Methods

		/*  When the designer modifies the MetroSetTabControl.TabPages collection,
                the Properties window is not updated until the control is deselected and then reselected. To
                correct this defect, you need to explicitly notify the IDE that a change has been made by using
                the PropertyDescriptor for the property. */

		private void OnAddTab(Object sender, EventArgs e)
		{
			var parentControl = Control as MetroSetTabControl;

			var oldTabs = parentControl?.TabPages;

			// Notify the IDE that the TabPages collection property of the current tab control has changed.
			RaiseComponentChanging(TypeDescriptor.GetProperties(parentControl)["TabPages"]);
			var newTab = (MetroSetSetTabPage)_designerHost.CreateComponent(typeof(MetroSetSetTabPage));
			newTab.Text = newTab.Name;
			parentControl?.TabPages.Add(newTab);
			if (parentControl == null)
				return;
			parentControl.SelectedTab = newTab;
			RaiseComponentChanged(TypeDescriptor.GetProperties(parentControl)["TabPages"], oldTabs,
								  parentControl.TabPages);
		}

		private void OnRemoveTab(Object sender, EventArgs e)
		{
			var parentControl = Control as MetroSetTabControl;

			if (parentControl != null && parentControl.SelectedIndex < 0)
				return;

			var oldTabs = parentControl?.TabPages;

			// Notify the IDE that the TabPages collection property of the current tab control has changed.
			RaiseComponentChanging(TypeDescriptor.GetProperties(parentControl)["TabPages"]);
			_designerHost.DestroyComponent(parentControl?.SelectedTab);
			RaiseComponentChanged(TypeDescriptor.GetProperties(parentControl)["TabPages"], oldTabs, parentControl?.TabPages);
		}

		private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			if (!(e.Component is MetroSetTabControl parentControl) || e.Member.Name != "TabPages")
				return;
			foreach (DesignerVerb verb in Verbs)
			{
				if (verb.Text != "Remove Tab")
					continue;
				switch (parentControl.TabPages.Count)
				{
					case 0:
						verb.Enabled = false;
						break;

					default:
						verb.Enabled = true;
						break;
				}

				break;
			}
		}

		#endregion Helper Methods
	}
}