﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ViewROSpecialRequest.aspx.cs" Inherits="Team3ADProject.Protected.ViewROSpecialRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--    <script>
        $(function () {
            $(".datepicker1").datepicker({ minDate: 1 });
        })

    </script>--%>

    <h1>Special Requests</h1>
    <br />
    <br />

    <div class="col-md-12">
        <span>Search by Requisition Order ID:</span>
        <asp:TextBox ID="txt_searchByRO" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:Button ID="btn_SortingSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btn_SortingSearch_Click" />
    </div>

    <br />
    <br />
    <div>
        <asp:Label ID="Label1" runat="server" Text="Requisition Order ID:"></asp:Label>
        <asp:Label ID="Label_ViewRO" runat="server" Text=""></asp:Label>

        <%--        <asp:TextBox ID="TextBox1" runat="server" CssClass="datepicker1"></asp:TextBox>--%>

        <asp:GridView ID="gv_ViewRO" runat="server" AutoGenerateColumns="False" CssClass="table table-hover align-center" EmptyDataText="The Requisition Order you entered was not found ">
            <Columns>
                <asp:BoundField DataField="item_number" HeaderText="Item Number" ReadOnly="True" SortExpression="item_number" />
                <asp:BoundField DataField="description" HeaderText="Description" ReadOnly="True" SortExpression="description" />
                <asp:BoundField DataField="unit_of_measurement" HeaderText="UOM" ReadOnly="True" SortExpression="unit_of_measurement" />
                <asp:BoundField DataField="item_pending_quantity" HeaderText="Qty Ordered" ReadOnly="True" SortExpression="item_pending_quantity" />

                <asp:BoundField DataField="current_quantity" HeaderText="Qty Available" ReadOnly="True" SortExpression="current_quantity" />

                <asp:TemplateField HeaderText="Qty Prepared">
                    <ItemTemplate>
                        <asp:TextBox ID="txt_QtyPrepared" runat="server" Text='<%# (Convert.ToInt32(Eval("item_pending_quantity")) <= Convert.ToInt32(Eval("current_quantity")) ? Eval("item_pending_quantity") : Eval("current_quantity")) %>'></asp:TextBox>
                        <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>

                        <asp:CompareValidator ID="CompareValidator_txt_QtyPrepared" runat="server" ValidationGroup='valGroup1'
                            ControlToValidate="txt_QtyPrepared" ErrorMessage="Can't be -ve number."
                            Operator="GreaterThanEqual" Type="Integer" ForeColor="Red"
                            ValueToCompare="0" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_QtyPrepared" ValidationGroup='valGroup1' ID="RequiredValidator_txt_QtyPrepared" ErrorMessage="Enter a number." ForeColor="Red" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_txt_QtyPrepared" runat="server" ControlToValidate="txt_QtyPrepared" ErrorMessage="Enter only numbers." ForeColor="Red" ValidationExpression="^\d+$" ValidationGroup='valGroup1'></asp:RegularExpressionValidator>

                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Adjustment">
                    <ItemTemplate>
                        <asp:Button ID="btn_Adjustment" runat="server" Text="ADJ" UseSubmitBehavior="false" OnClick="btn_Adjustment_Click" CssClass="btn btn-primary" />
                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("item_number") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>

        <asp:Label ID="Label2" runat="server" Text="Please enter a date"></asp:Label>
        <asp:TextBox ID="TextBox_Collect_Date" runat="server" ReadOnly="true"></asp:TextBox>

        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup='valGroup1' runat="server" ErrorMessage="Please enter a date" ForeColor="Red" ControlToValidate="TextBox_Collect_Date"></asp:RequiredFieldValidator>

        <asp:Calendar ID="Calendar_Collect_Date" runat="server" OnSelectionChanged="Calendar_Collect_Date_SelectionChanged" OnDayRender="Calendar_Collect_Date_DayRender"></asp:Calendar>

        <asp:Button ID="btn_readyForCollect" ValidationGroup='valGroup1' runat="server" Text="Ready for Collection" OnClick="btn_readyForCollect_Click" CssClass="btn btn-primary" />
    </div>
</asp:Content>