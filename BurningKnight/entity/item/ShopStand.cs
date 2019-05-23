using BurningKnight.assets;
using BurningKnight.entity.creature.player;
using BurningKnight.entity.events;
using ImGuiNET;
using Lens.entity;
using Lens.graphics;
using Microsoft.Xna.Framework;

namespace BurningKnight.entity.item {
	public class ShopStand : ItemStand {
		public bool Sells = true;
		private int price;
		private string priceString;
		private float priceX;

		protected override string GetSprite() {
			return "shop_stand";
		}

		protected override bool CanTake(Entity entity) {
			if (!base.CanTake(entity)) {
				return false;
			}

			if (!Sells) {
				return true;
			}

			if (!entity.TryGetComponent<ConsumablesComponent>(out var component)) {
				return false;
			}

			if (component.Coins < price) {
				return false;
			}

			component.Coins -= price;
			Sells = false;
			
			return true;
		}

		public override void Render() {
			base.Render();

			if (Item != null && Sells) {
				Graphics.Print(priceString, Font.Medium, Position + new Vector2(priceX, 18));
			}
		}

		public override bool HandleEvent(Event e) {
			if (e is ItemPlacedEvent) {
				price = PriceCalculator.Calculate(Item);
				CalculatePriceSize();
			}
			
			return base.HandleEvent(e);
		}

		private void CalculatePriceSize() {
			priceString = $"{price}";
			priceX = (Width - Font.Small.MeasureString(priceString).Width) / 2f;
		}

		public override void RenderImDebug() {
			base.RenderImDebug();

			if (ImGui.InputInt("Price", ref price)) {
				CalculatePriceSize();
			}
		}
	}
}