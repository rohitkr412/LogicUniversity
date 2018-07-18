<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AcknowledgeDistributionList.aspx.cs" Inherits="Team3ADProject.Protected.AcknowledgeDistributionList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br /><br />
    <table>
        <tr>
            <th>
                Department Name : 
            </th>
            <td>
                <asp:Label ID="DepartmentNameLabel" Text="Department Name" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                Department Representative :
            </th>
            <td>
                <asp:Label ID="DepartmentRepresentativeLabel" Text="Department Representative" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                Date : 
            </th>
            <td>
                <asp:Label ID="DateLabel" Text="Date " runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                Time :
            </th>
            <td>
                <asp:Label ID="TimeLabel" Text="Time" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                Location :
            </th>
            <td>
                <asp:Label ID="LocationLabel" Text="Location" runat="server"></asp:Label>
            </td>
        </tr>
         
    </table>
    <br /><br /><br />
        
        <asp:GridView ID="gridview1" runat="server"  AutoGenerateColumns="false" >
                <Columns>
                    <asp:BoundField DataField="item_number" ReadOnly="true" HeaderText="Item Number" SortExpression="item_number"/>
                    <asp:BoundField DataField="description" ReadOnly="true" HeaderText="Description" SortExpression="description"/>
                    <asp:BoundField DataField="item_requisition_quantity" ReadOnly="true" HeaderText="Quantity Requested" SortExpression="item_requisition_quantity"/>
                   <asp:BoundField DataField="item_distributed_quantity" ReadOnly="true" HeaderText="Quantity Received" SortExpression="item_distributed_quantity"/>                    
                </Columns>

            </asp:GridView>   

    <br /><br /><br />
    Enter Department Password :
    <asp:TextBox ID="PinTextBox" runat="server"></asp:TextBox>
    <br /><br />
    <asp:Button ID="AcknowledgeButton" runat="server" Text="Acknowledge" OnClick="AcknowledgeButtonClick" />
</asp:Content>
