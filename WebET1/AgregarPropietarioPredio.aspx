﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgregarPropietarioPredio.aspx.cs" Inherits="WebET1.AgregarPropietarioPredio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Agregar Propietario - Predio</h2>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger" />

    <div class="form-group">
        <label>Propietario:</label>
        <asp:DropDownList ID="ddlPropietario" runat="server" CssClass="form-control" AppendDataBoundItems="true">
            <asp:ListItem Text="Seleccione un Propietario" Value="0" />
        </asp:DropDownList>
    </div>

    <div class="form-group">
        <label>Predio:</label>
        <asp:DropDownList ID="ddlPredio" runat="server" CssClass="form-control" AppendDataBoundItems="true">
            <asp:ListItem Text="Seleccione un Predio" Value="0" />
        </asp:DropDownList>
    </div>

    <div class="form-group">
        <label>Alícuota:</label>
        <asp:TextBox ID="txtAlicuota" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label>Años de Posesión:</label>
        <asp:TextBox ID="txtAniosPosesion" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label>Observación:</label>
        <asp:TextBox ID="txtObservacion" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label>Tiene Escritura (1 = Sí, 0 = No):</label>
        <asp:TextBox ID="txtTieneEscritura" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label>Fecha Inscripción:</label>
        <asp:TextBox ID="txtFechaInscripcion" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label>Fecha Registro:</label>
        <asp:TextBox ID="txtFechaRegistro" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label>Área Escritura:</label>
        <asp:TextBox ID="txtAreaEscritura" runat="server" CssClass="form-control" />
    </div>

    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success mt-3" OnClick="btnGuardar_Click" />
    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary mt-3 ml-2" OnClick="btnCancelar_Click" />
</asp:Content>
