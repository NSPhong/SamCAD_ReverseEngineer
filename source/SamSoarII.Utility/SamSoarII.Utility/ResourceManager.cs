using System.Collections.Generic;

namespace SamSoarII.Utility;

public class ResourceManager<T> where T : IResource
{
	private T template;

	private T[] data;

	private int count;

	private int usedcount;

	public ResourceManager(T _template, int _count, params object[] args)
	{
		template = _template;
		count = _count;
		usedcount = 0;
		data = new T[count * 4];
		for (int i = 0; i < count; i++)
		{
			data[i] = (T)template.Create(args);
			data[i].ResourceID = i;
		}
	}

	public IEnumerable<T> GetAllResources()
	{
		for (int i = 0; i < count; i++)
		{
			yield return data[i];
		}
	}

	public T Create(params object[] args)
	{
		if (usedcount >= count)
		{
			if (count >= data.Length)
			{
				template = (T)template.Create(args);
				template.ResourceID = -1;
				return template;
			}
			data[count] = (T)template.Create(args);
			ref readonly T reference = ref data[count];
			int resourceID = count;
			reference.ResourceID = resourceID;
			count++;
		}
		data[usedcount].Recreate(args);
		return data[usedcount++];
	}

	public void Dispose(T item)
	{
		if (item.ResourceID != -1)
		{
			int resourceID = item.ResourceID;
			if (resourceID < usedcount)
			{
				int num = --usedcount;
				data[resourceID].ResourceID = num;
				data[num].ResourceID = resourceID;
				template = data[resourceID];
				data[resourceID] = data[num];
				data[num] = template;
			}
		}
	}
}
