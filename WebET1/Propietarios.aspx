<%@ Page Title="Listado de Propietarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Propietarios.aspx.cs" Inherits="WebET1.Propietarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Listado de Propietarios</h2>
    <asp:GridView ID="GridViewPropietarios" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField DataField="pro_id" HeaderText="ID" />
            <asp:BoundField DataField="pro_nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="pro_apellido" HeaderText="Apellido" />
            <asp:BoundField DataField="pro_num_identificacion" HeaderText="Identificación" />
            <asp:BoundField DataField="pro_correo_electronico" HeaderText="Correo" />
            <asp:BoundField DataField="pro_telefono1" HeaderText="Teléfono" />
            <asp:BoundField DataField="pro_direccion_ciudad" HeaderText="Ciudad" />
        </Columns>
    </asp:GridView>
</asp:Content>
