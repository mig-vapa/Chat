<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Chat.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<link href="Offline%20Frameworks/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
		<%--<asp:Label ID="lblRetorno" runat="server" Text="Label"></asp:Label>--%>
        <div class="container" id="app">
            <p v-for="m in messages">
                {{ m }}
            </p>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
                <asp:ServiceReference Path="~/Chat.svc" />
            </Services>
        </asp:ScriptManager>
    </form>
    <script src="Offline%20Frameworks/jquery-3.3.1.min.js"></script>
	<script src="Offline%20Frameworks/bootstrap.min.js"></script>
	<script src="Offline%20Frameworks/vue.devep.js"></script>
	<script src="js/index.js"></script>
</body>
</html>
