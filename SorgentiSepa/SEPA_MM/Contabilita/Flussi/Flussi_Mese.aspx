<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Flussi_Mese.aspx.vb" Inherits="Contabilita_Flussi_Flussi_Mese" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Flussi Finanziari</title>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-weight: bold;
            text-align: center;
            font-size: 16px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td class="style1">
                    F L U S S I&nbsp;&nbsp; F I N A N Z I A R I&nbsp;&nbsp;&nbsp; A N N O&nbsp;
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                &nbsp; M E S E &nbsp; D I&nbsp;
                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <b>F L U S S I&nbsp;&nbsp; F I N A N Z I A R I&nbsp;&nbsp; D E L L A&nbsp;&nbsp; G E S T I O N E</b></td>
            </tr>
            <tr>
                <td style="text-align: center">
                    La presente tabella espone la distribuzione dell&#39;incasso riferito al mese di
                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
&nbsp;<asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                    .</td>
            </tr>
            <tr>
                <td style="text-align: center">
                    &nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="lblTabella" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    
                    &nbsp;&nbsp;La presente tabella espone il totale del bollettato per il mese 
                    esaminato, il totale del bollettato scaduto per il mese esaminato alla data 
                    odierna, il totale dell&#39;incassato alla data odierna per il mese esaminato, il 
                    totale dell&#39;incassato scaduto alla data odierna per il mese esaminato, la 
                    percentuale di incasso rispetto al bollettato e la percentuale dell&#39;incassato 
                    scaduto rispetto al bollettato scaduto.<br />
                    <table style="border: 1px solid #000000; width: 100%;" cellpadding="0" 
                        cellspacing="0">
                        <tr height="30">
                            <td style="font-family: ARIAL; font-size: 14px; font-weight: bold; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000; font-style: italic;" 
                                align="left">
                                <b>VOCE</b>&nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 14px; font-weight: bold; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000; font-style: italic;" 
                                align="right">
                                BOLLETTATO</td>
                            <td style="font-family: ARIAL; font-size: 14px; font-weight: bold; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000; font-style: italic;" 
                                align="right">
                                SCADUTO&nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 14px; font-weight: bold; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000; font-style: italic;" 
                                align="right">
                                INCASSATO</td>
                            <td style="font-family: ARIAL; font-size: 14px; font-weight: bold; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000; font-style: italic;" 
                                align="right">
                                INCASSATO SCADUTO</td>
                            <td style="font-family: ARIAL; font-size: 14px; font-weight: bold; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000; font-style: italic;" 
                                align="right">
                                % (incassato / bollettato)</td>
                            <td style="font-family: ARIAL; font-size: 14px; font-weight: bold; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000; font-style: italic;" 
                                align="right">
                                % (incassato scaduto<br />
                                / bollettato scaduto)</td>
                        </tr>
                        <tr height="30">
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="left">
                                <b>COMPETENZE COMUNE</b></td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                        </tr>
                        <tr height="30">
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="left">
                                CANONI</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_BOLLETTATO_1" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_SCADUTO_1" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_INCASSATO_1" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_INCASSATO_SC_1" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_PERCENTUALE_A_1" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_PERCENTUALE_1" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr height="30">
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                lang="30" align="left">
                                IMPOSTE DI REGISTRO</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_BOLLETTATO_7" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_SCADUTO_7" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_INCASSATO_7" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_INCASSATO_SC_7" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_PERCENTUALE_A_7" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_PERCENTUALE_7" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr height="30">
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                lang="30" align="left">
                                IMPOSTE DI
                                BOLLO SU CONTRATTI</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_BOLLETTATO_8" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_SCADUTO_8" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_INCASSATO_8" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_INCASSATO_SC_8" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_PERCENTUALE_A_8" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_PERCENTUALE_8" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr height="30">
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                lang="30" align="left">
                                SPESE MAV</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_BOLLETTATO_6" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_SCADUTO_6" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_INCASSATO_6" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_INCASSATO_SC_6" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_PERCENTUALE_A_6" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_PERCENTUALE_6" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr height="30">
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                lang="30" align="left">
                                <b>COMPETENZA GESTORE</b></td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                        </tr>
                        <tr height="30">
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                lang="30" align="left">
                                ONERI ACCESSORI</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_BOLLETTATO_2" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_SCADUTO_2" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_INCASSATO_2" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_INCASSATO_SC_2" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_PERCENTUALE_A_2" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_PERCENTUALE_2" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr height="30">
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="left">
                                BOLLI SU MAV</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_BOLLETTATO_5" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_SCADUTO_5" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_INCASSATO_5" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_INCASSATO_SC_5" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_PERCENTUALE_A_5" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_PERCENTUALE_5" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr height="30">
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="left">
                                &nbsp; &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                        </tr>
                        <tr height="30">
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="left">
                                DEPOSITI CAUZIONALI</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_BOLLETTATO_3" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_SCADUTO_3" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_INCASSATO_3" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_INCASSATO_SC_3" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_PERCENTUALE_A_3" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_PERCENTUALE_3" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr height="30">
                            <td style="font-family: ARIAL; font-size: 12px;  border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="left">
                                &nbsp; &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                        </tr>
                        <tr height="30">
                            <td style="font-family: ARIAL; font-size: 12px;  border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="left">
                                QUOTA SINDACALE</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_BOLLETTATO_4" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_SCADUTO_4" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_INCASSATO_4" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_INCASSATO_SC_4" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_PERCENTUALE_A_4" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                <asp:Label ID="LBL_PERCENTUALE_4" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr height="30">
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="left">
                                &nbsp;&nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;&nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp; &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;&nbsp;&nbsp; &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp;</td>
                            <td style="font-family: ARIAL; font-size: 12px; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                align="right">
                                &nbsp; &nbsp;</td>
                        </tr>
                        <tr height="30">
                            <td style="font-family: ARIAL; font-size: 12px; font-weight: bold; " 
                                align="left">
                                TOTALE</td>
                            <td align="right" 
                                style="font-family: ARIAL; font-size: 12px; font-weight: bold;">
                                <asp:Label ID="LBL_BOLLETTATO_9" runat="server"></asp:Label>
                            </td>
                            <td align="right" 
                                style="font-family: ARIAL; font-size: 12px; font-weight: bold;">
                                <asp:Label ID="LBL_SCADUTO_9" runat="server"></asp:Label>
                            </td>
                            <td align="right" 
                                style="font-family: ARIAL; font-size: 12px; font-weight: bold;">
                                <asp:Label ID="LBL_INCASSATO_9" runat="server"></asp:Label>
                            </td>
                            <td align="right" 
                                style="font-family: ARIAL; font-size: 12px; font-weight: bold;">
                                <asp:Label ID="LBL_INCASSATO_SC_9" runat="server"></asp:Label>
                            </td>
                            <td align="right" 
                                style="font-family: ARIAL; font-size: 12px; font-weight: bold;">
                                <asp:Label ID="LBL_PERCENTUALE_A_9" runat="server"></asp:Label>
                            </td>
                            <td align="right" 
                                style="font-family: ARIAL; font-size: 12px; font-weight: bold;">
                                <asp:Label ID="LBL_PERCENTUALE_9" runat="server"></asp:Label>
                            </td>
                        </tr>

                    </table>
                    
                </td>
            </tr>
            <tr>
                        <td>
                            <br />
                    <asp:ImageButton ID="ImgPDF" runat="server" 
                        ImageUrl="~/NuoveImm/Img_Stampa_Grande.png" style="height: 20px" />
&nbsp;&nbsp;&nbsp; <img style="cursor:pointer" alt="" src="../../NuoveImm/Img_Export_Grande.png" 
                        onclick="window.open('Export.aspx?ID=3','','');" id="imgExport"/></td>
                        </tr>
        </table>
    
    </div>
    </form>
            <script  language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
