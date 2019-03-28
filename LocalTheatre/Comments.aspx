<%@ Page Title="Comments" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Comments.aspx.cs" Inherits="LocalTheatre.Comments" %>
<asp:Content ID="Content1" 
    ContentPlaceHolderID="MainContent"
    runat="server">
    <asp:LinqDataSource ID="LinqDataSource1" 
        runat="server" ContextTypeName="LocalTheatre.Models.ApplicationDbContext"
        EntityTypeName="" GroupBy="Date" Select="new (key as Date, it as Comments)" 
        TableName="Comments">
    </asp:LinqDataSource> 

    <br />
    <asp:GridView ID="GridView1" emptydatatext="No data available." runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" ShowFooter="True" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:TemplateField>
                <FooterTemplate>
                    <br />
                    <asp:Button ID="btnLeaveAComment" CssClass="btn" runat="server" Text="Leave a comment" OnClick="btnLeaveAComment_Click" />
                <br />
                    <br />
                    
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblUserId" runat="server" Text='<%# Eval("UserId") %>'></asp:Label>
                    <br />
                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                    <br />
                    <asp:Label ID="lblText" runat="server" Text='<%# Eval("Text") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FFF1D4" />
        <SortedAscendingHeaderStyle BackColor="#B95C30" />
        <SortedDescendingCellStyle BackColor="#F1E5CE" />
        <SortedDescendingHeaderStyle BackColor="#93451F" />
    </asp:GridView>
    <asp:Panel ID="Panel1"  emptydatatext="No data available." runat ="server" visible ="false">
                    <asp:TextBox ID="tbComment" runat="server" TextMode="MultiLine"></asp:TextBox>
                    <asp:Button ID="btnSubmit" Text="Submit" runat ="server" OnClick="btnSubmit_Click" />
                     </asp:Panel>
</asp:Content>
