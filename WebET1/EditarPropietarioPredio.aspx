<%@ Page Title="Editar Propietario - Predio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarPropietarioPredio.aspx.cs" Inherits="WebET1.EditarPropietarioPredio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card shadow p-4 mb-4">
        <h3 class="mb-4 text-center">Editar Propietario - Predio</h3>

        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger" />

        <div class="form-group">
            <label>Propietario:</label>
            <asp:DropDownList ID="ddlPropietario" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                <asp:ListItem Text="-- Seleccione --" Value="" />
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvPropietario" runat="server" ControlToValidate="ddlPropietario" InitialValue="" ErrorMessage="Seleccione un propietario." CssClass="text-danger" />
        </div>

        <div class="form-group">
            <label>Predio:</label>
            <asp:DropDownList ID="ddlPredio" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                <asp:ListItem Text="-- Seleccione --" Value="" />
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvPredio" runat="server" ControlToValidate="ddlPredio" InitialValue="" ErrorMessage="Seleccione un predio." CssClass="text-danger" />
        </div>

        <div class="form-group">
            <label>Alícuota (%):</label>
            <asp:TextBox ID="txtAlicuota" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvAlicuota" runat="server" ControlToValidate="txtAlicuota" ErrorMessage="Ingrese la alícuota." CssClass="text-danger" />
        </div>

        <div class="form-group">
            <label>Años de Posesión:</label>
            <asp:TextBox ID="txtAniosPosesion" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvAniosPosesion" runat="server" ControlToValidate="txtAniosPosesion" ErrorMessage="Ingrese los años de posesión." CssClass="text-danger" />
        </div>

        <div class="form-group">
            <label>Observación:</label>
            <asp:TextBox ID="txtObservacion" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label>Tiene Escritura:</label>
            <asp:DropDownList ID="ddlTieneEscritura" runat="server" CssClass="form-control">
                <asp:ListItem Text="-- Seleccione --" Value="" />
                <asp:ListItem Text="Sí" Value="1" />
                <asp:ListItem Text="No" Value="0" />
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvTieneEscritura" runat="server" ControlToValidate="ddlTieneEscritura" InitialValue="" ErrorMessage="Seleccione si tiene escritura." CssClass="text-danger" />
        </div>

        <div class="form-group">
            <label>Fecha de Inscripción:</label>
            <asp:TextBox ID="txtFechaInscripcion" runat="server" CssClass="form-control" TextMode="Date" />
            <asp:RequiredFieldValidator ID="rfvFechaInscripcion" runat="server" ControlToValidate="txtFechaInscripcion" ErrorMessage="Ingrese la fecha de inscripción." CssClass="text-danger" />
        </div>

        <div class="form-group">
            <label>Fecha de Registro:</label>
            <asp:TextBox ID="txtFechaRegistro" runat="server" CssClass="form-control" TextMode="Date" />
            <asp:RequiredFieldValidator ID="rfvFechaRegistro" runat="server" ControlToValidate="txtFechaRegistro" ErrorMessage="Ingrese la fecha de registro." CssClass="text-danger" />
        </div>

        <div class="form-group">
            <label>Área de Escritura (m²):</label>
            <asp:TextBox ID="txtAreaEscritura" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvAreaEscritura" runat="server" ControlToValidate="txtAreaEscritura" ErrorMessage="Ingrese el área de escritura." CssClass="text-danger" />
        </div>

        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-success mt-3" OnClick="btnGuardar_Click" />
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary mt-3 ml-2" OnClick="btnCancelar_Click" />
    </div>

</asp:Content>
