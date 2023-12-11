﻿using Serilog;

namespace Substrate.Hexalem.Engine
{
    public partial class HexaTile
    {
        public static implicit operator byte(HexaTile p) => p.Value;

        public static implicit operator HexaTile(byte p) => new HexaTile(p);

        public byte Value { get; set; }

        public HexaTile(TileType hexTileType, byte hexTileLevel, TilePattern hexTilePattern)
        {
            Build(hexTileType, hexTileLevel, hexTilePattern);
        }

        private void Build(TileType hexTileType, byte hexTileLevel, TilePattern hexTilePattern)
        {
            var level = (hexTileLevel & 0x3) << 6;
            var type = ((byte)hexTileType & 0x7) << 3;
            var pattern = (byte)hexTilePattern & 0x7;

            Value = (byte)(level | type | pattern);
        }

        public HexaTile(byte value)
        {
            Value = value;
        }

        /// <summary>
        /// Is an empty tile
        /// </summary>
        public bool IsEmpty()
        {
            return TileType == TileType.Empty;
        }

        /// <summary>
        /// Same tile type
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        internal bool Same(HexaTile? v)
        {
            if (v == null)
            {
                return false;
            }

            return TileType == v.TileType;
        }

        /// <summary>
        /// Determine if a tile can be upgrade
        /// </summary>
        /// <returns></returns>
        public bool CanUpgrade()
        {
            if (TileLevel == 3)
            {
                Log.Debug($"Can not upgrade past level {3}");
                return false;
            }

            if (TileType == TileType.Empty)
            {
                Log.Debug($"{nameof(TileType.Empty)} cannot be upgrade");
                return false;
            }

            var upgradable = GameConfig.UpgradableTypeTile();

            if (!upgradable.Exists(x => x == TileType))
            {
                Log.Debug("{TileType} cannot be upgrade", TileType);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Upgrade a tile
        /// </summary>
        internal void Upgrade()
        {
            if (CanUpgrade())
            {
                TileLevel += 1;
            }
        }

        public HexaTile Clone()
        {
            var cloneTile = new HexaTile(Value);
            return cloneTile;
        }

        public override string ToString()
        {
            return $"{TileType} - {TileLevel} - {TilePattern}";
        }
    }

    public partial class HexaTile
    {
        /// <summary>
        /// 2 bits
        /// </summary>
        public byte TileLevel
        {
            get => (byte)((Value >> 6) & 0x3);
            set => Value = (byte)((Value & 0x3F) | ((value & 0x3) << 6));
        }

        /// <summary>
        /// 3 bits
        /// </summary>
        public TileType TileType
        {
            get => (TileType)((Value >> 3) & 0x7);
            set => Value = (byte)((Value & 0xCF) | (((byte)value & 0x7) << 3));
        }

        /// <summary>
        /// 3 bits
        /// </summary>
        public TilePattern TilePattern
        {
            get => (TilePattern)(Value & 0x7);
            set => Value = (byte)((Value & 0xF8) | (byte)value & 0x7);
        }
    }
}