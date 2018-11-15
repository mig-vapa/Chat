using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace Chat
{

    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Chat
    {
        BancoToJs b = new BancoToJs();

        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        
      
        [OperationContract]
        public string ReadMessages()
        {
			try
			{
				List<List<string>> matrix = b.CarregaDados(
						"select * from mensagem where idmensagem >" + Compartilha.ultimoCod);
				if (matrix != null && matrix.Count != 0)
				{
					Compartilha.ultimoCod = matrix[matrix.Count - 1][0];
					String s = b.MatrixToJsObjectList(matrix, new List<string>() { "'id'",
						"'id-sender'", "'id-reciever'", "'text'" });
					return s;
				}
				else return "vazio";
			}
			catch (Exception ex)
			{
				return "error: " + ex.Message;
			}
        }

		[OperationContract]
		public bool SendMessage(string a)
		{
			b.InsereDados("");

			return a.Contains("f");
		}

        // Add more operations here and mark them with [OperationContract]
    }
}
