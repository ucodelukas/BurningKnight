using System;
using BurningKnight.entity.creature.player;
using Lens.entity;

namespace BurningKnight.entity.item.stand {
	public class RogerStand : CustomStand {
		protected override string GetIcon() {
			return "deal_bomb";
		}

		protected override string GetSprite() {
			return "roger_stand";
		}

		public override ItemPool GetPool() {
			return ItemPool.Roger;
		}

		protected override int CalculatePrice() {
			return (int) Math.Max(1, PriceCalculator.GetModifier(Item) * 2);
		}

		protected override bool TryPay(Entity entity) {
			if (!entity.TryGetComponent<ConsumablesComponent>(out var component)) {
				return false;
			}

			if (component.Bombs < Price) {
				return false;
			}

			component.Bombs -= Price;
			return true;
		}
	}
}