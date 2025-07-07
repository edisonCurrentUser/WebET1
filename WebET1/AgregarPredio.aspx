<%@ Page Title="Agregar Predio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgregarPredio.aspx.cs" Inherits="WebET1.AgregarPredio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card shadow p-4 mb-4">
        <h3 class="mb-4 text-center">Agregar Predio</h3>

        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger" />

        <div class="form-group">
            <label>Código Catastral:</label>
            <asp:TextBox ID="txtCodigoCatastral" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvCodigoCatastral" runat="server" ControlToValidate="txtCodigoCatastral" ErrorMessage="Ingrese el código catastral." CssClass="text-danger" />
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
            <asp:RequiredFieldValidator ID="rfvNombrePredio" runat="server" ControlToValidate="txtNombrePredio" ErrorMessage="Ingrese el nombre del predio." CssClass="text-danger" />
        </div>

        <div class="form-group">
            <label>Área del Terreno (m²):</label>
            <asp:TextBox ID="txtAreaTerreno" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvAreaTerreno" runat="server" ControlToValidate="txtAreaTerreno" ErrorMessage="Ingrese el área del terreno." CssClass="text-danger" />
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
            <asp:RequiredFieldValidator ID="rfvDireccionPrincipal" runat="server" ControlToValidate="txtDireccionPrincipal" ErrorMessage="Ingrese la dirección principal." CssClass="text-danger" />
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
            <asp:RequiredFieldValidator ID="rfvManzana" runat="server" ControlToValidate="ddlManzana" InitialValue="" ErrorMessage="Seleccione una manzana." CssClass="text-danger" />
        </div>
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary mt-3" OnClick="btnGuardar_Click" CausesValidation="false" />

        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary mt-3 ml-2" OnClick="btnCancelar_Click" />
    </div>

</asp:Content>
