using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2D
{
	public class GenericList<T>
	{
		T[] obj = new T[Constants.ENEMIES_TEXTURES];
		int count = 0;

		// Adding items mechanism into generic type
		public void Add(T item)
		{
			// Checking length
			if (count + 1 < Constants.ENEMIES_TEXTURES)
			{
				obj[count] = item;
			}
			count++;
		}

		public int getSize()
		{
			return count;
		}

		public T getOnIndex(int index)
		{
			return obj[index];
		}

		public void setOnIndex(int index, T item)
		{
			obj[index] = item;
		}

	}
}
