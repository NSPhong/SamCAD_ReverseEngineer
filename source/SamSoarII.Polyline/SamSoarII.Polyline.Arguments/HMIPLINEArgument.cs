using System.ComponentModel;
using System.Text;
using SamSoarII.Core.Files;

namespace SamSoarII.Polyline.Arguments;

public class HMIPLINEArgument : PolylineArgument, IHMIPLINEArgument, IPolylineArgument, INotifyPropertyChanged
{
	private StartupConditions condition;

	private string value;

	private string running;

	private string finallysignal;

	private string xoffset;

	private string yoffset;

	private string velocity;

	private string accelerate;

	private string decelerate;

	private string endsource;

	private string enddestination;

	public StartupConditions Condition
	{
		get
		{
			return condition;
		}
		set
		{
			condition = value;
			InvokePropertyChanged("Condition");
		}
	}

	public int ConditionIndex
	{
		get
		{
			return (int)condition;
		}
		set
		{
			condition = (StartupConditions)value;
			InvokePropertyChanged("ConditionIndex");
		}
	}

	public string Value
	{
		get
		{
			return value;
		}
		set
		{
			this.value = value;
			InvokePropertyChanged("Value");
		}
	}

	public string Running
	{
		get
		{
			return running;
		}
		set
		{
			running = value;
			InvokePropertyChanged("Running");
		}
	}

	public string Finally
	{
		get
		{
			return finallysignal;
		}
		set
		{
			finallysignal = value;
			InvokePropertyChanged("Finally");
		}
	}

	public string XOffset
	{
		get
		{
			return xoffset;
		}
		set
		{
			xoffset = value;
			InvokePropertyChanged("XOffset");
		}
	}

	public string YOffset
	{
		get
		{
			return yoffset;
		}
		set
		{
			yoffset = value;
			InvokePropertyChanged("YOffset");
		}
	}

	public string Velocity
	{
		get
		{
			return velocity;
		}
		set
		{
			velocity = value;
			InvokePropertyChanged("Velocity");
		}
	}

	public string Accelerate
	{
		get
		{
			return accelerate;
		}
		set
		{
			accelerate = value;
			InvokePropertyChanged("Accelerate");
		}
	}

	public string Decelerate
	{
		get
		{
			return decelerate;
		}
		set
		{
			decelerate = value;
			InvokePropertyChanged("Decelerate");
		}
	}

	public string EndSource
	{
		get
		{
			return endsource;
		}
		set
		{
			endsource = value;
			InvokePropertyChanged("EndSource");
		}
	}

	public string EndDestination
	{
		get
		{
			return enddestination;
		}
		set
		{
			enddestination = value;
			InvokePropertyChanged("EndDestination");
		}
	}

	public HMIPLINEArgument()
	{
		condition = StartupConditions.Jog;
		value = "M0";
		running = "M1";
		finallysignal = "M2";
		xoffset = "K0";
		yoffset = "K0";
		velocity = "K0";
		accelerate = "K0";
		decelerate = "K0";
		endsource = "K0";
		enddestination = "D0";
	}

	public override IPolylineArgument Clone()
	{
		HMIPLINEArgument hMIPLINEArgument = new HMIPLINEArgument();
		hMIPLINEArgument.Load(this);
		return hMIPLINEArgument;
	}

	public override void Load(IPolylineArgument that)
	{
		base.Load(that);
		if (that is HMIPLINEArgument)
		{
			HMIPLINEArgument hMIPLINEArgument = (HMIPLINEArgument)that;
			condition = hMIPLINEArgument.condition;
			value = hMIPLINEArgument.value;
			running = hMIPLINEArgument.running;
			finallysignal = hMIPLINEArgument.finallysignal;
			xoffset = hMIPLINEArgument.xoffset;
			yoffset = hMIPLINEArgument.yoffset;
			velocity = hMIPLINEArgument.velocity;
			accelerate = hMIPLINEArgument.accelerate;
			decelerate = hMIPLINEArgument.decelerate;
			endsource = hMIPLINEArgument.endsource;
			enddestination = hMIPLINEArgument.enddestination;
		}
	}

	public override LocatedFileHeader AllocHeader()
	{
		return FileFormat.AllocHeader(FileHeaderTypes.HMIPLINEArgument);
	}

