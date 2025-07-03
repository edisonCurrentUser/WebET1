<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarPredio.aspx.cs" Inherits="WebET1.EditarPredio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card shadow p-4 mb-4">
        <h3 class="mb-4 text-center">Editar Predio</h3>

        <asp:TextBox ID="txtCodigoCatastral" runat="server" CssClass="form-control mb-3" placeholder="Código Catastral" />
        <asp:TextBox ID="txtCodigoAnterior" runat="server" CssClass="form-control mb-3" placeholder="Código Anterior" />
        <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control mb-3" placeholder="Número" />
        <asp:TextBox ID="txtNombrePredio" runat="server" CssClass="form-control mb-3" placeholder="Nombre del Predio" />
        <asp:TextBox ID="txtAreaTerreno" runat="server" CssClass="form-control mb-3" placeholder="Área del Terreno" />
        <asp:TextBox ID="txtAreaConstruccion" runat="server" CssClass="form-control mb-3" placeholder="Área de Construcción" />
        <asp:TextBox ID="txtFondoRelativo" runat="server" CssClass="form-control mb-3" placeholder="Fondo Relativo" />
        <asp:TextBox ID="txtFrenteFondo" runat="server" CssClass="form-control mb-3" placeholder="Frente Fondo" />
        <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control mb-3" placeholder="Observaciones" />
        <asp:TextBox ID="txtDimTomadoPlanos" runat="server" CssClass="form-control mb-3" placeholder="Dimensiones Tomadas de Planos" />
        <asp:TextBox ID="txtOtraFuenteInfo" runat="server" CssClass="form-control mb-3" placeholder="Otra Fuente de Información" />
        <asp:TextBox ID="txtNumNuevoBloque" runat="server" CssClass="form-control mb-3" placeholder="Número Nuevo Bloque" />
        <asp:TextBox ID="txtNumAmpliBloque" runat="server" CssClass="form-control mb-3" placeholder="Número Ampliación Bloque" />
        <asp:TextBox ID="txtTipo" runat="server" CssClass="form-control mb-3" placeholder="Tipo" />
        <asp:TextBox ID="txtEstado" runat="server" CssClass="form-control mb-3" placeholder="Estado" />
        <asp:TextBox ID="txtDominio" runat="server" CssClass="form-control mb-3" placeholder="Dominio" />
        <asp:TextBox ID="txtDireccionPrincipal" runat="server" CssClass="form-control mb-3" placeholder="Dirección Principal" />
        <asp:TextBox ID="txtNumHabitantes" runat="server" CssClass="form-control mb-3" placeholder="Número de Habitantes" />
        <asp:TextBox ID="txtPropietarioAnterior" runat="server" CssClass="form-control mb-3" placeholder="Propietario Anterior" />

        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />

    </div>
</asp:Content>

