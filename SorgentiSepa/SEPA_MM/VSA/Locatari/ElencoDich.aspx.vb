
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
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            BindGrid()
        End If

    End Sub

    
    Private Sub BindGrid()

        Try
            Dim strSQL0 As String = ""
            Dim strSQL As String = ""
            Dim conta As Integer = 0
            Dim annoReddito As String = ""
            Dim completaSQL As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim dtDomande As New Data.DataTable

            strSQL = "SELECT COGNOME||' '||NOME AS RICH,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE,T_STATI_DICHIARAZIONE.DESCRIZIONE AS STATO_DICH,(CASE WHEN DOMANDE_BANDO_VSA.FL_AUTORIZZAZIONE = 1 THEN 'AUTORIZZATA' ELSE (CASE WHEN VSA_DECISIONI_REV_C.COD_DECISIONE = 1 THEN 'SOTTOPOSTA A DECISIONE' WHEN VSA_DECISIONI_REV_C.COD_DECISIONE = 2 THEN 'ACCOLTA' WHEN VSA_DECISIONI_REV_C.COD_DECISIONE = 3 THEN 'NON ACCOLTA' WHEN VSA_DECISIONI_REV_C.COD_DECISIONE = 4 THEN 'SOTTOPOSTA A REVISIONE' WHEN VSA_DECISIONI_REV_C.COD_DECISIONE = 5 THEN 'REVISIONE ACCOLTA' WHEN VSA_DECISIONI_REV_C.COD_DECISIONE = 6 THEN 'REVISIONE NON ACCOLTA' ELSE 'NESSUNA DECISIONE' END) END) AS STATO_DOM, " _
                & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../max.aspx?CH=2$ID='||DICHIARAZIONI_VSA.ID||''',''Dichiarazione'',''height=550,top=200,left=350,width=670'');£>'||DICHIARAZIONI_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') as  PG_DICHIARAZIONE, " _
                & "TO_CHAR(TO_DATE(DICHIARAZIONI_VSA.DATA_PG,'yyyymmdd'),'dd/mm/yyyy') AS DATA_PG,TO_CHAR(TO_DATE(DOMANDE_BANDO_VSA.DATA_EVENTO,'yyyymmdd'),'dd/mm/yyyy') AS DATA_EVENTO," _
                & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../NuovaDomandaVSA/domandaNuova.aspx?CH=1$ID='||DOMANDE_BANDO_VSA.ID||''',''Domande'');£>'||DOMANDE_BANDO_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') as PG_DOMANDA " _
                & "FROM COMP_NUCLEO_VSA,DICHIARAZIONI_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,VSA_DECISIONI_REV_C,T_STATI_DICHIARAZIONE WHERE DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE and DICHIARAZIONI_VSA.ID_STATO=T_STATI_DICHIARAZIONE.COD AND VSA_DECISIONI_REV_C.DATA = (SELECT MAX(DATA) FROM VSA_DECISIONI_REV_C WHERE ID_DOMANDA=DOMANDE_BANDO_VSA.ID) " _
                & "AND COMP_NUCLEO_VSA.ID_DICHIARAZIONE = DICHIARAZIONI_VSA.ID AND PROGR = 0 AND T_MOTIVO_DOMANDA_VSA.ID = DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA AND DOMANDE_BANDO_VSA.CONTRATTO_NUM = '" & Request.QueryString("COD") & "' AND SUBSTR(data_evento,0,4) = '" & Request.QueryString("ANNO") & "' AND VSA_DECISIONI_REV_C.ID_DOMANDA(+) = DOMANDE_BANDO_VSA.ID " _
                & "UNION " _
                & "SELECT COGNOME||' '||NOME AS RICH,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE,T_STATI_DICHIARAZIONE.DESCRIZIONE AS STATO_DICH,'NESSUNA DECISIONE' AS STATO_DOM, " _
                & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../max.aspx?CH=2$ID='||DICHIARAZIONI_VSA.ID||''',''Dichiarazione'',''height=550,top=200,left=350,width=670'');£>'||DICHIARAZIONI_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') as  PG_DICHIARAZIONE, " _
                & "TO_CHAR(TO_DATE(DICHIARAZIONI_VSA.DATA_PG,'yyyymmdd'),'dd/mm/yyyy') AS DATA_PG,TO_CHAR(TO_DATE(DOMANDE_BANDO_VSA.DATA_EVENTO,'yyyymmdd'),'dd/mm/yyyy') AS DATA_EVENTO," _
                & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../NuovaDomandaVSA/domandaNuova.aspx?CH=1$ID='||DOMANDE_BANDO_VSA.ID||''',''Domande'');£>'||DOMANDE_BANDO_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') as PG_DOMANDA " _
                & "FROM COMP_NUCLEO_VSA,DICHIARAZIONI_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,T_STATI_DICHIARAZIONE WHERE DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE and DICHIARAZIONI_VSA.ID_STATO=T_STATI_DICHIARAZIONE.COD " _
                & "AND COMP_NUCLEO_VSA.ID_DICHIARAZIONE = DICHIARAZIONI_VSA.ID AND PROGR = 0 AND T_MOTIVO_DOMANDA_VSA.ID = DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA AND DOMANDE_BANDO_VSA.CONTRATTO_NUM = '" & Request.QueryString("COD") & "' AND SUBSTR(data_evento,0,4) = '" & Request.QueryString("ANNO") & "' AND DOMANDE_BANDO_VSA.ID NOT IN (SELECT id_domanda FROM VSA_DECISIONI_REV_C) ORDER BY PG_DOMANDA DESC"


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
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
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
