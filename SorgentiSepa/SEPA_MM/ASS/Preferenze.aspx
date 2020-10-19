<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Preferenze.aspx.vb" Inherits="ASS_Preferenze" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Preferenze Domanda</title>
    <script language="javascript" type="text/javascript">

        function TastoInvio(e) {
            sKeyPressed1 = e.which;
            if (sKeyPressed1 == 13) {
                e.preventDefault();
                //document.getElementById('USCITA').value = '0';
                //document.getElementById('txtModificato').value = '111';
            }
        }


        function $onkeydown() {

            if (event.keyCode == 13) {
                event.keyCode = 0;
                // document.getElementById('USCITA').value = '0';
                //document.getElementById('txtModificato').value = '111';
            }
        }

        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\-\,]/g
        }


        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
            o.value = o.value.replace('.', ',');
            //document.getElementById('txtModificato').value = '1';
        }









        function $onkeydown() {
            if ((event.keyCode == 46) || (event.keyCode == 8) || (event.keyCode == 116)) {
                event.keyCode = 0;
            }
        }








        function ApriStampa() {



            window.open('Doc_Preferenze/SchedaPreferenze.aspx?PROV=' + document.getElementById('Provenienza').value + '&TIPO=' + document.getElementById('Tipo').value + '&IDDOMANDA=' + document.getElementById('sValoreID').value, 'DocPreferenze', 'resizable=yes');

        }






    </script>
