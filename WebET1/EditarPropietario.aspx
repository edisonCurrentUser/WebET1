<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarPropietario.aspx.cs" Inherits="WebET1.EditarPropietario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Editar Propietario</h2>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger" />

    <div class="form-group">
        <label>Tipo de Identificación:</label>
        <asp:TextBox ID="txtTipoIdentificacion" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label>Número de Identificación:</label>
        <asp:TextBox ID="txtNumIdentificacion" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label>Nombre:</label>
        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label>Apellido:</label>
        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label>Ciudad:</label>
        <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label>Correo Electrónico:</label>
        <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label>Teléfono:</label>
        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
    </div>

    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-success mt-3" OnClick="btnGuardar_Click" />
    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary mt-3 ml-2" OnClick="btnCancelar_Click" />
</asp:Content>