	public override void Save(IFileHeader header)
	{
		base.Save(header);
		if (header is HMIPLINEArgumentHeader)
		{
			HMIPLINEArgumentHeader hMIPLINEArgumentHeader = (HMIPLINEArgumentHeader)header;
			FileFormat.AllocHeaderString(hMIPLINEArgumentHeader, 0, value);
			FileFormat.AllocHeaderString(hMIPLINEArgumentHeader, 1, running);
			FileFormat.AllocHeaderString(hMIPLINEArgumentHeader, 2, finallysignal);
			FileFormat.AllocHeaderString(hMIPLINEArgumentHeader, 3, xoffset);
			FileFormat.AllocHeaderString(hMIPLINEArgumentHeader, 4, yoffset);
			FileFormat.AllocHeaderString(hMIPLINEArgumentHeader, 5, velocity);
			FileFormat.AllocHeaderString(hMIPLINEArgumentHeader, 6, accelerate);
			FileFormat.AllocHeaderString(hMIPLINEArgumentHeader, 7, decelerate);
			FileFormat.AllocHeaderString(hMIPLINEArgumentHeader, 8, endsource);
			FileFormat.AllocHeaderString(hMIPLINEArgumentHeader, 9, enddestination);
			hMIPLINEArgumentHeader.bCondition = (byte)Condition;
		}
	}

	public override void Load(IFileHeader header)
	{
		base.Load(header);
		if (header is HMIPLINEArgumentHeader)
		{
			HMIPLINEArgumentHeader hMIPLINEArgumentHeader = (HMIPLINEArgumentHeader)header;
			value = FileFormat.GetString(hMIPLINEArgumentHeader.spValue);
			running = FileFormat.GetString(hMIPLINEArgumentHeader.spRunning);
			finallysignal = FileFormat.GetString(hMIPLINEArgumentHeader.spFinally);
			xoffset = FileFormat.GetString(hMIPLINEArgumentHeader.spXOffset);
			yoffset = FileFormat.GetString(hMIPLINEArgumentHeader.spYOffset);
			velocity = FileFormat.GetString(hMIPLINEArgumentHeader.spVelocity);
			accelerate = FileFormat.GetString(hMIPLINEArgumentHeader.spAccelerate);
			decelerate = FileFormat.GetString(hMIPLINEArgumentHeader.spDecelerate);
			endsource = FileFormat.GetString(hMIPLINEArgumentHeader.spEndSource);
			enddestination = FileFormat.GetString(hMIPLINEArgumentHeader.spEndDestination);
			condition = (StartupConditions)hMIPLINEArgumentHeader.bCondition;
		}
	}

	public override void Save(DownloadWriter dw)
	{
		base.Save(dw);
		dw.Write((byte)condition);
		if (condition == StartupConditions.Pulse)
		{
			dw.Skip(6);
		}
		else
		{
			dw.WriteI(value);
		}
		dw.WriteI(running);
		dw.WriteI(finallysignal);
		dw.WriteF(endsource);
		dw.WriteF(enddestination);
		dw.WriteF(xoffset);
		dw.WriteF(yoffset);
		dw.WriteF(velocity);
		dw.WriteI(accelerate);
		dw.WriteI(decelerate);
		dw.Skip(30);
	}

	public override void Load(UploadReader ur)
	{
		base.Load(ur);
		condition = (StartupConditions)ur.Read8();
		value = ur.ReadI();
		running = ur.ReadI();
		finallysignal = ur.ReadI();
		endsource = ur.ReadF();
		enddestination = ur.ReadF();
		xoffset = ur.ReadF();
		yoffset = ur.ReadF();
		velocity = ur.ReadF();
		accelerate = ur.ReadI();
		decelerate = ur.ReadI();
		ur.Skip(30);
	}

	public override void Save(StringBuilder sb)
	{
		base.Save(sb);
		sb.Append((int)condition);
		sb.Append(",");
		sb.Append(value);
		sb.Append(",");
		sb.Append(running);
		sb.Append(",");
		sb.Append(xoffset);
		sb.Append(",");
		sb.Append(yoffset);
		sb.Append(",");
		sb.Append(velocity);
		sb.Append(",");
		sb.Append(accelerate);
		sb.Append(",");
		sb.Append(decelerate);
		sb.Append(",");
		sb.Append(endsource);
		sb.Append(",");
		sb.Append(enddestination);
	}

	public override void Load(string[] args, int start)
	{
		base.Load(args, start);
		condition = (StartupConditions)int.Parse(args[start]);
		value = args[start + 1];
		running = args[start + 2];
		xoffset = args[start + 3];
		yoffset = args[start + 4];
		velocity = args[start + 5];
		accelerate = args[start + 6];
		decelerate = args[start + 7];
		endsource = args[start + 8];
		enddestination = args[start + 9];
	}
}
