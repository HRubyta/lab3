<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="instructor-register.aspx.cs" Inherits="comp2084_lesson10.instructor_register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Instructor registration</h1>
    <h5>All fields required</h5>

    <fieldset>
        <label for="txtFirstName" class="col-sm-2">First name:</label>
        <asp:TextBox ID="txtFirstName" runat="server" required MaxLength="50" />
    </fieldset>
    <fieldset>
        <label for="txtLastName" class="col-sm-2">Last name: </label>
        <asp:TextBox ID="txtLastName" runat="server" required MaxLength="50" />
    </fieldset>
    <fieldset>
        <label for="txtUsername" class="col-sm-2">Username: </label>
        <asp:TextBox ID="txtUsername" runat="server" required MaxLength="50" />
    </fieldset>
    <fieldset>
        <label for="txtPassword" class="col-sm-2">Password: </label>
        <asp:TextBox ID="txtPassword" runat="server" required MaxLength="50" TextMode="Password" />
    </fieldset>
    <fieldset>
        <label for="txtConfirm" class="col-sm-2">Confirm password: </label>
        <asp:TextBox ID="txtConfirm" runat="server" required MaxLength="50" TextMode="Password" />
        <asp:CompareValidator runat="server" ControlToValidate="txtPassword" ControlToCompare="txtConfirm"
             CssClass="label label-danger" ErrorMessage="password must match" operator="Equal"/>
    </fieldset>
    <fieldset>
        <label for="ddlDepartment" class="col-sm-2">Department: </label>
        <asp:DropDownList ID="ddlDepartment" runat="server" DataTextField="Name"
             DataValueField="DepartmentID" />
    </fieldset>
    <div class="col-sm-offset-2">
        <asp:Button ID="btnSubmit" runat="server" Text="Register" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
    </div>
</asp:Content>
