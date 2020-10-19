<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AssegnaPatrimonio.aspx.vb" Inherits="ANAUT_AssegnaPatrimonio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Selezionato;

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link type="text/css" href="css/smoothness/jquery-ui-1.8.23.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.23.custom.min.js"></script>
    <script type="text/javascript" src="js/jquery.ui.datepicker-it.js"></script>
    <script type="text/javascript" src="js/jsfunzioni.js"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
<base target="_self"/>
    <script type="text/javascript" language="javascript">
        window.name = "modal";
        </script>
    <title>Assegna Patrimonio</title>
    <style type="text/css">
        .style1
        {
            color: #FFFFFF;
        }
        #contenitore
        {
            top: 541px;
            left: 235px;
        }
        .style2
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10pt;
        }
        .style3
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10pt;
            height: 26px;
        }
        .style4
        {
            height: 26px;
            text-align: left;
        }
        #conte
        {
            top: 68px;
            left: 3px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" target="modal">
                <asp:ScriptManager ID="ScriptManagerBando" runat="server">
    </asp:ScriptManager>
    <div>
    
        <table style="width:100%;">
            <tr bgcolor="Maroon">
                <td class="style1" style="text-align: center; font-weight: 700">
                    <asp:Label ID="lblTitolo" runat="server" Font-Names="arial" Font-Size="10pt">ASSEGNA PATRIMONIO IMMOBILIARE</asp:Label>
                </td>
            </tr>
            <tr bgcolor="#FFFFCC">
                <td class="style1" style="text-align: center; font-weight: 700">
                    <asp:Label ID="lblTitolo0" runat="server" ForeColor="Black" Font-Names="arial" 
                        Font-Size="8pt">Elenco Unità non ancora assegnati</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                <div id="conte" 
                        
                        style="border: 1px solid #000080; overflow: auto; position: absolute; width: 575px; height: 332px">
                    <table style="width:100%;">
                        <tr>
                            <td width="80%">
                                <asp:CheckBoxList style="position:absolute; top: 0px; left: 0px;" 
                        ID="ListaVoci" runat="server" Font-Names="arial" 
                        Font-Size="9pt">
                    </asp:CheckBoxList></td>
                        </tr>
                    </table>
                    </div>
                        
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                                &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">
                                <img id="imgChiudi" alt="" src="../NuoveImm/Img_EsciCorto.png" 
                                    style="cursor:pointer;position:absolute; top: 442px; left: 523px;" 
                                    onclick="self.close();"/></td>
            </tr>
        </table>
    <asp:HiddenField ID="LBLID" runat="server" Value="" />
    <asp:HiddenField ID="H1" runat="server" Value="" />
    </div>
    <div id="contenitore" 
        
        
        style="position: absolute; width: 600px; height: 480px; visibility: hidden; background-color: #CCCCCC; top: 0px; left: 0px;">
    
        <table style="width: 100%;">
            <tr>
                <td height="100%" style="text-align: center" valign="middle" width="100%">
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <table align="center" 
                        style="border: 2px solid #0000FF; width: 50%; background-color: #FFFFFF;">
                        <tr>
                            <td class="style2" style="text-align: left">
                                &nbsp; &nbsp;</td>
                            <td>
                                &nbsp; &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2" style="text-align: left">
                                Giorno</td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="cmbGiorno" runat="server" TabIndex="1">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style3" style="text-align: left">
                                Mese</td>
                            <td class="style4">
                                <asp:DropDownList ID="cmbMese" runat="server" TabIndex="2">
                                </asp:DropDownList>
                            </td>
                            <td class="style4">
                                </td>
                        </tr>
                        <tr>
                            <td class="style3" style="text-align: left">
                                Anno</td>
                            <td class="style4">
                                <asp:DropDownList ID="cmbAnno" runat="server" TabIndex="3">
                                </asp:DropDownList>
                            </td>
                            <td class="style4">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style3" style="text-align: left">
                                Note</td>
                            <td class="style4">
                                <asp:TextBox ID="txtNote" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    Width="307px" TabIndex="4"></asp:TextBox>
                            </td>
                            <td class="style4">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2" style="text-align: left">
                                &nbsp;</td>
                            <td>
                                &nbsp; &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td style="text-align: right">
                                <asp:ImageButton ID="imgSalva" runat="server" 
                                    ImageUrl="~/NuoveImm/Img_Salva.png" onclientclick="Sparisci();" 
                                    TabIndex="5" />
                            </td>
                            <td>
                                <img id="imgAnnulla" alt="" src="../NuoveImm/Img_Esci.png" style="cursor:pointer" onclick="Sparisci();"/></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    <asp:ImageButton ID="btnEliminaGiorno" runat="server" 
                ImageUrl="~/NuoveImm/Img_Elimina.png" 
                style="position:absolute; top: -100px; left: -100px;"/>
    </div>
    <script type="text/javascript" language="javascript">
        function Conta() {
            var contatore;
            contatore = 0;
            re = new RegExp(':' + document.getElementById('ChSelezionato') + '$')  //generated control
            for (i = 0; i < document.forms[0].elements.length; i++) {
                elm = document.forms[0].elements[i]
                if (elm.type == 'checkbox') {
                    if (elm.checked == true) {
                        contatore = contatore + 1;
                    }
                }
            }
            if (document.all) {
                document.getElementById('lblSelezionati').innerText = contatore;
            }
            else {
                document.getElementById('lblSelezionati').textContent = contatore;
            }

        }

        function VerificaSelezionato() {

            if (document.getElementById('LBLID').value != '-1' && document.getElementById('LBLID').value != '') {
                document.getElementById('H1').value = '1';
                SceltaFunzione('Eliminare il giorno selezionato? Non sarà più possibile annullare questa operazione.');
            }
            else {
                document.getElementById('H1').value = '0';
                Messaggio('Selezionare un giorno dalla lista!');
            }
        }

        function Messaggio(TestoMessaggio) {

            $(document).ready(function () {
                $('#ScriptMsg').text(TestoMessaggio);
                $('#ScriptMsg').dialog({ autoOpen: true, modal: true, show: 'blind', hide: 'explode', title: 'Attenzione...', buttons: { 'Ok': function () { $(this).dialog('close'); } } });
            });
        }

        function SceltaFunzione(TestoMessaggio) {
            $(document).ready(function () {
                $('#ScriptScelta').text(TestoMessaggio);
                $('#ScriptScelta').dialog({ autoOpen: true, modal: true, show: 'blind', hide: 'explode', title: 'Attenzione', buttons: { 'Si': function () { __doPostBack('btnEliminaGiorno', ''); { $(this).dialog('close'); } }, 'No': function () { $(this).dialog('close'); " & Funzione2 & " } } });
            });
        }

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            
        </ContentTemplate>
        </asp:UpdatePanel>
                        <div id="ScriptMsg" title="Basic dialog" style="display: none; font-size: 10pt; font-family: Arial;
        width: 1000px">
    </div>
<div id="ScriptScelta" title="Finestra di Conferma" style="display: none; font-size: 10pt;
        font-family: Arial">
    </div>
                <asp:ImageButton ID="ImageButton1" runat="server" 
                    ImageUrl="~/NuoveImm/img_SalvaModelli.png" 
                    style="position:absolute; top: 443px; left: 433px;"/>
       </form>
</body>
<script language="javascript" type="text/javascript">
    document.getElementById('divLoading').style.visibility = 'hidden';
</script>
</html>
