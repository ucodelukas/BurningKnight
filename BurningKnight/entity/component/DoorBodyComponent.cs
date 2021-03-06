﻿using BurningKnight.entity.creature.player;
using BurningKnight.entity.projectile;
using Lens.entity;
using VelcroPhysics.Dynamics;

namespace BurningKnight.entity.component {
	public class DoorBodyComponent : RectBodyComponent {
		public DoorBodyComponent(float x, float y, float w, float h, BodyType type = BodyType.Dynamic, bool sensor = false, bool center = false) : base(x, y, w, h, type, sensor, center) {
			
		}

		public override void Init() {
			base.Init();
			Body.IsSensor = false;
		}

		private bool ShouldBeSensor() {
			return !(Entity.TryGetComponent<LockComponent>(out var l) && l.Lock.IsLocked);
		}

		public override bool ShouldCollide(Entity entity) {
			/*if (entity is Projectile) {
				return false;
			}*/
			
			if (entity is Player && ShouldBeSensor()) {
				return false;
			}

			return true;
		}
	}
}