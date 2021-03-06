﻿using System;
using System.Collections.Generic;

namespace Lens.util.timer {
	public static class Timer {
		private static List<TimerTask> tasks = new List<TimerTask>();

		public static void Cancel(TimerTask task) {
			tasks.Remove(task);
		}

		public static TimerTask Add(Action fn, float Delay) {
			if (Delay <= 0) {
				fn();
				return null;
			}

			var t = new TimerTask(fn, Delay);
			tasks.Add(t);

			return t;
		}

		public static void Clear() {
			tasks.Clear();
		}
		
		public static void Update(float dt) {
			for (int i = tasks.Count - 1; i >= 0; i--) {
				TimerTask task = tasks[i];

				if (task == null) {
					tasks.RemoveAt(i);
					continue;
				}
				
				task.Delay -= dt;

				if (task.Delay <= 0) {
					try {
						task.Fn?.Invoke();
					} catch (Exception e) {
						Log.Error(e);
					}
					
					tasks.RemoveAt(i);
				}
			}
		}
	}
}