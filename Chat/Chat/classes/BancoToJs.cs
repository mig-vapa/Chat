using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;

public class BancoToJs
{
	ClasseConexao cs = new ClasseConexao();
	DataSet ds = new DataSet();

	public List<List<String>> DataTableToMatrix(DataTable d, bool colsFirst)
	{

		List<List<String>> matrix = new List<List<String>>();
		List<String> subMat;
		try
		{
			if (colsFirst)
			{
				subMat = new List<string>();
				foreach (DataColumn c in d.Columns)
				{
					subMat.Add(c.ToString());
				}
				matrix.Add(subMat);
			}
			for (int i = 0; i < d.Rows.Count; i++)
			{
				subMat = new List<string>();
				foreach (DataColumn c in d.Columns)
				{
					subMat.Add(d.Rows[i][c.ToString()].ToString());
				}
				matrix.Add(subMat);
			}
		}
		catch (Exception ex)
		{
			if (ex is NullReferenceException) matrix = null;
		}
		return matrix;
	}

	public List<List<String>> CarregaDados(String sql)
	{
		return this.CarregaDados(sql, false);
	}

	public List<List<String>> CarregaDados(String sql, bool colsFirst)
	{
		try
		{
			ds = new DataSet();
			cs = new ClasseConexao();
			ds = cs.executa_sql(sql);
			return DataTableToMatrix(ds.Tables[0], colsFirst);
		}
		catch
		{
			return new List<List<string>>();
		}
	}

	public List<List<string>> CarregaDadosProcedure(String sql, List<List<string>> parameters)
	{
		return CarregaDadosProcedure(sql, parameters, false);
	}

	public List<List<string>> CarregaDadosProcedure(String sql, List<List<string>> parameters, bool colsFirst)
	{
		ds = new DataSet();
		cs = new ClasseConexao();
		return DataTableToMatrix(cs.executa_Procedure(sql, parameters), colsFirst);

	}

	public int InsereDados(string sql)
	{
		try
		{
			ds = new DataSet();
			cs = new ClasseConexao();
			return cs.executa_IncAltExcParametros(new SqlCommand(sql));
		}
		catch
		{
			return 0;
		}
	}


	public string MatrixToJs(List<List<String>> matrix)
	{
		try
		{
			String js = "[";
			foreach (List<String> l in matrix)
			{
				js += "[";
				foreach (string s in l)
				{
					js += "'" + s + "', ";
				}
				js = js.Remove(js.Length - 2, 2);
				js += "], ";
			}
			js = js.Remove(js.Length - 2, 2);
			js += "]";
			return js;
		}
		catch
		{
			return "{}";
		}
	}

	public string MatrixToJsObjectList(List<List<String>> matrix, List<String> cols)
	{
		try
		{
			String js = "[";
			foreach (List<String> l in matrix)
			{
				js += "{";
				for (int i = 0; i < cols.Count; i++)
				{
					js += cols[i] + ": '" + l[i] + "', ";
				}
				js = js.Remove(js.Length - 2, 2);
				js += "}, ";
			}
			js = js.Remove(js.Length - 2, 2);
			js += "]";
			return js;
		}
		catch
		{
			return "{}";
		}
	}

	public string MatrixToJsObjectList(List<List<String>> matrix, List<String> cols, List<int> excludeCol)
	{
		try
		{
			String js = "[";
			int j;
			foreach (List<String> l in matrix)
			{
				j = 0;
				js += "{";
				for (int i = 0; i < l.Count; i++)
				{
					if (excludeCol.Contains(i))
					{
						continue;
					}
					else
					{
						js += cols[j] + ": '" + l[i] + "', ";
						j++;
					}
				}
				js = js.Remove(js.Length - 2, 2);
				js += "}, ";
			}
			js = js.Remove(js.Length - 2, 2);
			js += "]";
			return js;
		}
		catch
		{
			return "{}";
		}
	}

	public string MatrixToJsObjectList(List<List<String>> matrix, List<String> cols, List<int> excludeCol, List<int> noQuotesCols)
	{
		try
		{
			String js = "[";
			int j;
			foreach (List<String> l in matrix)
			{
				j = 0;
				js += "{";
				for (int i = 0; i < l.Count; i++)
				{
					if (excludeCol.Contains(i))
					{
						continue;
					}
					else
					{
						js += cols[j] + (noQuotesCols.Contains(i) ? ": " : ": '") + l[i] + (noQuotesCols.Contains(i) ? ", " : "', ");
						j++;
					}
				}
				js = js.Remove(js.Length - 2, 2);
				js += "}, ";
			}
			js = js.Remove(js.Length - 2, 2);
			js += "]";
			return js;
		}
		catch
		{
			return "{}";
		}
	}

	public string MatrixToJs(List<List<String>> matrix, bool firstRowCols)
	{
		try
		{
			if (firstRowCols)
			{
				List<string> m0 = matrix[0];
				matrix.RemoveAt(matrix.Count - 1);
				return MatrixToJsObjectList(matrix, m0);
			}
			else return MatrixToJs(matrix);
		}
		catch
		{
			return "{}";
		}
	}

	public string VectorToJsObject(List<String> matrix, List<string> cols)
	{
		try
		{
			String js = "{";
			for (int i = 0; i < cols.Count; i++)
			{
				js += "'" + cols[i] + "': '" + matrix[i] + "', ";
			}
			js = js.Remove(js.Length - 2, 2);
			js += "}";
			return js;
		}
		catch
		{
			return "{}";
		}
	}

	public string VectorToJsObject(List<String> matrix, List<string> cols, List<int> noCommasCols)
	{
		try
		{
			String js = "{";
			for (int i = 0; i < cols.Count; i++)
			{
				js += "'" + cols[i] + "': " + (noCommasCols.Contains(i) ? "" : "'") + matrix[i] + (noCommasCols.Contains(i) ? "" : "'") +  ", ";
			}
			js = js.Remove(js.Length - 2, 2);
			js += "}";
			return js;
		}
		catch
		{
			return "{}";
		}
	}

	public string VectorToJsObject(List<String> matrix, List<string> cols, string aditional)
	{
		try
		{
			String js = "{" + aditional + ", ";
			for (int i = 0; i < cols.Count; i++)
			{
				js += "'" + cols[i] + "': '" + matrix[i] + "', ";
			}
			js = js.Remove(js.Length - 2, 2);
			js += "}";
			return js;
		}
		catch
		{
			return "{}";
		}
	}

	public string ListToJS(List<String> list)
	{
		try
		{
			String js = "[";
			foreach (String l in list)
			{
				js += "'" + l + "', ";
			}
			js = js.Remove(js.Length - 2, 2);
			js += "]";
			return js;
		}
		catch
		{
			return "{}";
		}
	}

	//public List<string> JsToVector(string list)
	//{
	//    //list = list.Split(',');
	//    return null;
	//}
}
