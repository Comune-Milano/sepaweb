<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Disponibilita.aspx.vb" Inherits="ASS_Disponibilita" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Disponibilità Alloggi</title>
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








        //        function ApriStampa() {



        //            window.open('Doc_Preferenze/SchedaPreferenze.aspx?TIPO=' + document.getElementById('Tipo').value + '&PROV=0&IDDOMANDA=' + document.getElementById('sValoreID').value, 'DocPreferenze', 'resizable=yes');

        //        }


        function ApriPreferenze() {



            window.open('GestionePreferenze.aspx?T=' + document.getElementById('Tipo').value + '&PROV=0&ID=' + document.getElementById('sValoreID').value + '&PG=' + document.getElementById('sPG').value, 'PrefDom', 'height=620,top=0,left=0,width=800,scrollbars=no');

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
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;Disponibilità
                        Alloggi</strong></span>
                </td>
            </tr>
            <tr>
                <td height="10px">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td height="16px" valign="top">
                    <asp:Label ID="Label12" runat="server" Font-Names="Arial" Font-Size="9pt" Text="DATI DOMANDA"
                        Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="border: 1px solid #996600;" valign="middle" height="80px">
                    <table style="width: 100%; height: 85px" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 1%;">
                            </td>
                            <td style="height: 10px" width="9%">
                                <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="9pt" 
                                    Text="PG:"></asp:Label>
                            </td>
                            <td style="height: 10px" width="18%">
                                <asp:Label ID="lbl_PG" runat="server" Font-Names="Arial" Font-Size="9pt" 
                                    Font-Bold="True"></asp:Label>
                            </td>
                            <td style="height: 10px" width="13%">
                                <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="9pt" 
                                    Text="Nominativo:"></asp:Label>
                            </td>
                            <td style="height: 10px" width="16%">
                                <asp:Label ID="lbl_nominativo" runat="server" Font-Names="Arial" 
                                    Font-Size="9pt" Font-Bold="True"></asp:Label>
                            </td>
                            <td style="height: 10px" width="15%">
                                &nbsp;
                            </td>
                            <td style="height: 10px" width="18%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px">
                            </td>
                            <td style="height: 10px">
                                <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="9pt" 
                                    Text="ISBARC/R:"></asp:Label>
                            </td>
                            <td style="height: 10px">
                                <asp:Label ID="lbl_isbarc" runat="server" Font-Names="Arial" Font-Size="9pt" 
                                    Font-Bold="True"></asp:Label>
                            </td>
                            <td style="height: 10px">
                                <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="9pt" 
                                    Text="ISEE:"></asp:Label>
                            </td>
                            <td style="height: 10px">
                                <asp:Label ID="lbl_isee" runat="server" Font-Names="Arial" Font-Size="9pt" 
                                    Font-Bold="True"></asp:Label>
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
                                <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="9pt" 
                                    Text="N. Comp.:"></asp:Label>
                            </td>
                            <td style="height: 10px">
                                <asp:Label ID="lbl_comp" runat="server" Font-Names="Arial" Font-Size="9pt" 
                                    Font-Bold="True"></asp:Label>
                            </td>
                            <td style="height: 10px">
                                <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="9pt" 
                                    Text="N. Anziani (>=65):"></asp:Label>
                            </td>
                            <td style="height: 10px">
                                <asp:Label ID="lbl_anziani" runat="server" Font-Names="Arial" Font-Size="9pt" 
                                    Font-Bold="True"></asp:Label>
                            </td>
                            <td style="height: 10px">
                                <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="9pt" 
                                    Text="Invalidi (66%-100%):"></asp:Label>
                            </td>
                            <td style="height: 10px">
                                <asp:Label ID="lbl_invalidi" runat="server" Font-Names="Arial" Font-Size="9pt" 
                                    Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="30px">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td height="16px" valign="top">
                    <asp:Label ID="lbl_UIPref" runat="server" Font-Names="Arial" Font-Size="10pt" 
                        Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="border: 1px solid #996600;" width= "670px" valign="top">
                    <table style=" height: 100%;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 670px">
                             <div style="overflow: auto; height: 100%; width:770px">
                                <asp:DataGrid ID="DataGrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    BackColor="white" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Navy"
                                    HorizontalAlign="Left" Width="1500px" PageSize="8">
                                    <PagerStyle Mode="NumericPages" />
                                    <HeaderStyle BackColor="PapayaWhip" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" />
                                    <Columns>
                                            <asp:BoundColumn DataField="ID_ALLOGGIO" HeaderText="ID_ALLOGGIO" Visible="False">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ID_QUARTIERE" HeaderText="ID_QUARTIERE" Visible="False">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ID_PIANO" HeaderText="ID_PIANO" Visible="False">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ID_COMUNE" HeaderText="ID_COMUNE" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="COD_ALLOGGIO" HeaderText="COD ALLOGGIO"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="COMUNE" HeaderText="COMUNE"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ZONA" HeaderText="ZONA"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="QUARTIERE" HeaderText="QUARTIERE"></asp:BoundColumn>
                                             <asp:BoundColumn DataField="COMPLESSO" HeaderText="COMPLESSO"></asp:BoundColumn>
                                              <asp:BoundColumn DataField="EDIFICIO" HeaderText="EDIFICIO"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="NUM_LOCALI" HeaderText="N. LOCALI"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="PIANO" HeaderText="PIANO"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SUP" HeaderText="SUP."></asp:BoundColumn>
                                             <asp:BoundColumn DataField="ELEVATORE" HeaderText="ASC."></asp:BoundColumn>
                                              <asp:BoundColumn DataField="BARRIERE" HeaderText="BARR. ARC."></asp:BoundColumn>
                                            <asp:BoundColumn DataField="CONDOMINIO" HeaderText="CONDOMINIO"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DATA_DISPONIBILITA" HeaderText="DATA DISP."></asp:BoundColumn>
                                            <asp:BoundColumn DataField="PROPRIETA" HeaderText="PROPRIETA'"></asp:BoundColumn>
               
                                        </Columns>
                                </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="30px">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td height="16px" valign="top">
                    <asp:Label ID="lbl_UIGeneral" runat="server" Font-Names="Arial" Font-Size="10pt"
                        Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
               <td style="border: 1px solid #996600;" width= "670px" valign="top">
                    <table style=" height: 100%;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 670px">
                             <div style="overflow: auto; height: 100%; width:770px">
                                    <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        BackColor="white" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Navy"
                                        HorizontalAlign="Left" Width="1500px" PageSize="8">
                                        <PagerStyle Mode="NumericPages" />
                                        <HeaderStyle BackColor="PapayaWhip" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" />
                                        <Columns>
                                            <asp:BoundColumn DataField="ID_ALLOGGIO" HeaderText="ID_ALLOGGIO" Visible="False">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ID_QUARTIERE" HeaderText="ID_QUARTIERE" Visible="False">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ID_COMUNE" HeaderText="ID_COMUNE" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="COD_ALLOGGIO" HeaderText="COD ALLOGGIO"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="COMUNE" HeaderText="COMUNE"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ZONA" HeaderText="ZONA"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="QUARTIERE" HeaderText="QUARTIERE"></asp:BoundColumn>
                                              <asp:BoundColumn DataField="COMPLESSO" HeaderText="COMPLESSO"></asp:BoundColumn>
                                              <asp:BoundColumn DataField="EDIFICIO" HeaderText="EDIFICIO"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="NUM_LOCALI" HeaderText="N. LOCALI"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="PIANO" HeaderText="PIANO"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SUP" HeaderText="SUP."></asp:BoundColumn>
                                             <asp:BoundColumn DataField="ELEVATORE" HeaderText="ASC."></asp:BoundColumn>
                                              <asp:BoundColumn DataField="BARRIERE" HeaderText="BARR. ARC."></asp:BoundColumn>
                                            <asp:BoundColumn DataField="CONDOMINIO" HeaderText="CONDOMINIO"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DATA_DISPONIBILITA" HeaderText="DATA DISP."></asp:BoundColumn>
                                            <asp:BoundColumn DataField="PROPRIETA" HeaderText="PROPRIETA'"></asp:BoundColumn>
               
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 25px">
                </td>
            </tr>
            <tr>
                <td style="height: 26px;">
                    <table>
                        <tr>
                            <td style="width: 70%;">
                            </td>
                            <td style="width: 20%;">
                                <asp:ImageButton ID="btn_preferenze" runat="server" OnClientClick="ApriPreferenze();return false;"
                                    CausesValidation="False" ImageUrl="~/NuoveImm/Img_VisPreferenze.png" ToolTip="Visualizza Preferenze"
                                    TabIndex="11" />
                            </td>
                            <td style="width: 20%;">
                                <asp:ImageButton ID="btn_esci" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                                    OnClientClick="window.close();" ToolTip="Esci" TabIndex="11" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="sValoreID" runat="server" />
          <asp:HiddenField ID="sPG" runat="server" />
        <asp:HiddenField ID="Tipo" runat="server" />
        <%--   <asp:HiddenField ID="Provenienza" runat="server" />--%>
    </div>

     <script type="text/javascript">

         document.getElementById('caric').style.visibility = 'hidden';

    </script>
    </form>
   
</body>
</html>
