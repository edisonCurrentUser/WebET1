<%@ Page Title="Agregar Propietario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgregarPropietario.aspx.cs" Inherits="WebET1.AgregarPropietario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Agregar Propietario</h2>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger" />

    <div class="form-group">
        <label>Tipo de Identificación:</label>
        <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="form-control" AppendDataBoundItems="true">
            <asp:ListItem Text="-- Seleccione --" Value="" />
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvTipoIdentificacion" runat="server" ControlToValidate="ddlTipoIdentificacion"
            InitialValue="" ErrorMessage="Seleccione un tipo de identificación." CssClass="text-danger" />
    </div>

    <div class="form-group">
        <label>Número de Identificación:</label>
        <asp:TextBox ID="txtNumIdentificacion" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvNumIdentificacion" runat="server" ControlToValidate="txtNumIdentificacion"
            ErrorMessage="Ingrese el número de identificación." CssClass="text-danger" />
    </div>

    <div class="form-group">
        <label>Nombre:</label>
        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre"
            ErrorMessage="Ingrese el nombre." CssClass="text-danger" />
    </div>

    <div class="form-group">
        <label>Apellido:</label>
        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ControlToValidate="txtApellido"
            ErrorMessage="Ingrese el apellido." CssClass="text-danger" />
    </div>

    <div class="form-group">
        <label>Ciudad:</label>
        <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvCiudad" runat="server" ControlToValidate="txtCiudad"
            ErrorMessage="Ingrese la ciudad." CssClass="text-danger" />
    </div>

    <div class="form-group">
        <label>Correo Electrónico:</label>
        <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvCorreo" runat="server" ControlToValidate="txtCorreo"
            ErrorMessage="Ingrese el correo electrónico." CssClass="text-danger" />
    </div>

    <div class="form-group">
        <label>Teléfono:</label>
        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" ControlToValidate="txtTelefono"
            ErrorMessage="Ingrese el teléfono." CssClass="text-danger" />
    </div>

    <div class="form-group">
        <label>Estado Civil:</label>
        <asp:DropDownList ID="ddlEstadoCivil" runat="server" CssClass="form-control" AppendDataBoundItems="true">
            <asp:ListItem Text="-- Seleccione --" Value="" />
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvEstadoCivil" runat="server" ControlToValidate="ddlEstadoCivil"
            InitialValue="" ErrorMessage="Seleccione el estado civil." CssClass="text-danger" />
    </div>

    <div class="form-group">
        <label>Tipo Conadis:</label>
        <asp:DropDownList ID="ddlTipoConadis" runat="server" CssClass="form-control" AppendDataBoundItems="true">
            <asp:ListItem Text="-- Seleccione --" Value="" />
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvTipoConadis" runat="server" ControlToValidate="ddlTipoConadis"
            InitialValue="" ErrorMessage="Seleccione el tipo Conadis." CssClass="text-danger" />
    </div>

    <div class="form-group">
        <label>Tipo de Entidad:</label>
        <asp:DropDownList ID="ddlTipoEntidad" runat="server" CssClass="form-control" AppendDataBoundItems="true">
            <asp:ListItem Text="-- Seleccione --" Value="" />
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvTipoEntidad" runat="server" ControlToValidate="ddlTipoEntidad"
            InitialValue="" ErrorMessage="Seleccione el tipo de entidad." CssClass="text-danger" />
    </div>

    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success mt-3" OnClick="btnGuardar_Click" />
    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary mt-3 ml-2" OnClick="btnCancelar_Click" />

</asp:Content>
