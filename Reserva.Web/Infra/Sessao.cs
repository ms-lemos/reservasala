using System.Web;

namespace Reserva.Web.Infra
{
	public static class Sessao
	{
		public static dynamic UsuarioAtivo
		{
			get
			{
				var usuario = (dynamic)HttpContext.Current.Session["Usuario"];

				return usuario;
			}
			private set { HttpContext.Current.Session["Usuario"] = value; }
		}
		public static void GravaSessao(dynamic usr)
		{
			LimpaSessao();

			UsuarioAtivo = usr;
		}
		public static void LimpaSessao()
		{
			HttpContext.Current.Session.Clear();
		}
	}
}