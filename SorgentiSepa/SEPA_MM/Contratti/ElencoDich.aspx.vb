
Partial Class Contratti_ElencoDich
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            BindGrid()
        End If

    End Sub

    Private Function CalcoloDecorrenze(ByVal causaleDom As Integer, ByVal dataPr As String) As String

        Dim annoReddito As String = ""
        Try
            
            par.cmd.CommandText = "SELECT * FROM CALCOLO_DECORRENZE_REV_C WHERE CAUSALE_DOMANDA_VSA=" & causaleDom
            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt1 As New Data.DataTable()
            da1.Fill(dt1)
            da1.Dispose()

            If dt1.Rows.Count > 0 Then
                For Each row As Data.DataRow In dt1.Rows
                    If causaleDom = 18 And row.Item("ANNO_REDDITO") = 2007 Then
                        If dataPr > row.Item("DATA_INIZIO") And dataPr < row.Item("DATA_FINE") Then
                            annoReddito &= row.Item("ANNO_REDDITO")
                        End If
                    End If
                    If causaleDom = 17 Then
                        If row.Item("ANNO_REDDITO") Mod 2 = 0 Then 'ANNO PARI
                            If dataPr >= row.Item("DATA_INIZIO") And dataPr < row.Item("DATA_FINE") Then
                                annoReddito &= row.Item("ANNO_REDDITO")
                            End If
                        Else
                            If dataPr >= row.Item("DATA_INIZIO") And dataPr < row.Item("DATA_FINE") Then
                                annoReddito &= row.Item("ANNO_REDDITO")
                            End If
                        End If
                    ElseIf causaleDom = 18 And row.Item("ANNO_REDDITO") <> 2007 Then
                        If row.Item("ANNO_REDDITO") Mod 2 = 0 Then 'ANNO PARI
                            If dataPr >= row.Item("DATA_INIZIO") And dataPr < row.Item("DATA_FINE") Then
                                annoReddito &= row.Item("ANNO_REDDITO")
                            End If
                        Else
                            If dataPr >= row.Item("DATA_INIZIO") And dataPr < row.Item("DATA_FINE") Then
                                annoReddito &= row.Item("ANNO_REDDITO")
                            End If
                        End If
                    End If
                Next
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

        Return annoReddito

    End Function

    Private Sub BindGrid()

        Try
            Dim strSQL0 As String = ""
            Dim strSQL As String = ""
            Dim conta As Integer = 0
            Dim annoReddito As String = ""
            Dim completaSQL As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim condizioneQuery As String = ""
            Dim idDomandaAggRedd As Long = 0
            par.cmd.CommandText = "select id_domanda from siscom_mi.rapporti_utenza where cod_contratto='" & Request.QueryString("COD") & "' and id_domanda in (select id from domande_bando_vsa where id_motivo_domanda=11)"
            Dim myReaderE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderE.Read Then
                idDomandaAggRedd = par.IfNull(myReaderE(0), 0)
                If idDomandaAggRedd <> 0 Then
                    condizioneQuery = "AND (DOMANDE_BANDO_VSA.CONTRATTO_NUM = '" & Request.QueryString("COD") & "' or DOMANDE_BANDO_VSA.id= " & idDomandaAggRedd & ")"
                End If
            End If
            myReaderE.Close()


            If idDomandaAggRedd = 0 Then
                par.cmd.CommandText = "select id_isee from siscom_mi.rapporti_utenza where cod_contratto='" & Request.QueryString("COD") & "' and id_isee in (select id_dichiarazione from domande_bando_vsa where id_motivo_domanda=11)"
                myReaderE = par.cmd.ExecuteReader()
                If myReaderE.Read Then
                    idDomandaAggRedd = par.IfNull(myReaderE(0), 0)
                    If idDomandaAggRedd <> 0 Then
                        condizioneQuery = "AND (DOMANDE_BANDO_VSA.CONTRATTO_NUM = '" & Request.QueryString("COD") & "' or dichiarazioni_vsa.id= " & idDomandaAggRedd & ")"
                    End If
                End If
                myReaderE.Close()
            End If


            If idDomandaAggRedd = 0 Then
                condizioneQuery = "AND DOMANDE_BANDO_VSA.CONTRATTO_NUM = '" & Request.QueryString("COD") & "'"
            End If

            Dim dtDomande As New Data.DataTable

            strSQL = "Select T_MOTIVO_DOMANDA_VSA.DESCRIZIONE,T_STATI_DICHIARAZIONE.DESCRIZIONE As STATO_DICH,(Case When DOMANDE_BANDO_VSA.FL_AUTORIZZAZIONE = 1 Then 'AUTORIZZATA' ELSE (CASE WHEN VSA_DECISIONI_REV_C.COD_DECISIONE = 1 THEN 'SOTTOPOSTA A DECISIONE' WHEN VSA_DECISIONI_REV_C.COD_DECISIONE = 2 THEN 'ACCOLTA' WHEN VSA_DECISIONI_REV_C.COD_DECISIONE = 3 THEN 'NON ACCOLTA' WHEN VSA_DECISIONI_REV_C.COD_DECISIONE = 4 THEN 'SOTTOPOSTA A REVISIONE' WHEN VSA_DECISIONI_REV_C.COD_DECISIONE = 5 THEN 'REVISIONE ACCOLTA' WHEN VSA_DECISIONI_REV_C.COD_DECISIONE = 6 THEN 'REVISIONE NON ACCOLTA' ELSE 'NESSUNA DECISIONE' END) END) AS STATO_DOM, " _
                & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../VSA/NuovaDichiarazioneVSA/DichAUnuova.aspx?CH=2$ID='||DICHIARAZIONI_VSA.ID||''',''Dichiarazione'',''top=200,left=350,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes'');£>'||DICHIARAZIONI_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') as  PG_DICHIARAZIONE, " _
                & "TO_CHAR(TO_DATE(DICHIARAZIONI_VSA.DATA_PG,'yyyymmdd'),'dd/mm/yyyy') AS DATA_PG," _
                & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../VSA/NuovaDomandaVSA/domandaNuova.aspx?CH=1$ID='||DOMANDE_BANDO_VSA.ID||''',''Domande'',''top=200,left=350,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes'');£>'||DOMANDE_BANDO_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') as PG_DOMANDA, " _
