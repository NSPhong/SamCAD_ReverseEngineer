using System.Text;

namespace SamSoarII.Utility.Files.Delta;

public class DeltaOutputRectUnit : DeltaRectUnit
{
	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("DeltaOutputRectUnit : ");
		stringBuilder.Append(base.Symbol);
		stringBuilder.Append(" In = {");
		foreach (string @in in base.Ins)
		{
			stringBuilder.Append(@in);
			stringBuilder.Append(",");
		}
		stringBuilder.Append("} Out = {");
		foreach (string @out in base.Outs)
		{
			stringBuilder.Append(@out);
			stringBuilder.Append(",");
		}
		stringBuilder.Append("}");
		return stringBuilder.ToString();
	}
}
