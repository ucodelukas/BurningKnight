using Lens.entity;
using Lens.entity.component;

namespace BurningKnight.entity.component {
	public class OrbitalComponent : Component {
		public float Radius = 24;
		public Entity Orbiting;
	}
}