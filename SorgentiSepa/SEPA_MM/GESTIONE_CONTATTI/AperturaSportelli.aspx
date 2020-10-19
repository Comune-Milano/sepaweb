<%@ Page Title="" Language="VB" MasterPageFile="~/GESTIONE_CONTATTI/HomePage.master"
    AutoEventWireup="false" CodeFile="AperturaSportelli.aspx.vb" Inherits="GESTIONE_CONTATTI_AperturaSportelli" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1 {
            width: 9%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label runat="server" ID="lblTitolo" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPFooter" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Button ID="btnSalva" runat="server" Text="Salva" CssClass="bottone" />
            </td>
            <td>
                <asp:Button ID="btnEsci" runat="server" Text="Esci" CssClass="bottone" />
            </td>
            <td>
                <asp:Button ID="btnElimina" runat="server" Text="Button" Style="display: none" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table border="0" cellpadding="2" cellspacing="2" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table border="0" cellpadding="2" cellspacing="2" width="100%">
                    <tr>
                        <td class="style1">Sede territoriale
                        </td>
                        <td style="width: 85%">
                            <asp:DropDownList ID="DropDownListSedeTerritoriale" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">Apertura dal*
                        </td>
                        <td style="width: 85%">
                            <asp:TextBox ID="TextBoxDal" runat="server" Width="70px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">Apertura al*
                        </td>
                        <td style="width: 85%">
                            <asp:TextBox ID="TextBoxAl" runat="server" Width="70px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Giorno*
                        </td>
                        <td>
                            <asp:CheckBoxList ID="chkGiorni" runat="server" Font-Names="Arial" Font-Size="8pt">
                                <asp:ListItem Value="Lunedì">Lunedì</asp:ListItem>
                                <asp:ListItem Value="Martedì">Martedì</asp:ListItem>
                                <asp:ListItem Value="Mercoledì">Mercoledì</asp:ListItem>
                                <asp:ListItem Value="Giovedì">Giovedì</asp:ListItem>
                                <asp:ListItem Value="Venerdì">Venerdì</asp:ListItem>
                                <asp:ListItem Value="Sabato">Sabato</asp:ListItem>
                                <asp:ListItem Value="Domenica">Domenica</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <table border="0" cellpadding="5" cellspacing="5">
                    <tr>
                        <td>Fascia oraria
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" />
                            SP 1
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" />
                            SP 2
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox3" runat="server" AutoPostBack="True" />
                            SP 3
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox4" runat="server" AutoPostBack="True" />
                            SP 4
                        </td>
                    </tr>
                    <tr>
                        <td>Slot 09:00-09:30
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox11" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox12" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox13" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox14" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Slot 09:30-10:00
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox21" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox22" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox23" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox24" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Slot 10:00-10:30
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox31" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox32" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox33" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox34" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Slot 10:30-11:00
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox41" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox42" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox43" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox44" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Slot 11:00-11:30
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox51" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox52" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox53" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox54" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Slot 11:30-12:00
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox61" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox62" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox63" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox64" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Slot 14:00-14:30
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox71" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox72" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox73" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox74" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Slot 14:30-15:00
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox81" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox82" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox83" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox84" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Slot 15:00-15:30
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox91" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox92" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox93" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox94" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Slot 15:30-16:00
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox101" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox102" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox103" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox104" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="idEliminazione" runat="server" ClientIDMode="Static" />
    <script type="text/javascript">
        initialize();
        function initialize() {
            $("#CPContenuto_TextBoxDal").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });
            $("#CPContenuto_TextBoxAl").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });
        };
    </script>
    <script type="text/javascript">
        validNavigation = false;
    </script>
</asp:Content>
