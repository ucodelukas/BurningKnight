using System.Collections.Generic;
using BurningKnight.entity.level.biome;
using BurningKnight.entity.level.rooms;
using BurningKnight.entity.level.tile;

namespace BurningKnight.entity.level.hub {
	public class HubLevel : RegularLevel {
		public HubLevel() : base(BiomeRegistry.Defined[Biome.Castle]) {
			
		}

		protected override List<RoomDef> CreateRooms() {
			var rooms = new List<RoomDef>();
			
			rooms.Add(new HubEntranceRoom());
			rooms.Add(new HubExitRoom());

			return rooms;
		}

		public override Tile GetFilling() {
			return Tile.Grass;
		}
	}
}