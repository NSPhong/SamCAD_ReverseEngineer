using System.Windows;

namespace SamSoarII.Utility;

public class Complex
{
	protected Vector data;

	public double Real => data.X;

	public double Virt => data.Y;

	public Complex(double _real, double _virt)
	{
		data = new Vector(_real, _virt);
	}

	public Complex(Vector _data)
	{
		data = _data;
	}

	public static Complex operator +(Complex c1, Complex c2)
	{
		return new Complex(c1.data + c2.data);
	}

	public static Complex operator -(Complex c1, Complex c2)
	{
		return new Complex(c1.data - c2.data);
	}

	public static Complex operator *(Complex c1, Complex c2)
	{
		return new Complex(c1.Real * c2.Real - c1.Virt * c2.Virt, c1.Real * c2.Virt + c2.Real * c1.Virt);
	}

	public static Complex operator /(Complex c1, Complex c2)
	{
		return new Complex(new Vector(Vector.Multiply(c1.data, c2.data), Vector.CrossProduct(c1.data, c2.data)) / c1.data.LengthSquared);
	}
}
