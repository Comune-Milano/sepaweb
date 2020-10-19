<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SituazioneAppuntamenti.aspx.vb" Inherits="ANAUT_SituazioneAppuntamenti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />
    <title></title>
    <style type="text/css">
        #INTESTA
        {
            top: 209px;
            left: 11px;
        }
        .style1
        {
            font-family: Arial;
            font-size: xx-small;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
   
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
         

    <div id="INTESTA" 
        
        
        
        style="position: absolute; background-color: #FFFFCC; width: 920px; height: 29px;">
        <table style="width:100%;">
            <tr>
                <td style="text-align: center">
    <asp:label id="lblFiliale" runat="server" 
                Font-Size="10pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 110; left: 9px;"></asp:label>
                &nbsp;&nbsp;
                <asp:label id="lblSportello" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 110; left: 120px;"></asp:label> 
                </td>
            </tr>
        </table>
    </div>

    <div id="Div3" 
               
        style="position: absolute; background-color: #FFFFCC; width: 148px; height: 164px; top: 15px; left: 387px;">
        <table style="width:100%;" cellpadding="1" cellspacing="1">
            <tr>
                <td style="text-align: left">
                    <img alt="" src="info-icon.png" /></td>
                <td style="text-align: left">
                    <asp:Label ID="Label26" runat="server" Font-Names="arial" Font-Size="8pt" 
                        Text="Informazioni sul contratto che occupa lo slot"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <img alt="" src="SlotLibero.png" /></td>
                <td style="text-align: left">
                    <asp:Label ID="Label27" runat="server" Font-Names="arial" Font-Size="8pt" 
                        Text="Indica che lo slot è libero. Gli slot."></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <img alt="" src="info-icon1.png" />
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label28" runat="server" Font-Names="arial" Font-Size="8pt" 
                        Text="Contratto selezionato"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    


   <asp:label id="Label1" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="False" 
                style="z-index: 110; left: 774px; position:absolute; top: 184px;">Giorni con disponibilità</asp:label>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                            <div id="Div1" 
        
        
        
        
        
        style="position: absolute; background-color: #FFFFCC; width: 185px; height: 164px; top: 15px; left: 546px;">
        <table style="width:86%;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="text-align: left">
    <asp:CheckBox ID="chFuoriOrario" runat="server" CausesValidation="True"
        Font-Names="rial" Font-Size="9pt" Text="Abilita Fuori Orario*" AutoPostBack="True" 
                        Font-Bold="True" Checked="True" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                        <asp:label id="Label2" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="False" 
                style="z-index: 110; left: 9px;" Width="200px">* 8.00-9.00 / 13.00-14.30 / 16.30-18.30</asp:label>
                &nbsp;&nbsp;
                </td>
            </tr>
                        <tr>
            <td>
            &nbsp;&nbsp;
            </td>
            </tr>
            <tr>
            <td style="width:180px">
                <span class="style1">Per creare nuovi slot liberi in date o giorni non programmati, in orari in cui non ci sono più slot 
                liberi, premere il seguente pulsante</span>
            </td>
            </tr>
            <tr>
            <td style="text-align: center">
                &nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:ImageButton ID="imgNuovoSlot" runat="server" 
                        ImageUrl="~/NuoveImm/Img_NuovoSlot.png" onclientclick="ApriNuovoSlot();" />
                </td>
            </tr>
        </table>
    </div>
                        <div id="Div2" 
        
        
        
        
        
        style="border: 1px solid #000000; position: absolute; background-color: #FFFFCC; width: 364px; height: 164px; top: 15px; left: 10px;">
        <table style="width:100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="text-align: left">
                    &nbsp;&nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center">
    <asp:label id="Label3" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 110; left: 9px;"></asp:label>
                </td>
            </tr>
                        <tr style="font-family: ARIAL, Helvetica, sans-serif; font-size: 10pt; font-weight: bold">
                <td style="text-align: center">
                    &nbsp; &nbsp;</td>
            </tr>
                        <tr style="font-family: ARIAL, Helvetica, sans-serif; font-size: 10pt; font-weight: bold">
                            <td style="text-align: center">
                                SELEZIONA IL GIORNO DAL CALENDARIO</td>
            </tr>
            <tr style="font-family: ARIAL, Helvetica, sans-serif; font-size: 10pt; font-weight: bold">
                <td style="text-align: center">
                    &nbsp; &nbsp;</td>
            </tr>
                        <tr>
                <td style="text-align: right">
                    <asp:Image alt="Esci" ID="Image3" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png" onclick="Esci()" style="cursor:pointer" />
                            </td>
            </tr>
        </table>
    </div>
                    <div id="DIVCONFERMA"                            
                            style="position: absolute; background-color: #c3c3bb; width: 950px; height: 700px; top: 0px; left: 0px; background-repeat: no-repeat; visibility: hidden; z-index: 1000; text-align: left;">


        <table style="background-position: center; width: 100%; height: 100%; background-repeat: no-repeat; z-index: 2000; text-align: left; background-image: url('../ImmDiv/SfondoDim1.jpg');">
        <tr style="font-family: arial, Helvetica, sans-serif; font-size: 12pt">
        <td>
        <table style="position: absolute; width: 436px; height: 226px; top: 214px; left: 243px;">
        <tr>
        <td style="text-align: center">
            <asp:Label ID="Label25" runat="server" Text="Label" Font-Bold="True" 
                Font-Names="ARIAL" Font-Size="12pt"></asp:Label><br />
            <asp:Label ID="Label4" runat="server" Text="Label" Font-Bold="True" 
                Font-Names="ARIAL" Font-Size="12pt"></asp:Label>
        </td>
        </tr>
                <tr>
        <td>
            
            &nbsp; &nbsp;</td>
        </tr>
                <tr style="text-align: right">
        <td>
            
            <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_SI.png"
                        Style="cursor:pointer" 
                        TabIndex="8" 
                onclientclick="document.getElementById('DIVCONFERMA').style.visibility = 'hidden';" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            <img alt="Annulla" src="../NuoveImm/Img_NO.png" 
        style="cursor:pointer" onclick="document.getElementById('DIVCONFERMA').style.visibility = 'hidden';"/></td>
        </tr>
        </table>
        </td>
        </tr>
        
        </table>
    </div>
        <asp:Calendar ID="Calendar1" runat="server" BackColor="White" 
        BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
        Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" 
        style="position:absolute; top: 9px; left: 741px; height: 112px; width: 200px;" 
                            FirstDayOfWeek="Sunday">
                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                    <NextPrevStyle VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#808080" />
                    <SelectedDayStyle BackColor="#0000CC" Font-Bold="True" ForeColor="White" />
                    <SelectorStyle BackColor="#CCCCFF" />
                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                    <TodayDayStyle BackColor="#3399FF" ForeColor="Black" />
                    <WeekendDayStyle BackColor="#FFFFCC" />
    </asp:Calendar>
        <div id="Elenco"   
                            
                            
                            
                                style="border: 1px solid #000000; position:absolute; width: 930px; height: 420px; top: 242px; left: 10px; overflow: auto;">
        <table style="width:97%;" cellpadding="0" cellspacing="0">
            <%=TabellaGiorni %>
            
        </table>
   
    </div>
    <asp:HiddenField ID="GRUPPO" runat="server" />
     <asp:HiddenField ID="IDA" runat="server" />
     <asp:HiddenField ID="IDCONVOCAZIONE" runat="server" />
    <asp:HiddenField ID="SPORTELLO" runat="server" />
    <asp:HiddenField ID="FILIALE" runat="server" />
    <asp:HiddenField ID="SLOTDESTINATARIO" runat="server" />
    <asp:HiddenField ID="CodiceContratto" runat="server" />
    <asp:HiddenField ID="TIPO" runat="server" />
        <asp:HiddenField ID="yPos" runat="server" Value="0" />
    <asp:HiddenField ID="xPos" runat="server" Value="0" />
     </ContentTemplate>
                </asp:UpdatePanel>
               
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <img alt="Giorni Con disponibilità" src="LegendaDisp.png" 
        style="position:absolute; top: 187px; left: 742px; right: 791px;"/><br />
                <br />
                <br />
   
    <asp:HiddenField ID="opfatta" runat="server" />

    <script type="text/javascript">

        document.getElementById('DIVCONFERMA').style.visibility = 'hidden';

        function Sposta(indice, giorno, ora,sportello) {
            document.getElementById('SLOTDESTINATARIO').value = indice;
            

            var miogiorno = String(giorno);

            if (navigator.appName == 'Microsoft Internet Explorer') {
                document.getElementById('Label4').innerText = miogiorno.substr(6, 2) + '/' + miogiorno.substr(4, 2) + '/' + miogiorno.substr(0, 4) + ' ore ' + ora + ' Operatore ' + sportello + ' ?';
            }
            else {
                document.getElementById('Label4').textContent = miogiorno.substr(6, 2) + '/' + miogiorno.substr(4, 2) + '/' + miogiorno.substr(0, 4) + ' ore ' + ora + ' Operatore ' + sportello + ' ?';
            }

            document.getElementById('DIVCONFERMA').style.visibility = 'visible';
        }

        function Esci() {
            self.close();
        }

        function ApriNuovoSlot() {
            window.showModalDialog('CreaNuovoSlot.aspx?IDC=' + document.getElementById('IDCONVOCAZIONE').value, window, 'status:no;dialogWidth:500px;dialogHeight:400px;dialogHide:true;help:no;scroll:no');
        }
    </script>
    
    <asp:UpdateProgress ID="UpdateProgressGenerale" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div style="margin: 0px; background-color: #C0C0C0; width: 100%; height: 100%; position: fixed;
                top: 0px; left: 0px; filter: alpha(opacity='75'); opacity: 0.75; background-color: #eeeeee;">
                <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
                    margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');">
                    <table style="width: 100%; height: 100%">
                        <tr>
                            <td valign="middle" align="center">
                                <asp:Image ID="Image2" runat="server" ImageUrl="../NuoveImm/load.gif" />
                                <br />
                                <br />
                                <asp:Label ID="Label24" runat="server" Text="caricamento in corso..." Font-Names="Arial"
                                    Font-Size="10pt"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    </form>
    
</body>
<script  language="javascript" type="text/javascript">
   // document.getElementById('dvvvPre').style.visibility = 'hidden';
        </script>
</html>
