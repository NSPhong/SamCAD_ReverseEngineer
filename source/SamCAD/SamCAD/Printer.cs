using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using SamSoarII.Polyline;
using SamSoarII.Polyline.Entity;
using SamSoarII.Shell;

namespace SamCAD;

public class Printer : IDisposable
{
	private static readonly Pen Pen_Real = new Pen(Color.Black, 1f);

	private static readonly Pen Pen_Virt = new Pen(Color.Black, 1f)
	{
		DashPattern = new float[2] { 4f, 4f }
	};

	private bool isdisposed = false;

	private IPolylineProject project;

	private IPolylineImage image;

	private PrinterStatus status;

	private PrintDocument document;

	private PrinterSettings setting;

	private PageSettings pagesetting;

	private Rectangle pagebounds;

	private Rectangle marginbounds;

	public bool IsDisposed => isdisposed;

	public IPolylineProject Project
	{
		get
		{
			return project;
		}
		set
		{
			project = value;
		}
	}

	public IPolylineImage Image => image;

	public PrinterStatus Status => status;

	public PrintDocument Document => document;

	public PrinterSettings Setting => setting;

	public PageSettings PageSetting => pagesetting;

	public Rectangle PageBounds => pagebounds;

	public Rectangle MarginBounds => marginbounds;

	public Printer()
	{
		project = null;
		image = null;
		status = PrinterStatus.None;
		document = new PrintDocument();
		setting = new PrinterSettings();
		pagesetting = new PageSettings();
		document.PrinterSettings = setting;
		document.DefaultPageSettings = pagesetting;
		document.PrintPage += Document_PrintPage;
	}

	public void Dispose()
	{
		if (!isdisposed)
		{
			isdisposed = true;
			document.Dispose();
			document = null;
			image = null;
			status = PrinterStatus.None;
			setting = null;
			pagesetting = null;
		}
	}

	public void ShowDialog_PageSetup()
	{
		using PageSetupDialog pageSetupDialog = new PageSetupDialog();
		pageSetupDialog.Document = document;
		pageSetupDialog.PrinterSettings = setting;
		pageSetupDialog.PageSettings = pagesetting;
		pageSetupDialog.ShowDialog();
	}

	public void ShowDialog_Print()
	{
		using PrintDialog printDialog = new PrintDialog();
		printDialog.Document = document;
		printDialog.PrinterSettings = setting;
		printDialog.ShowDialog();
	}

	public void ShowDialog_PrintPreview()
	{
		using PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
		printPreviewDialog.Document = document;
		printPreviewDialog.ShowDialog();
	}

	protected PointF GetVisualPosition(System.Windows.Point p)
	{
		PointF result = new PointF
		{
			X = (float)((p.X - image.Left) / image.Width * (double)marginbounds.Width),
			Y = (float)((p.Y - image.Top) / image.Height * (double)marginbounds.Height)
		};
		result.X += marginbounds.Left;
		result.Y += marginbounds.Top;
		return result;
	}

	protected void DrawEntity(Graphics g, IPolylineEntity entity)
	{
		switch (entity.Type)
		{
		case PolylineType.Line:
		{
			IPolylineLine polylineLine = (IPolylineLine)entity;
			PointF visualPosition5 = GetVisualPosition(polylineLine.From);
			PointF visualPosition6 = GetVisualPosition(polylineLine.To);
			Pen pen = (polylineLine.IsReal ? Pen_Real : Pen_Virt);
			g.DrawLine(pen, visualPosition5, visualPosition6);
			break;
		}
		case PolylineType.Arch:
		{
			IPolylineArch polylineArch = (IPolylineArch)entity;
			IGridPenningArch gridPenningArch = (IGridPenningArch)entity;
			PointF visualPosition2 = GetVisualPosition(polylineArch.From);
			PointF visualPosition3 = GetVisualPosition(polylineArch.To);
			PointF visualPosition4 = GetVisualPosition(polylineArch.Center);
			float num3 = (float)(polylineArch.Radius * (double)marginbounds.Width / image.Width);
			float num4 = (float)(polylineArch.Radius * (double)marginbounds.Height / image.Height);
			float num5 = (float)(gridPenningArch.StartAngle / Math.PI * 180.0);
			float num6 = (float)(gridPenningArch.EndAngle / Math.PI * 180.0);
			float num7 = num6 - num5;
			Pen pen_Real2 = Pen_Real;
			if (polylineArch.IsClockwise)
			{
				num7 = 360f - num7;
			}
			for (; num7 < 0f; num7 += 360f)
			{
			}
			while (num7 > 360f)
			{
				num7 -= 360f;
			}
			if (polylineArch.IsClockwise)
			{
				num7 = 0f - num7;
			}
			g.DrawArc(pen_Real2, visualPosition4.X - num3, visualPosition4.Y - num4, num3 * 2f, num4 * 2f, num5, num7);
			break;
		}
		case PolylineType.Circle:
		{
			IPolylineCircle polylineCircle = (IPolylineCircle)entity;
			PointF visualPosition = GetVisualPosition(polylineCircle.Center);
			float num = (float)(polylineCircle.Radius * (double)marginbounds.Width / image.Width);
			float num2 = (float)(polylineCircle.Radius * (double)marginbounds.Height / image.Height);
			Pen pen_Real = Pen_Real;
			g.DrawEllipse(pen_Real, visualPosition.X - num, visualPosition.Y - num2, num * 2f, num2 * 2f);
			break;
		}
		}
	}

	private void Document_PrintPage(object sender, PrintPageEventArgs e)
	{
		Graphics graphics = e.Graphics;
		pagebounds = e.PageBounds;
		marginbounds = e.MarginBounds;
		graphics.Transform = new Matrix(1f, 0f, 0f, -1f, 0f, e.PageBounds.Height);
		if (status == PrinterStatus.None || status == PrinterStatus.Done)
		{
			image = project.Items.FirstOrDefault();
			status = PrinterStatus.PrintImage;
		}
		switch (status)
		{
		case PrinterStatus.PrintImage:
		{
			foreach (IPolylineEntity item in image.Items)
			{
				DrawEntity(graphics, item);
			}
			int num = project.Items.IndexOf(image);
			if (num + 1 >= project.Items.Count)
			{
				image = null;
				status = PrinterStatus.Done;
				e.HasMorePages = false;
			}
			else
			{
				image = project.Items[num + 1];
				status = PrinterStatus.PrintImage;
				e.HasMorePages = true;
			}
			break;
		}
		}
	}
}
