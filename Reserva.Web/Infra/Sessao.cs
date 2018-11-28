using System.Web;
using Reserva.Domain.Entities;

namespace Reserva.Web.Infra
{
	public static class Sessao
	{
		public static Usuario UsuarioAtivo
		{
			get
			{
				var usuario = (Usuario)HttpContext.Current.Session["Usuario"];

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