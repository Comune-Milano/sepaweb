Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class Contabilita_Flussi_Home
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




    Private Function Carica()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)


            Dim BOLLETTATO_1 As Double = 0
            Dim BOLLETTATO_2 As Double = 0
            Dim BOLLETTATO_3 As Double = 0
            Dim BOLLETTATO_4 As Double = 0
            Dim BOLLETTATO_5 As Double = 0
            Dim BOLLETTATO_6 As Double = 0
            Dim BOLLETTATO_7 As Double = 0
            Dim BOLLETTATO_8 As Double = 0

            Dim SCADUTO_1 As Double = 0
            Dim SCADUTO_2 As Double = 0
            Dim SCADUTO_3 As Double = 0
            Dim SCADUTO_4 As Double = 0
            Dim SCADUTO_5 As Double = 0
            Dim SCADUTO_6 As Double = 0
            Dim SCADUTO_7 As Double = 0
            Dim SCADUTO_8 As Double = 0

            Dim INCASSATO_1 As Double = 0
            Dim INCASSATO_2 As Double = 0
            Dim INCASSATO_3 As Double = 0
            Dim INCASSATO_4 As Double = 0
            Dim INCASSATO_5 As Double = 0
            Dim INCASSATO_6 As Double = 0
            Dim INCASSATO_7 As Double = 0
            Dim INCASSATO_8 As Double = 0

            Dim INCASSATO_SC_1 As Double = 0
            Dim INCASSATO_SC_2 As Double = 0
            Dim INCASSATO_SC_3 As Double = 0
            Dim INCASSATO_SC_4 As Double = 0
            Dim INCASSATO_SC_5 As Double = 0
            Dim INCASSATO_SC_6 As Double = 0
            Dim INCASSATO_SC_7 As Double = 0
            Dim INCASSATO_SC_8 As Double = 0


            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("Modello.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            contenuto = Replace(contenuto, "$anno$", "")
            contenuto = Replace(contenuto, "$testo$", "La presente tabella espone il totale del bollettato alla data odierna, il totale del bollettato scaduto alla data odierna, il totale dell'incassato alla data odierna, il totale dell'incassato scaduto alla data odierna, la percentuale di incasso rispetto al bollettato e la percentuale dell'incassato scaduto rispetto al bollettato scaduto.")
            contenuto = Replace(contenuto, "$aggiunta$", "")

            par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.importo,T_VOCI_BOLLETTA.competenza,ID_CAPITOLO FROM siscom_mi.BOL_BOLLETTE_VOCI,siscom_mi.T_VOCI_BOLLETTA,siscom_mi.BOL_BOLLETTE " _
                                & "WHERE BOL_BOLLETTE.ID>=0 AND BOL_BOLLETTE.fl_annullata='0' AND BOL_BOLLETTE.fl_stampato='1' AND BOL_BOLLETTE.ID=BOL_BOLLETTE_VOCI.id_bolletta " _
                               & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.id_voce "
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReaderA.Read
                If myReaderA("competenza") = 1 And myReaderA("ID_CAPITOLO") <> 8 And myReaderA("ID_CAPITOLO") <> 5 And myReaderA("ID_CAPITOLO") <> 6 Then
                    BOLLETTATO_1 = BOLLETTATO_1 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("competenza") = 2 And myReaderA("ID_CAPITOLO") <> 4 Then
                    BOLLETTATO_2 = BOLLETTATO_2 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("competenza") = 0 Then
                    BOLLETTATO_3 = BOLLETTATO_3 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("competenza") = 3 Then
                    BOLLETTATO_4 = BOLLETTATO_4 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("id_capitolo") = 4 Then
                    BOLLETTATO_5 = BOLLETTATO_5 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("id_capitolo") = 8 Then
                    BOLLETTATO_6 = BOLLETTATO_6 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("id_capitolo") = 5 Then
                    BOLLETTATO_7 = BOLLETTATO_7 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("id_capitolo") = 6 Then
                    BOLLETTATO_8 = BOLLETTATO_8 + par.IfNull(myReaderA("importo"), "0")
                End If

            Loop
            myReaderA.Close()


            LBL_BOLLETTATO_1.Text = Format(BOLLETTATO_1, "##,##0.00")
            LBL_BOLLETTATO_2.Text = Format(BOLLETTATO_2, "##,##0.00")
            LBL_BOLLETTATO_3.Text = Format(BOLLETTATO_3, "##,##0.00")
            LBL_BOLLETTATO_4.Text = Format(BOLLETTATO_4, "##,##0.00")
            LBL_BOLLETTATO_5.Text = Format(BOLLETTATO_5, "##,##0.00")
            LBL_BOLLETTATO_6.Text = Format(BOLLETTATO_6, "##,##0.00")
            LBL_BOLLETTATO_7.Text = Format(BOLLETTATO_7, "##,##0.00")
            LBL_BOLLETTATO_8.Text = Format(BOLLETTATO_8, "##,##0.00")
            TOTALE_COLONNA_1 = BOLLETTATO_1 + BOLLETTATO_2 + BOLLETTATO_3 + BOLLETTATO_4 + BOLLETTATO_5 + BOLLETTATO_6 + BOLLETTATO_7 + BOLLETTATO_8
            Vv1 = BOLLETTATO_1
            Vv2 = BOLLETTATO_2
            Vv3 = BOLLETTATO_3
            Vv4 = BOLLETTATO_4
            Vv5 = BOLLETTATO_5
            Vv6 = BOLLETTATO_6
            Vv7 = BOLLETTATO_7
            Vv8 = BOLLETTATO_8

            LBL_BOLLETTATO_9.Text = Format(TOTALE_COLONNA_1, "##,##0.00")
            Vv9 = LBL_BOLLETTATO_9.Text



            par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.importo,T_VOCI_BOLLETTA.competenza,ID_CAPITOLO FROM siscom_mi.BOL_BOLLETTE_VOCI,siscom_mi.T_VOCI_BOLLETTA,siscom_mi.BOL_BOLLETTE " _
                                & "WHERE  BOL_BOLLETTE.DATA_SCADENZA<" & Format(Now, "yyyyMMdd") & " AND BOL_BOLLETTE.ID>=0 AND BOL_BOLLETTE.fl_annullata='0' AND BOL_BOLLETTE.fl_stampato='1' AND BOL_BOLLETTE.ID=BOL_BOLLETTE_VOCI.id_bolletta " _
                                & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.id_voce "
            myReaderA = par.cmd.ExecuteReader()
            Do While myReaderA.Read
                If myReaderA("competenza") = 1 And myReaderA("ID_CAPITOLO") <> 8 And myReaderA("ID_CAPITOLO") <> 5 And myReaderA("ID_CAPITOLO") <> 6 Then
                    SCADUTO_1 = SCADUTO_1 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("competenza") = 2 And myReaderA("ID_CAPITOLO") <> 4 Then
                    SCADUTO_2 = SCADUTO_2 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("competenza") = 0 Then
                    SCADUTO_3 = SCADUTO_3 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("competenza") = 3 Then
                    SCADUTO_4 = SCADUTO_4 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("id_capitolo") = 4 Then
                    SCADUTO_5 = SCADUTO_5 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("id_capitolo") = 8 Then
                    SCADUTO_6 = SCADUTO_6 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("id_capitolo") = 5 Then
                    SCADUTO_7 = SCADUTO_7 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("id_capitolo") = 6 Then
                    SCADUTO_8 = SCADUTO_8 + par.IfNull(myReaderA("importo"), "0")
                End If

            Loop
            myReaderA.Close()


            LBL_SCADUTO_1.Text = Format(SCADUTO_1, "##,##0.00")
            LBL_SCADUTO_2.Text = Format(SCADUTO_2, "##,##0.00")
            LBL_SCADUTO_3.Text = Format(SCADUTO_3, "##,##0.00")
            LBL_SCADUTO_4.Text = Format(SCADUTO_4, "##,##0.00")
            LBL_SCADUTO_5.Text = Format(SCADUTO_5, "##,##0.00")
            LBL_SCADUTO_6.Text = Format(SCADUTO_6, "##,##0.00")
            LBL_SCADUTO_7.Text = Format(SCADUTO_7, "##,##0.00")
            LBL_SCADUTO_8.Text = Format(SCADUTO_8, "##,##0.00")
            TOTALE_COLONNA_4 = SCADUTO_1 + SCADUTO_2 + SCADUTO_3 + SCADUTO_4 + SCADUTO_5 + SCADUTO_6 + SCADUTO_7 + SCADUTO_8
            v1 = SCADUTO_1
            v2 = SCADUTO_2
            v3 = SCADUTO_3
            v4 = SCADUTO_4
            v5 = SCADUTO_5
            v6 = SCADUTO_6
            v7 = SCADUTO_7
            v8 = SCADUTO_8

            LBL_SCADUTO_9.Text = Format(TOTALE_COLONNA_4, "##,##0.00")
            v9 = LBL_SCADUTO_9.Text



            par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.importo,T_VOCI_BOLLETTA.competenza,ID_CAPITOLO FROM siscom_mi.BOL_BOLLETTE_VOCI,siscom_mi.T_VOCI_BOLLETTA,siscom_mi.BOL_BOLLETTE " _
                                & "WHERE  (BOL_BOLLETTE.DATA_PAGAMENTO IS NOT NULL OR BOL_BOLLETTE.DATA_PAGAMENTO<>'') AND BOL_BOLLETTE.ID>=0 AND BOL_BOLLETTE.fl_annullata='0' AND BOL_BOLLETTE.fl_stampato='1' AND BOL_BOLLETTE.ID=BOL_BOLLETTE_VOCI.id_bolletta " _
                                & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.id_voce "
            myReaderA = par.cmd.ExecuteReader()
            Do While myReaderA.Read
                If myReaderA("competenza") = 1 And myReaderA("ID_CAPITOLO") <> 8 And myReaderA("ID_CAPITOLO") <> 5 And myReaderA("ID_CAPITOLO") <> 6 Then
                    INCASSATO_1 = INCASSATO_1 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("competenza") = 2 And myReaderA("ID_CAPITOLO") <> 4 Then
                    INCASSATO_2 = INCASSATO_2 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("competenza") = 0 Then
                    INCASSATO_3 = INCASSATO_3 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("competenza") = 3 Then
                    INCASSATO_4 = INCASSATO_4 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("id_capitolo") = 4 Then
                    INCASSATO_5 = INCASSATO_5 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("id_capitolo") = 8 Then
                    INCASSATO_6 = INCASSATO_6 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("id_capitolo") = 5 Then
                    INCASSATO_7 = INCASSATO_7 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("id_capitolo") = 6 Then
                    INCASSATO_8 = INCASSATO_8 + par.IfNull(myReaderA("importo"), "0")
                End If

            Loop
            myReaderA.Close()


            LBL_INCASSATO_1.Text = Format(INCASSATO_1, "##,##0.00")
            LBL_INCASSATO_2.Text = Format(INCASSATO_2, "##,##0.00")
            LBL_INCASSATO_3.Text = Format(INCASSATO_3, "##,##0.00")
            LBL_INCASSATO_4.Text = Format(INCASSATO_4, "##,##0.00")
            LBL_INCASSATO_5.Text = Format(INCASSATO_5, "##,##0.00")
            LBL_INCASSATO_6.Text = Format(INCASSATO_6, "##,##0.00")
            LBL_INCASSATO_7.Text = Format(INCASSATO_7, "##,##0.00")
            LBL_INCASSATO_8.Text = Format(INCASSATO_8, "##,##0.00")
            TOTALE_COLONNA_2 = INCASSATO_1 + INCASSATO_2 + INCASSATO_3 + INCASSATO_4 + INCASSATO_5 + INCASSATO_6 + INCASSATO_7 + INCASSATO_8

            LBL_PERCENTUALE_A_1.Text = Format((100 * INCASSATO_1) / Vv1, "0.00")
            LBL_PERCENTUALE_A_2.Text = Format((100 * INCASSATO_2) / Vv2, "0.00")
            LBL_PERCENTUALE_A_3.Text = Format((100 * INCASSATO_3) / Vv3, "0.00")
            LBL_PERCENTUALE_A_4.Text = Format((100 * INCASSATO_4) / Vv4, "0.00")
            LBL_PERCENTUALE_A_5.Text = Format((100 * INCASSATO_5) / Vv5, "0.00")
            LBL_PERCENTUALE_A_6.Text = Format((100 * INCASSATO_6) / Vv6, "0.00")
            LBL_PERCENTUALE_A_7.Text = Format((100 * INCASSATO_7) / Vv7, "0.00")
            LBL_PERCENTUALE_A_8.Text = Format((100 * INCASSATO_8) / Vv8, "0.00")

            LBL_INCASSATO_9.Text = Format(TOTALE_COLONNA_2, "##,##0.00")
            LBL_PERCENTUALE_A_9.Text = Format((100 * TOTALE_COLONNA_2) / TOTALE_COLONNA_1, "0.00")




            par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.importo,T_VOCI_BOLLETTA.competenza,ID_CAPITOLO FROM siscom_mi.BOL_BOLLETTE_VOCI,siscom_mi.T_VOCI_BOLLETTA,siscom_mi.BOL_BOLLETTE " _
                                & "WHERE (BOL_BOLLETTE.DATA_PAGAMENTO IS NOT NULL OR BOL_BOLLETTE.DATA_PAGAMENTO<>'') and data_scadenza<" & Format(Now, "yyyyMMdd") & " AND BOL_BOLLETTE.ID>=0 AND BOL_BOLLETTE.fl_annullata='0' AND BOL_BOLLETTE.fl_stampato='1' AND BOL_BOLLETTE.ID=BOL_BOLLETTE_VOCI.id_bolletta " _
                                & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.id_voce "
            myReaderA = par.cmd.ExecuteReader()
            Do While myReaderA.Read
                If myReaderA("competenza") = 1 And myReaderA("ID_CAPITOLO") <> 8 And myReaderA("ID_CAPITOLO") <> 5 And myReaderA("ID_CAPITOLO") <> 6 Then
                    INCASSATO_SC_1 = INCASSATO_SC_1 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("competenza") = 2 And myReaderA("ID_CAPITOLO") <> 4 Then
                    INCASSATO_SC_2 = INCASSATO_SC_2 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("competenza") = 0 Then
                    INCASSATO_SC_3 = INCASSATO_SC_3 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("competenza") = 3 Then
                    INCASSATO_SC_4 = INCASSATO_SC_4 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("id_capitolo") = 4 Then
                    INCASSATO_SC_5 = INCASSATO_SC_5 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("id_capitolo") = 8 Then
                    INCASSATO_SC_6 = INCASSATO_SC_6 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("id_capitolo") = 5 Then
                    INCASSATO_SC_7 = INCASSATO_SC_7 + par.IfNull(myReaderA("importo"), "0")
                End If

                If myReaderA("id_capitolo") = 6 Then
                    INCASSATO_SC_8 = INCASSATO_SC_8 + par.IfNull(myReaderA("importo"), "0")
                End If

            Loop
            myReaderA.Close()


            LBL_INCASSATO_SC_1.Text = Format(INCASSATO_SC_1, "##,##0.00")
            LBL_INCASSATO_SC_2.Text = Format(INCASSATO_SC_2, "##,##0.00")
            LBL_INCASSATO_SC_3.Text = Format(INCASSATO_SC_3, "##,##0.00")
            LBL_INCASSATO_SC_4.Text = Format(INCASSATO_SC_4, "##,##0.00")
            LBL_INCASSATO_SC_5.Text = Format(INCASSATO_SC_5, "##,##0.00")
            LBL_INCASSATO_SC_6.Text = Format(INCASSATO_SC_6, "##,##0.00")
            LBL_INCASSATO_SC_7.Text = Format(INCASSATO_SC_7, "##,##0.00")
            LBL_INCASSATO_SC_8.Text = Format(INCASSATO_SC_8, "##,##0.00")
            TOTALE_COLONNA_5 = INCASSATO_SC_1 + INCASSATO_SC_2 + INCASSATO_SC_3 + INCASSATO_SC_4 + INCASSATO_SC_5 + INCASSATO_SC_6 + INCASSATO_SC_7 + INCASSATO_SC_8

            LBL_PERCENTUALE_1.Text = Format((100 * INCASSATO_SC_1) / v1, "0.00")
            LBL_PERCENTUALE_2.Text = Format((100 * INCASSATO_SC_2) / v2, "0.00")
            LBL_PERCENTUALE_3.Text = Format((100 * INCASSATO_SC_3) / v3, "0.00")
            LBL_PERCENTUALE_4.Text = Format((100 * INCASSATO_SC_4) / v4, "0.00")
            LBL_PERCENTUALE_5.Text = Format((100 * INCASSATO_SC_5) / v5, "0.00")
            LBL_PERCENTUALE_6.Text = Format((100 * INCASSATO_SC_6) / v6, "0.00")
            LBL_PERCENTUALE_7.Text = Format((100 * INCASSATO_SC_7) / v7, "0.00")
            LBL_PERCENTUALE_8.Text = Format((100 * INCASSATO_SC_8) / v8, "0.00")



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



            Session.Add("e_MIADTS1", DT)


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

            Session.Add("MIADTS1", contenuto)

            Dim kk As Integer = 2009

            For kk = 2009 To Year(Now)
                If kk = 2009 Then
                    Img2009.Visible = True
                    Img2009.Attributes.Add("onclick", "javascript:window.open('Flussi_Anno.aspx?A=2009','Flussi_2009','');")
                End If
                If kk = 2010 Then
                    Img2010.Visible = True
                    Img2010.Attributes.Add("onclick", "javascript:window.open('Flussi_Anno.aspx?A=2010','Flussi_2010','');")
                End If
                If kk = 2011 Then
                    Img2011.Visible = True
                    Img2011.Attributes.Add("onclick", "javascript:window.open('Flussi_Anno.aspx?A=2011','Flussi_2011','');")
                End If
                If kk = 2012 Then
                    Img2012.Visible = True
                    Img2012.Attributes.Add("onclick", "javascript:window.open('Flussi_Anno.aspx?A=2012','Flussi_2012','');")
                End If
                If kk = 2013 Then
                    Img2013.Visible = True
                    Img2013.Attributes.Add("onclick", "javascript:window.open('Flussi_Anno.aspx?A=2013','Flussi_2013','');")
                End If
                If kk = 2014 Then
                    Img2014.Visible = True
                    Img2014.Attributes.Add("onclick", "javascript:window.open('Flussi_Anno.aspx?A=2014','Flussi_2014','');")
                End If
                If kk = 2015 Then
                    Img2015.Visible = True
                    Img2015.Attributes.Add("onclick", "javascript:window.open('Flussi_Anno.aspx?A=2015','Flussi_2015','');")
                End If
            Next


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()



        Catch ex As Exception
            '    par.OracleConn.Close()
            '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            '    Response.Write(ex.Message)
        End Try
    End Function


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
            ' Carica()
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)


                Dim sr1 As StreamReader = New StreamReader(Server.MapPath("Modello.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                Dim contenuto As String = sr1.ReadToEnd()
                sr1.Close()

                contenuto = Replace(contenuto, "$anno$", "")
                contenuto = Replace(contenuto, "$testo$", "La presente tabella espone il totale del bollettato alla data odierna, il totale del bollettato scaduto alla data odierna, il totale dell'incassato alla data odierna, il totale dell'incassato scaduto alla data odierna, la percentuale di incasso rispetto al bollettato e la percentuale dell'incassato scaduto rispetto al bollettato scaduto.")
                contenuto = Replace(contenuto, "$aggiunta$", "")

                'COLONNA BOLLETTATO

                'COMUNE
                'par.cmd.CommandText = "select sum(bol_bollette_voci.importo) as importo from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette where bol_bollette.id>=0 and bol_bollette.fl_annullata='0' and bol_bollette.fl_stampato='1' and bol_bollette.id=bol_bollette_voci.id_bolletta and t_voci_bolletta.id=bol_bollette_voci.id_voce and t_voci_bolletta.competenza=1 AND ID_CAPITOLO<>8 AND ID_CAPITOLO<>5 AND ID_CAPITOLO<>6"
                par.cmd.CommandText = "SELECT SUM(BOL_FLUSSI.BOLLETTATO_CANONI) AS BOLLETTATO_CANONI,SUM(BOL_FLUSSI.BOLLETTATO_ONERI_ACCESSORI) AS BOLLETTATO_ONERI_ACCESSORI," _
                    & "SUM(BOL_FLUSSI.BOLLETTATO_DEP_CAUZIONALE) AS BOLLETTATO_DEP_CAUZIONALE,SUM(BOL_FLUSSI.BOLLETTATO_QUOTA_SIND) AS BOLLETTATO_QUOTA_SIND," _
                    & "SUM(BOL_FLUSSI.BOLLETTATO_BOLLI_MAV) AS BOLLETTATO_BOLLI_MAV,SUM(BOL_FLUSSI.BOLLETTATO_SPESE_MAV) AS BOLLETTATO_SPESE_MAV," _
                    & "SUM(BOL_FLUSSI.BOLLETTATO_IMPOSTE_REG) AS BOLLETTATO_IMPOSTE_REG,SUM(BOL_FLUSSI.BOLLETTATO_IMPOSTE_BOLLO) AS BOLLETTATO_IMPOSTE_BOLLO FROM siscom_mi.BOL_FLUSSI,siscom_mi.BOL_BOLLETTE WHERE bol_bollette.id>=0 and  BOL_BOLLETTE.fl_annullata='0' AND BOL_BOLLETTE.fl_stampato='1' AND BOL_BOLLETTE.ID=BOL_FLUSSI.id_bolletta"

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
                                    & " FROM siscom_mi.BOL_FLUSSI,siscom_mi.BOL_BOLLETTE WHERE  BOL_BOLLETTE.DATA_SCADENZA<" & Format(Now, "yyyyMMdd") & " AND bol_bollette.id>=0 and  BOL_BOLLETTE.fl_annullata='0' AND BOL_BOLLETTE.fl_stampato='1' AND BOL_BOLLETTE.ID=BOL_FLUSSI.id_bolletta"

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
                    & " FROM siscom_mi.BOL_FLUSSI,siscom_mi.BOL_BOLLETTE WHERE  (BOL_BOLLETTE.DATA_PAGAMENTO IS NOT NULL OR BOL_BOLLETTE.DATA_PAGAMENTO<>'') AND  bol_bollette.id>=0 and  BOL_BOLLETTE.fl_annullata='0' AND BOL_BOLLETTE.fl_stampato='1' AND BOL_BOLLETTE.ID=BOL_FLUSSI.id_bolletta"

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
    & " FROM siscom_mi.BOL_FLUSSI,siscom_mi.BOL_BOLLETTE WHERE  (BOL_BOLLETTE.DATA_PAGAMENTO IS NOT NULL OR BOL_BOLLETTE.DATA_PAGAMENTO<>'') and data_scadenza<" & Format(Now, "yyyyMMdd") & " AND   bol_bollette.id>=0 and  BOL_BOLLETTE.fl_annullata='0' AND BOL_BOLLETTE.fl_stampato='1' AND BOL_BOLLETTE.ID=BOL_FLUSSI.id_bolletta"

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



                Session.Add("e_MIADTS1", DT)


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

                Session.Add("MIADTS1", contenuto)

                Dim kk As Integer = 2009

                For kk = 2009 To Year(Now)
                    If kk = 2009 Then
                        Img2009.Visible = True
                        Img2009.Attributes.Add("onclick", "javascript:window.open('Flussi_Anno.aspx?A=2009','Flussi_2009','');")
                    End If
                    If kk = 2010 Then
                        Img2010.Visible = True
                        Img2010.Attributes.Add("onclick", "javascript:window.open('Flussi_Anno.aspx?A=2010','Flussi_2010','');")
                    End If
                    If kk = 2011 Then
                        Img2011.Visible = True
                        Img2011.Attributes.Add("onclick", "javascript:window.open('Flussi_Anno.aspx?A=2011','Flussi_2011','');")
                    End If
                    If kk = 2012 Then
                        Img2012.Visible = True
                        Img2012.Attributes.Add("onclick", "javascript:window.open('Flussi_Anno.aspx?A=2012','Flussi_2012','');")
                    End If
                    If kk = 2013 Then
                        Img2013.Visible = True
                        Img2013.Attributes.Add("onclick", "javascript:window.open('Flussi_Anno.aspx?A=2013','Flussi_2013','');")
                    End If
                    If kk = 2014 Then
                        Img2014.Visible = True
                        Img2014.Attributes.Add("onclick", "javascript:window.open('Flussi_Anno.aspx?A=2014','Flussi_2014','');")
                    End If
                    If kk = 2015 Then
                        Img2015.Visible = True
                        Img2015.Attributes.Add("onclick", "javascript:window.open('Flussi_Anno.aspx?A=2015','Flussi_2015','');")
                    End If
                Next


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
        Try


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
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(Session.Item("MIADTS1"), url & nomefile)

            Response.Write("<script>window.open('../../FileTemp/" & nomefile & "','Flussi','');</script>")
            'pdfConverter1.SavePdfFromUrlToFile(url & ".htm", url & ".pdf")
            'IO.File.Delete(url & ".htm")

        Catch ex As Exception
           
            Label1.Text = ex.Message
        End Try
    End Sub
End Class
