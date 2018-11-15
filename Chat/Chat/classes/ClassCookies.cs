using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

    public class ClassCookies
    {
        HttpCookie cookie;

        public void carrinho_adicionar(string codigo_produto, System.Web.UI.Page pagina)
        {
            bool achou = false;
            cookie = (HttpCookie)pagina.Request.Cookies["Carrinho"];
            if (cookie == null)
            {
                cookie = new HttpCookie("Carrinho");
                cookie.Expires = DateTime.Now.AddHours(2);
                cookie.Values.Add(codigo_produto, null); //adiciona o primeiro produto
            }
            else
            {
                foreach (string itens in cookie.Values.AllKeys)
                {
                    if (itens==codigo_produto)
                    {
                        achou=true;
                        break;
                    }
                }
                if(!achou)
                cookie.Values.Add(codigo_produto, null); // adiciona o produto se não existir na lista
            }
            pagina.Response.Cookies.Add(cookie); //grava o cookie
        }

        public string carrinho_gravados(System.Web.UI.Page pagina)
        {
            int contar = 1;
            string concatena = null;
            cookie = (HttpCookie)pagina.Request.Cookies["Carrinho"];
            if (cookie != null)
            {
                foreach (string itens in cookie.Values.AllKeys)
                {
                    concatena=concatena+itens;
                    if (contar < cookie.Values.Count)
                    concatena=concatena+",";
                    contar=contar+1;
                }
            }
            return concatena;
        }

        public HttpCookie carrinho_remover(string codigo_produtos, System.Web.UI.Page pagina)
        {
            cookie = (HttpCookie)pagina.Request.Cookies["Carrinho"];
            if(cookie!=null)
            {
                cookie.Values.Remove(codigo_produtos);
                pagina.Response.Cookies.Add(cookie);
                return cookie;
            }
            else
                return null;
        }
    }