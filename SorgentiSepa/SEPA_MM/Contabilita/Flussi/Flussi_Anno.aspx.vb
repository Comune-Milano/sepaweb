Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class Contabilita_Flussi_Flussi_Anno
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim TOTALE_COLONNA_1 As Double = 0
    Dim TOTALE_COLONNA_2 As Double = 0
    Dim TOTALE_COLONNA_3 As Double = 0
    Dim TOTALE_COLONNA_4 As Double = 0
    Dim TOTALE_COLONNA_5 As Double = 0
    Dim v1 As Double = 0
    Dim v2 As Double = 0
    Dim v3 As Double = 0
    Dim v4 As Double = 0
    Dim v5 As Double = 0
    Dim v6 As Double = 0
    Dim v7 As Double = 0
    Dim v8 As Double = 0
    Dim v9 As Double = 0

    Dim Vv1 As Double = 0
    Dim Vv2 As Double = 0
    Dim Vv3 As Double = 0
    Dim Vv4 As Double = 0
    Dim Vv5 As Double = 0
    Dim Vv6 As Double = 0
    Dim Vv7 As Double = 0
    Dim Vv8 As Double = 0
    Dim Vv9 As Double = 0
    Dim DT As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String = ""

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../IMMCONTABILITA/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)

                Dim sr1 As StreamReader = New StreamReader(Server.MapPath("Modello.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                Dim contenuto As String = sr1.ReadToEnd()
                sr1.Close()



                Dim anno As String = Request.QueryString("A")
                Label1.Text = anno
                contenuto = Replace(contenuto, "$anno$", "ANNO " & Label1.Text)
                contenuto = Replace(contenuto, "$testo$", "La presente tabella espone il totale del bollettato per l'anno esaminato, il totale del bollettato scaduto alla data odierna  per l'anno esaminato, il totale dell'incassato alla data odierna  per l'anno esaminato, il totale dell'incassato scaduto alla data odierna  per l'anno esaminato, la percentuale di incasso rispetto al bollettato e la percentuale dell'incassato scaduto rispetto al bollettato scaduto.")
                contenuto = Replace(contenuto, "$aggiunta$", "")

                'COLONNA BOLLETTATO

                par.cmd.CommandText = "SELECT SUM(BOL_FLUSSI.BOLLETTATO_CANONI) AS BOLLETTATO_CANONI,SUM(BOL_FLUSSI.BOLLETTATO_ONERI_ACCESSORI) AS BOLLETTATO_ONERI_ACCESSORI," _
                    & "SUM(BOL_FLUSSI.BOLLETTATO_DEP_CAUZIONALE) AS BOLLETTATO_DEP_CAUZIONALE,SUM(BOL_FLUSSI.BOLLETTATO_QUOTA_SIND) AS BOLLETTATO_QUOTA_SIND," _
                    & "SUM(BOL_FLUSSI.BOLLETTATO_BOLLI_MAV) AS BOLLETTATO_BOLLI_MAV,SUM(BOL_FLUSSI.BOLLETTATO_SPESE_MAV) AS BOLLETTATO_SPESE_MAV," _
                    & "SUM(BOL_FLUSSI.BOLLETTATO_IMPOSTE_REG) AS BOLLETTATO_IMPOSTE_REG,SUM(BOL_FLUSSI.BOLLETTATO_IMPOSTE_BOLLO) AS BOLLETTATO_IMPOSTE_BOLLO " _
                    & " FROM siscom_mi.BOL_FLUSSI,siscom_mi.BOL_BOLLETTE WHERE (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and " _
                    & "bol_bollette.id>=0 and  BOL_BOLLETTE.fl_annullata='0' AND BOL_BOLLETTE.fl_stampato='1' AND BOL_BOLLETTE.ID=BOL_FLUSSI.id_bolletta"

                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    If par.IfNull(myReaderA("BOLLETTATO_CANONI"), "0") <> "0" Then
                        LBL_BOLLETTATO_1.Text = Format(par.IfNull(myReaderA("BOLLETTATO_CANONI"), "0"), "##,##0.00")
                    Else
                        LBL_BOLLETTATO_1.Text = "0,00"
                    End If

                    TOTALE_COLONNA_1 = TOTALE_COLONNA_1 + par.IfNull(myReaderA("BOLLETTATO_CANONI"), 0)
                    Vv1 = par.IfNull(myReaderA("BOLLETTATO_CANONI"), 0)



                    If par.IfNull(myReaderA("BOLLETTATO_ONERI_ACCESSORI"), "0") <> "0" Then
                        LBL_BOLLETTATO_2.Text = Format(par.IfNull(myReaderA("BOLLETTATO_ONERI_ACCESSORI"), "0"), "##,##0.00")
                    Else
                        LBL_BOLLETTATO_2.Text = "0,00"
                    End If
                    TOTALE_COLONNA_1 = TOTALE_COLONNA_1 + par.IfNull(myReaderA("BOLLETTATO_ONERI_ACCESSORI"), 0)
                    Vv2 = par.IfNull(myReaderA("BOLLETTATO_ONERI_ACCESSORI"), 0)


                    If par.IfNull(myReaderA("BOLLETTATO_DEP_CAUZIONALE"), "0") <> "0" Then
                        LBL_BOLLETTATO_3.Text = Format(par.IfNull(myReaderA("BOLLETTATO_DEP_CAUZIONALE"), "0"), "##,##0.00")
                    Else
                        LBL_BOLLETTATO_3.Text = "0,00"
                    End If
                    TOTALE_COLONNA_1 = TOTALE_COLONNA_1 + par.IfNull(myReaderA("BOLLETTATO_DEP_CAUZIONALE"), 0)
                    Vv3 = par.IfNull(myReaderA("BOLLETTATO_DEP_CAUZIONALE"), 0)


                    If par.IfNull(myReaderA("BOLLETTATO_QUOTA_SIND"), "0") <> "0" Then
                        LBL_BOLLETTATO_4.Text = Format(par.IfNull(myReaderA("BOLLETTATO_QUOTA_SIND"), "0"), "##,##0.00")
                    Else
                        LBL_BOLLETTATO_4.Text = "0,00"
                    End If
                    TOTALE_COLONNA_1 = TOTALE_COLONNA_1 + par.IfNull(myReaderA("BOLLETTATO_QUOTA_SIND"), 0)
                    Vv4 = par.IfNull(myReaderA("BOLLETTATO_QUOTA_SIND"), 0)

                    If par.IfNull(myReaderA("BOLLETTATO_BOLLI_MAV"), "0") <> "0" Then
                        LBL_BOLLETTATO_5.Text = Format(par.IfNull(myReaderA("BOLLETTATO_BOLLI_MAV"), "0"), "##,##0.00")
                    Else
                        LBL_BOLLETTATO_5.Text = "0,00"
                    End If
                    TOTALE_COLONNA_1 = TOTALE_COLONNA_1 + par.IfNull(myReaderA("BOLLETTATO_BOLLI_MAV"), 0)
                    Vv5 = par.IfNull(myReaderA("BOLLETTATO_BOLLI_MAV"), 0)


                    If par.IfNull(myReaderA("BOLLETTATO_SPESE_MAV"), "0") <> "0" Then
                        LBL_BOLLETTATO_6.Text = Format(par.IfNull(myReaderA("BOLLETTATO_SPESE_MAV"), "0"), "##,##0.00")
                    Else
                        LBL_BOLLETTATO_6.Text = "0,00"
                    End If
                    TOTALE_COLONNA_1 = TOTALE_COLONNA_1 + par.IfNull(myReaderA("BOLLETTATO_SPESE_MAV"), 0)
                    Vv6 = par.IfNull(myReaderA("BOLLETTATO_SPESE_MAV"), 0)

                    If par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_REG"), "0") <> "0" Then
                        LBL_BOLLETTATO_7.Text = Format(par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_REG"), "0"), "##,##0.00")
                    Else
                        LBL_BOLLETTATO_7.Text = "0,00"
                    End If
                    TOTALE_COLONNA_1 = TOTALE_COLONNA_1 + par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_REG"), 0)
                    Vv7 = par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_REG"), 0)

                    If par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_BOLLO"), "0") <> "0" Then
                        LBL_BOLLETTATO_8.Text = Format(par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_BOLLO"), "0"), "##,##0.00")
                    Else
                        LBL_BOLLETTATO_8.Text = "0,00"
                    End If
                    TOTALE_COLONNA_1 = TOTALE_COLONNA_1 + par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_BOLLO"), 0)
                    Vv8 = par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_BOLLO"), 0)

                End If
                myReaderA.Close()

                LBL_BOLLETTATO_9.Text = Format(TOTALE_COLONNA_1, "##,##0.00")



                ' COLONNA SCADUTO

                par.cmd.CommandText = "SELECT SUM(BOL_FLUSSI.BOLLETTATO_CANONI) AS BOLLETTATO_CANONI,SUM(BOL_FLUSSI.BOLLETTATO_ONERI_ACCESSORI) AS BOLLETTATO_ONERI_ACCESSORI," _
                                    & "SUM(BOL_FLUSSI.BOLLETTATO_DEP_CAUZIONALE) AS BOLLETTATO_DEP_CAUZIONALE,SUM(BOL_FLUSSI.BOLLETTATO_QUOTA_SIND) AS BOLLETTATO_QUOTA_SIND," _
                                    & "SUM(BOL_FLUSSI.BOLLETTATO_BOLLI_MAV) AS BOLLETTATO_BOLLI_MAV,SUM(BOL_FLUSSI.BOLLETTATO_SPESE_MAV) AS BOLLETTATO_SPESE_MAV," _
                                    & "SUM(BOL_FLUSSI.BOLLETTATO_IMPOSTE_REG) AS BOLLETTATO_IMPOSTE_REG,SUM(BOL_FLUSSI.BOLLETTATO_IMPOSTE_BOLLO) AS BOLLETTATO_IMPOSTE_BOLLO " _
                                    & " FROM siscom_mi.BOL_FLUSSI,siscom_mi.BOL_BOLLETTE WHERE (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and " _
                                    & "BOL_BOLLETTE.DATA_SCADENZA<" & Format(Now, "yyyyMMdd") & " AND bol_bollette.id>=0 and  BOL_BOLLETTE.fl_annullata='0' AND BOL_BOLLETTE.fl_stampato='1' AND BOL_BOLLETTE.ID=BOL_FLUSSI.id_bolletta"

                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    If par.IfNull(myReaderA("BOLLETTATO_CANONI"), "0") <> "0" Then
                        LBL_SCADUTO_1.Text = Format(par.IfNull(myReaderA("BOLLETTATO_CANONI"), "0"), "##,##0.00")
                    Else
                        LBL_SCADUTO_1.Text = "0,00"
                    End If
                    TOTALE_COLONNA_4 = TOTALE_COLONNA_4 + par.IfNull(myReaderA("BOLLETTATO_CANONI"), 0)
                    v1 = par.IfNull(myReaderA("BOLLETTATO_CANONI"), 0)



                    If par.IfNull(myReaderA("BOLLETTATO_ONERI_ACCESSORI"), "0") <> "0" Then
                        LBL_SCADUTO_2.Text = Format(par.IfNull(myReaderA("BOLLETTATO_ONERI_ACCESSORI"), "0"), "##,##0.00")
                    Else
                        LBL_SCADUTO_2.Text = "0,00"
                    End If
                    TOTALE_COLONNA_4 = TOTALE_COLONNA_4 + par.IfNull(myReaderA("BOLLETTATO_ONERI_ACCESSORI"), 0)
                    v2 = par.IfNull(myReaderA("BOLLETTATO_ONERI_ACCESSORI"), 0)

                    If par.IfNull(myReaderA("BOLLETTATO_DEP_CAUZIONALE"), "0") <> "0" Then
                        LBL_SCADUTO_3.Text = Format(par.IfNull(myReaderA("BOLLETTATO_DEP_CAUZIONALE"), "0"), "##,##0.00")
                    Else
                        LBL_SCADUTO_3.Text = "0,00"
                    End If
                    TOTALE_COLONNA_4 = TOTALE_COLONNA_4 + par.IfNull(myReaderA("BOLLETTATO_DEP_CAUZIONALE"), 0)
                    v3 = par.IfNull(myReaderA("BOLLETTATO_DEP_CAUZIONALE"), 0)

                    If par.IfNull(myReaderA("BOLLETTATO_QUOTA_SIND"), "0") <> "0" Then
                        LBL_SCADUTO_4.Text = Format(par.IfNull(myReaderA("BOLLETTATO_QUOTA_SIND"), "0"), "##,##0.00")
                    Else
                        LBL_SCADUTO_4.Text = "0,00"
                    End If
                    TOTALE_COLONNA_4 = TOTALE_COLONNA_4 + par.IfNull(myReaderA("BOLLETTATO_QUOTA_SIND"), 0)
                    v4 = par.IfNull(myReaderA("BOLLETTATO_QUOTA_SIND"), 0)

                    If par.IfNull(myReaderA("BOLLETTATO_BOLLI_MAV"), "0") <> "0" Then
                        LBL_SCADUTO_5.Text = Format(par.IfNull(myReaderA("BOLLETTATO_BOLLI_MAV"), "0"), "##,##0.00")
                    Else
                        LBL_SCADUTO_5.Text = "0,00"
                    End If
                    TOTALE_COLONNA_4 = TOTALE_COLONNA_4 + par.IfNull(myReaderA("BOLLETTATO_BOLLI_MAV"), 0)
                    v5 = par.IfNull(myReaderA("BOLLETTATO_BOLLI_MAV"), 0)


                    If par.IfNull(myReaderA("BOLLETTATO_SPESE_MAV"), "0") <> "0" Then
                        LBL_SCADUTO_6.Text = Format(par.IfNull(myReaderA("BOLLETTATO_SPESE_MAV"), "0"), "##,##0.00")
                    Else
                        LBL_SCADUTO_6.Text = "0,00"
                    End If
                    TOTALE_COLONNA_4 = TOTALE_COLONNA_4 + par.IfNull(myReaderA("BOLLETTATO_SPESE_MAV"), 0)
                    v6 = par.IfNull(myReaderA("BOLLETTATO_SPESE_MAV"), 0)

                    If par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_REG"), "0") <> "0" Then
                        LBL_SCADUTO_7.Text = Format(par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_REG"), "0"), "##,##0.00")
                    Else
                        LBL_SCADUTO_7.Text = "0,00"
                    End If
                    TOTALE_COLONNA_4 = TOTALE_COLONNA_4 + par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_REG"), 0)
                    v7 = par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_REG"), 0)

                    If par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_BOLLO"), "0") <> "0" Then
                        LBL_SCADUTO_8.Text = Format(par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_BOLLO"), "0"), "##,##0.00")
                    Else
                        LBL_SCADUTO_8.Text = "0,00"
                    End If
                    TOTALE_COLONNA_4 = TOTALE_COLONNA_4 + par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_BOLLO"), 0)
                    v8 = par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_BOLLO"), 0)

                End If
                myReaderA.Close()

                LBL_BOLLETTATO_9.Text = Format(TOTALE_COLONNA_1, "##,##0.00")

                v9 = TOTALE_COLONNA_4
                LBL_SCADUTO_9.Text = Format(TOTALE_COLONNA_4, "##,##0.00")

               

                ' COLONNA INCASSATO
                par.cmd.CommandText = "SELECT SUM(BOL_FLUSSI.BOLLETTATO_CANONI) AS BOLLETTATO_CANONI,SUM(BOL_FLUSSI.BOLLETTATO_ONERI_ACCESSORI) AS BOLLETTATO_ONERI_ACCESSORI," _
                    & "SUM(BOL_FLUSSI.BOLLETTATO_DEP_CAUZIONALE) AS BOLLETTATO_DEP_CAUZIONALE,SUM(BOL_FLUSSI.BOLLETTATO_QUOTA_SIND) AS BOLLETTATO_QUOTA_SIND," _
                    & "SUM(BOL_FLUSSI.BOLLETTATO_BOLLI_MAV) AS BOLLETTATO_BOLLI_MAV,SUM(BOL_FLUSSI.BOLLETTATO_SPESE_MAV) AS BOLLETTATO_SPESE_MAV," _
                    & "SUM(BOL_FLUSSI.BOLLETTATO_IMPOSTE_REG) AS BOLLETTATO_IMPOSTE_REG,SUM(BOL_FLUSSI.BOLLETTATO_IMPOSTE_BOLLO) AS BOLLETTATO_IMPOSTE_BOLLO " _
                    & " FROM siscom_mi.BOL_FLUSSI,siscom_mi.BOL_BOLLETTE WHERE  (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and  (BOL_BOLLETTE.DATA_PAGAMENTO IS NOT NULL OR BOL_BOLLETTE.DATA_PAGAMENTO<>'') AND  bol_bollette.id>=0 and  BOL_BOLLETTE.fl_annullata='0' AND BOL_BOLLETTE.fl_stampato='1' AND BOL_BOLLETTE.ID=BOL_FLUSSI.id_bolletta"

                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    If par.IfNull(myReaderA("BOLLETTATO_CANONI"), "0") <> "0" Then
                        LBL_INCASSATO_1.Text = Format(par.IfNull(myReaderA("BOLLETTATO_CANONI"), "0"), "##,##0.00")
                    Else
                        LBL_INCASSATO_1.Text = "0,00"
                    End If
                    TOTALE_COLONNA_2 = TOTALE_COLONNA_2 + par.IfNull(myReaderA("BOLLETTATO_CANONI"), 0)
                    LBL_PERCENTUALE_A_1.Text = Format((100 * par.IfNull(myReaderA("BOLLETTATO_CANONI"), 0)) / Vv1, "0.00")


                    If par.IfNull(myReaderA("BOLLETTATO_ONERI_ACCESSORI"), "0") <> "0" Then
                        LBL_INCASSATO_2.Text = Format(par.IfNull(myReaderA("BOLLETTATO_ONERI_ACCESSORI"), "0"), "##,##0.00")
                    Else
                        LBL_INCASSATO_2.Text = "0,00"
                    End If
                    TOTALE_COLONNA_2 = TOTALE_COLONNA_2 + par.IfNull(myReaderA("BOLLETTATO_ONERI_ACCESSORI"), 0)
                    LBL_PERCENTUALE_A_2.Text = Format((100 * par.IfNull(myReaderA("BOLLETTATO_ONERI_ACCESSORI"), 0)) / Vv2, "0.00")


                    If par.IfNull(myReaderA("BOLLETTATO_DEP_CAUZIONALE"), "0") <> "0" Then
                        LBL_INCASSATO_3.Text = Format(par.IfNull(myReaderA("BOLLETTATO_DEP_CAUZIONALE"), "0"), "##,##0.00")
                    Else
                        LBL_INCASSATO_3.Text = "0,00"
                    End If
                    TOTALE_COLONNA_2 = TOTALE_COLONNA_2 + par.IfNull(myReaderA("BOLLETTATO_DEP_CAUZIONALE"), 0)
                    LBL_PERCENTUALE_A_3.Text = Format((100 * par.IfNull(myReaderA("BOLLETTATO_DEP_CAUZIONALE"), 0)) / Vv3, "0.00")


                    If par.IfNull(myReaderA("BOLLETTATO_QUOTA_SIND"), "0") <> "0" Then
                        LBL_INCASSATO_4.Text = Format(par.IfNull(myReaderA("BOLLETTATO_QUOTA_SIND"), "0"), "##,##0.00")
                    Else
                        LBL_INCASSATO_4.Text = "0,00"
                    End If
                    TOTALE_COLONNA_2 = TOTALE_COLONNA_2 + par.IfNull(myReaderA("BOLLETTATO_QUOTA_SIND"), 0)
                    LBL_PERCENTUALE_A_4.Text = Format((100 * par.IfNull(myReaderA("BOLLETTATO_QUOTA_SIND"), 0)) / Vv4, "0.00")

                    If par.IfNull(myReaderA("BOLLETTATO_BOLLI_MAV"), "0") <> "0" Then
                        LBL_INCASSATO_5.Text = Format(par.IfNull(myReaderA("BOLLETTATO_BOLLI_MAV"), "0"), "##,##0.00")
                    Else
                        LBL_INCASSATO_5.Text = "0,00"
                    End If
                    TOTALE_COLONNA_2 = TOTALE_COLONNA_2 + par.IfNull(myReaderA("BOLLETTATO_BOLLI_MAV"), 0)
                    LBL_PERCENTUALE_A_5.Text = Format((100 * par.IfNull(myReaderA("BOLLETTATO_BOLLI_MAV"), 0)) / Vv5, "0.00")


                    If par.IfNull(myReaderA("BOLLETTATO_SPESE_MAV"), "0") <> "0" Then
                        LBL_INCASSATO_6.Text = Format(par.IfNull(myReaderA("BOLLETTATO_SPESE_MAV"), "0"), "##,##0.00")
                    Else
                        LBL_INCASSATO_6.Text = "0,00"
                    End If
                    TOTALE_COLONNA_2 = TOTALE_COLONNA_2 + par.IfNull(myReaderA("BOLLETTATO_SPESE_MAV"), 0)
                    LBL_PERCENTUALE_A_6.Text = Format((100 * par.IfNull(myReaderA("BOLLETTATO_SPESE_MAV"), 0)) / Vv6, "0.00")



                    If par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_REG"), "0") <> "0" Then
                        LBL_INCASSATO_7.Text = Format(par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_REG"), "0"), "##,##0.00")
                    Else
                        LBL_INCASSATO_7.Text = "0,00"
                    End If
                    TOTALE_COLONNA_2 = TOTALE_COLONNA_2 + par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_REG"), 0)
                    LBL_PERCENTUALE_A_7.Text = Format((100 * par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_REG"), 0)) / Vv7, "0.00")


                    If par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_BOLLO"), "0") <> "0" Then
                        LBL_INCASSATO_8.Text = Format(par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_BOLLO"), "0"), "##,##0.00")
                    Else
                        LBL_INCASSATO_8.Text = "0,00"
                    End If
                    TOTALE_COLONNA_2 = TOTALE_COLONNA_2 + par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_BOLLO"), 0)
                    LBL_PERCENTUALE_A_8.Text = Format((100 * par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_BOLLO"), 0)) / Vv8, "0.00")

                End If
                myReaderA.Close()

                LBL_INCASSATO_9.Text = Format(TOTALE_COLONNA_2, "##,##0.00")
                LBL_PERCENTUALE_A_9.Text = Format((100 * TOTALE_COLONNA_2) / TOTALE_COLONNA_1, "0.00")


                'COLONNA INCASSATO SCADUTO

                par.cmd.CommandText = "SELECT SUM(BOL_FLUSSI.BOLLETTATO_CANONI) AS BOLLETTATO_CANONI,SUM(BOL_FLUSSI.BOLLETTATO_ONERI_ACCESSORI) AS BOLLETTATO_ONERI_ACCESSORI," _
   & "SUM(BOL_FLUSSI.BOLLETTATO_DEP_CAUZIONALE) AS BOLLETTATO_DEP_CAUZIONALE,SUM(BOL_FLUSSI.BOLLETTATO_QUOTA_SIND) AS BOLLETTATO_QUOTA_SIND," _
   & "SUM(BOL_FLUSSI.BOLLETTATO_BOLLI_MAV) AS BOLLETTATO_BOLLI_MAV,SUM(BOL_FLUSSI.BOLLETTATO_SPESE_MAV) AS BOLLETTATO_SPESE_MAV," _
   & "SUM(BOL_FLUSSI.BOLLETTATO_IMPOSTE_REG) AS BOLLETTATO_IMPOSTE_REG,SUM(BOL_FLUSSI.BOLLETTATO_IMPOSTE_BOLLO) AS BOLLETTATO_IMPOSTE_BOLLO " _
   & " FROM siscom_mi.BOL_FLUSSI,siscom_mi.BOL_BOLLETTE WHERE (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and  (BOL_BOLLETTE.DATA_PAGAMENTO IS NOT NULL OR BOL_BOLLETTE.DATA_PAGAMENTO<>'') and data_scadenza<" & Format(Now, "yyyyMMdd") & " AND   bol_bollette.id>=0 and  BOL_BOLLETTE.fl_annullata='0' AND BOL_BOLLETTE.fl_stampato='1' AND BOL_BOLLETTE.ID=BOL_FLUSSI.id_bolletta"

                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then

                    If par.IfNull(myReaderA("BOLLETTATO_CANONI"), "0") <> "0" Then
                        LBL_INCASSATO_SC_1.Text = Format(par.IfNull(myReaderA("BOLLETTATO_CANONI"), "0"), "##,##0.00")
                    Else
                        LBL_INCASSATO_SC_1.Text = "0,00"
                    End If
                    TOTALE_COLONNA_5 = TOTALE_COLONNA_5 + par.IfNull(myReaderA("BOLLETTATO_CANONI"), 0)
                    LBL_PERCENTUALE_1.Text = Format((100 * par.IfNull(myReaderA("BOLLETTATO_CANONI"), 0)) / v1, "0.00")

                    If par.IfNull(myReaderA("BOLLETTATO_ONERI_ACCESSORI"), "0") <> "0" Then
                        LBL_INCASSATO_SC_2.Text = Format(par.IfNull(myReaderA("BOLLETTATO_ONERI_ACCESSORI"), "0"), "##,##0.00")
                    Else
                        LBL_INCASSATO_SC_2.Text = "0,00"
                    End If
                    TOTALE_COLONNA_5 = TOTALE_COLONNA_5 + par.IfNull(myReaderA("BOLLETTATO_ONERI_ACCESSORI"), 0)
                    LBL_PERCENTUALE_2.Text = Format((100 * par.IfNull(myReaderA("BOLLETTATO_ONERI_ACCESSORI"), 0)) / v2, "0.00")

                    If par.IfNull(myReaderA("BOLLETTATO_DEP_CAUZIONALE"), "0") <> "0" Then
                        LBL_INCASSATO_SC_3.Text = Format(par.IfNull(myReaderA("BOLLETTATO_DEP_CAUZIONALE"), "0"), "##,##0.00")
                    Else
                        LBL_INCASSATO_SC_3.Text = "0,00"
                    End If
                    TOTALE_COLONNA_5 = TOTALE_COLONNA_5 + par.IfNull(myReaderA("BOLLETTATO_DEP_CAUZIONALE"), 0)
                    LBL_PERCENTUALE_3.Text = Format((100 * par.IfNull(myReaderA("BOLLETTATO_DEP_CAUZIONALE"), 0)) / v3, "0.00")

                    If par.IfNull(myReaderA("BOLLETTATO_QUOTA_SIND"), "0") <> "0" Then
                        LBL_INCASSATO_SC_4.Text = Format(par.IfNull(myReaderA("BOLLETTATO_QUOTA_SIND"), "0"), "##,##0.00")
                    Else
                        LBL_INCASSATO_SC_4.Text = "0,00"
                    End If
                    TOTALE_COLONNA_5 = TOTALE_COLONNA_5 + par.IfNull(myReaderA("BOLLETTATO_QUOTA_SIND"), 0)
                    LBL_PERCENTUALE_4.Text = Format((100 * par.IfNull(myReaderA("BOLLETTATO_QUOTA_SIND"), 0)) / v4, "0.00")

                    If par.IfNull(myReaderA("BOLLETTATO_BOLLI_MAV"), "0") <> "0" Then
                        LBL_INCASSATO_SC_5.Text = Format(par.IfNull(myReaderA("BOLLETTATO_BOLLI_MAV"), "0"), "##,##0.00")
                    Else
                        LBL_INCASSATO_SC_5.Text = "0,00"
                    End If
                    TOTALE_COLONNA_5 = TOTALE_COLONNA_5 + par.IfNull(myReaderA("BOLLETTATO_BOLLI_MAV"), 0)
                    LBL_PERCENTUALE_5.Text = Format((100 * par.IfNull(myReaderA("BOLLETTATO_BOLLI_MAV"), 0)) / v5, "0.00")

                    If par.IfNull(myReaderA("BOLLETTATO_SPESE_MAV"), "0") <> "0" Then
                        LBL_INCASSATO_SC_6.Text = Format(par.IfNull(myReaderA("BOLLETTATO_SPESE_MAV"), "0"), "##,##0.00")
                    Else
                        LBL_INCASSATO_SC_6.Text = "0,00"
                    End If
                    TOTALE_COLONNA_5 = TOTALE_COLONNA_5 + par.IfNull(myReaderA("BOLLETTATO_SPESE_MAV"), 0)
                    LBL_PERCENTUALE_6.Text = Format((100 * par.IfNull(myReaderA("BOLLETTATO_SPESE_MAV"), 0)) / v6, "0.00")


                    If par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_REG"), "0") <> "0" Then
                        LBL_INCASSATO_SC_7.Text = Format(par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_REG"), "0"), "##,##0.00")
                    Else
                        LBL_INCASSATO_SC_7.Text = "0,00"
                    End If
                    TOTALE_COLONNA_5 = TOTALE_COLONNA_5 + par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_REG"), 0)
                    LBL_PERCENTUALE_7.Text = Format((100 * par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_REG"), 0)) / v7, "0.00")

                    If par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_BOLLO"), "0") <> "0" Then
                        LBL_INCASSATO_SC_8.Text = Format(par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_BOLLO"), "0"), "##,##0.00")
                    Else
                        LBL_INCASSATO_SC_8.Text = "0,00"
                    End If
                    TOTALE_COLONNA_5 = TOTALE_COLONNA_5 + par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_BOLLO"), 0)
                    LBL_PERCENTUALE_8.Text = Format((100 * par.IfNull(myReaderA("BOLLETTATO_IMPOSTE_BOLLO"), 0)) / v8, "0.00")

                End If
                myReaderA.Close()

                LBL_INCASSATO_SC_9.Text = Format(TOTALE_COLONNA_5, "##,##0.00")
                LBL_PERCENTUALE_9.Text = Format((100 * TOTALE_COLONNA_5) / TOTALE_COLONNA_4, "0.00")



                DT.Columns.Add("VOCE")
                DT.Columns.Add("BOLLETTATO")
                DT.Columns.Add("SCADUTO")
                DT.Columns.Add("INCASSATO")
                DT.Columns.Add("INCASSATO_SCADUTO")
                DT.Columns.Add("P_INC_BOLL")
                DT.Columns.Add("P_INC_SCA_BOLL_SCA")

                Dim RIGA As System.Data.DataRow

                RIGA = DT.NewRow()
                RIGA.Item("VOCE") = "COMPETENZA COMUNE"
                RIGA.Item("BOLLETTATO") = ""
                RIGA.Item("SCADUTO") = ""
                RIGA.Item("INCASSATO") = ""
                RIGA.Item("INCASSATO_SCADUTO") = ""
                RIGA.Item("P_INC_BOLL") = ""
                RIGA.Item("P_INC_SCA_BOLL_SCA") = ""
                DT.Rows.Add(RIGA)


                RIGA = DT.NewRow()
                RIGA.Item("VOCE") = "CANONI"
                RIGA.Item("BOLLETTATO") = LBL_BOLLETTATO_1.Text
                RIGA.Item("SCADUTO") = LBL_SCADUTO_1.Text
                RIGA.Item("INCASSATO") = LBL_INCASSATO_1.Text
                RIGA.Item("INCASSATO_SCADUTO") = LBL_INCASSATO_SC_1.Text
                RIGA.Item("P_INC_BOLL") = LBL_PERCENTUALE_A_1.Text
                RIGA.Item("P_INC_SCA_BOLL_SCA") = LBL_PERCENTUALE_1.Text
                DT.Rows.Add(RIGA)

                RIGA = DT.NewRow()
                RIGA.Item("VOCE") = "IMPOSTE DI REGISTRO"
                RIGA.Item("BOLLETTATO") = LBL_BOLLETTATO_7.Text
                RIGA.Item("SCADUTO") = LBL_SCADUTO_7.Text
                RIGA.Item("INCASSATO") = LBL_INCASSATO_7.Text
                RIGA.Item("INCASSATO_SCADUTO") = LBL_INCASSATO_SC_7.Text
                RIGA.Item("P_INC_BOLL") = LBL_PERCENTUALE_A_7.Text
                RIGA.Item("P_INC_SCA_BOLL_SCA") = LBL_PERCENTUALE_7.Text
                DT.Rows.Add(RIGA)

                RIGA = DT.NewRow()
                RIGA.Item("VOCE") = "IMPOSTE DI BOLLO SU CONTRATTI"
                RIGA.Item("BOLLETTATO") = LBL_BOLLETTATO_8.Text
                RIGA.Item("SCADUTO") = LBL_SCADUTO_8.Text
                RIGA.Item("INCASSATO") = LBL_INCASSATO_8.Text
                RIGA.Item("INCASSATO_SCADUTO") = LBL_INCASSATO_SC_8.Text
                RIGA.Item("P_INC_BOLL") = LBL_PERCENTUALE_A_8.Text
                RIGA.Item("P_INC_SCA_BOLL_SCA") = LBL_PERCENTUALE_8.Text
                DT.Rows.Add(RIGA)

                RIGA = DT.NewRow()
                RIGA.Item("VOCE") = "SPESE MAV"
                RIGA.Item("BOLLETTATO") = LBL_BOLLETTATO_6.Text
                RIGA.Item("SCADUTO") = LBL_SCADUTO_6.Text
                RIGA.Item("INCASSATO") = LBL_INCASSATO_6.Text
                RIGA.Item("INCASSATO_SCADUTO") = LBL_INCASSATO_SC_6.Text
                RIGA.Item("P_INC_BOLL") = LBL_PERCENTUALE_A_6.Text
                RIGA.Item("P_INC_SCA_BOLL_SCA") = LBL_PERCENTUALE_6.Text
                DT.Rows.Add(RIGA)

                RIGA = DT.NewRow()
                RIGA.Item("VOCE") = "COMPETENZA GESTORE"
                RIGA.Item("BOLLETTATO") = ""
                RIGA.Item("SCADUTO") = ""
                RIGA.Item("INCASSATO") = ""
                RIGA.Item("INCASSATO_SCADUTO") = ""
                RIGA.Item("P_INC_BOLL") = ""
                RIGA.Item("P_INC_SCA_BOLL_SCA") = ""
                DT.Rows.Add(RIGA)


                RIGA = DT.NewRow()
                RIGA.Item("VOCE") = "ONERI ACCESSORI"
                RIGA.Item("BOLLETTATO") = LBL_BOLLETTATO_2.Text
                RIGA.Item("SCADUTO") = LBL_SCADUTO_2.Text
                RIGA.Item("INCASSATO") = LBL_INCASSATO_2.Text
                RIGA.Item("INCASSATO_SCADUTO") = LBL_INCASSATO_SC_2.Text
                RIGA.Item("P_INC_BOLL") = LBL_PERCENTUALE_A_2.Text
                RIGA.Item("P_INC_SCA_BOLL_SCA") = LBL_PERCENTUALE_2.Text
                DT.Rows.Add(RIGA)

                RIGA = DT.NewRow()
                RIGA.Item("VOCE") = "BOLLI SU MAV"
                RIGA.Item("BOLLETTATO") = LBL_BOLLETTATO_5.Text
                RIGA.Item("SCADUTO") = LBL_SCADUTO_5.Text
                RIGA.Item("INCASSATO") = LBL_INCASSATO_5.Text
                RIGA.Item("INCASSATO_SCADUTO") = LBL_INCASSATO_SC_5.Text
                RIGA.Item("P_INC_BOLL") = LBL_PERCENTUALE_A_5.Text
                RIGA.Item("P_INC_SCA_BOLL_SCA") = LBL_PERCENTUALE_5.Text
                DT.Rows.Add(RIGA)

                RIGA = DT.NewRow()
                RIGA.Item("VOCE") = "DEPOSITI CAUZIONALI"
                RIGA.Item("BOLLETTATO") = LBL_BOLLETTATO_3.Text
                RIGA.Item("SCADUTO") = LBL_SCADUTO_3.Text
                RIGA.Item("INCASSATO") = LBL_INCASSATO_3.Text
                RIGA.Item("INCASSATO_SCADUTO") = LBL_INCASSATO_SC_3.Text
                RIGA.Item("P_INC_BOLL") = LBL_PERCENTUALE_A_3.Text
                RIGA.Item("P_INC_SCA_BOLL_SCA") = LBL_PERCENTUALE_3.Text
                DT.Rows.Add(RIGA)

                RIGA = DT.NewRow()
                RIGA.Item("VOCE") = "QUOTA SINDACALE"
                RIGA.Item("BOLLETTATO") = LBL_BOLLETTATO_4.Text
                RIGA.Item("SCADUTO") = LBL_SCADUTO_4.Text
                RIGA.Item("INCASSATO") = LBL_INCASSATO_4.Text
                RIGA.Item("INCASSATO_SCADUTO") = LBL_INCASSATO_SC_4.Text
                RIGA.Item("P_INC_BOLL") = LBL_PERCENTUALE_A_4.Text
                RIGA.Item("P_INC_SCA_BOLL_SCA") = LBL_PERCENTUALE_4.Text
                DT.Rows.Add(RIGA)


                RIGA = DT.NewRow()
                RIGA.Item("VOCE") = "TOTALE"
                RIGA.Item("BOLLETTATO") = LBL_BOLLETTATO_9.Text
                RIGA.Item("SCADUTO") = LBL_SCADUTO_9.Text
                RIGA.Item("INCASSATO") = LBL_INCASSATO_9.Text
                RIGA.Item("INCASSATO_SCADUTO") = LBL_INCASSATO_SC_9.Text
                RIGA.Item("P_INC_BOLL") = LBL_PERCENTUALE_A_9.Text
                RIGA.Item("P_INC_SCA_BOLL_SCA") = LBL_PERCENTUALE_9.Text
                DT.Rows.Add(RIGA)



                Session.Add("e_MIADTS2", DT)


                contenuto = Replace(contenuto, "$A1$", LBL_BOLLETTATO_1.Text)
                contenuto = Replace(contenuto, "$A2$", LBL_BOLLETTATO_7.Text)
                contenuto = Replace(contenuto, "$A3$", LBL_BOLLETTATO_8.Text)
                contenuto = Replace(contenuto, "$A4$", LBL_BOLLETTATO_6.Text)
                contenuto = Replace(contenuto, "$A5$", LBL_BOLLETTATO_2.Text)
                contenuto = Replace(contenuto, "$A6$", LBL_BOLLETTATO_5.Text)
                contenuto = Replace(contenuto, "$A7$", LBL_BOLLETTATO_3.Text)
                contenuto = Replace(contenuto, "$A8$", LBL_BOLLETTATO_4.Text)
                contenuto = Replace(contenuto, "$A9$", LBL_BOLLETTATO_9.Text)

                contenuto = Replace(contenuto, "$B1$", LBL_SCADUTO_1.Text)
                contenuto = Replace(contenuto, "$B2$", LBL_SCADUTO_7.Text)
                contenuto = Replace(contenuto, "$B3$", LBL_SCADUTO_8.Text)
                contenuto = Replace(contenuto, "$B4$", LBL_SCADUTO_6.Text)
                contenuto = Replace(contenuto, "$B5$", LBL_SCADUTO_2.Text)
                contenuto = Replace(contenuto, "$B6$", LBL_SCADUTO_5.Text)
                contenuto = Replace(contenuto, "$B7$", LBL_SCADUTO_3.Text)
                contenuto = Replace(contenuto, "$B8$", LBL_SCADUTO_4.Text)
                contenuto = Replace(contenuto, "$B9$", LBL_SCADUTO_9.Text)

                contenuto = Replace(contenuto, "$C1$", LBL_INCASSATO_1.Text)
                contenuto = Replace(contenuto, "$C2$", LBL_INCASSATO_7.Text)
                contenuto = Replace(contenuto, "$C3$", LBL_INCASSATO_8.Text)
                contenuto = Replace(contenuto, "$C4$", LBL_INCASSATO_6.Text)
                contenuto = Replace(contenuto, "$C5$", LBL_INCASSATO_2.Text)
                contenuto = Replace(contenuto, "$C6$", LBL_INCASSATO_5.Text)
                contenuto = Replace(contenuto, "$C7$", LBL_INCASSATO_3.Text)
                contenuto = Replace(contenuto, "$C8$", LBL_INCASSATO_4.Text)
                contenuto = Replace(contenuto, "$C9$", LBL_INCASSATO_9.Text)

                contenuto = Replace(contenuto, "$D1$", LBL_INCASSATO_SC_1.Text)
                contenuto = Replace(contenuto, "$D2$", LBL_INCASSATO_SC_7.Text)
                contenuto = Replace(contenuto, "$D3$", LBL_INCASSATO_SC_8.Text)
                contenuto = Replace(contenuto, "$D4$", LBL_INCASSATO_SC_6.Text)
                contenuto = Replace(contenuto, "$D5$", LBL_INCASSATO_SC_2.Text)
                contenuto = Replace(contenuto, "$D6$", LBL_INCASSATO_SC_5.Text)
                contenuto = Replace(contenuto, "$D7$", LBL_INCASSATO_SC_3.Text)
                contenuto = Replace(contenuto, "$D8$", LBL_INCASSATO_SC_4.Text)
                contenuto = Replace(contenuto, "$D9$", LBL_INCASSATO_SC_9.Text)


                contenuto = Replace(contenuto, "$E1$", LBL_PERCENTUALE_A_1.Text)
                contenuto = Replace(contenuto, "$E2$", LBL_PERCENTUALE_A_7.Text)
                contenuto = Replace(contenuto, "$E3$", LBL_PERCENTUALE_A_8.Text)
                contenuto = Replace(contenuto, "$E4$", LBL_PERCENTUALE_A_6.Text)
                contenuto = Replace(contenuto, "$E5$", LBL_PERCENTUALE_A_2.Text)
                contenuto = Replace(contenuto, "$E6$", LBL_PERCENTUALE_A_5.Text)
                contenuto = Replace(contenuto, "$E7$", LBL_PERCENTUALE_A_3.Text)
                contenuto = Replace(contenuto, "$E8$", LBL_PERCENTUALE_A_4.Text)
                contenuto = Replace(contenuto, "$E9$", LBL_PERCENTUALE_A_9.Text)

                contenuto = Replace(contenuto, "$F1$", LBL_PERCENTUALE_1.Text)
                contenuto = Replace(contenuto, "$F2$", LBL_PERCENTUALE_7.Text)
                contenuto = Replace(contenuto, "$F3$", LBL_PERCENTUALE_8.Text)
                contenuto = Replace(contenuto, "$F4$", LBL_PERCENTUALE_6.Text)
                contenuto = Replace(contenuto, "$F5$", LBL_PERCENTUALE_2.Text)
                contenuto = Replace(contenuto, "$F6$", LBL_PERCENTUALE_5.Text)
                contenuto = Replace(contenuto, "$F7$", LBL_PERCENTUALE_3.Text)
                contenuto = Replace(contenuto, "$F8$", LBL_PERCENTUALE_4.Text)
                contenuto = Replace(contenuto, "$F9$", LBL_PERCENTUALE_9.Text)

                Session.Add("MIADTS2", contenuto)

                ''COMPETENZE COMUNE
                'par.cmd.CommandText = "select sum(bol_bollette_voci.importo) as importo from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette where  (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and bol_bollette.fl_annullata='0' and bol_bollette.fl_stampato='1' and bol_bollette.id=bol_bollette_voci.id_bolletta and t_voci_bolletta.id=bol_bollette_voci.id_voce and t_voci_bolletta.competenza=1 AND ID_CAPITOLO<>8 AND ID_CAPITOLO<>5 AND ID_CAPITOLO<>6 "
                'Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    If par.IfNull(myReaderA("importo"), "0") <> "0" Then
                '        LBL_BOLLETTATO_1.Text = Format(par.IfNull(myReaderA("importo"), "0"), "##,##0.00")
                '    Else
                '        LBL_BOLLETTATO_1.Text = "0,00"
                '    End If

                '    TOTALE_COLONNA_1 = TOTALE_COLONNA_1 + par.IfNull(myReaderA("importo"), 0)

                'End If
                'myReaderA.Close()

                ''COMPETENZE GESTORE
                'par.cmd.CommandText = "select sum(bol_bollette_voci.importo) as importo from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette where   (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and bol_bollette.fl_annullata='0' and bol_bollette.fl_stampato='1' and bol_bollette.id=bol_bollette_voci.id_bolletta and t_voci_bolletta.id=bol_bollette_voci.id_voce and t_voci_bolletta.competenza=2 AND ID_CAPITOLO<>4 "
                'myReaderA = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    If par.IfNull(myReaderA("importo"), "0") <> "0" Then
                '        LBL_BOLLETTATO_2.Text = Format(par.IfNull(myReaderA("importo"), "0"), "##,##0.00")
                '    Else
                '        LBL_BOLLETTATO_2.Text = "0,00"
                '    End If
                '    TOTALE_COLONNA_1 = TOTALE_COLONNA_1 + par.IfNull(myReaderA("importo"), 0)
                '    'v2 = par.IfNull(myReaderA("importo"), 0)
                'End If
                'myReaderA.Close()

                ''DEPOSITO CAUZIONALE
                'par.cmd.CommandText = "select sum(bol_bollette_voci.importo) as importo from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette where  (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and bol_bollette.fl_annullata='0' and bol_bollette.fl_stampato='1' and bol_bollette.id=bol_bollette_voci.id_bolletta and t_voci_bolletta.id=bol_bollette_voci.id_voce and t_voci_bolletta.COMPETENZA=0"
                'myReaderA = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    If par.IfNull(myReaderA("importo"), "0") <> "0" Then
                '        LBL_BOLLETTATO_3.Text = Format(par.IfNull(myReaderA("importo"), "0"), "##,##0.00")
                '    Else
                '        LBL_BOLLETTATO_3.Text = "0,00"
                '    End If
                '    TOTALE_COLONNA_1 = TOTALE_COLONNA_1 + par.IfNull(myReaderA("importo"), 0)
                '    'v3 = par.IfNull(myReaderA("importo"), 0)
                'End If
                'myReaderA.Close()

                ''QUOTA SINDACALE
                'par.cmd.CommandText = "select sum(bol_bollette_voci.importo) as importo from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette where  (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and bol_bollette.fl_annullata='0' and bol_bollette.fl_stampato='1' and bol_bollette.id=bol_bollette_voci.id_bolletta and t_voci_bolletta.id=bol_bollette_voci.id_voce and t_voci_bolletta.competenza=3"
                'myReaderA = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    If par.IfNull(myReaderA("importo"), "0") <> "0" Then
                '        LBL_BOLLETTATO_4.Text = Format(par.IfNull(myReaderA("importo"), "0"), "##,##0.00")
                '    Else
                '        LBL_BOLLETTATO_4.Text = "0,00"
                '    End If
                '    TOTALE_COLONNA_1 = TOTALE_COLONNA_1 + par.IfNull(myReaderA("importo"), 0)
                '    'v4 = par.IfNull(myReaderA("importo"), 0)
                'End If
                'myReaderA.Close()


                ''BOLLO SU MAV
                'par.cmd.CommandText = "select sum(bol_bollette_voci.importo) as importo from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette where  (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and bol_bollette.fl_annullata='0' and bol_bollette.fl_stampato='1' and bol_bollette.id=bol_bollette_voci.id_bolletta and t_voci_bolletta.id=bol_bollette_voci.id_voce and t_voci_bolletta.ID_CAPITOLO=4"
                'myReaderA = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    If par.IfNull(myReaderA("importo"), "0") <> "0" Then
                '        LBL_BOLLETTATO_5.Text = Format(par.IfNull(myReaderA("importo"), "0"), "##,##0.00")
                '    Else
                '        LBL_BOLLETTATO_5.Text = "0,00"
                '    End If
                '    TOTALE_COLONNA_1 = TOTALE_COLONNA_1 + par.IfNull(myReaderA("importo"), 0)
                '    'v5 = par.IfNull(myReaderA("importo"), 0)
                'End If
                'myReaderA.Close()

                ''SPESE MAV E POSTALI
                'par.cmd.CommandText = "select sum(bol_bollette_voci.importo) as importo from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette where  (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and  bol_bollette.fl_annullata='0' and bol_bollette.fl_stampato='1' and bol_bollette.id=bol_bollette_voci.id_bolletta and t_voci_bolletta.id=bol_bollette_voci.id_voce and t_voci_bolletta.ID_CAPITOLO=8"
                'myReaderA = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    If par.IfNull(myReaderA("importo"), "0") <> "0" Then
                '        LBL_BOLLETTATO_6.Text = Format(par.IfNull(myReaderA("importo"), "0"), "##,##0.00")
                '    Else
                '        LBL_BOLLETTATO_6.Text = "0,00"
                '    End If
                '    TOTALE_COLONNA_1 = TOTALE_COLONNA_1 + par.IfNull(myReaderA("importo"), 0)
                '    'v6 = par.IfNull(myReaderA("importo"), 0)
                'End If
                'myReaderA.Close()

                ''IMPOSTE REGISTRO
                'par.cmd.CommandText = "select sum(bol_bollette_voci.importo) as importo from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette where   (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and bol_bollette.fl_annullata='0' and bol_bollette.fl_stampato='1' and bol_bollette.id=bol_bollette_voci.id_bolletta and t_voci_bolletta.id=bol_bollette_voci.id_voce and t_voci_bolletta.ID_CAPITOLO=5"
                'myReaderA = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    If par.IfNull(myReaderA("importo"), "0") <> "0" Then
                '        LBL_BOLLETTATO_7.Text = Format(par.IfNull(myReaderA("importo"), "0"), "##,##0.00")
                '    Else
                '        LBL_BOLLETTATO_7.Text = "0,00"
                '    End If
                '    TOTALE_COLONNA_1 = TOTALE_COLONNA_1 + par.IfNull(myReaderA("importo"), 0)

                'End If
                'myReaderA.Close()

                ''IMPOSTE BOLLO SU CONTRATTI
                'par.cmd.CommandText = "select sum(bol_bollette_voci.importo) as importo from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette where  (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and bol_bollette.fl_annullata='0' and bol_bollette.fl_stampato='1' and bol_bollette.id=bol_bollette_voci.id_bolletta and t_voci_bolletta.id=bol_bollette_voci.id_voce and t_voci_bolletta.ID_CAPITOLO=6"
                'myReaderA = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    If par.IfNull(myReaderA("importo"), "0") <> "0" Then
                '        LBL_BOLLETTATO_8.Text = Format(par.IfNull(myReaderA("importo"), "0"), "##,##0.00")
                '    Else
                '        LBL_BOLLETTATO_8.Text = "0,00"
                '    End If
                '    TOTALE_COLONNA_1 = TOTALE_COLONNA_1 + par.IfNull(myReaderA("importo"), 0)
                '    'v8 = par.IfNull(myReaderA("importo"), 0)
                'End If
                'myReaderA.Close()


                ''v9 = TOTALE_COLONNA_1
                'LBL_BOLLETTATO_9.Text = Format(TOTALE_COLONNA_1, "##,##0.00")





                '' COLONNA SCADUTO

                ''COMUNE
                'par.cmd.CommandText = "select sum(bol_bollette_voci.importo) as importo from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette where BOL_BOLLETTE.DATA_SCADENZA<" & Format(Now, "yyyyMMdd") & " and (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and bol_bollette.fl_annullata='0' and bol_bollette.fl_stampato='1' and bol_bollette.id=bol_bollette_voci.id_bolletta and t_voci_bolletta.id=bol_bollette_voci.id_voce and t_voci_bolletta.competenza=1 AND ID_CAPITOLO<>8 AND ID_CAPITOLO<>5 AND ID_CAPITOLO<>6 "
                'myReaderA = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    If par.IfNull(myReaderA("importo"), "0") <> "0" Then
                '        LBL_SCADUTO_1.Text = Format(par.IfNull(myReaderA("importo"), "0"), "##,##0.00")
                '    Else
                '        LBL_SCADUTO_1.Text = "0,00"
                '    End If
                '    v1 = par.IfNull(myReaderA("importo"), 0)
                '    TOTALE_COLONNA_4 = TOTALE_COLONNA_4 + par.IfNull(myReaderA("importo"), 0)

                'End If
                'myReaderA.Close()

                ''GESTORE
                'par.cmd.CommandText = "select sum(bol_bollette_voci.importo) as importo from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette where BOL_BOLLETTE.DATA_SCADENZA<" & Format(Now, "yyyyMMdd") & " and   (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and bol_bollette.fl_annullata='0' and bol_bollette.fl_stampato='1' and bol_bollette.id=bol_bollette_voci.id_bolletta and t_voci_bolletta.id=bol_bollette_voci.id_voce and t_voci_bolletta.competenza=2 AND ID_CAPITOLO<>4"
                'myReaderA = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    If par.IfNull(myReaderA("importo"), "0") <> "0" Then
                '        LBL_SCADUTO_2.Text = Format(par.IfNull(myReaderA("importo"), "0"), "##,##0.00")
                '    Else
                '        LBL_SCADUTO_2.Text = "0,00"
                '    End If
                '    TOTALE_COLONNA_4 = TOTALE_COLONNA_4 + par.IfNull(myReaderA("importo"), 0)
                '    v2 = par.IfNull(myReaderA("importo"), 0)

                'End If
                'myReaderA.Close()

                ''DEP.CAUZIONALE
                'par.cmd.CommandText = "select sum(bol_bollette_voci.importo) as importo from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette where BOL_BOLLETTE.DATA_SCADENZA<" & Format(Now, "yyyyMMdd") & " and  (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and bol_bollette.fl_annullata='0' and bol_bollette.fl_stampato='1' and bol_bollette.id=bol_bollette_voci.id_bolletta and t_voci_bolletta.id=bol_bollette_voci.id_voce and t_voci_bolletta.COMPETENZA=0"
                'myReaderA = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    If par.IfNull(myReaderA("importo"), "0") <> "0" Then
                '        LBL_SCADUTO_3.Text = Format(par.IfNull(myReaderA("importo"), "0"), "##,##0.00")
                '    Else
                '        LBL_SCADUTO_3.Text = "0,00"
                '    End If
                '    TOTALE_COLONNA_4 = TOTALE_COLONNA_4 + par.IfNull(myReaderA("importo"), 0)
                '    v3 = par.IfNull(myReaderA("importo"), 0)
                'End If
                'myReaderA.Close()

                ''QUOTA SINDACALE
                'par.cmd.CommandText = "select sum(bol_bollette_voci.importo) as importo from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette where BOL_BOLLETTE.DATA_SCADENZA<" & Format(Now, "yyyyMMdd") & " and  (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and bol_bollette.fl_annullata='0' and bol_bollette.fl_stampato='1' and bol_bollette.id=bol_bollette_voci.id_bolletta and t_voci_bolletta.id=bol_bollette_voci.id_voce and t_voci_bolletta.competenza=3"
                'myReaderA = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    If par.IfNull(myReaderA("importo"), "0") <> "0" Then
                '        LBL_SCADUTO_4.Text = Format(par.IfNull(myReaderA("importo"), "0"), "##,##0.00")
                '    Else
                '        LBL_SCADUTO_4.Text = "0,00"
                '    End If
                '    TOTALE_COLONNA_4 = TOTALE_COLONNA_4 + par.IfNull(myReaderA("importo"), 0)
                '    v4 = par.IfNull(myReaderA("importo"), 0)
                'End If
                'myReaderA.Close()


                ''BOLLO MAV
                'par.cmd.CommandText = "select sum(bol_bollette_voci.importo) as importo from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette where BOL_BOLLETTE.DATA_SCADENZA<" & Format(Now, "yyyyMMdd") & " and  (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and bol_bollette.fl_annullata='0' and bol_bollette.fl_stampato='1' and bol_bollette.id=bol_bollette_voci.id_bolletta and t_voci_bolletta.id=bol_bollette_voci.id_voce and t_voci_bolletta.ID_CAPITOLO=4"
                'myReaderA = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    If par.IfNull(myReaderA("importo"), "0") <> "0" Then
                '        LBL_SCADUTO_5.Text = Format(par.IfNull(myReaderA("importo"), "0"), "##,##0.00")
                '    Else
                '        LBL_SCADUTO_5.Text = "0,00"
                '    End If
                '    TOTALE_COLONNA_4 = TOTALE_COLONNA_4 + par.IfNull(myReaderA("importo"), 0)
                '    v5 = par.IfNull(myReaderA("importo"), 0)
                'End If
                'myReaderA.Close()

                ''SPESE MAV
                'par.cmd.CommandText = "select sum(bol_bollette_voci.importo) as importo from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette where BOL_BOLLETTE.DATA_SCADENZA<" & Format(Now, "yyyyMMdd") & " and  (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and  bol_bollette.fl_annullata='0' and bol_bollette.fl_stampato='1' and bol_bollette.id=bol_bollette_voci.id_bolletta and t_voci_bolletta.id=bol_bollette_voci.id_voce and t_voci_bolletta.ID_CAPITOLO=8"
                'myReaderA = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    If par.IfNull(myReaderA("importo"), "0") <> "0" Then
                '        LBL_SCADUTO_6.Text = Format(par.IfNull(myReaderA("importo"), "0"), "##,##0.00")
                '    Else
                '        LBL_SCADUTO_6.Text = "0,00"
                '    End If
                '    TOTALE_COLONNA_4 = TOTALE_COLONNA_4 + par.IfNull(myReaderA("importo"), 0)
                '    v6 = par.IfNull(myReaderA("importo"), 0)
                'End If
                'myReaderA.Close()

                ''IMP.REGISTRO
                'par.cmd.CommandText = "select sum(bol_bollette_voci.importo) as importo from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette where BOL_BOLLETTE.DATA_SCADENZA<" & Format(Now, "yyyyMMdd") & " and   (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and bol_bollette.fl_annullata='0' and bol_bollette.fl_stampato='1' and bol_bollette.id=bol_bollette_voci.id_bolletta and t_voci_bolletta.id=bol_bollette_voci.id_voce and t_voci_bolletta.ID_CAPITOLO=5"
                'myReaderA = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    If par.IfNull(myReaderA("importo"), "0") <> "0" Then
                '        LBL_SCADUTO_7.Text = Format(par.IfNull(myReaderA("importo"), "0"), "##,##0.00")
                '    Else
                '        LBL_SCADUTO_7.Text = "0,00"
                '    End If
                '    TOTALE_COLONNA_4 = TOTALE_COLONNA_4 + par.IfNull(myReaderA("importo"), 0)
                '    v7 = par.IfNull(myReaderA("importo"), 0)
                'End If
                'myReaderA.Close()

                ''IMPO.BOLLO SU CONTRATTI
                'par.cmd.CommandText = "select sum(bol_bollette_voci.importo) as importo from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette where BOL_BOLLETTE.DATA_SCADENZA<" & Format(Now, "yyyyMMdd") & " and  (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and bol_bollette.fl_annullata='0' and bol_bollette.fl_stampato='1' and bol_bollette.id=bol_bollette_voci.id_bolletta and t_voci_bolletta.id=bol_bollette_voci.id_voce and t_voci_bolletta.ID_CAPITOLO=6"
                'myReaderA = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    If par.IfNull(myReaderA("importo"), "0") <> "0" Then
                '        LBL_SCADUTO_8.Text = Format(par.IfNull(myReaderA("importo"), "0"), "##,##0.00")
                '    Else
                '        LBL_SCADUTO_8.Text = "0,00"
                '    End If
                '    TOTALE_COLONNA_4 = TOTALE_COLONNA_4 + par.IfNull(myReaderA("importo"), 0)
                '    v8 = par.IfNull(myReaderA("importo"), 0)
                'End If
                'myReaderA.Close()


                'v9 = TOTALE_COLONNA_4
                'LBL_SCADUTO_9.Text = Format(TOTALE_COLONNA_4, "##,##0.00")


                '' incassato

                ''COMUNE
                'par.cmd.CommandText = "select sum(bol_bollette_voci.importo) as importo from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette where bol_bollette.data_pagamento<='" & Format(Now, "yyyyMMdd") & "' and (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and (BOL_BOLLETTE.DATA_PAGAMENTO IS NOT NULL OR BOL_BOLLETTE.DATA_PAGAMENTO<>'') AND  bol_bollette.fl_annullata='0' and bol_bollette.fl_stampato='1' and bol_bollette.id=bol_bollette_voci.id_bolletta and t_voci_bolletta.id=bol_bollette_voci.id_voce and t_voci_bolletta.competenza=1 AND ID_CAPITOLO<>8 AND ID_CAPITOLO<>5 AND ID_CAPITOLO<>6 "
                'myReaderA = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    If par.IfNull(myReaderA("importo"), "0") <> "0" Then
                '        LBL_INCASSATO_1.Text = Format(par.IfNull(myReaderA("importo"), "0"), "##,##0.00")
                '    Else
                '        LBL_INCASSATO_1.Text = "0,00"
                '    End If
                '    TOTALE_COLONNA_2 = TOTALE_COLONNA_2 + par.IfNull(myReaderA("importo"), 0)
                '    LBL_PERCENTUALE_1.Text = Format((100 * par.IfNull(myReaderA("importo"), 0)) / v1, "0.000")
                'End If
                'myReaderA.Close()

                ''GESTORE
                'par.cmd.CommandText = "select sum(bol_bollette_voci.importo) as importo from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette where bol_bollette.data_pagamento<='" & Format(Now, "yyyyMMdd") & "' and   (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and (BOL_BOLLETTE.DATA_PAGAMENTO IS NOT NULL OR BOL_BOLLETTE.DATA_PAGAMENTO<>'') AND bol_bollette.fl_annullata='0' and bol_bollette.fl_stampato='1' and bol_bollette.id=bol_bollette_voci.id_bolletta and t_voci_bolletta.id=bol_bollette_voci.id_voce and t_voci_bolletta.competenza=2 AND ID_CAPITOLO<>4"
                'myReaderA = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    If par.IfNull(myReaderA("importo"), "0") <> "0" Then
                '        LBL_INCASSATO_2.Text = Format(par.IfNull(myReaderA("importo"), "0"), "##,##0.00")
                '    Else
                '        LBL_INCASSATO_2.Text = "0,00"
                '    End If
                '    TOTALE_COLONNA_2 = TOTALE_COLONNA_2 + par.IfNull(myReaderA("importo"), 0)
                '    LBL_PERCENTUALE_2.Text = Format((100 * par.IfNull(myReaderA("importo"), 0)) / v2, "0.000")
                'End If
                'myReaderA.Close()

                ''DEP.CAUZIONALI
                'par.cmd.CommandText = "select sum(bol_bollette_voci.importo) as importo from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette where bol_bollette.data_pagamento<='" & Format(Now, "yyyyMMdd") & "' and   (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and (BOL_BOLLETTE.DATA_PAGAMENTO IS NOT NULL OR BOL_BOLLETTE.DATA_PAGAMENTO<>'') AND bol_bollette.fl_annullata='0' and bol_bollette.fl_stampato='1' and bol_bollette.id=bol_bollette_voci.id_bolletta and t_voci_bolletta.id=bol_bollette_voci.id_voce and t_voci_bolletta.COMPETENZA=0"
                'myReaderA = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    If par.IfNull(myReaderA("importo"), "0") <> "0" Then
                '        LBL_INCASSATO_3.Text = Format(par.IfNull(myReaderA("importo"), "0"), "##,##0.00")
                '    Else
                '        LBL_INCASSATO_3.Text = "0,00"
                '    End If
                '    TOTALE_COLONNA_2 = TOTALE_COLONNA_2 + par.IfNull(myReaderA("importo"), 0)
                '    LBL_PERCENTUALE_3.Text = Format((100 * par.IfNull(myReaderA("importo"), 0)) / v3, "0.000")
                'End If
                'myReaderA.Close()

                ''QUOTA SINDACALE
                'par.cmd.CommandText = "select sum(bol_bollette_voci.importo) as importo from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette where bol_bollette.data_pagamento<='" & Format(Now, "yyyyMMdd") & "' and   (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and (BOL_BOLLETTE.DATA_PAGAMENTO IS NOT NULL OR BOL_BOLLETTE.DATA_PAGAMENTO<>'') AND bol_bollette.fl_annullata='0' and bol_bollette.fl_stampato='1' and bol_bollette.id=bol_bollette_voci.id_bolletta and t_voci_bolletta.id=bol_bollette_voci.id_voce and t_voci_bolletta.competenza=3"
                'myReaderA = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    If par.IfNull(myReaderA("importo"), "0") <> "0" Then
                '        LBL_INCASSATO_4.Text = Format(par.IfNull(myReaderA("importo"), "0"), "##,##0.00")
                '    Else
                '        LBL_INCASSATO_4.Text = "0,00"
                '    End If
                '    TOTALE_COLONNA_2 = TOTALE_COLONNA_2 + par.IfNull(myReaderA("importo"), 0)
                '    LBL_PERCENTUALE_4.Text = Format((100 * par.IfNull(myReaderA("importo"), 0)) / v4, "0.000")
                'End If
                'myReaderA.Close()


                ''BOLLO MAV
                'par.cmd.CommandText = "select sum(bol_bollette_voci.importo) as importo from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette where bol_bollette.data_pagamento<='" & Format(Now, "yyyyMMdd") & "' and  (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and (BOL_BOLLETTE.DATA_PAGAMENTO IS NOT NULL OR BOL_BOLLETTE.DATA_PAGAMENTO<>'') AND bol_bollette.fl_annullata='0' and bol_bollette.fl_stampato='1' and bol_bollette.id=bol_bollette_voci.id_bolletta and t_voci_bolletta.id=bol_bollette_voci.id_voce and t_voci_bolletta.ID_CAPITOLO=4"
                'myReaderA = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    If par.IfNull(myReaderA("importo"), "0") <> "0" Then
                '        LBL_INCASSATO_5.Text = Format(par.IfNull(myReaderA("importo"), "0"), "##,##0.00")
                '    Else
                '        LBL_INCASSATO_5.Text = "0,00"
                '    End If
                '    TOTALE_COLONNA_2 = TOTALE_COLONNA_2 + par.IfNull(myReaderA("importo"), 0)
                '    LBL_PERCENTUALE_5.Text = Format((100 * par.IfNull(myReaderA("importo"), 0)) / v5, "0.000")
                'End If
                'myReaderA.Close()

                ''SPESE MAV
                'par.cmd.CommandText = "select sum(bol_bollette_voci.importo) as importo from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette where bol_bollette.data_pagamento<='" & Format(Now, "yyyyMMdd") & "' and   (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and (BOL_BOLLETTE.DATA_PAGAMENTO IS NOT NULL OR BOL_BOLLETTE.DATA_PAGAMENTO<>'') AND bol_bollette.fl_annullata='0' and bol_bollette.fl_stampato='1' and bol_bollette.id=bol_bollette_voci.id_bolletta and t_voci_bolletta.id=bol_bollette_voci.id_voce and t_voci_bolletta.ID_CAPITOLO=8"
                'myReaderA = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    If par.IfNull(myReaderA("importo"), "0") <> "0" Then
                '        LBL_INCASSATO_6.Text = Format(par.IfNull(myReaderA("importo"), "0"), "##,##0.00")
                '    Else
                '        LBL_INCASSATO_6.Text = "0,00"
                '    End If
                '    TOTALE_COLONNA_2 = TOTALE_COLONNA_2 + par.IfNull(myReaderA("importo"), 0)
                '    LBL_PERCENTUALE_6.Text = Format((100 * par.IfNull(myReaderA("importo"), 0)) / v6, "0.000")
                'End If
                'myReaderA.Close()

                ''IMP.REGISTRO
                'par.cmd.CommandText = "select sum(bol_bollette_voci.importo) as importo from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette where bol_bollette.data_pagamento<='" & Format(Now, "yyyyMMdd") & "' and   (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and (BOL_BOLLETTE.DATA_PAGAMENTO IS NOT NULL OR BOL_BOLLETTE.DATA_PAGAMENTO<>'') AND bol_bollette.fl_annullata='0' and bol_bollette.fl_stampato='1' and bol_bollette.id=bol_bollette_voci.id_bolletta and t_voci_bolletta.id=bol_bollette_voci.id_voce and t_voci_bolletta.ID_CAPITOLO=5"
                'myReaderA = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    If par.IfNull(myReaderA("importo"), "0") <> "0" Then
                '        LBL_INCASSATO_7.Text = Format(par.IfNull(myReaderA("importo"), "0"), "##,##0.00")
                '    Else
                '        LBL_INCASSATO_7.Text = "0,00"
                '    End If
                '    TOTALE_COLONNA_2 = TOTALE_COLONNA_2 + par.IfNull(myReaderA("importo"), 0)
                '    LBL_PERCENTUALE_7.Text = Format((100 * par.IfNull(myReaderA("importo"), 0)) / v7, "0.000")
                'End If
                'myReaderA.Close()

                ''IMP.BOLLO
                'par.cmd.CommandText = "select sum(bol_bollette_voci.importo) as importo from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette where bol_bollette.data_pagamento<='" & Format(Now, "yyyyMMdd") & "' and   (bol_bollette.riferimento_da>='" & anno & "0101' and bol_bollette.riferimento_a<='" & anno & "1231') and (BOL_BOLLETTE.DATA_PAGAMENTO IS NOT NULL OR BOL_BOLLETTE.DATA_PAGAMENTO<>'') AND bol_bollette.fl_annullata='0' and bol_bollette.fl_stampato='1' and bol_bollette.id=bol_bollette_voci.id_bolletta and t_voci_bolletta.id=bol_bollette_voci.id_voce and t_voci_bolletta.ID_CAPITOLO=6"
                'myReaderA = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    If par.IfNull(myReaderA("importo"), "0") <> "0" Then
                '        LBL_INCASSATO_8.Text = Format(par.IfNull(myReaderA("importo"), "0"), "##,##0.00")
                '    Else
                '        LBL_INCASSATO_8.Text = "0,00"
                '    End If
                '    TOTALE_COLONNA_2 = TOTALE_COLONNA_2 + par.IfNull(myReaderA("importo"), 0)
                '    LBL_PERCENTUALE_8.Text = Format((100 * par.IfNull(myReaderA("importo"), 0)) / v8, "0.000")
                'End If
                'myReaderA.Close()



                'LBL_INCASSATO_9.Text = Format(TOTALE_COLONNA_2, "##,##0.00")
                'LBL_PERCENTUALE_9.Text = Format((100 * TOTALE_COLONNA_2) / TOTALE_COLONNA_4, "0.000")


                






                ImgGennaio.Attributes.Add("onclick", "javascript:window.open('Flussi_Mese.aspx?A=" & Label1.Text & "&M=01','Flussi_Gennaio_" & Label1.Text & "','');")
                ImgFebbraio.Attributes.Add("onclick", "javascript:window.open('Flussi_Mese.aspx?A=" & Label1.Text & "&M=02','Flussi_Febbraio_" & Label1.Text & "','');")
                ImgMarzo.Attributes.Add("onclick", "javascript:window.open('Flussi_Mese.aspx?A=" & Label1.Text & "&M=03','Flussi_Marzo_" & Label1.Text & "','');")
                ImgAprile.Attributes.Add("onclick", "javascript:window.open('Flussi_Mese.aspx?A=" & Label1.Text & "&M=04','Flussi_Aprile_" & Label1.Text & "','');")
                ImgMaggio.Attributes.Add("onclick", "javascript:window.open('Flussi_Mese.aspx?A=" & Label1.Text & "&M=05','Flussi_Maggio_" & Label1.Text & "','');")
                ImgGiugno.Attributes.Add("onclick", "javascript:window.open('Flussi_Mese.aspx?A=" & Label1.Text & "&M=06','Flussi_Giugno_" & Label1.Text & "','');")
                ImgLuglio.Attributes.Add("onclick", "javascript:window.open('Flussi_Mese.aspx?A=" & Label1.Text & "&M=07','Flussi_Luglio_" & Label1.Text & "','');")
                ImgAgosto.Attributes.Add("onclick", "javascript:window.open('Flussi_Mese.aspx?A=" & Label1.Text & "&M=08','Flussi_Agosto_" & Label1.Text & "','');")
                ImgSettembre.Attributes.Add("onclick", "javascript:window.open('Flussi_Mese.aspx?A=" & Label1.Text & "&M=09','Flussi_Settembre_" & Label1.Text & "','');")
                ImgOttobre.Attributes.Add("onclick", "javascript:window.open('Flussi_Mese.aspx?A=" & Label1.Text & "&M=10','Flussi_Ottobre_" & Label1.Text & "','');")
                ImgNovembre.Attributes.Add("onclick", "javascript:window.open('Flussi_Mese.aspx?A=" & Label1.Text & "&M=11','Flussi_Novembre_" & Label1.Text & "','');")
                ImgDicembre.Attributes.Add("onclick", "javascript:window.open('Flussi_Mese.aspx?A=" & Label1.Text & "&M=12','Flussi_Dicembre_" & Label1.Text & "','');")




                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write(ex.Message)
            End Try
        End If
    End Sub

    Protected Sub ImgPDF_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgPDF.Click
        Dim url As String = Server.MapPath("..\..\FileTemp\")
        Dim pdfConverter1 As PdfConverter = New PdfConverter

        Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
        If Licenza <> "" Then
            pdfConverter1.LicenseKey = Licenza
        End If

        pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
        pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
        pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
        pdfConverter1.PdfDocumentOptions.ShowHeader = False
        pdfConverter1.PdfDocumentOptions.ShowFooter = False
        pdfConverter1.PdfDocumentOptions.LeftMargin = 10
        pdfConverter1.PdfDocumentOptions.RightMargin = 10
        pdfConverter1.PdfDocumentOptions.TopMargin = 10
        pdfConverter1.PdfDocumentOptions.BottomMargin = 10
        pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

        pdfConverter1.PdfDocumentOptions.ShowHeader = False
        pdfConverter1.PdfFooterOptions.FooterText = ("")
        pdfConverter1.PdfFooterOptions.FooterTextColor = Color.Blue
        pdfConverter1.PdfFooterOptions.DrawFooterLine = False
        pdfConverter1.PdfFooterOptions.PageNumberText = ""
        pdfConverter1.PdfFooterOptions.ShowPageNumber = False

        Dim nomefile As String = "Export_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
        pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(Session.Item("MIADTS2"), url & nomefile)

        Response.Write("<script>window.open('../../FileTemp/" & nomefile & "','Flussi','');</script>")
    End Sub
End Class
