<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="comp2084_lesson10.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <h1>Login</h1>

    <asp:Label ID="lblError" runat="server" CssClass="label label-danger" />

    <fieldset>
        <label for="txtUsername" class="col-sm-2">Username: </label>
        <asp:TextBox ID="txtUsername" runat="server" required MaxLength="50" />
    </fieldset>
    <fieldset>
        <label for="txtPassword" class="col-sm-2">Password: </label>
        <asp:TextBox ID="txtPassword" runat="server" required MaxLength="50" TextMode="Password" />
    </fieldset>
    <div class="col-sm-2">
    <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary" Text="Login"
         OnClick="btnLogin_Click" />
    </div>
</asp:Content>