& "(CASE WHEN DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = 2 OR DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = 3 THEN replace(replace('<img alt=£Cambio£ title=£Cambia intestatario domanda£ src=£../NuoveImm/Img_CambioDich.png£ onclick=£window.open(''../VSA/Locatari/CambioIntestazione1.aspx?ID='||DOMANDE_BANDO_VSA.ID||'$PG='||DOMANDE_BANDO_VSA.PG||''',''CambioDich'',''height=550,top=200,left=350,width=670'');£ style=£cursor: pointer£ />','$','&'),'£','" & Chr(34) & "') ELSE '' END) as CAMBIO " _
& "FROM DICHIARAZIONI_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,VSA_DECISIONI_REV_C,T_STATI_DICHIARAZIONE WHERE DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE and DICHIARAZIONI_VSA.ID_STATO=T_STATI_DICHIARAZIONE.COD AND VSA_DECISIONI_REV_C.DATA = (SELECT MAX(DATA) FROM VSA_DECISIONI_REV_C WHERE ID_DOMANDA=DOMANDE_BANDO_VSA.ID) " _
                & "AND DOMANDE_BANDO_VSA.data_pg>='20120101' AND T_MOTIVO_DOMANDA_VSA.ID = DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA " & condizioneQuery & " AND VSA_DECISIONI_REV_C.ID_DOMANDA(+) = DOMANDE_BANDO_VSA.ID " _
                & "UNION " _
                & "SELECT T_MOTIVO_DOMANDA_VSA.DESCRIZIONE,T_STATI_DICHIARAZIONE.DESCRIZIONE AS STATO_DICH,'NESSUNA DECISIONE' AS STATO_DOM, " _
                & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../VSA/NuovaDichiarazioneVSA/DichAUnuova.aspx?CH=2$ID='||DICHIARAZIONI_VSA.ID||''',''Dichiarazione'',''top=200,left=350,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes'');£>'||DICHIARAZIONI_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') as  PG_DICHIARAZIONE, " _
                & "TO_CHAR(TO_DATE(DICHIARAZIONI_VSA.DATA_PG,'yyyymmdd'),'dd/mm/yyyy') AS DATA_PG," _
                & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../VSA/NuovaDomandaVSA/domandaNuova.aspx?CH=1$ID='||DOMANDE_BANDO_VSA.ID||''',''Domande'',''top=200,left=350,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes'');£>'||DOMANDE_BANDO_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') as PG_DOMANDA, " _
& "(CASE WHEN DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = 2 OR DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = 3 THEN replace(replace('<img alt=£Cambio£ title=£Cambia intestatario domanda£ src=£../NuoveImm/Img_CambioDich.png£ onclick=£window.open(''../VSA/Locatari/CambioIntestazione1.aspx?ID='||DOMANDE_BANDO_VSA.ID||'$PG='||DOMANDE_BANDO_VSA.PG||''',''CambioDich'',''height=550,top=200,left=350,width=670'');£ style=£cursor: pointer£ />','$','&'),'£','" & Chr(34) & "') ELSE '' END) as CAMBIO " _
& "FROM DICHIARAZIONI_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,T_STATI_DICHIARAZIONE WHERE DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE and DICHIARAZIONI_VSA.ID_STATO=T_STATI_DICHIARAZIONE.COD " _
                & "AND DOMANDE_BANDO_VSA.data_pg>='20120101' AND T_MOTIVO_DOMANDA_VSA.ID = DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA  " & condizioneQuery & " AND DOMANDE_BANDO_VSA.ID NOT IN (SELECT id_domanda FROM VSA_DECISIONI_REV_C) ORDER BY PG_DOMANDA DESC"


            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(strSQL, par.OracleConn)
            da.Fill(dtDomande)
            DataGrid1.DataSource = dtDomande
            conta = dtDomande.Rows.Count
            DataGrid1.DataBind()

            Label9.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & conta
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='gainsboro';}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='';}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='red';")

        End If


    End Sub
End Class
