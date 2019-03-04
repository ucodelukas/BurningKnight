using BurningKnight.entity.component;
using BurningKnight.entity.level;
using BurningKnight.save;
using Lens.entity;
using Lens.entity.component.logic;

namespace BurningKnight.entity.creature {
	public class Creature : SaveableEntity {
		public override void AddComponents() {
			base.AddComponents();
			
			AddComponent(new HealthComponent());
			AddComponent(new StateComponent());
		}
		
		public void Kill(Entity w) {
			GetComponent<HealthComponent>().Kill(w);
		}
	}
}