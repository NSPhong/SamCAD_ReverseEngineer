using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using SamSoarII.Core.Files;
using SamSoarII.Polyline.Arguments;
using SamSoarII.Polyline.Entity.User;
using SamSoarII.Polyline.Expands;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public interface IPolylineEntity : INotifyPropertyChanged, IDisposable, IGridPenningEntity
{
	PolylineType Type { get; }

	string Name { get; set; }

	IPolylineImage Parent { get; }

	IPolylineGroup Group { get; set; }

	ImageArgumentTypes ArgumentType { get; set; }

	IPolylineArgument Argument { get; set; }

	IPolylineEntity Prev { get; }

	IPolylineEntity Next { get; }

	int ID { get; set; }

	Point From { get; set; }

	Point To { get; set; }

	bool IsReal { get; set; }

	bool IsMouseOver { get; set; }

	bool IsSelected { get; set; }

	bool IsSpecial { get; }

	bool IsSlot { get; set; }

	Rect Bounding { get; }

	Vector Tangent { get; }

	Vector TangentBack { get; }

	IEnumerable<IPolylineControlPoint> ControlPoints { get; }

	IEnumerable<IGridPenningEntity> Pennings { get; }

	IList<IPolylineUserObject> UserObjs { get; }

	bool HasRealName();

	LocatedFileHeader AllocHeader();

	void Save(PolylineEntityHeader header);

	void Load(PolylineEntityHeader header);

	void Save(DownloadWriter dw);

	void Load(UploadReader ur);

	void Save(StringBuilder sb);

	void Load(string text);

	IPolylineEntity Clone();

	IPolylineEntity Move(Vector v);

	IPolylineEntity Reverse();

	IPolylineEntity Mirror(Point p, Vector v);

	IPolylineEntity Rotate(Point s, double a);

	IPolylineEntity Scale(Point s, double xs, double ys);

	IPolylineExpand Expand(double r);

	IPolylineExpand ExpandPoint(double r);

	Vector? LawerFrom(double r);

	Vector? LawerTo(double r);

	Vector? HorizonFrom();

	Vector? HorizonTo();

	void Repair();

	void Load(IPolylineEntity that);

	double GetDist(Point p);

	double GetLength();

	double GetLength(Point p);

	Point ReflectInline(Point p, bool xlock, bool ylock);

	void RefreshUserObjs();
}
