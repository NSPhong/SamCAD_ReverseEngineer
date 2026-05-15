using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using SamSoarII.Core.Files;
using SamSoarII.Polyline.Arguments;
using SamSoarII.Polyline.Entity;
using SamSoarII.Polyline.Entity.User;
using SamSoarII.Shell;

namespace SamSoarII.Polyline;

public interface IPolylineImage : INotifyPropertyChanged, IDisposable, IGridPenningSourceEX, IGridPenningSource
{
	string Name { get; set; }

	double Left { get; set; }

	double Top { get; set; }

	double Width { get; set; }

	double Height { get; set; }

	Point StartPoint { get; set; }

	IList<IPolylineEntity> Items { get; }

	IList<IPolylineGroup> Groups { get; }

	IList<IPolylineUserFormat> UserFmts { get; }

	IList<IPolylineAction> Undos { get; }

	IList<IPolylineAction> Redos { get; }

	ImageArgumentTypes ArgumentType { get; set; }

	IImageArgument Argument { get; set; }

	IEnumerable<IPolylineEntity> SelectedEntities { get; }

	IEnumerable<IPolylineGroup> SelectedGroups { get; }

	IPolylineEntity SelectedEntity { get; }

	int SelectedStart { get; }

	int SelectedCount { get; }

	void Select(int _selectedstart, int _selectedindex);

	void SelectUpdate(IPolylineEntity item);

	void Save(PolylineImageHeader header);

	void Load(PolylineImageHeader header);

	void Save(DownloadWriter dw);

	void Load(UploadReader ur);

	IPolylineAction Insert(int index, IPolylineEntity entity);

	IPolylineAction Insert(int index, IEnumerable<IPolylineEntity> entities);

	IPolylineAction Remove(IPolylineEntity entity);

	IPolylineAction Remove(IEnumerable<IPolylineEntity> entities);

	IPolylineAction Remove(int index);

	IPolylineAction Remove(int index, int count);

	IPolylineAction Replace(int index, int count, IEnumerable<IPolylineEntity> entities);

	void ActionBegin(IPolylineEntity _nowitem, IPolylineEntity _nexitem);

	IPolylineAction ActionEnd(IPolylineEntity _nowitem, IPolylineEntity _nexitem);

	void ActionEscape(IPolylineEntity _nowitem, IPolylineEntity _nexitem);

	bool CanUndo();

	bool CanRedo();

	IPolylineAction Undo();

	IPolylineAction Redo();

	IPolylineAction Copy(IEnumerable<IPolylineEntity> entities);

	IPolylineAction Cut(IEnumerable<IPolylineEntity> entities);

	IPolylineAction Paste(int index);

	void GroupUpdate();

	void GroupResize(IPolylineGroup group, int delta);

	IPolylineAction GroupMerge(IList<IPolylineGroup> groups);

	IPolylineAction GroupSplit(IList<IPolylineEntity> reals);

	IPolylineAction GroupMove(IList<IPolylineEntity> moves, Vector v);

	IPolylineAction GroupReverse(IList<IPolylineGroup> groups);

	IPolylineAction GroupMirror(IList<IPolylineGroup> groups, Point s, Vector v);

	IPolylineAction GroupRotate(IList<IPolylineGroup> groups, Point s, double a);

	IPolylineAction GroupScale(IList<IPolylineGroup> groups, Point s, double xs, double ys);

	IPolylineAction GroupMatrix(IList<IPolylineGroup> groups, int row, int column, Point offset, MatrixStrategy strategy, MatrixPriority priority);

	IPolylineAction GroupExpand(IList<IPolylineGroup> groups, double r, bool removeold);

	IPolylineAction GroupFill(IList<IPolylineGroup> groups, double r, FillStrategy strategy, bool removeold);

	void ArchToLine();

	IPolylineAction ReorderSpecific(IList<IPolylineReorderingGroup> regroups);

	IPolylineAction ReorderIntelligence(ReorderingStrategy strategy);

	IPolylineAction EntityBreak(IPolylineEntity entity, Point b);

	IPolylineArch EntityRoundDemo(IPolylineEntity entity, double r);

	IPolylineAction EntityRound(IPolylineEntity entity, double r);

	IPolylineLine EntityBevelDemo(IPolylineEntity entity, double r);

	IPolylineAction EntityBevel(IPolylineEntity entity, double r);

	IPolylineCorner EntitySharpDemo(IPolylineEntity entity);

	IPolylineAction EntitySharp(IPolylineEntity entity);

	void Rect4To(IPolylineLine topleft, IPolylineLine topright, IPolylineLine bottomright, IPolylineLine bottomleft, Rect rect);

	bool IsEqualP(Point p1, Point p2);

	bool IsEqualX(double x1, double x2);

	bool IsEqualY(double y1, double y2);

	void RepairAll();

	void RepairFor(IPolylineEntity entity);

	void UserStart();

	void UserYes(IList<IPolylineUserFormat> _users);

	void UserNo();
}
