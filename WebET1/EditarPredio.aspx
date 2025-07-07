<%@ Page Title="Editar Predio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarPredio.aspx.cs" Inherits="WebET1.EditarPredio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card shadow p-4 mb-4">
        <h3 class="mb-4 text-center">Editar Predio</h3>

        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger" />

        <div class="form-group">
            <label>Código Catastral:</label>
            <asp:TextBox ID="txtCodigoCatastral" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label>Código Anterior:</label>
            <asp:TextBox ID="txtCodigoAnterior" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label>Número:</label>
            <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label>Nombre del Predio:</label>
            <asp:TextBox ID="txtNombrePredio" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label>Área del Terreno (m²):</label>
            <asp:TextBox ID="txtAreaTerreno" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label>Área de Construcción (m²):</label>
            <asp:TextBox ID="txtAreaConstruccion" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label>Estado:</label>
            <asp:TextBox ID="txtEstado" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label>Dominio:</label>
            <asp:TextBox ID="txtDominio" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label>Dirección Principal:</label>
            <asp:TextBox ID="txtDireccionPrincipal" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label>Número de Habitantes:</label>
            <asp:TextBox ID="txtNumHabitantes" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label>Propietario Anterior:</label>
            <asp:TextBox ID="txtPropietarioAnterior" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label>Manzana:</label>
            <asp:DropDownList ID="ddlManzana" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                <asp:ListItem Text="-- Seleccione --" Value="" />
            </asp:DropDownList>
        </div>

        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-success mt-3" OnClick="btnGuardar_Click" />
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary mt-3 ml-2" OnClick="btnCancelar_Click" />

    </div>

</asp:Content>
