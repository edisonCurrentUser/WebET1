<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Propietarios.aspx.cs" Inherits="WebET1.Propietarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Listado de Propietarios</h2>

    <asp:Button ID="btnAgregar" runat="server" Text="Agregar Nuevo" CssClass="btn btn-primary mb-3" OnClick="btnAgregar_Click" />

    <asp:GridView ID="GridViewPropietarios" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10"
        CssClass="table table-bordered table-striped" DataKeyNames="pro_id"
        OnPageIndexCha  nging="GridViewPropietarios_PageIndexChanging"
        OnRowCommand="GridViewPropietarios_RowCommand">
        <Columns>
            <asp:BoundField DataField="pro_id" HeaderText="ID" />
            <asp:BoundField DataField="pro_num_identificacion" HeaderText="Identificación" />
            <asp:BoundField DataField="pro_nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="pro_apellido" HeaderText="Apellido" />
            <asp:BoundField DataField="pro_correo_electronico" HeaderText="Correo" />
            <asp:BoundField DataField="pro_telefono1" HeaderText="Teléfono" />
            <asp:BoundField DataField="pro_direccion_ciudad" HeaderText="Ciudad" />

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:Button ID="btnEditar" runat="server" CommandName="Editar" Text="Editar" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-warning btn-sm" />
                    <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" Text="Eliminar" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-danger btn-sm ml-2" OnClientClick="return confirm('¿Está seguro de eliminar este registro?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