</head>
<body style="background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat: repeat-x;
    width: 780px;" bgcolor="#fcfcfc">

    <form id="form1" runat="server">
    <div>

     <div id="caric" style="margin: 0px; background-color: #C0C0C0; width: 100%; height: 100%;
        position: fixed; top: 0px; left: 0px; filter: alpha(opacity='75'); opacity: 0.75;
        background-color: #eeeeee; z-index: 500">
        <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
            margin-top: -48px; background-image: url('../NuoveImm/sfondo2.png');">
            <table style="width: 100%; height: 100%">
                <tr>
                    <td valign="middle" align="center">
                        <asp:Image ID="Image2" runat="server" ImageUrl="../NuoveImm/load.gif" />
                        <br />
                        <br />
                        <asp:Label ID="lblcarica" runat="server" Text="caricamento in corso..." Font-Names="Arial"
                            Font-Size="10pt"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
        <% Response.Flush()%>
        <table width="99%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="height: 35px">
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;Preferenze
                        Utente</strong></span>
                </td>
            </tr>
            <tr>
                <td height="8px">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td height="16px" valign="top">
                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Posizioni geografiche di preferenza"
                        Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>


            <td style="border: 1px solid #996600;" valign="middle" height="175px">
              
                            <table style="width: 100%; height: 175px" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td
                                        width="1%" align="center" 
                                        style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #996600">
                                        &nbsp;</td>
                                    <td style="border-style: none solid solid none; border-color: #FFFFFF #996600 #996600 #FFFFFF;
                                        height: 10px; border-bottom-width: 1px; border-right-width: 1px;"
                                        width="15%" align="center" colspan="2">
                                        <asp:Label ID="Label32" runat="server" Font-Names="Arial" Font-Size="8pt" Text="1° Opzione"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-bottom-width: 1px; border-top-style: none; border-bottom-style: solid; border-left-style: none; border-top-color: #FFFFFF; border-bottom-color: #996600; border-left-color: #FFFFFF;" 
                                        align="center" width="1%">
                                        &nbsp;</td>
                                    <td style="border-style: none solid solid none; border-color: #FFFFFF #996600 #996600 #FFFFFF;
                                        height: 10px; border-bottom-width: 1px; border-right-width: 1px;" 
                                        align="center" colspan="2">
                                        <asp:Label ID="Label48" runat="server" Font-Names="Arial" Font-Size="8pt" Text="2° Opzione"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #996600;"
                                        align="center" width="1%">
                                        &nbsp;</td>
                                    <td style="height: 10px; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #996600;"
                                        align="center" colspan="2">
                                        <asp:Label ID="Label33" runat="server" Font-Names="Arial" Font-Size="8pt" Text="3° Opzione"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px" width="1%" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" width="9%" align="left">
                                        <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Località"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left" width="23%">
                                     <asp:Label ID="lbl_localita1" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Width="160px"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left" width="10%">
                                        <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Località"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left">
                                        <asp:Label ID="lbl_localita2" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Width="160px"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left" width="10%">
                                        <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Località"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="left">
                                      <asp:Label ID="lbl_localita3" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Width="160px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Zona"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left">
                                          <asp:Label ID="lbl_zona1" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                              Width="160px"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Zona"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left">
                                        <asp:Label ID="lbl_zona2" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Width="160px"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Zona"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="lbl_zona3" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Width="160px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Quartiere"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left">
                                         <asp:Label ID="lbl_quart1" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                             Width="160px"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Quartiere"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left">
                                         <asp:Label ID="lbl_quart2" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                             Width="160px"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Quartiere"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="left">
                                          <asp:Label ID="lbl_quart3" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                              Width="160px"></asp:Label>
                                    </td>
                                </tr>
                     
                                <tr>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label56" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Complesso"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left">
                                          <asp:Label ID="lbl_complesso1" runat="server" Font-Names="Arial" 
                                              Font-Size="8pt" Width="160px"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label57" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Complesso"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left">
                                          <asp:Label ID="lbl_complesso2" runat="server" Font-Names="Arial" 
                                              Font-Size="8pt" Width="160px"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label58" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Complesso"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="left">
                                         <asp:Label ID="lbl_complesso3" runat="server" Font-Names="Arial" 
                                             Font-Size="8pt" Width="160px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label59" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Edificio"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left">
                                          <asp:Label ID="lbl_edificio1" runat="server" Font-Names="Arial" 
                                              Font-Size="8pt" Width="160px"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label60" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Edificio"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left">
                                          <asp:Label ID="lbl_edificio2" runat="server" Font-Names="Arial" 
                                              Font-Size="8pt" Width="160px"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label61" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Edificio"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="left">
                                         <asp:Label ID="lbl_edificio3" runat="server" Font-Names="Arial" 
                                             Font-Size="8pt" Width="160px"></asp:Label>
                                    </td>
                                </tr>

                                          <tr>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label11" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Indirizzo"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left">
                                          <asp:Label ID="lbl_indirizzo1" runat="server" Font-Names="Arial" 
                                              Font-Size="8pt" Width="160px"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label12" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Indirizzo"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left">
                                          <asp:Label ID="lbl_indirizzo2" runat="server" Font-Names="Arial" 
                                              Font-Size="8pt" Width="160px"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label13" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Indirizzo"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="left">
                                         <asp:Label ID="lbl_indirizzo3" runat="server" Font-Names="Arial" 
                                             Font-Size="8pt" Width="160px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
               
                </td>

            </tr>
            <tr>
                <td height="8px">
                </td>
            </tr>
            <tr>
                <td height="16px" valign="top">
                    <asp:Label ID="Label26" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Posizioni geografiche escluse"
                        Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
               <td style="border: 1px solid #996600;" valign="middle" height="175px">
              
                            <table style="width: 100%; height: 175px" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="height: 10px; border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: #996600;"
                                        width="1%" align="center">
                                        &nbsp;</td>
                                    <td style="border-style: none solid solid none; border-color: #FFFFFF #996600 #996600 #FFFFFF;
                                        height: 10px; border-bottom-width: 1px; border-right-width: 1px;"
                                        width="15%" align="center" colspan="2">
                                        <asp:Label ID="Label14" runat="server" Font-Names="Arial" Font-Size="8pt" Text="1° Opzione"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-bottom-width: 1px; border-top-style: none; border-bottom-style: solid; border-left-style: none; border-top-color: #FFFFFF; border-bottom-color: #996600; border-left-color: #FFFFFF;" 
                                        align="center" width="1%">
                                        &nbsp;</td>
                                    <td style="border-style: none solid solid none; border-color: #FFFFFF #996600 #996600 #FFFFFF;
                                        height: 10px; border-bottom-width: 1px; border-right-width: 1px;" 
                                        align="center" colspan="2">
                                        <asp:Label ID="Label15" runat="server" Font-Names="Arial" Font-Size="8pt" Text="2° Opzione"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #996600;"
                                        align="center" width="1%">
                                        &nbsp;</td>
                                    <td style="height: 10px; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #996600;"
                                        align="center" colspan="2">
                                        <asp:Label ID="Label16" runat="server" Font-Names="Arial" Font-Size="8pt" Text="3° Opzione"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px" width="1%" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" width="9%" align="left">
                                        <asp:Label ID="Label17" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Località"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left" width="23%">
                                     <asp:Label ID="lbl_localita1ex" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Width="160px"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left" width="10%">
                                        <asp:Label ID="Label19" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Località"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left">
                                        <asp:Label ID="lbl_localita2ex" runat="server" Font-Names="Arial" 
                                            Font-Size="8pt" Width="160px"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left" width="10%">
                                        <asp:Label ID="Label21" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Località"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="left">
                                      <asp:Label ID="lbl_localita3ex" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Width="160px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label23" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Zona"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left">
                                          <asp:Label ID="lbl_zona1ex" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                              Width="160px"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label25" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Zona"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left">
                                        <asp:Label ID="lbl_zona2ex" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Width="160px"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label35" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Zona"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="lbl_zona3ex" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Width="160px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label37" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Quartiere"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left">
                                         <asp:Label ID="lbl_quart1ex" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                             Width="160px"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label39" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Quartiere"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left">
                                         <asp:Label ID="lbl_quart2ex" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                             Width="160px"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label49" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Quartiere"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="left">
                                          <asp:Label ID="lbl_quart3ex" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                              Width="160px"></asp:Label>
                                    </td>
                                </tr>
             
                                <tr>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label62" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Complesso"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left">
                                          <asp:Label ID="lbl_complesso1ex" runat="server" Font-Names="Arial" 
                                              Font-Size="8pt" Width="160px"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label63" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Complesso"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left">
                                          <asp:Label ID="lbl_complesso2ex" runat="server" Font-Names="Arial" 
                                              Font-Size="8pt" Width="160px"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label64" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Complesso"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="left">
                                         <asp:Label ID="lbl_complesso3ex" runat="server" Font-Names="Arial" 
                                             Font-Size="8pt" Width="160px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label65" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Edificio"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left">
                                          <asp:Label ID="lbl_edificio1ex" runat="server" Font-Names="Arial" 
                                              Font-Size="8pt" Width="160px"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label66" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Edificio"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left">
                                          <asp:Label ID="lbl_edificio2ex" runat="server" Font-Names="Arial" 
                                              Font-Size="8pt" Width="160px"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label67" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Edificio"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="left">
                                         <asp:Label ID="lbl_edificio3ex" runat="server" Font-Names="Arial" 
                                             Font-Size="8pt" Width="160px"></asp:Label>
                                    </td>
                                </tr>

                                                   <tr>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label51" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Indirizzo"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left">
                                          <asp:Label ID="lbl_indirizzo1ex" runat="server" Font-Names="Arial" 
                                              Font-Size="8pt" Width="160px"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label53" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Indirizzo"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left">
                                          <asp:Label ID="lbl_indirizzo2ex" runat="server" Font-Names="Arial" 
                                              Font-Size="8pt" Width="160px"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td style="height: 10px" align="left">
                                        <asp:Label ID="Label55" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Indirizzo"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="left">
                                         <asp:Label ID="lbl_indirizzo3ex" runat="server" Font-Names="Arial" 
                                             Font-Size="8pt" Width="160px"></asp:Label>
                                    </td>
                                </tr>

                            </table>
               
                </td>
            </tr>
            <tr>
                <td height="8px">
                </td>
            </tr>
                <tr>
                <td height="16px" valign="top">
                    <asp:Label ID="Label18" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Piani di preferenza"
                        Font-Bold="True"></asp:Label>
                </td>
            </tr>
           
            <tr>
                <td style="border: 1px solid #996600;" valign="middle" height="80px">
                
                            <table style="width: 100%;height:80px;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="height: 10px; border-bottom-width: 1px; border-top-style: none; border-bottom-style: solid; border-left-style: none; border-top-color: #FFFFFF; border-bottom-color: #996600; border-left-color: #FFFFFF;" 
                                        align="center" width="1%">
                                        &nbsp;</td>
                                    <td align="center" colspan="2" style="border-style: none solid solid none; border-color: #FFFFFF #996600 #996600 #FFFFFF;
                                        height: 10px; border-bottom-width: 1px; border-right-width: 1px;">
                                        <asp:Label ID="Label31" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="1° Opzione"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-bottom-width: 1px; border-top-style: none; border-bottom-style: solid; border-left-style: none; border-top-color: #FFFFFF; border-bottom-color: #996600; border-left-color: #FFFFFF;" 
                                        align="center" width="1%">
                                        &nbsp;</td>
                                    <td align="center" colspan="2" style="border-style: none solid solid none; border-color: #FFFFFF #996600 #996600 #FFFFFF;
                                        height: 10px; border-bottom-width: 1px; border-right-width: 1px;">
                                        <asp:Label ID="Label34" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="2° Opzione"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-bottom-width: 1px; border-top-style: none; border-bottom-style: solid;
                                        border-left-style: none; border-top-color: #FFFFFF; border-bottom-color: #996600;
                                        border-left-color: #FFFFFF;" align="center" width="1%">
                                        &nbsp;</td>
                                    <td align="center" colspan="2" style="height: 10px; border-bottom-width: 1px; border-top-style: none; border-bottom-style: solid;
                                        border-left-style: none; border-top-color: #FFFFFF; border-bottom-color: #996600;
                                        border-left-color: #FFFFFF;">
                                        <asp:Label ID="Label41" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="3° Opzione"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td align="left" style="height: 10px" width="16%">
                                        <asp:Label ID="Label42" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Piano escl. NO asc." Width="100px"></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left" width="16%">
                                        <asp:Label ID="lbl_piano1SA" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td align="left" style="height: 10px" width="16%">
                                        <asp:Label ID="Label43" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Piano escl. NO asc."></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left" width="17%">
                                        <asp:Label ID="lbl_piano2SA" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td align="left" style="height: 10px" width="16%">
                                        <asp:Label ID="Label44" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Piano escl. NO asc."></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="left" width="17%">
                                        <asp:Label ID="lbl_piano3SA" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td align="left" style="height: 10px">
                                        <asp:Label ID="Label45" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Piano escl. CON asc."></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left">
                                        <asp:Label ID="lbl_piano1CA" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td align="left" style="height: 10px">
                                        <asp:Label ID="Label46" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Piano escl. CON asc."></asp:Label>
                                    </td>
                                    <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                        align="left">
                                        <asp:Label ID="lbl_piano2CA" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="center">
                                        &nbsp;</td>
                                    <td align="left" style="height: 10px">
                                        <asp:Label ID="Label20" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Piano escl. CON asc."></asp:Label>
                                    </td>
                                    <td style="height: 10px" align="left" width="18%">
                                        <asp:Label ID="lbl_piano3CA" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                      
                </td>
            </tr>
            <tr>
                <td height="8px">
                </td>
            </tr>
            <tr>
                <td height="16px" valign="top">
                    <asp:Label ID="Label27" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Dati Unità Immobiliare"
                        Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="border: 1px solid #996600;" valign="middle" height="97px">
                  
                            <table style="width: 100%; height: 96px" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 1%;">
                                    </td>
                                    <td style="height: 10px; width: 24%;">
                                        <asp:Label ID="Label28" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Superficie Minima"></asp:Label>
                                    </td>
                                    <td style="height: 10px; width: 20%;" colspan="1" width="50%">
                                        <asp:Label ID="lbl_supMin" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                    </td>
                                    <td style="height: 10px">
                                    </td>
                                    <td style="height: 10px; width: 3%;">
                                        &nbsp;
                                    </td>
                                    <td style="height: 10px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 1%;">
                                    </td>
                                    <td style="height: 10px;">
                                        <asp:Label ID="Label40" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Superficie Massima"></asp:Label>
                                    </td>
                                    <td style="height: 10px;" width="50%">
                                        <asp:Label ID="lbl_supMax" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                    </td>
                                    <td style="height: 10px">
                                        &nbsp;
                                    </td>
                                    <td style="height: 10px">
                                        &nbsp;
                                    </td>
                                    <td style="height: 10px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                    </td>
                                    <td style="height: 10px">
                                        <asp:Label ID="Label22" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Escluso Condominio"></asp:Label>
                                    </td>
                                    <td style="height: 10px">
                                        <asp:Label ID="lbl_condominio" runat="server" Font-Names="Arial" 
                                            Font-Size="8pt"></asp:Label>
                                    </td>
                                    <td style="height: 10px">
                                        &nbsp;
                                    </td>
                                    <td style="height: 10px">
                                        &nbsp;
                                    </td>
                                    <td style="height: 10px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                    </td>
                                    <td style="height: 10px">
                                        <asp:Label ID="Label29" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Senza Barr. Architettoniche"></asp:Label>
                                    </td>
                                    <td style="height: 10px">
                                        <asp:Label ID="lbl_barrArch" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                    </td>
                                    <td style="height: 10px">
                                        &nbsp;
                                    </td>
                                    <td style="height: 10px">
                                        &nbsp;
                                    </td>
                                    <td style="height: 10px">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                       
                </td>
            </tr>
            <tr>
                <td height="8px">
                </td>
            </tr>
            <tr>
                <td height="16px" >
                    <asp:Label ID="Label30" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Note"
                        Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="border: 1px solid #996600;" valign="top" height="45px">
                    <table style="width: 99%; height: 38px" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 1%;">
                            </td>
                            <td style="width: 99%;">
                            
                                <asp:Label ID="lbl_note" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                            
                            </td>
                       
                        </tr>
                    </table>
        </td> </tr>
        <tr style="height: 40px">
            <td>
                <table style="width: 100%; height: 95%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 60%;">
                        </td>
                        <td style="height: 15px; width: 17%;">
                        </td>
                        <td style="height: 15px; width: 15%;">
                            <asp:ImageButton ID="btn_stampa" runat="server" OnClientClick="ApriStampa();return false;"
                                ImageUrl="~/NuoveImm/Img_StampaContratto.png" ToolTip="Stampa" TabIndex="10" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btn_annulla" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                                OnClientClick="window.close();" ToolTip="Annulla" TabIndex="11" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
        <asp:HiddenField ID="sValoreID" runat="server" />
        <asp:HiddenField ID="Tipo" runat="server" />
         <asp:HiddenField ID="Provenienza" runat="server" />
    </div>

       
    <script language="javascript" type="text/javascript">


        document.getElementById('caric').style.visibility = 'hidden';

    </script>
    </form>
</body>
</html>
