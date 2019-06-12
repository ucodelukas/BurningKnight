using BurningKnight.entity.component;
using BurningKnight.entity.creature.mob.boss;
using BurningKnight.entity.events;
using BurningKnight.level.entities;
using BurningKnight.state;
using BurningKnight.ui;
using Lens;
using Lens.entity;
using Lens.entity.component.logic;

namespace BurningKnight.entity.creature.bk {
	public class BurningKnight : Boss {
		private HealthBar healthBar;
		private BossPatternSet<BurningKnight> set;
	
		public override void AddComponents() {
			base.AddComponents();
			
			AddTag(Tags.BurningKnight);

			Width = 42;
			Height = 42;
			Flying = true;

			var b = new RectBodyComponent(16, 16, Width - 32, Height - 32) {
				KnockbackModifier = 0
			};

			AddComponent(b);
			b.Body.LinearDamping = 4;
			
			// FIXME: TMP sprite and size, obv
			AddComponent(new AnimationComponent("burning_knight"));

			var health = GetComponent<HealthComponent>();
			health.InitMaxHealth = 64 + (Run.Depth - 1) * 20;
			
			GetComponent<StateComponent>().Become<IdleState>();
		}

		public override void Update(float dt) {
			base.Update(dt);

			if (set == null) {
				set = BurningKnightAttackRegistry.PatternSetRegistry.Generate(Run.Level.Biome.Id);
			}

			if (healthBar == null) {
				healthBar = new HealthBar(this);
				Engine.Instance.State.Ui.Add(healthBar);
			}
		}

		public override void SelectAttack() {
			base.SelectAttack();
			GetComponent<StateComponent>().PushState(BurningKnightAttackRegistry.GetNext(set));
		}

		private bool died;

		public override bool HandleEvent(Event e) {
			if (e is DiedEvent && !died) {
				died = true;
				// Done = false;

				var exit = new Exit();
				Area.Add(exit);
				
				exit.To = Run.Depth + 1;
				exit.Center = Center;
			}
			
			return base.HandleEvent(e);
		}
	}
}