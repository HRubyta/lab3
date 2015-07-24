<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="courses.aspx.cs" Inherits="comp2084_lesson10.courses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Courses</h1>

    <a href="course-details.aspx">Add Courses</a>

    <asp:GridView runat="server" ID="grdCourses" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="CourseID" HeaderText="Course ID" />
            <asp:BoundField DataField="Title" HeaderText="Course Title" />
            <asp:BoundField DataField="Credits" HeaderText="Course Credit" />
            <asp:BoundField DataField="Department.Name" HeaderText="Department" />
            <asp:HyperLinkField HeaderText="Edit" NavigateUrl="~/course-details.aspx" 
                 Text="Edit" DataNavigateUrlFormatString="course-details.aspx?CourseID={0}"
                 DataNavigateUrlFields="CourseID" />
        </Columns>
    </asp:GridView>
</asp:Content>
