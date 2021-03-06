using System;
using System.Collections.Generic;
using Lens.assets;

namespace BurningKnight.ui.dialog {
	public class CombineDialog : ChoiceDialog {
		public string Ar;
		public string Br;
		
		public CombineDialog(string id, string ar = null, string br = null, Action<string, int> callback = null) : base(id, new[] {
			$"{id}_a", $"{id}_b"
		}, new List<string[]> {
			new[] { $"{id}_ar" }, 
			new[] { $"{id}_br" }
		}, callback) {
			Ar = ar;
			Br = br;
		}

		public override Dialog GetNext() {
			var str = DecideNext();
			return new Dialog($"[sp 2]{Locale.Get(Id)}\n{Locale.Get(Options[Last])}\n{Locale.Get(str)}", new [] {
				Last == 0 ? Ar : Br
			});
		}
	}
}