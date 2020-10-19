<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UnitaImmDisponibili.aspx.vb" Inherits="ASS_UnitaImmDisponibili" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Selezionato;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self"/>
   <script type="text/javascript" language="javascript" >
       window.name = "modal";

       </script>
    <title>Unita immediatamente disponibili</title>
</head>
<body>
    <form id="form1" runat="server" target="modal">
    <div>
    <table style="left: 0px; BACKGROUND-IMAGE: url('../NuoveImm/SfondoMascheraContratti.jpg'); WIDTH: 798px;
            position: absolute; top: 0px">
            <tr>
                <td style="width: 706px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                    </strong>&nbsp;<asp:Label ID="lblNumRisultati" runat="server" Font-Bold="True" ForeColor="#801F1C"
                            Font-Size="14pt" Font-Names="Arial"></asp:Label></span><br />
                    <br />
                    <div id="contenitore1" style="position: absolute; width: 760px; height: 400px; overflow: scroll;
                        top: 72px; left: 10px;">
                        <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" Font-Names="Arial"
                            Font-Size="8pt" 
                            Style="z-index: 121; left: 0px; position: absolute; top: 0px; width: 1183px;" 
                            HorizontalAlign="Left" TabIndex="3">
                            <PagerStyle Mode="NumericPages" />
                            <HeaderStyle BackColor="PapayaWhip" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COD_ALLOGGIO" HeaderText="COD" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PROPRIETA" HeaderText="PROPRIETA" ReadOnly="True" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="COD_ALLOGGIO" HeaderText="CODICE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPO_ALL" HeaderText="TIPO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="NOME_QUARTIERE" HeaderText="QUARTIERE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPO_VIA" HeaderText="INDIR."></asp:BoundColumn>
                                <asp:BoundColumn DataField="INDIRIZZO" HeaderText=" "></asp:BoundColumn>
                                <asp:BoundColumn DataField="NUM_CIVICO" HeaderText="CIV."></asp:BoundColumn>
                                <asp:BoundColumn DataField="N_ALL" HeaderText="NUM."></asp:BoundColumn>
                                <asp:BoundColumn DataField="ZONA" HeaderText="ZONA"></asp:BoundColumn>
                                <asp:BoundColumn DataField="NUM_LOCALI" HeaderText="LOC."></asp:BoundColumn>
                                <asp:BoundColumn DataField="PIANO" HeaderText="PIANO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="NETTA" HeaderText="NETTA"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CONV" HeaderText="CONV."></asp:BoundColumn>
                                <asp:BoundColumn DataField="ELEVATORE" HeaderText="ASC."></asp:BoundColumn>
                                <asp:BoundColumn DataField="HANDICAP" HeaderText="HANDICAP"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_DISPONIBILITA1" HeaderText="DISP."></asp:BoundColumn>
                                <asp:BoundColumn DataField="PROPRIETA1" HeaderText="PROPR."></asp:BoundColumn>
                                <asp:BoundColumn DataField="Visualizza"></asp:BoundColumn>
                                <asp:BoundColumn DataField="VisualizzaCanone"></asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />

                        
                    <br />
    
                    <br />
    
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:TextBox ID="TextBox3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
                    Style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                    border-bottom: white 1px solid; left: -1px; top: 45px;" Width="777px">Nessuna Selezione</asp:TextBox>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="LBLID" runat="server" />
    <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                        
            Style="left: 615px; position: absolute; top: 541px; right: 600px;" TabIndex="1" 
            visible="false" />


<asp:Button ID="btn_abbina" runat="server" Style="left: 517px; position: absolute; top: 536px" 
            Text="Abbina e Memorizza" BackColor="#EAEAEA" Font-Bold="True"  
            Font-Names="Arial" Font-Size="9pt" Height="30px"
            ForeColor="Blue" TabIndex="5" visible="false"  
            onclientclick="ConfermaAbbinamento();" />




        <asp:Label ID="lbl_scadOff" runat="server" Text="Data Scadenza Offerta:" 
            Style="left: 7px; position: absolute; top: 525px" visible="False" 
            Font-Names="arial" Font-Size="10pt"></asp:Label>

        <asp:TextBox ID="txt_scadOff" runat="server" MaxLength="10" 
            Style="left: 156px; position: absolute; top: 524px" visible="false"
            ToolTip="Formato dd/mm/aaaa"></asp:TextBox>

                        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                        Style="z-index: 101; left: 712px; position: absolute; top: 541px" 
                        ToolTip="Esci" CausesValidation="False" 
            onclientclick="ConfermaEsci();" TabIndex="2" />
    </div>
    <asp:HiddenField ID="HIDDENtipo" runat="server" />
        <asp:HiddenField ID="HIDDENidDom" runat="server" />
        <asp:HiddenField ID="HIDDENdataPG" runat="server" />
        <asp:HiddenField ID="HIDDENnumPG" runat="server" />
        <asp:HiddenField ID="HIDDENnome" runat="server" />
        <asp:HiddenField ID="HIDDENidBando" runat="server" />
         <asp:HiddenField ID="provenienza" runat="server" Value="0" />
      <asp:HiddenField ID="txt_conferma" runat="server" Value="0" />
      
      </form>

    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';



        function CloseModal(returnParameter) {
            window.returnValue = returnParameter;
            window.close();
        }


        function ConfermaEsci() {
          

                    var chiediConferma
                    chiediConferma = window.confirm("Sei sicuro di voler uscire?");
                    if (chiediConferma == true) {
                        self.close();
                    }

                }



                function ConfermaAbbinamento() {


                    var sicuro = window.confirm('Attenzione! L\'operazione abbinerà l\'alloggio selezionato alla domanda di riferimento. Continuare?');
                    if (sicuro == true) {
                        document.getElementById('txt_conferma').value = '1';
                    }
                    else {
                        document.getElementById('txt_conferma').value = '0';
                    }

                }



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



                function DelPointer(obj) {
                    obj.value = obj.value.replace('.', '');
                    document.getElementById(obj.id).value = obj.value;

                }

    </script>


</body>
</html>
