<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BandoAU.aspx.vb" Inherits="BANDO_BandoERP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
<base target="_self"/>
    <link type="text/css" href="css/smoothness/jquery-ui-1.8.23.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.23.custom.min.js"></script>
    <script type="text/javascript" src="js/jquery.ui.datepicker-it.js"></script>
    <script type="text/javascript" src="js/jsfunzioni.js"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    
    <title>Anagrafe Utenza</title>
    <style type="text/css">
        .style1
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: medium;
            font-weight: bold;
            color: #FFFFFF;
        }
        
   
     .CssMaiuscolo
    {
        text-transform: uppercase;
    text-align: left;
}   


    </style>
</head>

<script type="text/javascript">
        function CompletaData(e, obj) {
            // Check if the key is a number
            var sKeyPressed;

            sKeyPressed = (window.event) ? event.keyCode : e.which;

            if (sKeyPressed < 48 || sKeyPressed > 57) {
                if (sKeyPressed != 8 && sKeyPressed != 0) {
                    // don't insert last non-numeric character
                    if (window.event) {
                        event.keyCode = 0;
                    }
                    else {
                        e.preventDefault();
                    }
                }
            }
            else {
                if (obj.value.length == 2) {
                    obj.value += "/";
                }
                else if (obj.value.length == 5) {
                    obj.value += "/";
                }
                else if (obj.value.length > 9) {
                    var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                    if (selText.length == 0) {
                        // make sure the field doesn't exceed the maximum length
                        if (window.event) {
                            event.keyCode = 0;
                        }
                        else {
                            e.preventDefault();
                        }
                    }
                }
            }
        }
</script>
<body>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManagerBando" runat="server">
    </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

    <div>
    
        <table style="width: 100%;">
            <tr>
                <td bgcolor="#990000" class="style1" style="text-align: center">
                    DATI ANAGRAFE UTENZA</td>
            </tr>
            <tr>
                <td>
                    &nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table style="width:100%;">
                        <tr>
                            <td>
                                <asp:Label ID="lblCognome" runat="server" Text="Descrizione" Font-Names="arial" 
                                    Font-Size="10pt"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescrizione" runat="server" Font-Names="Arial" 
                                    Font-Size="10pt" Width="343px" CssClass="CssMaiuscolo" TabIndex="1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCognome0" runat="server" Text="Anno" Font-Names="arial" 
                                    Font-Size="10pt"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtannoAU" runat="server" CssClass="CssMaiuscolo" 
                                    Font-Names="Arial" Font-Size="10pt" MaxLength="4" TabIndex="2" Width="48px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCognome3" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    Text="Anno Redditi"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbAnnoRedditi" runat="server" AutoPostBack="True" 
                                    CausesValidation="True" Width="73px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCognome5" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    Text="Tasso Rendimento Annuo"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTasso" runat="server" CssClass="CssMaiuscolo" 
                                    Font-Names="Arial" Font-Size="10pt" MaxLength="4" TabIndex="4" 
                                    Width="48px" Enabled="False"></asp:TextBox>
                                &nbsp;<asp:Label ID="lblCognome6" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    Text="%"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCognome1" runat="server" Text="Data Inizio Raccolta" 
                                    Font-Names="arial" Font-Size="10pt"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataInizio" runat="server" Font-Names="Arial" 
                                    Font-Size="10pt" Width="81px" CssClass="CssMaiuscolo" TabIndex="5"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCognome2" runat="server" Text="Data Fine Raccolta" Font-Names="arial" 
                                    Font-Size="10pt"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataFine" runat="server" Font-Names="Arial" 
                                    Font-Size="10pt" Width="81px" CssClass="CssMaiuscolo" TabIndex="6"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCognome7" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    Text="Data Inizio Validità Canone"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataInizio0" runat="server" CssClass="CssMaiuscolo" 
                                    Font-Names="Arial" Font-Size="10pt" TabIndex="7" Width="81px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCognome8" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    Text="Data Fine Validità Canone"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataFine0" runat="server" CssClass="CssMaiuscolo" 
                                    Font-Names="Arial" Font-Size="10pt" TabIndex="8" Width="81px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="lblCognome4" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    Text="Tipologia Contratti da verificare"></asp:Label>
                            </td>
                            <td>
                                <table style="width:100%;">
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="ChERP1" runat="server" Font-Names="ARIAL" Font-Size="8pt" 
                                                TabIndex="9" Text="ERP SOCIALE" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="Ch43198" runat="server" Font-Names="ARIAL" Font-Size="8pt" 
                                                TabIndex="14" Text="431/98" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="ChERP2" runat="server" Font-Names="ARIAL" Font-Size="8pt" 
                                                TabIndex="10" Text="ERP MODERATO" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="Ch39278" runat="server" Font-Names="ARIAL" Font-Size="8pt" 
                                                TabIndex="15" Text="392/78" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="ChERP6" runat="server" Font-Names="ARIAL" Font-Size="8pt" 
                                                TabIndex="11" Text="ERP FORZE DELL'ORDINE" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="ChERP3" runat="server" Font-Names="ARIAL" Font-Size="8pt" 
                                                TabIndex="16" Text="ERP Contratti precari Art. 15 let. a, b" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="ChERP7" runat="server" Font-Names="ARIAL" Font-Size="8pt" 
                                                TabIndex="12" Text="ERP ART. 22 C.10" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="ChERP4" runat="server" Font-Names="ARIAL" Font-Size="8pt" 
                                                TabIndex="17" Text="ERP art.15 comma 2-vizi amministrativi" 
                                                Enabled="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="ChERP8" runat="server" Font-Names="ARIAL" Font-Size="8pt" 
                                                TabIndex="13" Text="ERP CONVENZIONATO" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="ChERP5" runat="server" Font-Names="ARIAL" Font-Size="8pt" 
                                                TabIndex="18" Text="ERP Legge 10/86" Enabled="False" Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="ChERP9" runat="server" Font-Names="ARIAL" Font-Size="8pt" 
                                                TabIndex="13" Text="OCCUPAZIONI ABUSIVE" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: xx-small; font-weight: 700;">
                    Le date di inizio e fine ANAGRAFE UTENZA sono indicative. Le operazioni di 
                    inserimento dichiarazioni sono subordinate allo stato del bando. L&#39;apertura e la 
                    chiusura sono quindi indipendenti da tali date.</td>
            </tr>
            <tr>
                <td style="text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: xx-small; font-weight: 700;">
                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Size="10pt" 
                        NavigateUrl="~/ANAUT/GestioneTabelleBando.aspx" Target="_blank">Clicca qui per gestire le tabelle ISEE e indici ISTAT</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="lblErrore" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                    <asp:ImageButton ID="btnSalva" runat="server" 
                        ImageUrl="~/NuoveImm/Img_SalvaVal.png" TabIndex="19" 
                        style="height: 16px" />
                    &nbsp; &nbsp;<img id="btnAnnulla" alt="" src="../NuoveImm/Img_AnnullaVal.png" onclick="self.close();" style="cursor:pointer"/></td>
            </tr>
        </table>
    
    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <p>
            &nbsp;</p>
                <div id="ScriptMsg" title="Basic dialog" style="display: none; font-size: 10pt; font-family: Arial;
        width: 1000px">
    </div>
    </form>
</body>
</html>
