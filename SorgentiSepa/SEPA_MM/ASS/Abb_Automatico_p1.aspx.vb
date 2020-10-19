
Partial Class ASS_Abb_Automatico_p1
    Inherits PageSetIdMode
    Dim PAR As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)

        If IsPostBack = False Then
            Response.Flush()

            BindGrid()
        End If
    End Sub

    Private Sub BindGrid()
        Try

            PAR.OracleConn.Open()
            par.SettaCommand(par)

            Dim sStringaSQL1 As String = ""
            Dim filtri As String = ""

            sStringaSQL1 = "SELECT (SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND ID_UNITA_IMMOBILIARE=UNITA_IMMOBILIARI.ID) AS NETTA,(SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_CONV' AND ID_UNITA_IMMOBILIARE=UNITA_IMMOBILIARI.ID) AS CONV," _
                & " (CASE WHEN (SELECT DISTINCT UNITA_IMMOBILIARI.ID  FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE,SISCOM_MI.UNITA_IMMOBILIARI UI WHERE UI.ID=unita_immobiliari.ID AND IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI_SCALE.ID_SCALA=UI.ID_SCALA AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)>0 THEN 'SI' END) AS elevatore," _
                & "T_TIPO_PROPRIETA.DESCRIZIONE AS ""PROPRIETA1"",TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",ALLOGGI.*,T_TIPO_ALL_ERP.DESCRIZIONE as TIPO_ALL," _
                       & "T_TIPO_INDIRIZZO.DESCRIZIONE AS ""TIPO_VIA"",TAB_QUARTIERI.NOME AS NOME_QUARTIERE,ALLOGGI.ZONA,(CASE WHEN (SELECT DISTINCT UNITA_IMMOBILIARI.ID  FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE,SISCOM_MI.UNITA_IMMOBILIARI UI WHERE UI.ID=unita_immobiliari.ID AND IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI_SCALE.ID_SCALA=UI.ID_SCALA AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)>0 THEN 'SI' END) AS ELEVATORE,(CASE WHEN ALLOGGI.H_MOTORIO = 1 THEN 'SI' else 'NO' END) as ""HANDICAP"", " _
                       & "('<a href=""javascript:window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID='||UNITA_IMMOBILIARI.ID||''',''Dettagli'',''height=580,top=0,left=0,width=780'');void(0);"">'||'<img alt=" & Chr(34) & "Dettagli Unità, Foto e Planimetrie" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " src=" & Chr(34) & "../NuoveImm/Abbina_Foto.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " />'||'</a>') as Visualizza," _
                       & "TO_CHAR(TO_DATE(ALLOGGI.DATA_DISPONIBILITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_DISPONIBILITA1"", ALLOGGI.NUM_ALLOGGIO AS ""N_ALL"" FROM T_TIPO_PROPRIETA,ALLOGGI," _
                       & "T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.TAB_QUARTIERI WHERE " _
                       & " unita_immobiliari.ID IN (SELECT id_unita FROM SISCOM_MI.UNITA_STATO_MANUTENTIVO WHERE UNITA_STATO_MANUTENTIVO.RIASSEGNABILE=1 and (UNITA_STATO_MANUTENTIVO.TIPO_RIASSEGNABILE = '1' OR (UNITA_STATO_MANUTENTIVO.TIPO_RIASSEGNABILE = '0' AND UNITA_STATO_MANUTENTIVO.FINE_LAVORI = 1))) AND " _
                       & "ALLOGGI.COD_ALLOGGIO=UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE (+) AND FL_POR='0' AND EQCANONE='0' AND TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND  " _
                       & " (unita_immobiliari.id_destinazione_uso=1 or unita_immobiliari.id_destinazione_uso=2) AND ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND " _
                       & " ASSEGNATO='0' AND PRENOTATO='0' AND alloggi.STATO=5 AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = 'LIBE' AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO AND TAB_QUARTIERI.ID =COMPLESSI_IMMOBILIARI.ID_QUARTIERE AND edifici.id = unita_immobiliari.id_Edificio AND fl_piano_vendita = 0 " _
                       & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) " & filtri & " ORDER BY ALLOGGI.TIPO_INDIRIZZO ASC, ALLOGGI.INDIRIZZO ASC, ALLOGGI.num_civico ASC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, PAR.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            DataGrid1.DataSource = dt
            DataGrid1.DataBind()
            If DataGrid1.Items.Count = 0 Then
                Label1.Text = "Risultati 0 Unità Libere per Abbinamento"
                Response.Write("<script>alert('I filtri di ricerca inseriti non hanno prodotto risultati!');</script>")
            Else
                Label1.Text = " - Risultati " & DataGrid1.Items.Count & " Unità Libere per Abbinamento"


                '**** 07/01/2013 DISABILITO CHECK SE L'UNITA NON HA LA SUPERFICIE NETTA ****
                Dim chkSelez As System.Web.UI.WebControls.CheckBox
                For Each oDataGridItem As DataGridItem In Me.DataGrid1.Items
                    chkSelez = oDataGridItem.FindControl("ChSelezionato")

                    If oDataGridItem.Cells(14).Text = "&nbsp;" Then
                        chkSelez.Enabled = False
                        For i As Integer = 0 To DataGrid1.Items.Count - 1
                            oDataGridItem.Cells(i).ToolTip = "Sup. netta mancante!"
                        Next
                    End If
                Next
                '**** 07/01/2013 FINE DISABILITO CHECK SE L'UNITA NON HA LA SUPERFICIE NETTA ****

            End If

            PAR.cmd.Dispose()
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'Label9.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnSelezionaTutti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSelezionaTutti.Click
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox


        For Each oDataGridItem In Me.DataGrid1.Items
            chkExport = oDataGridItem.FindControl("ChSelezionato")
            chkExport.Checked = True
        Next
    End Sub

    Protected Sub btnDeselezionaTutti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDeselezionaTutti.Click
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox


        For Each oDataGridItem In Me.DataGrid1.Items
            chkExport = oDataGridItem.FindControl("ChSelezionato")
            chkExport.Checked = False
        Next
    End Sub

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        Try

            PAR.OracleConn.Open()
            par.SettaCommand(par)

            Dim Elenco As String = ""
            Dim scriptblock As String = ""

            Dim oDataGridItem As DataGridItem
            Dim chkExport As System.Web.UI.WebControls.CheckBox

            For Each oDataGridItem In Me.DataGrid1.Items
                chkExport = oDataGridItem.FindControl("ChSelezionato")
                If chkExport.Checked Then
                    Elenco = Elenco & oDataGridItem.Cells(0).Text & ","
                End If
            Next

            If Elenco <> "" Then
                Elenco = "(" & Mid(Elenco, 1, Len(Elenco) - 1) & ")"

                PAR.cmd.CommandText = "select unita_immobiliari.id from siscom_mi.unita_immobiliari,siscom_mi.dimensioni where COD_UNITA_IMMOBILIARE IN (SELECT COD_ALLOGGIO FROM ALLOGGI WHERE ID IN " & Elenco & ") and dimensioni.COD_TIPOLOGIA='SUP_NETTA' AND DIMENSIONI.ID_UNITA_IMMOBILIARE=UNITA_IMMOBILIARI.ID order by DIMENSIONI.VALORE DESC"
                Dim myReaderBANDI As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                Elenco = ""
                Do While myReaderBANDI.Read
                    Elenco = Elenco & myReaderBANDI("ID") & ","
                Loop
                myReaderBANDI.Close()
                Elenco = "(" & Mid(Elenco, 1, Len(Elenco) - 1) & ")"
                Session.Add("ElencoSimulazione", Elenco)

                PAR.cmd.Dispose()
                PAR.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Response.Redirect("Abb_Automatico_p2.aspx")
            Else
                PAR.cmd.Dispose()
                PAR.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>alert('Selezionare almeno una unità!');</script>")
            End If


        Catch ex As Exception
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try

       
    End Sub
End Class
