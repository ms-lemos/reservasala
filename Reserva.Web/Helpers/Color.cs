using System;

namespace Reserva.Web.Helpers
{
	public class ColorHelper
	{
		private static Random _random;

		public static string Random()
		{
			return $"#{(_random ?? (_random = new Random())).Next(0x1000000):X6}";
		}

	    public static string GetBulletSrc(string cor)
	    {
            switch (cor)
            {
                case "1":
                    return "/Images/bullets/bullet_green.png";
                case "2":
                    return "/Images/bullets/bullet_yellow.png";
                case "3":
                    return "/Images/bullets/bullet_red.png";
                default:
                    throw new ArgumentOutOfRangeException(nameof(cor));
            }
        }
    }
}