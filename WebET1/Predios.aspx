<%@ Page Title="Listado de Predios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Predios.aspx.cs" Inherits="WebET1.Predios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card shadow p-4 mb-4">
        <h3 class="mb-4 text-center">Listado de Predios</h3>

        <asp:Button ID="btnAgregarPredio" runat="server" Text="Agregar Predio" CssClass="btn btn-success mb-3" OnClick="btnAgregarPredio_Click" />

        <asp:GridView ID="GridViewPredios" runat="server" CssClass="table table-bordered table-striped display"
                      AutoGenerateColumns="False"
                      AllowPaging="True" PageSize="10"
                      OnPageIndexChanging="GridViewPredios_PageIndexChanging"
                      OnRowEditing="GridViewPredios_RowEditing"
                      OnRowDeleting="GridViewPredios_RowDeleting"
                      DataKeyNames="pre_id">

            <PagerSettings Mode="NumericFirstLast"
                           FirstPageText="« Primero"
                           LastPageText="Último »"
                           NextPageText="Siguiente ›"
                           PreviousPageText="‹ Anterior" />
            <PagerStyle CssClass="pagination-container" />

            <Columns>
                <asp:BoundField DataField="pre_id" HeaderText="ID" SortExpression="pre_id" />
                <asp:BoundField DataField="pre_codigo_catastral" HeaderText="Código Catastral" SortExpression="pre_codigo_catastral" />
                <asp:BoundField DataField="pre_nombre_predio" HeaderText="Nombre del Predio" SortExpression="pre_nombre_predio" />
                <asp:BoundField DataField="pre_direccion_principal" HeaderText="Dirección" SortExpression="pre_direccion_principal" />
                <asp:BoundField DataField="pre_area_total_ter" HeaderText="Área Terreno" SortExpression="pre_area_total_ter" />
                <asp:BoundField DataField="pre_area_total_const" HeaderText="Área Construcción" SortExpression="pre_area_total_const" />
                <asp:BoundField DataField="pre_num_habitantes" HeaderText="Habitantes" SortExpression="pre_num_habitantes" />
                <asp:BoundField DataField="pre_estado" HeaderText="Estado" SortExpression="pre_estado" />

                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
