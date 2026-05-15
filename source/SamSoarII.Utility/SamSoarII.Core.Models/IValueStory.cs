using System;

namespace SamSoarII.Core.Models;

public interface IValueStory : IDisposable
{
	IValueStore Store { get; }

	int Count { get; }

	IValueStoryPoint First { get; }

	IValueStoryPoint Last { get; }

	IValueStoryPoint this[int id] { get; }

	void AddI(int _timems, int _value);

	void AddF(int _timems, float _value);
}
