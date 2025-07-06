<%@ Page Title="Listado de Propietarios-Predios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PropietariosPredios.aspx.cs" Inherits="WebET1.PropietarisoPredios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Listado de Propietarios - Predios</h2>

    <asp:Button ID="btnAgregar" runat="server" Text="Agregar Nuevo" CssClass="btn btn-primary mb-3" OnClick="btnAgregar_Click" />

    <asp:GridView ID="GridViewPropietariosPredios" runat="server"
        AutoGenerateColumns="False"
        AllowPaging="True"
        PageSize="10"
        CssClass="table table-bordered table-striped"
        OnPageIndexChanging="GridViewPropietariosPredios_PageIndexChanging"
        OnRowCommand="GridViewPropietariosPredios_RowCommand"
        DataKeyNames="prp_id"> 

        <Columns>
            <asp:BoundField DataField="prp_id" HeaderText="ID" />
            <asp:BoundField DataField="pro_id" HeaderText="Propietario ID" />
            <asp:BoundField DataField="pre_id" HeaderText="Predio ID" />
            <asp:BoundField DataField="prp_alicuota" HeaderText="Alícuota" />
            <asp:BoundField DataField="prp_anios_posesion" HeaderText="Años Posesión" />
            <asp:BoundField DataField="prp_tiene_escritura" HeaderText="Tiene Escritura" />
            <asp:BoundField DataField="prp_fecha_inscripcion" HeaderText="Fecha Inscripción" />
            <asp:BoundField DataField="prp_fecha_registro" HeaderText="Fecha Registro" />
            <asp:BoundField DataField="prp_area_escritura" HeaderText="Área Escritura" />

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:Button ID="btnEditar" runat="server" CommandName="Editar" Text="Editar" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-warning btn-sm" />
                    <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" Text="Eliminar" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-danger btn-sm ml-2" OnClientClick="return confirm('¿Está seguro de eliminar este registro?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
