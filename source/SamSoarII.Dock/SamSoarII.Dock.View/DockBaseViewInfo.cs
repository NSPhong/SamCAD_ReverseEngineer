using System;
using System.Collections.Generic;
using System.Xml.Linq;
using SamSoarII.Dock.Interface;

namespace SamSoarII.Dock.View;

internal class DockBaseViewInfo : IDockBaseViewInfo
{
	private DockManager parent;

	private int dockid;

	private double top;

	private double left;

	private double width;

	private double height;

	private int targetid;

	private ViewCommon.BaseViewRelations relation;

	private ViewCommon.BaseViewRelations lastside;

	private List<int> children;

	private IDockContent content;

	private IDockBaseView baseview;

	public DockManager Parent => parent;

	public int DockID => dockid;

	public double Top => top;

	public double Left => left;

	public double Width => width;

	public double Height => height;

	public int TargetID => targetid;

	public ViewCommon.BaseViewRelations Relation => relation;

	public ViewCommon.BaseViewRelations LastSide => lastside;

	public IList<int> Children => children;

	public IDockContent Content => content;

	public IDockBaseView BaseView => baseview;

	public DockBaseViewInfo(DockManager _parent, int _dockid)
	{
		parent = _parent;
		dockid = _dockid;
		top = 100.0;
		left = 100.0;
		width = 300.0;
		height = 300.0;
		targetid = -1;
		relation = ViewCommon.BaseViewRelations.Null;
		lastside = ViewCommon.BaseViewRelations.LeftSide;
		children = new List<int>();
	}

	public void Register(IDockContent _content, IDockBaseView _baseview)
	{
		content = _content;
		baseview = _baseview;
	}

	public void Unregister()
	{
		content = null;
		baseview = null;
	}

	public void SetSize(double _width, double _height)
	{
		SetRect(left, top, _width, _height);
	}

	public void SetRect(double _left, double _top, double _width, double _height)
	{
		if (!double.IsNaN(_top))
		{
			top = _top;
		}
		if (!double.IsNaN(_left))
		{
			left = _left;
		}
		if (!double.IsNaN(_width))
		{
			width = _width;
		}
		if (!double.IsNaN(_height))
		{
			height = _height;
		}
	}

	public void SetAttach(int _targetid, ViewCommon.BaseViewRelations _relation)
	{
		targetid = _targetid;
		relation = _relation;
		ViewCommon.BaseViewRelations baseViewRelations = relation;
		ViewCommon.BaseViewRelations baseViewRelations2 = baseViewRelations;
		if ((uint)(baseViewRelations2 - 6) <= 3u)
		{
			lastside = relation;
		}
	}

	public void Save(XElement xele)
	{
		xele.SetAttributeValue("DockID", dockid);
		xele.SetAttributeValue("Title", (content != null) ? content.Header : "");
		xele.SetAttributeValue("Top", top);
		xele.SetAttributeValue("Left", left);
		xele.SetAttributeValue("Width", width);
		xele.SetAttributeValue("Height", height);
		xele.SetAttributeValue("Target", targetid);
		xele.SetAttributeValue("Relation", Enum.GetName(typeof(ViewCommon.BaseViewRelations), relation));
		xele.SetAttributeValue("LastSide", Enum.GetName(typeof(ViewCommon.BaseViewRelations), lastside));
	}

	public void Load(XElement xele)
	{
		dockid = int.Parse(xele.Attribute("DockID").Value);
		top = double.Parse(xele.Attribute("Top").Value);
		left = double.Parse(xele.Attribute("Left").Value);
		width = double.Parse(xele.Attribute("Width").Value);
		height = double.Parse(xele.Attribute("Height").Value);
		targetid = int.Parse(xele.Attribute("Target").Value);
		relation = (ViewCommon.BaseViewRelations)Enum.Parse(typeof(ViewCommon.BaseViewRelations), xele.Attribute("Relation").Value);
		lastside = (ViewCommon.BaseViewRelations)Enum.Parse(typeof(ViewCommon.BaseViewRelations), xele.Attribute("LastSide").Value);
	}
}
