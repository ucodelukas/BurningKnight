using System;
using System.Collections.Generic;
using Lens.util;
using Lens.util.math;

namespace BurningKnight.entity.pool {
	public class Pool<T> {
		protected List<float> Chances = new List<float>();
		protected List<T> Classes = new List<T>();

		public int Size => Classes.Count;

		public virtual T Generate() {
			var I = Rnd.Chances(Chances);

			if (I == -1) {
				Log.Error("-1 as pool result!");
				return default(T);
			}

			return Classes[I];
		}

		public void Add(T Type, float Chance) {
			Classes.Add(Type);
			Chances.Add(Chance);
		}

		public void Clear() {
			Classes.Clear();
			Chances.Clear();
		}

		public void AddFrom(Pool<T> Pool) {
			Classes.AddRange(Pool.Classes);
			Chances.AddRange(Pool.Chances);
		}

		public T Get(int i) {
			return Classes[i];
		}
	}
}