using System;
using UnityEngine;

	[Serializable]
	public static class ColorManager 
	{
		//COLORS
		public static Color White = new Color32(255, 255, 255, 255);
		public static Color DarkWhite = new Color32(180, 180, 180, 255);
		public static Color Blurple = new Color32(88, 101, 242, 255);
		public static Color DarkBlurple = new Color32(38, 51, 192, 255);
		public static Color Yellow = new Color32(254, 231, 91, 255);
		public static Color DarkYellow = new Color32(204, 181, 41, 255);
		public static Color Pink = new Color32(235, 69, 158, 255);
		public static Color DarkPink = new Color32(185, 19, 108, 255);
		public static Color Red = new Color32(237, 66, 69, 255);
		public static Color DarkRed = new Color32(177, 6, 9, 255);


		public static Color Green = new Color32(87, 242, 135, 255);
		public static Color DarkGreen = new Color32(27, 182, 75, 255);

		public enum ItemColor
		{
			white,
			yellow,
			pink,
			blue,
			red
		}

		public static Color GetColor(ItemColor itemColor)
		{
			switch (itemColor)
			{
				case ItemColor.white:
					return White;
					break;
				case ItemColor.yellow:
					return Yellow;
					break;
				case ItemColor.pink:
					return Pink;
					break;
				case ItemColor.blue:
					return Blurple;
					break;
				case ItemColor.red:
					return Red;
					break;
				default:
					return Red;
					break;
			}
		}

		public static Color GetColorFromCategoryDark(ItemColor itemColor)
		{
			switch (itemColor)
			{
				case ItemColor.white:
					return DarkWhite;
					break;
				case ItemColor.yellow:
					return DarkYellow;
					break;
				case ItemColor.pink:
					return DarkPink;
					break;
				case ItemColor.blue:
					return DarkBlurple;
					break;
				case ItemColor.red:
					return DarkRed;
					break;
				default:
					return Red;
					break;
			}
		}
	}
