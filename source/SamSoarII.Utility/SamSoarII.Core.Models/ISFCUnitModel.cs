using System;
using System.ComponentModel;

namespace SamSoarII.Core.Models;

public interface ISFCUnitModel : IModel, IDisposable, INotifyPropertyChanged
{
	string AssertExpression(ISFCExpression expr, ISFCExprTree tree);
}
