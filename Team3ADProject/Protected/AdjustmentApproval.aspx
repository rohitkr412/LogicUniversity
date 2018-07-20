﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AdjustmentApproval.aspx.cs" Inherits="Team3ADProject.Protected.AdjustmentApproval" %>
<%@ Import Namespace="Team3ADProject.Protected" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">   
    
   

    <h1>Adjustment Form Approval</h1>
    
    <br />
    
    
    <br />
    
    
   
    <br/>
    <br />
    
    <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" OnDayRender="Calendar1_DayRender"></asp:Calendar> 
    
    
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="All" />
    
    
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Search" />
    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    
    
    <br/>
    <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
    <br/>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Height="99px" Width="1094px"  OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnPageIndexChanging="GridView1_PageIndexChanging" AllowPaging="True" PageSize="5"  >
        <Columns>
            
            <asp:TemplateField>
                 
                <HeaderTemplate>
                    <asp:CheckBox ID="chkSelectAll" runat="server" CssClass="chkHeader" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect"  runat="server" CssClass="chkItem" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="adjustment_id" HeaderText="Adj ID" InsertVisible="False" ReadOnly="True" SortExpression="adjustment_id" />
            <asp:BoundField DataField="adjustment_date" HeaderText="Adj date" SortExpression="adjustment_date" DataFormatString="{0:MM/dd/yyyy}" />
            <asp:BoundField DataField="employee_id" HeaderText="Employee Id" SortExpression="employee_id" />
            <asp:BoundField DataField="item_number" HeaderText="Item No." SortExpression="item_number" />
            <asp:BoundField DataField="adjustment_quantity" HeaderText="Adj Qty" SortExpression="adjustment_quantity" />
            <asp:BoundField DataField="adjustment_price" HeaderText="Adj Price" SortExpression="adjustment_price" />
            <asp:BoundField DataField="adjustment_status" HeaderText="Adj Status" SortExpression="adjustment_status" />
            <asp:BoundField DataField="employee_remark" HeaderText="Employee Remark" SortExpression="employee_remark" />
            <asp:TemplateField HeaderText="Manager Remark" SortExpression="manager_remark">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("manager_remark") %>'></asp:TextBox>
                </ItemTemplate>
               
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="True">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Approve" CommandArgument='<%# Eval("adjustment_id") %>'></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Reject"></asp:LinkButton>
                </ItemTemplate>
           
            </asp:TemplateField>
        </Columns>
      
        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
      
    </asp:GridView>
   
    <br />
    <br />
    <br />
    <br />
    <br/>
   
    <asp:LinkButton ID="LinkButton1"  runat="server" OnClick="LinkButton1_Click">Approve</asp:LinkButton>
   <br/>
    <asp:LinkButton ID="LinkButton3"  runat="server" OnClick="LinkButton3_Click">Reject</asp:LinkButton>
  
</asp:Content>


