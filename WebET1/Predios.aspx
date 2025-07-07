<%@ Page Title="Listado de Predios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Predios.aspx.cs" Inherits="WebET1.Predios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card shadow p-4 mb-4">
        <h3 class="mb-4 text-center">Listado de Predios</h3>

        <asp:Button ID="btnAgregarPredio" runat="server" Text="Agregar Predio" CssClass="btn btn-success mb-3" OnClick="btnAgregarPredio_Click" />

        <asp:GridView ID="GridViewPredios" runat="server" CssClass="table table-bordered table-striped display"
                      AutoGenerateColumns="False"
                      AllowPaging="True" PageSize="10"
                      OnPageIndexChanging="GridViewPredios_PageIndexChanging"
                      OnRowCommand="GridViewPredios_RowCommand"
                      DataKeyNames="pre_id">

            <PagerSettings Mode="NumericFirstLast"
                           FirstPageText="« Primero"
                           LastPageText="Último »"
                           NextPageText="Siguiente ›"
                           PreviousPageText="‹ Anterior" />
            <PagerStyle CssClass="pagination-container" />

            <Columns>
                <asp:BoundField DataField="pre_id" HeaderText="ID" />
                <asp:BoundField DataField="pre_codigo_catastral" HeaderText="Código Catastral" />
                <asp:BoundField DataField="pre_nombre_predio" HeaderText="Nombre del Predio" />
                <asp:BoundField DataField="pre_direccion_principal" HeaderText="Dirección" />
                <asp:BoundField DataField="pre_area_total_ter" HeaderText="Área Terreno" />
                <asp:BoundField DataField="pre_area_total_const" HeaderText="Área Construcción" />
                <asp:BoundField DataField="pre_num_habitantes" HeaderText="Habitantes" />
                <asp:BoundField DataField="pre_estado" HeaderText="Estado" />

                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnEditar" runat="server" CommandName="Editar" Text="Editar"
                            CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" CssClass="btn btn-warning btn-sm" />

                        <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" Text="Eliminar"
                            CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" CssClass="btn btn-danger btn-sm ml-2"
                            OnClientClick="return confirm('¿Está seguro de eliminar este registro?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
