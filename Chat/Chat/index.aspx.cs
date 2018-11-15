using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chat
{
	public partial class index : System.Web.UI.Page
	{
		BancoToJs b = new BancoToJs();

		protected void Page_Load(object sender, EventArgs e)
		{
			//try
			//{
			//	Compartilha.ultimoCod = "0";
			//	List<List<string>> matrix = b.CarregaDados("select * from mensagem", true);
			//	List<string> cols = matrix[0];
			//	matrix.RemoveAt(0);
			//	lblRetorno.Text = b.MatrixToJsObjectList(matrix, cols);
			//}
			//catch
			//{
			//	lblRetorno.Text = "erro";
			//}
			this.Controls.Add(new LiteralControl("<script>Chat.ReadMessages(successReadMessages);</script>"));
		}
	}
}